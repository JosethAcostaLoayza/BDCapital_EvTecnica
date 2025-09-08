using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using EvaluacionTecnica.Negocio;
using Ext.Net;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EvaluacionTecnica.Web
{
    public partial class CuotasPendientes : System.Web.UI.Page
    {
        private readonly CuotaService _cuotaService = new CuotaService();
        protected void Page_Load(object sender, EventArgs e)
        {
                DataTable dt = _cuotaService.ListarPrestamos();
                X.GetCmp<Store>("storePrestamos").DataSource = dt;
                X.GetCmp<Store>("storePrestamos").DataBind();
        }


        [DirectMethod]
        public void BuscarCuotas(int prestamoId)
        {
            DateTime fechaRef = X.GetCmp<DateField>("dtFechaRef").SelectedDate;

            DataTable dt = _cuotaService.BuscarCuotasPendientes(prestamoId, fechaRef);
            X.GetCmp<Store>("storeCuotas").DataSource = dt;
            X.GetCmp<Store>("storeCuotas").DataBind();
        }

        [DirectMethod]
        public void PagarCuota(int pagoId, int numeroCuota, int prestamoId)
        {
            try
            {
                _cuotaService.PagarCuota(pagoId, numeroCuota);

                X.Msg.Notify("Éxito", $"La cuota {numeroCuota} fue pagada correctamente.").Show();

                // refrescar cuotas
                BuscarCuotas(prestamoId);
            }
            catch (Exception ex)
            {
                X.Msg.Alert("Error", "No se pudo pagar la cuota: " + ex.Message).Show();
            }
        }

        [DirectMethod]
        public void ImprimirPrestamos()
        {
            try
            {
                // Obtener los datos
                DataTable dt = _cuotaService.ListarPrestamos();

                if (dt.Rows.Count == 0)
                {
                    X.Msg.Alert("Información", "No hay préstamos para mostrar.").Show();
                    return;
                }

                // Crear ReportDocument
                ReportDocument rpt = new ReportDocument();
                string rptPath = Server.MapPath("~/Reportes/ReportePrestamos.rpt");
                rpt.Load(rptPath);

                // Asignar DataTable al reporte
                rpt.SetDataSource(dt);

                // Enviar PDF al navegador
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "inline; filename=ListadoPrestamos.pdf");
                rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ListadoPrestamos");
                Response.Flush();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                X.Msg.Alert("Error", "No se pudo generar el PDF: " + ex.Message).Show();
            }
        }

    }
}