using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using EvaluacionTecnica.Negocio;
using System;
using System.Data;
using System.Web;

namespace EvaluacionTecnica.Web
{
    public partial class ReportePrestamos : System.Web.UI.Page
    {
        private readonly CuotaService _cuotaService = new CuotaService(); // Ajusta a tu servicio real

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // Obtener datos
                DataTable dt = _cuotaService.ListarPrestamos();

                if (dt == null || dt.Rows.Count == 0)
                {
                    Response.ContentType = "text/plain";
                    Response.Write("⚠️ No hay préstamos para mostrar.");
                    return;
                }

                dt.TableName = "Prestamos";

                // Ver cuántas filas llegan
                int total = dt.Rows.Count;

                foreach (DataRow row in dt.Rows)
                {
                    System.Diagnostics.Debug.WriteLine(
                        $"{row["PrestamoId"]} - {row["Cliente"]} - {row["Monto"]} - {row["FechaPrestamo"]}");
                }

                // Crear un DataSet y agregar la tabla
                DataSet ds = new DataSet("dsPrestamos"); // mismo nombre que tu .xsd
                ds.Tables.Add(dt.Copy());


                // Cargar reporte
                ReportDocument rpt = new ReportDocument();
                string rptPath = Server.MapPath("~/Reportes/ReportePrestamos.rpt");
                rpt.Load(rptPath);

                // Vincular DataTable
                rpt.SetDataSource(dt);

                // Exportar PDF
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "inline; filename=ListadoPrestamos.pdf");
                rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ListadoPrestamos");

                //Response.Flush();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                Response.ContentType = "text/plain";
                Response.Write("❌ Error al generar reporte: " + ex.Message);
            }
        }
    }
}
