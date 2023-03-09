namespace XefiAcademyAPI.Model
{
    public class ConnaissancesForeCastEntitity
    {
        public int IdConnaissance { get; set; }


        public int IdCategorie { get; set; }


        public string DescriptionCourte { get; set; }
        public string DescriptionLongue { get; set; }

        public string? Libelle { get; set; }


        public ConnaissancesForeCastEntitity()
        {

        }
        public ConnaissancesForeCastEntitity(int Id, int IdCategorie, string Libelle, string DescriptionCourte, string DescriptionLongue)
        {
            this.IdConnaissance = Id;
            this.IdCategorie = IdCategorie;
            this.Libelle = Libelle;
            this.DescriptionCourte = DescriptionCourte;
            this.DescriptionLongue = DescriptionLongue;


        }
    }
}
