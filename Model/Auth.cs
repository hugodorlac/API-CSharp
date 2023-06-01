using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;
using NuGet.Protocol;

namespace XefiAcademyAPI.Model
{
    public class Auth
    {
        private readonly IConfiguration _configuration;

        public Auth(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AuthEntitity GetUtilisateur(string Email, string MotDePasse)
        {
            var oSqlParamEmail = new SqlParameter("@Email", Email);
            var oSqlParamMotDePasse = new SqlParameter("@MotDePasse", MotDePasse);
            var oAuthEntity = new AuthEntitity();

            using (var oSqlConnection = new SqlConnection(_configuration.GetConnectionString("SQL")))
            {
                var oSqlCommand = new SqlCommand("SELECT MotDePasse, IdUtilisateur FROM Utilisateurs WHERE Email = @Email", oSqlConnection);
                oSqlCommand.Parameters.Add(oSqlParamEmail);

                var oSqlAdapter = new SqlDataAdapter(oSqlCommand);
                var oDt = new DataTable();

                oSqlConnection.Open();
                oSqlAdapter.Fill(oDt);
                oSqlConnection.Close();

                if (oDt.Rows.Count > 0)
                {
                    var storedPassword = oDt.Rows[0]["MotDePasse"].ToString();
                    var hashedPassword = ComputeHash(MotDePasse);
               
                    if (storedPassword == hashedPassword)
                    {
                        oAuthEntity.IdUtilisateur = (int)oDt.Rows[0]["IdUtilisateur"];
                        oAuthEntity.Email = Email;
                        oAuthEntity.MotDePasse = storedPassword;
                    }
                }
            }

            return oAuthEntity;
        }

        private string ComputeHash(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hashBytes).Replace("-", string.Empty);
            }
        }
        public bool UpdateToken(int IdUtilisateur, string token)
        {
            try
            {

                var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));

                var oSqlParam0 = new SqlParameter("@IdUtilisateur", IdUtilisateur);
                var oSqlParam1 = new SqlParameter("@Token", token);

                var oSqlCommand = new SqlCommand("Update Utilisateurs set Token = @Token Where IdUtilisateur = @IdUtilisateur;");

                oSqlCommand.Parameters.Add(oSqlParam0);
                oSqlCommand.Parameters.Add(oSqlParam1);

                oSqlCommand.Connection = oSqlConnection;
                oSqlConnection.Open();
                var Idretour = IdUtilisateur;
                oSqlCommand!.ExecuteNonQuery();
                oSqlConnection.Close();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool CheckToken(int IdUtilisateur, string Token)
        {
            var oSqlParamEmail = new SqlParameter("@Id", IdUtilisateur);
            var oAuthEntity = new AuthEntitity();

            using (var oSqlConnection = new SqlConnection(_configuration.GetConnectionString("SQL")))
            {
                var oSqlCommand = new SqlCommand("SELECT Token FROM Utilisateurs WHERE IdUtilisateur = @Id", oSqlConnection);
                oSqlCommand.Parameters.Add(oSqlParamEmail);
             
                var oSqlAdapter = new SqlDataAdapter(oSqlCommand);
                var oDt = new DataTable();

                oSqlConnection.Open();
                oSqlAdapter.Fill(oDt);
                oSqlConnection.Close();

                if (oDt.Rows.Count > 0)
                {
                    var TokenBDD = oDt.Rows[0]["Token"].ToString();

                    if (Token == TokenBDD)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
