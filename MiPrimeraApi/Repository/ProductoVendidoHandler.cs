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
        public static List<GetProductoVendido> ObtenerProductoVendidoPorIdVenta(int id)
        {
            List<GetProductoVendido> resultados = new List<GetProductoVendido>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.Connection.Open();
                    sqlCommand.CommandText = "SELECT * FROM ProductoVendido WHERE IdVenta = @Id;";

                    SqlParameter queryParameter = new SqlParameter("Id", System.Data.SqlDbType.BigInt);
                    queryParameter.Value = id;

                    sqlCommand.Parameters.Add(queryParameter);

                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                    sqlDataAdapter.SelectCommand = sqlCommand;

                    DataTable table = new DataTable();
                    sqlDataAdapter.Fill(table);

                    sqlCommand.Connection.Close();

                    foreach (DataRow row in table.Rows)
                    {
                        GetProductoVendido productoVendido = new GetProductoVendido();

                        productoVendido.Id = Convert.ToInt32(row["Id"]);
                        productoVendido.Stock = Convert.ToInt32(row["Stock"]);
                        productoVendido.IdProducto = Convert.ToInt32(row["IdUsuario"]);
                        productoVendido.IdVenta = Convert.ToInt32(row["Costo"]);

                        resultados.Add(productoVendido);

                    }
                }
            }

            return resultados;
        }
        public static void EliminarProductoVendidoPorIdVenta(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDeleteProducto = "DELETE FROM ProductoVendido WHERE IdVenta=@Id";

                SqlParameter sqlParameter = new SqlParameter("IdVenta", System.Data.SqlDbType.BigInt);
                sqlParameter.Value = id;

                sqlConnection.Open();

                using (SqlCommand sqlCommandProducto = new SqlCommand(queryDeleteProducto, sqlConnection))
                {
                    sqlCommandProducto.Parameters.Add(sqlParameter);
                    sqlCommandProducto.ExecuteNonQuery();
                }

                sqlConnection.Close();

            }
        }
        public static List<GetProductoVendido> ObtenerProductosVendidosPorUsuario(int id)
        {
            List<GetProductoVendido> resultados = new List<GetProductoVendido>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.Connection.Open();
                    sqlCommand.CommandText = "SELECT * FROM ProductoVendido WHERE IdProducto = @Id;";

                    SqlParameter queryParameter = new SqlParameter("Id", System.Data.SqlDbType.BigInt);
                    queryParameter.Value = id;

                    sqlCommand.Parameters.Add(queryParameter);

                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                    sqlDataAdapter.SelectCommand = sqlCommand;

                    DataTable table = new DataTable();
                    sqlDataAdapter.Fill(table);

                    sqlCommand.Connection.Close();

                    foreach (DataRow row in table.Rows)
                    {
                        GetProductoVendido productoVendido = new GetProductoVendido();

                        productoVendido.Id = Convert.ToInt32(row["Id"]);
                        productoVendido.Stock = Convert.ToInt32(row["Stock"]);
                        productoVendido.IdProducto = Convert.ToInt32(row["IdUsuario"]);
                        productoVendido.IdVenta = Convert.ToInt32(row["Costo"]);

                        resultados.Add(productoVendido);

                    }
                }
            }

            return resultados;
        }

    }
}
