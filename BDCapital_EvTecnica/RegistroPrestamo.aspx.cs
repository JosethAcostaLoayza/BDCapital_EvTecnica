using EvaluacionTecnica.Negocio;
using Ext.Net;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace EvaluacionTecnica.Web
{
    public partial class RegistroPrestamo : System.Web.UI.Page
    {
        private readonly PrestamoService _prestamoService = new PrestamoService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                // Llenar storeTipoDoc
                var tipoDoc = new List<object>
                {
                    new { Valor = 1, Nombre = "DNI" },
                    new { Valor = 2, Nombre = "Pasaporte" }
                };
                cmbTipoDoc.GetStore().DataSource = tipoDoc;
                cmbTipoDoc.GetStore().DataBind();

                // Llenar storeTipoCliente
                var tipoCliente = new List<object>
                {
                    new { Valor = 1, Nombre = "Natural" },
                    new { Valor = 2, Nombre = "Microempresa" }
                };
                cmbTipoCliente.GetStore().DataSource = tipoCliente;
                cmbTipoCliente.GetStore().DataBind();
            }
        }

        [DirectMethod]
        public void RegistrarPrestamo()
        {
            try
            {
                // Capturar datos de la UI
                string nombre = X.GetCmp<TextField>("txtNombre").Text;
                string tipoDoc = X.GetCmp<ComboBox>("cmbTipoDoc").SelectedItem.Text;
                string numDoc = X.GetCmp<TextField>("txtNumDoc").Text;
                string direccion = X.GetCmp<TextField>("txtDireccion").Text;
                string telefono = X.GetCmp<TextField>("txtTelefono").Text;
                string email = X.GetCmp<TextField>("txtEmail").Text;
                string tipoCliente = X.GetCmp<ComboBox>("cmbTipoCliente").SelectedItem.Text;
                decimal monto = Convert.ToDecimal(X.GetCmp<TextField>("txtMonto").Value);
                int plazoMeses = Convert.ToInt32(X.GetCmp<NumberField>("txtPlazo").Value);
                decimal tasaInteres = Convert.ToDecimal(X.GetCmp<NumberField>("txtTasa").Value);

                // Llamar a la capa negocio
                _prestamoService.RegistrarPrestamo(nombre, tipoDoc, numDoc, direccion, telefono, email, tipoCliente, monto, plazoMeses, tasaInteres);

                // Obtener cronograma
                DataTable dt = _prestamoService.ObtenerCronograma(numDoc);

                // Cargar resultados en el Store
                X.GetCmp<Store>("storeCronograma").DataSource = dt;
                X.GetCmp<Store>("storeCronograma").DataBind();

                X.Msg.Notify("Éxito", "Cliente y préstamo registrados correctamente.").Show();
            }
            catch (Exception ex)
            {
                X.Msg.Alert("Error", "No se pudo registrar Cliente y préstamo: " + ex.Message).Show();
            }

        }
    }
}