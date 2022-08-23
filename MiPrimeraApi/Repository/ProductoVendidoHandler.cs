using MiPrimeraApi.Controllers.DTOS;
using System.Data;
using System.Data.SqlClient;

namespace MiPrimeraApi.Repository
{
    public class ProductoVendidoHandler
    {
        public const string ConnectionString = "Server=INFORMATICA-DEV\\SQLEXPRESS;Initial Catalog=SistemaGestion;Trusted_Connection=True";
        public static bool EliminarVendidoProducto(int id)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDelete = "DELETE FROM ProductoVendido WHERE IdProducto=@Id";

                SqlParameter sqlParameter = new SqlParameter("Id", System.Data.SqlDbType.BigInt);
                sqlParameter.Value = id;

                sqlConnection.Open();

                using (SqlCommand sqlCommandProductoVenta = new SqlCommand(queryDelete, sqlConnection))
                {
                    sqlCommandProductoVenta.Parameters.Add(sqlParameter);
                    int numberOfRowsProductoVenta = sqlCommandProductoVenta.ExecuteNonQuery();
                    if (numberOfRowsProductoVenta > 0)
                    {
                        resultado = true;
                    }
                }
                sqlConnection.Close();
            }
            return resultado;
        }
        public static bool CargarProductoVendido(PostProductoVendido productoVendido)
        {

            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "INSERT INTO [SistemaGestion].[dbo].[ProductoVendido] " +
                    "(Stock, IdProducto, IdVenta) VALUES " +
                    "(@Stock, @IdProducto, @IdVenta);";

                SqlParameter stockParameter = new SqlParameter("Stock", SqlDbType.VarChar) { Value = productoVendido.Stock };
                SqlParameter idProductoParameter = new SqlParameter("IdProducto", SqlDbType.BigInt) { Value = productoVendido.IdProducto };
                SqlParameter idVentaParameter = new SqlParameter("IdVenta", SqlDbType.BigInt) { Value = productoVendido.IdVenta };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(stockParameter);
                    sqlCommand.Parameters.Add(idProductoParameter);
                    sqlCommand.Parameters.Add(idVentaParameter);

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


    }
}
