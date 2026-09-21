using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CapaEntidad;

namespace Capa_Presentacion.Utilidades
{
    /// <summary>
    /// Toda la aritmetica de una venta en curso: lineas, totales y reparto del
    /// presupuesto entre los participantes.
    ///
    /// Esta clase no sabe que existe un DataGridView ni un MessageBox. Se puede
    /// probar sola, y el formulario solo la consulta y la dibuja.
    ///
    /// PRESUPUESTO COMPARTIDO
    /// Varias personas pueden pagar una misma factura. Cada una llega a la caja
    /// y da su codigo. El presupuesto disponible es la SUMA de los saldos del
    /// grupo, y el consumo se reparte PROPORCIONALMENTE al saldo de cada quien.
    ///
    ///   Consumo 5.000, ella con 6.000 y el con 4.000 (total 10.000):
    ///      ella paga 5.000 * 6.000/10.000 = 3.000  ->  le quedan 3.000
    ///      el   paga 5.000 * 4.000/10.000 = 2.000  ->  le quedan 2.000
    ///
    ///   Los dos gastan el mismo porcentaje, asi que se les agota el
    ///   presupuesto el mismo dia y no hay que volver a partir facturas a mano.
    ///
    /// ADVERTENCIA: el reparto que se calcula aqui es una VISTA PREVIA para el
    /// cajero. El reparto que queda guardado lo calcula usp_RegistrarVenta con
    /// los saldos reales y las filas bloqueadas dentro de la transaccion. Si
    /// alguien consumio en otra caja entre medio, manda el del procedimiento.
    ///
    /// SEMANTICA DE LOS TOTALES:
    ///   SaldoPresupuesto = suma de los saldos de todos los participantes
    ///   Total            = suma de los subtotales de las lineas
    ///   PresupuestoUsado = cuanto de la venta cubre el presupuesto del grupo
    ///   ExtraAPagar      = cuanto tiene que pagar el grupo de su bolsillo
    ///   SaldoRestante    = presupuesto que le queda al grupo
    ///
    /// Siempre se cumple:  Total == PresupuestoUsado + ExtraAPagar
    /// </summary>
    public class CarritoVenta
    {
        private readonly List<LineaVenta> _lineas = new List<LineaVenta>();
        private readonly List<ParticipanteVenta> _participantes = new List<ParticipanteVenta>();

        #region Participantes

        public IList<ParticipanteVenta> Participantes
        {
            get { return _participantes.AsReadOnly(); }
        }

        public bool HayCliente
        {
            get { return _participantes.Count > 0; }
        }

        public int CantidadParticipantes
        {
            get { return _participantes.Count; }
        }

        /// <summary>Codigo del participante principal (el primero que se escaneo).</summary>
        public int CodigoCliente
        {
            get
            {
                ParticipanteVenta principal = _participantes.FirstOrDefault();
                return principal == null ? 0 : principal.CodigoCliente;
            }
        }

        /// <summary>Nombres unidos con " + ", para la cabecera de la factura.</summary>
        public string NombreCliente
        {
            get
            {
                return string.Join(" + ", _participantes
                    .Select(p => p.Nombre)
                    .Where(n => !string.IsNullOrWhiteSpace(n)));
            }
        }

        /// <summary>
        /// Suma de los saldos del grupo. Es el presupuesto disponible real para
        /// esta factura.
        /// </summary>
        public decimal SaldoPresupuesto
        {
            get { return _participantes.Sum(p => p.SaldoAntes); }
        }

        /// <summary>
        /// Agrega una persona al grupo. Rechaza duplicados: escanear dos veces
        /// al mismo cliente le contaria el saldo dos veces y la factura cuadraria
        /// contra un presupuesto que no existe.
        /// </summary>
        public bool AgregarParticipante(Cliente cliente, out string error)
        {
            error = string.Empty;

            if (cliente == null || cliente.CodigoCliente <= 0)
            {
                error = "El cliente no es valido.";
                return false;
            }

            if (_participantes.Any(p => p.CodigoCliente == cliente.CodigoCliente))
            {
                error = string.Format("El cliente {0} ya esta en esta factura.",
                                      cliente.CodigoCliente);
                return false;
            }

            string nombre = string.Join(" ", new[] { cliente.Nombre, cliente.Apellido }
                                .Where(s => !string.IsNullOrWhiteSpace(s))).Trim();

            _participantes.Add(new ParticipanteVenta
            {
                Orden = _participantes.Count + 1,
                CodigoCliente = cliente.CodigoCliente,
                Nombre = nombre,
                SaldoAntes = cliente.Presupuesto < 0 ? 0m : cliente.Presupuesto,
                Asignado = 0m
            });

            Recalcular();
            return true;
        }

        public void QuitarParticipante(int codigoCliente)
        {
            ParticipanteVenta participante =
                _participantes.FirstOrDefault(p => p.CodigoCliente == codigoCliente);

            if (participante == null) return;

            _participantes.Remove(participante);
            Renumerar();
            Recalcular();
        }

        public bool ExisteParticipante(int codigoCliente)
        {
            return _participantes.Any(p => p.CodigoCliente == codigoCliente);
        }

        public void QuitarTodosLosParticipantes()
        {
            _participantes.Clear();
            Recalcular();
        }

        private void Renumerar()
        {
            for (int i = 0; i < _participantes.Count; i++)
                _participantes[i].Orden = i + 1;
        }

        #endregion

        #region Reparto proporcional

        /// <summary>
        /// Reparte el consumo entre los participantes, en proporcion al saldo
        /// de cada uno. Se llama sola cada vez que cambia algo del carrito.
        ///
        /// El reparto se hace en tres pasos por una razon concreta: dividir
        /// montos entre varias personas casi nunca da cifras exactas en
        /// centavos, y si no se controla el redondeo la suma de lo asignado no
        /// coincide con el total y aparecen descuadres de uno o dos centavos
        /// que despues nadie sabe explicar.
        /// </summary>
        public void Recalcular()
        {
            foreach (ParticipanteVenta p in _participantes)
                p.Asignado = 0m;

            if (_participantes.Count == 0) return;

            decimal saldoTotal = SaldoPresupuesto;
            if (saldoTotal <= 0m) return;

            decimal consumo = Math.Min(Total, saldoTotal);
            if (consumo <= 0m) return;

            // Paso 1: parte entera de centavos, truncando hacia abajo.
            // Truncar y no redondear garantiza que la suma nunca se pase del
            // consumo; los centavos faltantes se reparten en el paso 2.
            decimal[] fraccion = new decimal[_participantes.Count];

            for (int i = 0; i < _participantes.Count; i++)
            {
                ParticipanteVenta p = _participantes[i];

                decimal exacto = consumo * p.SaldoAntes / saldoTotal;
                decimal truncado = Math.Floor(exacto * 100m) / 100m;

                p.Asignado = truncado;
                fraccion[i] = exacto - truncado;
            }

            // Paso 2: repartir los centavos sobrantes, uno por persona,
            // empezando por quien quedo con la fraccion mas alta
            // (metodo del resto mayor).
            decimal repartido = _participantes.Sum(p => p.Asignado);
            int centavos = (int)Math.Round((consumo - repartido) * 100m,
                                           MidpointRounding.AwayFromZero);

            if (centavos > 0)
            {
                int[] orden = Enumerable.Range(0, _participantes.Count)
                    .OrderByDescending(i => fraccion[i])
                    .ThenByDescending(i => _participantes[i].SaldoAntes)
                    .ThenBy(i => _participantes[i].CodigoCliente)
                    .ToArray();

                for (int k = 0; k < centavos && k < orden.Length; k++)
                    _participantes[orden[k]].Asignado += 0.01m;
            }

            // Paso 3: nadie puede quedar asignado por encima de su propio
            // saldo. El redondeo puede pasarse por un centavo.
            foreach (ParticipanteVenta p in _participantes)
                if (p.Asignado > p.SaldoAntes) p.Asignado = p.SaldoAntes;
        }

        #endregion

        #region Lineas

        public IList<LineaVenta> Lineas
        {
            get { return _lineas.AsReadOnly(); }
        }

        public bool EstaVacio
        {
            get { return _lineas.Count == 0; }
        }

        /// <summary>
        /// Agrega el articulo o incrementa su cantidad si ya estaba.
        /// Valida existencias contra el stock que tenia al cargarse.
        /// </summary>
        public bool Agregar(Inventario articulo, out string error)
        {
            error = string.Empty;

            if (articulo == null)
            {
                error = "El articulo no es valido.";
                return false;
            }

            if (Buscar(articulo.Codigo) != null)
                return Incrementar(articulo.Codigo, out error);

            if (articulo.Cantidad <= 0)
            {
                error = string.Format("No hay existencias de \"{0}\".", articulo.Descripcion);
                return false;
            }

            _lineas.Insert(0, new LineaVenta
            {
                IdInventario = articulo.IdInventario,
                Codigo = articulo.Codigo,
                Descripcion = articulo.Descripcion ?? string.Empty,
                PrecioUnitario = articulo.Precio,
                Cantidad = 1,
                StockDisponible = articulo.Cantidad
            });

            Recalcular();
            return true;
        }

        public bool Incrementar(string codigo, out string error)
        {
            error = string.Empty;

            LineaVenta linea = Buscar(codigo);
            if (linea == null)
            {
                error = "El articulo no esta en el detalle.";
                return false;
            }

            if (linea.Cantidad + 1 > linea.StockDisponible)
            {
                error = string.Format("Solo hay {0} unidad(es) de \"{1}\" en existencia.",
                                      linea.StockDisponible, linea.Descripcion);
                return false;
            }

            linea.Cantidad++;
            MoverAlInicio(linea);
            Recalcular();
            return true;
        }

        /// <summary>Baja una unidad. Si queda en cero, la linea se elimina.</summary>
        public void Disminuir(string codigo)
        {
            LineaVenta linea = Buscar(codigo);
            if (linea == null) return;

            linea.Cantidad--;

            if (linea.Cantidad <= 0) _lineas.Remove(linea);
            else MoverAlInicio(linea);

            Recalcular();
        }

        /// <summary>Fija una cantidad exacta (para edicion directa en el grid).</summary>
        public bool FijarCantidad(string codigo, int cantidad, out string error)
        {
            error = string.Empty;

            LineaVenta linea = Buscar(codigo);
            if (linea == null)
            {
                error = "El articulo no esta en el detalle.";
                return false;
            }

            if (cantidad <= 0)
            {
                _lineas.Remove(linea);
                Recalcular();
                return true;
            }

            if (cantidad > linea.StockDisponible)
            {
                error = string.Format("Solo hay {0} unidad(es) de \"{1}\" en existencia.",
                                      linea.StockDisponible, linea.Descripcion);
                return false;
            }

            linea.Cantidad = cantidad;
            Recalcular();
            return true;
        }

        public void Quitar(string codigo)
        {
            LineaVenta linea = Buscar(codigo);
            if (linea != null)
            {
                _lineas.Remove(linea);
                Recalcular();
            }
        }

        public void Limpiar()
        {
            _lineas.Clear();
            Recalcular();
        }

        public LineaVenta Buscar(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo)) return null;
            return _lineas.FirstOrDefault(l =>
                string.Equals(l.Codigo, codigo.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        public LineaVenta Primera
        {
            get { return _lineas.FirstOrDefault(); }
        }

        private void MoverAlInicio(LineaVenta linea)
        {
            if (_lineas.Count == 0 || _lineas[0] == linea) return;
            _lineas.Remove(linea);
            _lineas.Insert(0, linea);
        }

        #endregion

        #region Totales

        public decimal Total
        {
            get { return Math.Round(_lineas.Sum(l => l.SubTotal), 2, MidpointRounding.AwayFromZero); }
        }

        public int CantidadArticulos
        {
            get { return _lineas.Sum(l => l.Cantidad); }
        }

        public decimal PresupuestoUsado
        {
            get { return Math.Min(Total, SaldoPresupuesto); }
        }

        public decimal ExtraAPagar
        {
            get { return Math.Max(0m, Total - SaldoPresupuesto); }
        }

        public decimal SaldoRestante
        {
            get { return Math.Max(0m, SaldoPresupuesto - Total); }
        }

        #endregion

        #region Salida hacia CapaNegocio

        /// <summary>
        /// Construye el DataTable que espera el parametro estructurado
        /// dbo.EDetalle_Venta2 del procedimiento usp_RegistrarVenta.
        /// El orden y el tipo de las columnas debe coincidir con el UDT.
        /// </summary>
        public DataTable ConstruirDetalle()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("IdInventario", typeof(string));
            dt.Columns.Add("Detalle", typeof(string));
            dt.Columns.Add("Precio", typeof(decimal));
            dt.Columns.Add("Cantidad", typeof(int));
            dt.Columns.Add("MontoTotal", typeof(decimal));

            foreach (LineaVenta linea in _lineas)
            {
                dt.Rows.Add(linea.Codigo, linea.Descripcion,
                            linea.PrecioUnitario, linea.Cantidad, linea.SubTotal);
            }

            return dt;
        }

        /// <summary>
        /// Copia de los participantes para adjuntar a la entidad Venta.
        /// Se entrega una copia y no la lista interna para que limpiar el
        /// carrito despues de guardar no altere el objeto ya enviado.
        /// </summary>
        public List<ParticipanteVenta> ConstruirParticipantes()
        {
            return _participantes.Select(p => new ParticipanteVenta
            {
                Orden = p.Orden,
                CodigoCliente = p.CodigoCliente,
                Nombre = p.Nombre,
                SaldoAntes = p.SaldoAntes,
                Asignado = p.Asignado
            }).ToList();
        }

        #endregion
    }
}
