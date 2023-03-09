using System.Data;
using System.Data.SqlClient;


namespace XefiAcademyAPI.Model
{
    public class CategoriesForeCastRepo
    {
        private readonly IConfiguration? _configuration;
        public CategoriesForeCastRepo(IConfiguration? configuration)
        {
            _configuration = configuration;

        }

        public CategoriesForeCastEntitity GetCategorie(int id)
        {

            var oSqlParam = new SqlParameter("@Id", id);
            var oCategories = new CategoriesForeCastEntitity();
            var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
            var oSqlCommand = new SqlCommand("select * from Categories where IdCategorie = @Id");
            var oSqlAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlCommand.Parameters.Add(oSqlParam);


            oSqlCommand.Connection = oSqlConnection;
            var oDt = new DataTable();
            oSqlConnection.Open();
            oSqlAdapter.Fill(oDt);
            oSqlConnection.Close();




            if (oDt.Rows.Count > 0)
            {
                oCategories.IdCategorie = (int)oDt.Rows[0][0];
                oCategories.Libelle = (string)oDt.Rows[0][1];
            }

            return oCategories;

        }

        public List<CategoriesForeCastEntitity> GetAllCategories()
        {
            var oList = new List<CategoriesForeCastEntitity>();
            var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
            var oSqlCommand = new SqlCommand("Select * From Categories Order By IdCategorie");

            oSqlCommand.Connection = oSqlConnection;
            oSqlConnection.Open();

            var oSqlDataReader = oSqlCommand.ExecuteReader();
            while (oSqlDataReader.Read())
            {
                oList.Add(new CategoriesForeCastEntitity
                {
                    IdCategorie = (int)oSqlDataReader["IdCategorie"],
                    Libelle = (string)oSqlDataReader["Libelle"]
                }) ;

            };
            oSqlDataReader.Close();
            oSqlConnection.Close();


            return oList;

        }

        public bool UpdateProjet(CategoriesForeCastEntitity fc)
        {
            try
            {

                var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));

                var oSqlParam = new SqlParameter("@IdCategorie", fc.IdCategorie);
                var oSqlParam1 = new SqlParameter("@Libelle", fc.Libelle);

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

        public int CreateCategorie(CategoriesForeCastEntitity fc)
        {
            try
            {

                var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));

                var oSqlParam1 = new SqlParameter("@Libelle", fc.Libelle);

                var oSqlCommand = new SqlCommand("Insert Into  Categories(Libelle) Values (@Libelle);");

                oSqlCommand.Parameters.Add(oSqlParam1);


                oSqlCommand.Connection = oSqlConnection;
                oSqlConnection.Open();
                var Idretour = fc.IdCategorie;
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
