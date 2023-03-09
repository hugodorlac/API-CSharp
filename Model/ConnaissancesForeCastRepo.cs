using System.Data;
using System.Data.SqlClient;

namespace XefiAcademyAPI.Model
{
    public class ConnaissancesForeCastRepo
    {
        private readonly IConfiguration? _configuration;
        public ConnaissancesForeCastRepo(IConfiguration? configuration)
        {
            _configuration = configuration;

        }

        public ConnaissancesForeCastEntitity GetConnaissance(int id)
        {

            var oSqlParam = new SqlParameter("@Id", id);
            var oConnaissances = new ConnaissancesForeCastEntitity();
            var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
            var oSqlCommand = new SqlCommand("select * from Connaissances where IdConnaissance = @Id");
            var oSqlAdapter = new SqlDataAdapter(oSqlCommand);

            oSqlCommand.Parameters.Add(oSqlParam);


            oSqlCommand.Connection = oSqlConnection;
            var oDt = new DataTable();
            oSqlConnection.Open();
            oSqlAdapter.Fill(oDt);
            oSqlConnection.Close();

            if (oDt.Rows.Count > 0)
            {
                oConnaissances.IdConnaissance = (int)oDt.Rows[0][0];
                oConnaissances.IdCategorie = oDt.Rows[0][1] != DBNull.Value ? (int)oDt.Rows[0][1] : 0; //Si c'est NULL = 0
                oConnaissances.Libelle = (string)oDt.Rows[0][2];
                oConnaissances.DescriptionCourte = (string)oDt.Rows[0][3];
                oConnaissances.DescriptionLongue = (string)oDt.Rows[0][4];
            }

            return oConnaissances;

        }

        public bool UpdateConnaissances(ConnaissancesForeCastEntitity fc)
        {
            try
            {

                var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
                var oSqlParam = new SqlParameter("@Id", fc.IdConnaissance);
                var oSqlParam1 = new SqlParameter("@IdCategorie", fc.IdCategorie);
                var oSqlParam2 = new SqlParameter("@Libelle", fc.Libelle);
                var oSqlParam3 = new SqlParameter("@DescriptionCourte", fc.DescriptionCourte);
                var oSqlParam4 = new SqlParameter("@DescriptionLongue", fc.DescriptionLongue);

                var oSqlCommand = new SqlCommand("Update Connaissances Set Libelle=@Libelle,DescriptionCourte=@DescriptionCourte,DescriptionLongue=@DescriptionLongue   Where IdConnaissance = @Id ");

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

        public bool DeleteConnaissance(int Id)
        {
            try
            {

                var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
                var oSqlParam = new SqlParameter("@Id", Id);


                var oSqlCommand = new SqlCommand("Delete Connaissances Where IdConnaissance = @Id ");

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

        public int CreateConnaissance(ConnaissancesForeCastEntitity fc)
        {
            try
            {

                var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));

                var oSqlParam2 = new SqlParameter("@Libelle", fc.Libelle);
                var oSqlParam3 = new SqlParameter("@DescriptionCourte", fc.DescriptionCourte);
                var oSqlParam4 = new SqlParameter("@DescriptionLongue", fc.DescriptionLongue);

                var oSqlCommand = new SqlCommand("Insert Into  Connaissances(Libelle,DescriptionCourte,DescriptionLongue) Values (@Libelle,@DescriptionCourte, @DescriptionLongue);");

                oSqlCommand.Parameters.Add(oSqlParam2);
                oSqlCommand.Parameters.Add(oSqlParam3);
                oSqlCommand.Parameters.Add(oSqlParam4);


                oSqlCommand.Connection = oSqlConnection;
                oSqlConnection.Open();
                var Idretour = fc.IdConnaissance;
                oSqlCommand!.ExecuteNonQuery();
                oSqlConnection.Close();

                return Idretour;
            }
            catch (Exception)
            {

                return -1;
            }
        }

        public List<ConnaissancesForeCastEntitity> GetAllConnaissance()
        {
            var oList = new List<ConnaissancesForeCastEntitity>();
            var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
            var oSqlCommand = new SqlCommand("Select * From Connaissances Order By IdConnaissance");

            oSqlCommand.Connection = oSqlConnection;
            oSqlConnection.Open();

            var oSqlDataReader = oSqlCommand.ExecuteReader();
            while (oSqlDataReader.Read())
            {
                oList.Add(new ConnaissancesForeCastEntitity
                {
                    IdConnaissance = (int)oSqlDataReader["IdConnaissance"],
                    IdCategorie = oSqlDataReader["IdCategorie"] != DBNull.Value ? (int)oSqlDataReader["IdCategorie"] : 0, //Si c'est NULL = 0
                    Libelle = (string)oSqlDataReader["Libelle"],
                    DescriptionCourte = (string)oSqlDataReader["DescriptionCourte"],
                    DescriptionLongue = (string)oSqlDataReader["DescriptionLongue"]
                });
            };
            oSqlDataReader.Close();
            oSqlConnection.Close();


            return oList;

        }

        //public List<ConnaissancesForeCastEntitity> GetCity(string Libelle)
        //{
        //    var oList = new List<ConnaissancesForeCastEntitity>();
        //    var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
        //    var oSqlCommand = new SqlCommand($"SELECT * FROM weather w INNER JOIN city ON w.idVille = city.idVille where Libelle = '{Libelle}' ;");

        //    oSqlCommand.Connection = oSqlConnection;
        //    oSqlConnection.Open();

        //    var oSqlDataReader = oSqlCommand.ExecuteReader();
        //    while (oSqlDataReader.Read())
        //    {
        //        oList.Add(new ConnaissancesForeCastEntitity { Id = (int)oSqlDataReader["ID"], Date = DateOnly.FromDateTime((DateTime)oSqlDataReader["DATE"]), TemperatureC = (int)oSqlDataReader["TEMPERATUREC"], Summary = (string)oSqlDataReader["SUMMARY"], idVille = (int)oSqlDataReader["idVille"], Libelle = (string)oSqlDataReader["Libelle"], Pays = (string)oSqlDataReader["Pays"] });

        //    };
        //    oSqlDataReader.Close();
        //    oSqlConnection.Close();


        //    return oList;

        //}

        //public List<ConnaissancesForeCastEntitity> GetAllCity()
        //{
        //    var oList = new List<ConnaissancesForeCastEntitity>();
        //    var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
        //    var oSqlCommand = new SqlCommand("SELECT * FROM weather w INNER JOIN city ON w.idVille = city.idVille order by w.idVille;");

        //    oSqlCommand.Connection = oSqlConnection;
        //    oSqlConnection.Open();

        //    var oSqlDataReader = oSqlCommand.ExecuteReader();
        //    while (oSqlDataReader.Read())
        //    {
        //        oList.Add(new ConnaissancesForeCastEntitity { Id = (int)oSqlDataReader["ID"], Date = DateOnly.FromDateTime((DateTime)oSqlDataReader["DATE"]), TemperatureC = (int)oSqlDataReader["TEMPERATUREC"], Summary = (string)oSqlDataReader["SUMMARY"], idVille = (int)oSqlDataReader["idVille"], Libelle = (string)oSqlDataReader["Libelle"], Pays = (string)oSqlDataReader["Pays"] });

        //    };
        //    oSqlDataReader.Close();
        //    oSqlConnection.Close();


        //    return oList;

        //}

        public int InsertCity(ConnaissancesForeCastEntitity fc)
        {
            try
            {

                var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));

                var oSqlParam = new SqlParameter("@Id", fc.IdConnaissance);
                var oSqlParam2 = new SqlParameter("@Libelle", fc.Libelle);

                var oSqlCommand = new SqlCommand("Insert Into  city(idVille,Libelle,Pays) Values (@Id, @Libelle,@Pays);");

                oSqlCommand.Parameters.Add(oSqlParam);
                oSqlCommand.Parameters.Add(oSqlParam2);


                oSqlCommand.Connection = oSqlConnection;
                oSqlConnection.Open();
                var Idretour = fc.IdConnaissance;
                oSqlCommand!.ExecuteNonQuery();
                oSqlConnection.Close();

                return Idretour;
            }
            catch (Exception)
            {

                return -1;
            }
        }
    }
}
