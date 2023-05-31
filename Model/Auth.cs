using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;

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
                var oSqlCommand = new SqlCommand("SELECT MotDePasse FROM Utilisateurs WHERE Email = @Email", oSqlConnection);
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
    }
}
