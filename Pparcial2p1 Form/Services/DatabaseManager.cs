using Microsoft.Data.SqlClient;

namespace Pparcial2p1
{
    public class DatabaseManager
    {
        private readonly string _connectionString;

        public DatabaseManager()
        {
            _connectionString = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=dbparcial12;Data Source=DESKTOP-D2V3R58\SQLEXPRESS;TrustServerCertificate=True";
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public bool TestConnection()
        {
            try
            {
                using var connection = GetConnection();
                connection.Open();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error de conexión: {ex.Message}");
                return false;
            }
        }
    }
}


