using System.Data;
using System.Data.SqlClient;

namespace MiPrimeraApi.Repository
{
    public class VentaHandler
    {
        public const string ConnectionString = "Server=INFORMATICA-DEV\\SQLEXPRESS;Initial Catalog=SistemaGestion;Trusted_Connection=True";

        public static bool CargarVenta(string comentarios)
        {

            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "INSERT INTO [SistemaGestion].[dbo].[Venta] " +
                    "(Comentarios) VALUES " +
                    "(@Comentarios);";

                SqlParameter comentariosParameter = new SqlParameter("Comentarios", SqlDbType.VarChar) { Value = comentarios };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(comentariosParameter);

                    int numberOfRows = sqlCommand.ExecuteNonQuery();
                    if (numberOfRows > 0)
                    {
                        resultado = true;
                    }
                }

                sqlConnection.Close();
            }
            return resultado;
        }
        public static int GetLastIdVenta()
        {
            int id = 0;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryGetLastId = "SELECT TOP (1) [Id]" +
                    "FROM[SistemaGestion].[dbo].[Venta]" +
                    "ORDER BY Id DESC";

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryGetLastId, sqlConnection))
                {
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                id = Convert.ToInt32(dataReader["Id"]);
                            }
                        }
                    }
                }

                sqlConnection.Close();
            }
            return id;
        }
    }

}
