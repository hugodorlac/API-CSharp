using System.Data;
using System.Data.SqlClient;

namespace XefiAcademyAPI.Model
{
    public class Projets
    {
        private readonly IConfiguration? _configuration;
        public Projets(IConfiguration? configuration)
        {
            _configuration = configuration;

        }

        public ProjetsEntitity GetProjet(int id)
        {

            var oSqlParam = new SqlParameter("@Id", id);
            var oConnaissances = new ProjetsEntitity();
            var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
            var oSqlCommand = new SqlCommand("select * from Projets where IdProjet = @Id");
            var oSqlAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlCommand.Parameters.Add(oSqlParam);


            oSqlCommand.Connection = oSqlConnection;
            var oDt = new DataTable();
            oSqlConnection.Open();
            oSqlAdapter.Fill(oDt);
            oSqlConnection.Close();

            


            if (oDt.Rows.Count > 0)
            {
                oConnaissances.IdProjet = (int)oDt.Rows[0][0];
                oConnaissances.IdStatut = oDt.Rows[0][1] != DBNull.Value ? (int)oDt.Rows[0][1] : 0; //Si c'est NULL = 0
                oConnaissances.Description = (string)oDt.Rows[0][2];
                oConnaissances.DateCreation = (DateTime)oDt.Rows[0][3];
                oConnaissances.Auteur = (string)oDt.Rows[0][4];
            }

            return oConnaissances;

        }
        public List<ProjetsEntitity> GetAllProjets()
        {
            var oList = new List<ProjetsEntitity>();
            var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
            var oSqlCommand = new SqlCommand("Select * From Projets Order By IdProjet");

            oSqlCommand.Connection = oSqlConnection;
            oSqlConnection.Open();

            var oSqlDataReader = oSqlCommand.ExecuteReader();
            while (oSqlDataReader.Read())
            {
                oList.Add(new ProjetsEntitity
                {
                    IdProjet = (int)oSqlDataReader["IdProjet"],
                    IdStatut = oSqlDataReader["IdStatut"] != DBNull.Value ? (int)oSqlDataReader["IdStatut"] : 0, //Si c'est NULL = 0
                    Description = (string)oSqlDataReader["Description"],
                    DateCreation = (DateTime)oSqlDataReader["DateCreation"],
                    Auteur = (string)oSqlDataReader["Auteur"],
                });

            };
            oSqlDataReader.Close();
            oSqlConnection.Close();


            return oList;

        }

        public bool UpdateProjet(ProjetsEntitity fc)
        {
            try
            {

                var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));

                var oSqlParam = new SqlParameter("@IdProjet", fc.IdProjet);
                var oSqlParam1 = new SqlParameter("@Description", fc.Description);
                var oSqlParam2 = new SqlParameter("@DateCreation", fc.DateCreation);
                var oSqlParam3 = new SqlParameter("@Auteur", fc.Auteur);

                var oSqlCommand = new SqlCommand("Update Projets Set Description=@Description,DateCreation=@DateCreation,Auteur=@Auteur Where IdProjet = @IdProjet ");

                oSqlCommand.Parameters.Add(oSqlParam);
                oSqlCommand.Parameters.Add(oSqlParam1);
                oSqlCommand.Parameters.Add(oSqlParam2);
                oSqlCommand.Parameters.Add(oSqlParam3);

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

        public int CreateProjet(ProjetsEntitity fc)
        {
            try
            {

                var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));

                var oSqlParam1 = new SqlParameter("@Description", fc.Description);
                var oSqlParam2 = new SqlParameter("@DateCreation", fc.DateCreation);
                var oSqlParam3 = new SqlParameter("@Auteur", fc.Auteur);

                var oSqlCommand = new SqlCommand("Insert Into  Projets(Description,DateCreation,Auteur) Values (@Description,@DateCreation,@Auteur);");

                oSqlCommand.Parameters.Add(oSqlParam1);
                oSqlCommand.Parameters.Add(oSqlParam2);
                oSqlCommand.Parameters.Add(oSqlParam3);


                oSqlCommand.Connection = oSqlConnection;
                oSqlConnection.Open();
                var Idretour = fc.IdProjet;
                oSqlCommand!.ExecuteNonQuery();
                oSqlConnection.Close();

                return Idretour;
            }
            catch (Exception)
            {

                return -1;
            }
        }

        public bool DeleteProjet(int Id)
        {
            try
            {
                var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
                var oSqlParam = new SqlParameter("@IdProjet", Id);


                var oSqlCommand = new SqlCommand("Delete Projets Where IdProjet = @IdProjet ");

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
