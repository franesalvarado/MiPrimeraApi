using MiPrimeraApi.Controllers.DTOS;
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
        public static bool EliminarVenta(int id)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDeleteProducto = "DELETE FROM Venta WHERE Id=@Id";

                SqlParameter sqlParameter = new SqlParameter("Id", System.Data.SqlDbType.BigInt);
                sqlParameter.Value = id;

                sqlConnection.Open();

                using (SqlCommand sqlCommandProducto = new SqlCommand(queryDeleteProducto, sqlConnection))
                {
                    sqlCommandProducto.Parameters.Add(sqlParameter);
                    int numberOfRows = sqlCommandProducto.ExecuteNonQuery();
                    if (numberOfRows > 0)
                    {
                        resultado = true;
                    }
                }

                sqlConnection.Close();

            }
            return resultado;
        }
        public static List<TodasVentas> GetVentas()
        {
            List<TodasVentas> ventas = new List<TodasVentas>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.Connection.Open();
                    sqlCommand.CommandText = "SELECT " +
                        "Venta.Id as idVenta, " +
                        "Venta.Comentarios as ventaComentario, " +
                        "ProductoVendido.Id as productoVendidoId, " +
                        "ProductoVendido.Stock as productoVendidoStock, " +
                        "Producto.Id as productoId, " +
                        "Producto.Descripciones as productoDescripciones, " +
                        "Producto.Costo as productoCosto, " +
                        "Producto.PrecioVenta as productoPrecioVenta, " +
                        "Producto.Stock as productoStock " +
                        "FROM Venta " +
                        "INNER JOIN ProductoVendido ON Venta.Id = ProductoVendido.IdVenta " +
                        "INNER JOIN Producto ON Producto.Id = ProductoVendido.IdProducto; ";

                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();

                    sqlDataAdapter.SelectCommand = sqlCommand;

                    DataTable table = new DataTable();
                    sqlDataAdapter.Fill(table);

                    sqlCommand.Connection.Close();

                    foreach (DataRow row in table.Rows)
                    {
                        TodasVentas venta = new TodasVentas();

                        venta.IdVenta = Convert.ToInt32(row["idVenta"]);
                        venta.VentaComentario = row["ventaComentario"].ToString();
                        venta.ProductoVendidoId = Convert.ToInt32(row["productoVendidoId"]);
                        venta.ProductoVendidoStock = Convert.ToInt32(row["productoVendidoStock"]);
                        venta.ProductoId = Convert.ToInt32(row["productoId"]);
                        venta.ProductoDescripciones = row["productoDescripciones"].ToString();
                        venta.ProductoCosto = Convert.ToInt32(row["productoCosto"]);
                        venta.PrecioVenta = Convert.ToInt32(row["productoPrecioVenta"]);
                        venta.ProductoStock = Convert.ToInt32(row["productoStock"]);

                        ventas.Add(venta);

                    }
                }
            }
            return ventas;
        }


    }

}
