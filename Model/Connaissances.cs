using System.Data;
using System.Data.SqlClient;

namespace XefiAcademyAPI.Model
{
    public class Connaissances
    {
        private readonly IConfiguration? _configuration;
        public Connaissances(IConfiguration? configuration)
        {
            _configuration = configuration;

        }

        public ConnaissancesEntitity GetConnaissance(int id)
        {

            var oSqlParam = new SqlParameter("@Id", id);
            var oConnaissances = new ConnaissancesEntitity();
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

                if (oDt.Rows[0][1] != DBNull.Value)
                {
                    using (SqlConnection oSqlConnection1 = new SqlConnection(_configuration?.GetConnectionString("SQL")))
                    {
                        oSqlConnection1.Open();
                        using (SqlCommand oSqlCommand1 = new SqlCommand("SELECT * FROM Categories WHERE IdCategorie = @Id", oSqlConnection1))
                        {
                            oSqlCommand1.Parameters.AddWithValue("@Id", oDt.Rows[0][1]);

                            using (SqlDataReader reader = oSqlCommand1.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    oConnaissances.LibelleCategorie = (string)reader["Libelle"];
                                }
                            }
                        }
                    }
                }
            }
            return oConnaissances;
        }

        public bool UpdateConnaissances(ConnaissancesEntitity fc)
        {
            try
            {

                var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
                var oSqlParam = new SqlParameter("@Id", fc.IdConnaissance);
                var oSqlParam1 = new SqlParameter("@IdCategorie", fc.IdCategorie);
                var oSqlParam2 = new SqlParameter("@Libelle", fc.Libelle);
                var oSqlParam3 = new SqlParameter("@DescriptionCourte", fc.DescriptionCourte);
                var oSqlParam4 = new SqlParameter("@DescriptionLongue", fc.DescriptionLongue);

                var oSqlCommand = new SqlCommand("Update Connaissances Set IdCategorie=@IdCategorie,Libelle=@Libelle,DescriptionCourte=@DescriptionCourte,DescriptionLongue=@DescriptionLongue   Where IdConnaissance = @Id ");
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

        public decimal CreateConnaissance(ConnaissancesEntitity fc)
        {
            try
            {

                var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));

                var oSqlParam1 = new SqlParameter("@IdCategorie", fc.IdCategorie);
                var oSqlParam2 = new SqlParameter("@Libelle", fc.Libelle);
                var oSqlParam3 = new SqlParameter("@DescriptionCourte", fc.DescriptionCourte);
                var oSqlParam4 = new SqlParameter("@DescriptionLongue", fc.DescriptionLongue);

                var oSqlCommand = new SqlCommand("Insert Into  Connaissances(IdCategorie,Libelle,DescriptionCourte,DescriptionLongue) Values (@IdCategorie,@Libelle,@DescriptionCourte, @DescriptionLongue); SELECT @@identity;");

                oSqlCommand.Parameters.Add(oSqlParam1);
                oSqlCommand.Parameters.Add(oSqlParam2);
                oSqlCommand.Parameters.Add(oSqlParam3);
                oSqlCommand.Parameters.Add(oSqlParam4);


                oSqlCommand.Connection = oSqlConnection;
                oSqlConnection.Open();
                var Idretour = (decimal)oSqlCommand.ExecuteScalar();
                oSqlConnection.Close();

                return Idretour;
            }
            catch (Exception)
            {

                return -1;
            }
        }

        public List<ConnaissancesEntitity> GetAllConnaissance()
        {
            var oList = new List<ConnaissancesEntitity>();
            var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
            var oSqlCommand = new SqlCommand("Select * From Connaissances Order By IdConnaissance");

            oSqlCommand.Connection = oSqlConnection;
            oSqlConnection.Open();

            var oSqlDataReader = oSqlCommand.ExecuteReader();
            while (oSqlDataReader.Read())
            {
                // Create a new ConnaissancesForeCastEntitity object with the properties from the SqlDataReader
                ConnaissancesEntitity entity = new ConnaissancesEntitity
                {
                    IdConnaissance = (int)oSqlDataReader["IdConnaissance"],
                    IdCategorie = oSqlDataReader["IdCategorie"] != DBNull.Value ? (int)oSqlDataReader["IdCategorie"] : 0,
                    Libelle = (string)oSqlDataReader["Libelle"],
                    DescriptionCourte = (string)oSqlDataReader["DescriptionCourte"],
                    DescriptionLongue = (string)oSqlDataReader["DescriptionLongue"]
                };

                // Check if the IdCategorie is not null before executing the SQL query
                if (oSqlDataReader["IdCategorie"] != DBNull.Value)
                {
                    using (SqlConnection oSqlConnection1 = new SqlConnection(_configuration?.GetConnectionString("SQL")))
                    {
                        oSqlConnection1.Open();
                        using (SqlCommand oSqlCommand1 = new SqlCommand("SELECT Libelle FROM Categories WHERE IdCategorie = @Id", oSqlConnection1))
                        {
                            // Use the IdCategorie property of the entity, not the IdConnaissance property
                            oSqlCommand1.Parameters.AddWithValue("@Id", entity.IdCategorie);

                            using (SqlDataReader reader = oSqlCommand1.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    // Set the LibelleCategorie property of the entity, not the local variable
                                    entity.LibelleCategorie = (string)reader["Libelle"];
                                }
                            }
                        }
                    }
                }

                // Add the entity to the list
                oList.Add(entity);
            }
            oSqlDataReader.Close();
            oSqlConnection.Close();


            return oList;

        }

        public int ReadNumberConnaissances()
        {
            try
            {

                var oSqlConnection = new SqlConnection(_configuration?.GetConnectionString("SQL"));
                var oSqlCommand = new SqlCommand("select count(*) from Connaissances");
                oSqlCommand.Connection = oSqlConnection;

                oSqlConnection.Open();
                int count = (int)oSqlCommand.ExecuteScalar();
                oSqlConnection.Close();

                return count;
            }
            catch (Exception)
            {

                return -1;
            }
        }

    }
}
