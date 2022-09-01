using MiPrimeraApi.Controllers.DTOS;
using System.Data.SqlClient;

namespace MiPrimeraApi.Repository
{
    public class LoginHandler
    {
        public const string ConnectionString = "Server=INFORMATICA-DEV\\SQLEXPRESS;Initial Catalog=SistemaGestion;Trusted_Connection=True";

        public static bool Login(Login login)
        {
            bool result = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDelete = "SELECT * FROM Usuario WHERE NombreUsuario=@NombreUsuario AND Contraseña=@Contraseña";

                SqlParameter nombreUsuarioParameter = new SqlParameter("NombreUsuario", System.Data.SqlDbType.VarChar);
                SqlParameter contraseñaParameter = new SqlParameter("Contraseña", System.Data.SqlDbType.VarChar);
                nombreUsuarioParameter.Value = login.Usuario;
                contraseñaParameter.Value = login.Contraseña;

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                {
                    sqlCommand.Parameters.Add(nombreUsuarioParameter);
                    sqlCommand.Parameters.Add(contraseñaParameter);
                    int numberOfRows = sqlCommand.ExecuteNonQuery();
                    if (numberOfRows > 0)
                    {
                        result = true;
                    }
                }
                sqlConnection.Close();
            }


            return result;
        }
    }
}
