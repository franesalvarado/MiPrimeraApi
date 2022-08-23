﻿using MiPrimeraApi.Controllers.DTOS;
using MiPrimeraApi.Model;
using System.Data;
using System.Data.SqlClient;

namespace MiPrimeraApi.Repository
{
    public static class ProductoHandler
    {
        public const string ConnectionString = "Server=INFORMATICA-DEV\\SQLEXPRESS;Initial Catalog=SistemaGestion;Trusted_Connection=True";

        public static List<Producto> GetProductos()
        {
            List<Producto> resultados = new List<Producto>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.Connection.Open();
                    sqlCommand.CommandText = "SELECT * FROM Producto;";

                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();

                    sqlDataAdapter.SelectCommand = sqlCommand;

                    DataTable table = new DataTable();
                    sqlDataAdapter.Fill(table);

                    sqlCommand.Connection.Close();

                    foreach(DataRow row in table.Rows)
                    {
                        Producto producto = new Producto();

                        producto.Id = Convert.ToInt32(row["Id"]);
                        producto.Stock = Convert.ToInt32(row["Stock"]);
                        producto.IdUsuario = Convert.ToInt32(row["IdUsuario"]);
                        producto.Costo = Convert.ToInt32(row["Costo"]);
                        producto.PrecioVenta = Convert.ToInt32(row["PrecioVenta"]);
                        producto.Descripciones = row["Descripciones"].ToString();

                        resultados.Add(producto);

                    }
                }
            }

            return resultados;
        }

        public static bool CrearProducto(PostProducto producto)
        {
            if (string.IsNullOrEmpty(producto.Descripciones))
            {
                Console.WriteLine("El campo descripciones esta vacio o nulo");
                return false;
            }
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "INSERT INTO [SistemaGestion].[dbo].[[roducto] " +
                    "(Descripciones, Costo, PrecioVenta, Stock, IdUsuario) VALUES " +
                    "(@Descripciones, @Costo, @PrecioVenta, @Stock, @IdUsuario);";

                SqlParameter descripcionesParameter = new SqlParameter("Descripciones", SqlDbType.VarChar) { Value = producto.Descripciones };
                SqlParameter costoParameter = new SqlParameter("Costo", SqlDbType.Money) { Value = producto.Costo };
                SqlParameter precioVentaUsuarioParameter = new SqlParameter("PrecioVenta", SqlDbType.Money) { Value = producto.PrecioVenta };
                SqlParameter stockParameter = new SqlParameter("Stock", SqlDbType.Int) { Value = producto.Stock };
                SqlParameter idUsuarioParameter = new SqlParameter("IdUsuario", SqlDbType.BigInt) { Value = producto.IdUsuario };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(descripcionesParameter);
                    sqlCommand.Parameters.Add(costoParameter);
                    sqlCommand.Parameters.Add(precioVentaUsuarioParameter);
                    sqlCommand.Parameters.Add(stockParameter);
                    sqlCommand.Parameters.Add(idUsuarioParameter);

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
        public static bool ModificarProducto(PutProducto producto)
        {
            bool resultado = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "UPDATE [SistemaGestion].[dbo].[Producto] " +
                    "SET Descripciones = @Descripciones, Costo = @Costo, PrecioVenta = @PrecioVenta, Stock = @Stock, IdUsuario = @IdUsuario" +
                    "WHERE Id = @Id";

                SqlParameter idParameter = new SqlParameter("Id", SqlDbType.BigInt) { Value = producto.Id };
                SqlParameter descripcionesParameter = new SqlParameter("Descripciones", SqlDbType.VarChar) { Value = producto.Descripciones };
                SqlParameter costoParameter = new SqlParameter("Costo", SqlDbType.Money) { Value = producto.Costo };
                SqlParameter precioVentaParameter = new SqlParameter("PrecioVenta", SqlDbType.Money) { Value = producto.PrecioVenta };
                SqlParameter stockParameter = new SqlParameter("Stock", SqlDbType.Int) { Value = producto.Stock };
                SqlParameter idUsuarioParameter = new SqlParameter("IdUsuario", SqlDbType.BigInt) { Value = producto.IdUsuario };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(idParameter);
                    sqlCommand.Parameters.Add(descripcionesParameter);
                    sqlCommand.Parameters.Add(costoParameter);
                    sqlCommand.Parameters.Add(precioVentaParameter);
                    sqlCommand.Parameters.Add(stockParameter);
                    sqlCommand.Parameters.Add(idUsuarioParameter);

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
