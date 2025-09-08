using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;
using System.Data;

namespace EvaluacionTecnica.Datos
{
    public class CuotaRepository
    {
        private readonly string _connStr;

        public CuotaRepository()
        {
            _connStr = ConfigurationManager.ConnectionStrings["OracleConn"].ConnectionString;
        }

        public DataTable ListarPrestamos()
        {
            using (OracleConnection conn = new OracleConnection(_connStr))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("sp_ListarPrestamos", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_Cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    return dt;
                }
            }
        }

        public DataTable BuscarCuotasPendientes(int prestamoId, DateTime fechaRef)
        {
            using (OracleConnection conn = new OracleConnection(_connStr))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("sp_BuscarCuotasVencidasPendientes", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_PrestamoId", OracleDbType.Int32).Value = prestamoId;
                    cmd.Parameters.Add("p_FechaReferencia", OracleDbType.Date).Value = fechaRef;
                    cmd.Parameters.Add("p_Cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    return dt;
                }
            }
        }

        public void PagarCuota(int pagoId, int numeroCuota)
        {
            using (OracleConnection conn = new OracleConnection(_connStr))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("sp_PagarCuota", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_PagoId", OracleDbType.Int32).Value = pagoId;
                    cmd.Parameters.Add("p_NumeroCuota", OracleDbType.Int32).Value = numeroCuota;

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
