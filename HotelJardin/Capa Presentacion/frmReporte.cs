using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace Capa_Presentacion
{
    public partial class frmReporte : Form
    {
        public frmReporte()
        {
            InitializeComponent();
        }

        private void frmReporte_Load(object sender, EventArgs e)
        {

        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            List<ReporteVenta> lista = new List<ReporteVenta>();

            lista = new CN_Reporte().Venta(date1.Value.ToString(), date2.Value.ToString());

            dataGridViewReporte.Rows.Clear();

            foreach (ReporteVenta rv in lista)
            {
                dataGridViewReporte.Rows.Add(new object[]
                {
                    rv.FechaRegistro,
                    rv.IdUsuario,
                    rv.IdVenta,
                    rv.NombreCliente,
                    rv.ModoPago,
                    rv.MontoTotal.ToString("0.00")
                });
            }

        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            if (dataGridViewReporte.Rows.Count < 1)
            {
                MessageBox.Show("No hay datos para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DataTable dt = new DataTable();
            foreach (DataGridViewColumn column in dataGridViewReporte.Columns)
                dt.Columns.Add(column.HeaderText, typeof(string));

            foreach (DataGridViewRow row in dataGridViewReporte.Rows)
            {
                if (row.IsNewRow) continue;               // Skip the editable new row
                if (!row.Visible) continue;

                dt.Rows.Add(new object[]
                {
            row.Cells[0].Value?.ToString() ?? string.Empty,
            row.Cells[1].Value?.ToString() ?? string.Empty,
            row.Cells[2].Value?.ToString() ?? string.Empty,
            row.Cells[3].Value?.ToString() ?? string.Empty,
            row.Cells[4].Value?.ToString() ?? string.Empty,
            row.Cells[5].Value?.ToString() ?? string.Empty
                });
            }

            using (SaveFileDialog saveFile = new SaveFileDialog())
            {
                saveFile.FileName = $"ReporteVenta_{DateTime.Now:ddMMyyyy}.xlsx";
                saveFile.Filter = "Excel Files|*.xlsx";

                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var wb = new ClosedXML.Excel.XLWorkbook())
                        {
                            var hoja = wb.Worksheets.Add(dt, "ReporteVenta");
                            hoja.ColumnsUsed().AdjustToContents();
                            wb.SaveAs(saveFile.FileName);
                        }
                        MessageBox.Show("Reporte generado correctamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error al generar el reporte", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }
    }
}
