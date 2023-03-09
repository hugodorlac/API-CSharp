using System.Data;
using System.Data.SqlClient;

namespace XefiAcademyAPI.Model
{
    public class TypesRessourcesForeCastRepo
    {
        private readonly IConfiguration? _configuration;
        public TypesRessourcesForeCastRepo(IConfiguration? configuration)
        {
            _configuration = configuration;

        }

        public TypesRessourcesForeCastEntitity GetTypeRessource(int id)
        {

            var oSqlParam = new SqlParameter("@Id", id);
            var oTypeRessource = new TypesRessourcesForeCastEntitity();
            var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
            var oSqlCommand = new SqlCommand("select * from TypeRessources where IdTypeRessource = @Id");
            var oSqlAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlCommand.Parameters.Add(oSqlParam);


            oSqlCommand.Connection = oSqlConnection;
            var oDt = new DataTable();
            oSqlConnection.Open();
            oSqlAdapter.Fill(oDt);
            oSqlConnection.Close();






            if (oDt.Rows.Count > 0)
            {
                oTypeRessource.IdTypeRessource = (int)oDt.Rows[0][0];
                oTypeRessource.LienImage = (string)oDt.Rows[0][1];
            }

            return oTypeRessource;

        }

        public List<TypesRessourcesForeCastEntitity> GetAllTypeRessource()
        {
            var oList = new List<TypesRessourcesForeCastEntitity>();
            var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
            var oSqlCommand = new SqlCommand("Select * From TypeRessources Order By IdTypeRessource");

            oSqlCommand.Connection = oSqlConnection;
            oSqlConnection.Open();

            var oSqlDataReader = oSqlCommand.ExecuteReader();
            while (oSqlDataReader.Read())
            {
                oList.Add(new TypesRessourcesForeCastEntitity
                {
                    IdTypeRessource = (int)oSqlDataReader["IdTypeRessource"],
                    LienImage = (string)oSqlDataReader["LienImage"]
                });

            };
            oSqlDataReader.Close();
            oSqlConnection.Close();


            return oList;

        }

        public bool UpdateTypeRessource(TypesRessourcesForeCastEntitity fc)
        {
            try
            {

                var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));

                var oSqlParam = new SqlParameter("@IdCategorie", fc.IdTypeRessource);
                var oSqlParam1 = new SqlParameter("@Libelle", fc.LienImage);

                var oSqlCommand = new SqlCommand("Update Categories Set Libelle=@Libelle WHERE IdCategorie = @IdCategorie");

                oSqlCommand.Parameters.Add(oSqlParam);
                oSqlCommand.Parameters.Add(oSqlParam1);

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

        public int CreateTypeRessource(TypesRessourcesForeCastEntitity fc)
        {
            try
            {

                var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));

                var oSqlParam1 = new SqlParameter("@LienImage", fc.LienImage);

                var oSqlCommand = new SqlCommand("Insert Into  TypeRessources(LienImage) Values (@LienImage);");

                oSqlCommand.Parameters.Add(oSqlParam1);


                oSqlCommand.Connection = oSqlConnection;
                oSqlConnection.Open();
                var Idretour = fc.IdTypeRessource;
                oSqlCommand!.ExecuteNonQuery();
                oSqlConnection.Close();

                return Idretour;
            }
            catch (Exception)
            {

                return -1;
            }
        }

        public bool DeleteCategorie(int Id)
        {
            try
            {
                var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
                var oSqlParam = new SqlParameter("@IdCategorie", Id);


                var oSqlCommand = new SqlCommand("Delete Categories Where IdCategorie = @IdCategorie ");

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
