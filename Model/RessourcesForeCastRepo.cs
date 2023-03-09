using System.Data;
using System.Data.SqlClient;


namespace XefiAcademyAPI.Model
{
    public class RessourcesForeCastRepo
    {
        private readonly IConfiguration? _configuration;
        public RessourcesForeCastRepo(IConfiguration? configuration)
        {
            _configuration = configuration;

        }

        public RessourcesForeCastEntitity GetRessource(int id)
        {

            var oSqlParam = new SqlParameter("@Id", id);
            var oRessources = new RessourcesForeCastEntitity();
            var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
            var oSqlCommand = new SqlCommand("select * from Ressources where IdRessource = @Id");
            var oSqlAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlCommand.Parameters.Add(oSqlParam);


            oSqlCommand.Connection = oSqlConnection;
            var oDt = new DataTable();
            oSqlConnection.Open();
            oSqlAdapter.Fill(oDt);
            oSqlConnection.Close();




            if (oDt.Rows.Count > 0)
            {
                oRessources.IdRessource = (int)oDt.Rows[0][0];
                oRessources.IdConnaissance = oDt.Rows[0][1] != DBNull.Value ? (int)oDt.Rows[0][1] : 0; //Si c'est NULL = 0
                oRessources.IdTypeRessource = oDt.Rows[0][2] != DBNull.Value ? (int)oDt.Rows[0][1] : 0; //Si c'est NULL = 0
                oRessources.Auteur = (string)oDt.Rows[0][3];
                oRessources.Contenu = (string)oDt.Rows[0][4];
            }

            return oRessources;

        }

        public List<RessourcesForeCastEntitity> GetAllRessources()
        {
            var oList = new List<RessourcesForeCastEntitity>();
            var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
            var oSqlCommand = new SqlCommand("Select * From Ressources Order By IdRessource");

            oSqlCommand.Connection = oSqlConnection;
            oSqlConnection.Open();

            var oSqlDataReader = oSqlCommand.ExecuteReader();
            while (oSqlDataReader.Read())
            {
                oList.Add(new RessourcesForeCastEntitity
                {
                    IdRessource = (int)oSqlDataReader["IdRessource"],
                    IdConnaissance = oSqlDataReader["IdConnaissance"] != DBNull.Value ? (int)oSqlDataReader["IdConnaissance"] : 0, //Si c'est NULL = 0
                    IdTypeRessource = oSqlDataReader["IdTypeRessource"] != DBNull.Value ? (int)oSqlDataReader["IdTypeRessource"] : 0, //Si c'est NULL = 0
                    Auteur = (string)oSqlDataReader["Auteur"],
                    Contenu = (string)oSqlDataReader["Contenu"]
                });

            };
            oSqlDataReader.Close();
            oSqlConnection.Close();


            return oList;

        }

        public bool UpdateRessource(RessourcesForeCastEntitity fc)
        {
            try
            {

                var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));

                var oSqlParam = new SqlParameter("@IdRessource", fc.IdRessource);
                var oSqlParam1 = new SqlParameter("@IdConnaissance", fc.IdConnaissance);
                var oSqlParam2 = new SqlParameter("@IdTypeRessource", fc.IdTypeRessource);
                var oSqlParam3 = new SqlParameter("@Auteur", fc.Auteur);
                var oSqlParam4 = new SqlParameter("@Contenu", fc.Contenu);

                var oSqlCommand = new SqlCommand("Update Ressources Set Auteur=@Auteur, Contenu=@Contenu WHERE IdRessource = @IdRessource");

                oSqlCommand.Parameters.Add(oSqlParam);
                oSqlCommand.Parameters.Add(oSqlParam1);
                oSqlCommand.Parameters.Add(oSqlParam2);
                oSqlCommand.Parameters.Add(oSqlParam3);
                oSqlCommand.Parameters.Add(oSqlParam4);

                oSqlCommand.Connection = oSqlConnection;
                oSqlConnection.Open();

                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public int CreateRessource(RessourcesForeCastEntitity fc)
        {
            try
            {

                var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));

                var oSqlParam1 = new SqlParameter("@Auteur", fc.Auteur);
                var oSqlParam2 = new SqlParameter("@Contenu", fc.Contenu);

                var oSqlCommand = new SqlCommand("Insert Into  Ressources(Auteur, Contenu) Values (@Auteur, @Contenu);");

                oSqlCommand.Parameters.Add(oSqlParam1);
                oSqlCommand.Parameters.Add(oSqlParam2);


                oSqlCommand.Connection = oSqlConnection;
                oSqlConnection.Open();
                var Idretour = fc.IdRessource;
                oSqlCommand!.ExecuteNonQuery();
                oSqlConnection.Close();

                return Idretour;
            }
            catch (Exception)
            {

                return -1;
            }
        }

        public bool DeleteRessource(int Id)
        {
            try
            {
                var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
                var oSqlParam = new SqlParameter("@IdRessource", Id);


                var oSqlCommand = new SqlCommand("Delete Ressources Where IdRessource = @IdRessource ");

                oSqlCommand.Parameters.Add(oSqlParam);


                oSqlCommand.Connection = oSqlConnection;
                oSqlConnection.Open();

                oSqlCommand.ExecuteNonQuery();
                oSqlConnection.Close();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
