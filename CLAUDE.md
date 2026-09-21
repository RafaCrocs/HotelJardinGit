# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project overview

HotelJardin is a Windows Forms desktop application (.NET Framework 4.8, C#) for managing sales/inventory
for what looks like a hotel/store business ("Sistema de Ventas"). It talks to a SQL Server database
(`DbSistemaVentas7`) via raw ADO.NET (`SqlConnection`/`SqlCommand`), mostly through stored procedures.

The solution file is `HotelJardin/HotelJardin.sln` (there is a second, narrower `.sln` inside
`HotelJardin/Capa Presentacion/` — use the top-level one at `HotelJardin/HotelJardin.sln`).

## Build and run

This is a classic .NET Framework WinForms solution, built with MSBuild/Visual Studio — there is no
`dotnet` CLI project file here.

- Open `HotelJardin/HotelJardin.sln` in Visual Studio and build/run from there (F5), or:
- Build from the command line with MSBuild, e.g.:
  ```
  msbuild "HotelJardin/HotelJardin.sln" /p:Configuration=Debug
  ```
- NuGet packages are restored via `packages.config` per project (not PackageReference) — run
  `nuget restore HotelJardin/HotelJardin.sln` if packages are missing before building.
- There are no automated tests in this repository.

The app requires a reachable SQL Server instance with the `DbSistemaVentas7` database. The schema and
stored procedures are exported in `ScriptHotelJardin.sql` at the repo root (note: this file is UTF-16
encoded, as produced by SSMS's "Generate Scripts").

## Architecture

The solution follows a classic 4-layer ("N-capas") architecture, one C# project per layer, all under
`HotelJardin/`:

- **CapaEntidad** — plain data-holder classes (POCOs) such as `Cliente`, `Usuario`, `Venta`,
  `DetalleVenta`, `Inventario`, `Rol`, `Permiso`. No logic.
- **CapaDatos** — data access. One `CD_<Entidad>.cs` class per entity (e.g. `CD_Cliente`, `CD_Venta`,
  `CD_Usuario`), each opening its own `SqlConnection` per method and either running inline SQL or
  calling a stored procedure (`SP_RegistrarCliente`, `SP_ModificarCliente`, etc.). The connection
  string lives in `CapaDatos/Conexion.cs` as a mutable static field (`Conexion.cadena`), and can be
  changed at runtime from the login screen (`ConexionBL.ModificarCadenaConexion`) — that's how the app
  points at a different SQL Server host without a rebuild.
- **CapaNegocio** — business/validation layer. One `CN_<Entidad>.cs` class per entity, each wrapping a
  `CD_<Entidad>` instance. Validation (required fields, etc.) happens here before delegating to the
  data layer; methods generally return `bool` plus an `out string Mensaje` for success/error text.
- **Capa Presentacion** — WinForms UI (note the space in the folder/project name). Forms (`frm*.cs`)
  are the main screens (`frmVenta`, `frmClientes`, `frmInventario`, `frmUsuarios`, `frmReporte`);
  `Modales/` holds picker dialogs (`mdCliente`, `mdInventario`) that forms open via `ShowDialog()` to
  select a record and return it through a public property (e.g. `mdCliente._Cliente`) before closing
  with `DialogResult.OK`. `Login` is the entry form (set in `Program.cs`), which opens `Inicio` (the
  main shell) on successful login. `Utilidades/` holds view-layer helper classes (e.g. `CarritoVenta`,
  `OpcionCombo`) that aren't tied to a specific form.

Data flows strictly Presentacion → CapaNegocio → CapaDatos → CapaEntidad/SQL Server; forms never talk
to `CapaDatos` directly, and `CapaDatos` never references `CapaNegocio`/`Capa Presentacion`.

When adding a new entity/screen, follow the existing pattern: add the POCO to `CapaEntidad`, a
`CD_<Nombre>` class to `CapaDatos` with `Listar`/`Registrar`/`Editar`/`Eliminar` methods, a matching
`CN_<Nombre>` in `CapaNegocio` that validates and delegates, and a `frm<Nombre>` (plus optional
`md<Nombre>` picker) in `Capa Presentacion`.

## Notes

- Error handling in the data layer generally swallows exceptions into a `Mensaje`/`resultado` pair
  rather than throwing — follow this convention for consistency with existing code rather than
  introducing exceptions that propagate to the UI.
- Auth is done by comparing plaintext username/password against rows loaded via `CN_Usuario.Listar()`
  client-side (see `Login.cs`) — there's no hashing today; be aware of this if touching auth code.
