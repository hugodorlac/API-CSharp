namespace XefiAcademyAPI.Model
{
    public class ConnaissancesEntitity
    {
        public int IdConnaissance { get; set; }


        public int IdCategorie { get; set; }


        public string DescriptionCourte { get; set; }
        public string DescriptionLongue { get; set; }

        public string? Libelle { get; set; }
        public string? LibelleCategorie { get; set; }


        public ConnaissancesEntitity()
        {

        }
        public ConnaissancesEntitity(int Id, int IdCategorie, string Libelle, string DescriptionCourte, string DescriptionLongue, string LibelleCategorie)
        {
            this.IdConnaissance = Id;
            this.IdCategorie = IdCategorie;
            this.Libelle = Libelle;
            this.DescriptionCourte = DescriptionCourte;
            this.DescriptionLongue = DescriptionLongue;
            this.LibelleCategorie = LibelleCategorie;


        }
    }
}
