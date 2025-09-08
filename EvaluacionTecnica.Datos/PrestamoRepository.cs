using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;
using System.Data;

namespace EvaluacionTecnica.Datos
{
    public class PrestamoRepository
    {
        private readonly string _connStr;

        public PrestamoRepository()
        {
            _connStr = ConfigurationManager.ConnectionStrings["OracleConn"].ConnectionString;
        }

        public void RegistrarPrestamo(string nombre, string tipoDoc, string numDoc,
                                      string direccion, string telefono, string email,
                                      string tipoCliente, decimal monto, int plazoMeses, decimal tasaInteres)
        {
            using (OracleConnection conn = new OracleConnection(_connStr))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("sp_RegistrarPrestamo", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("p_NombreCompleto", OracleDbType.Varchar2).Value = nombre;
                    cmd.Parameters.Add("p_TipoDocumento", OracleDbType.Varchar2).Value = tipoDoc;
                    cmd.Parameters.Add("p_NumeroDocumento", OracleDbType.Varchar2).Value = numDoc;
                    cmd.Parameters.Add("p_Direccion", OracleDbType.Varchar2).Value = direccion;
                    cmd.Parameters.Add("p_Telefono", OracleDbType.Varchar2).Value = telefono;
                    cmd.Parameters.Add("p_Email", OracleDbType.Varchar2).Value = email;
                    cmd.Parameters.Add("p_TipoCliente", OracleDbType.Varchar2).Value = tipoCliente;

                    cmd.Parameters.Add("p_Monto", OracleDbType.Decimal).Value = monto;
                    cmd.Parameters.Add("p_PlazoMeses", OracleDbType.Int32).Value = plazoMeses;
                    cmd.Parameters.Add("p_TasaInteres", OracleDbType.Decimal).Value = tasaInteres;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable ObtenerCronograma(string numDoc)
        {
            using (OracleConnection conn = new OracleConnection(_connStr))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("sp_ObtenerCronogramaPorDocumento", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_NumeroDocumento", OracleDbType.Varchar2).Value = numDoc;
                    cmd.Parameters.Add("p_Cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    return dt;
                }
            }
        }
    }
}
