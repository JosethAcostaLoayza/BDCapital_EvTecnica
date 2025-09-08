using Oracle.ManagedDataAccess.Client;
using System.Configuration;

public class ConexionDB
{
    private static string connectionString = ConfigurationManager.ConnectionStrings["OracleDB"].ConnectionString;

    public static OracleConnection GetConnection()
    {
        OracleConnection cn = new OracleConnection(connectionString);
        cn.Open();
        return cn;
    }
}
