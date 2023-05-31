using System.Data;
using System.Data.SqlClient;

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
                    if (oDt.Rows[0]["MotDePasse"].ToString() == MotDePasse.ToString()) {
                        oAuthEntity.Email = Email;
                        oAuthEntity.MotDePasse = oDt.Rows[0]["MotDePasse"].ToString();
                    }
                }
            }

            return oAuthEntity;
        }
    }
}
