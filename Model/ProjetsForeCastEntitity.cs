namespace XefiAcademyAPI.Model
{
    public class ProjetsForeCastEntitity
    {
        public int IdProjet { get; set; }

        public int IdStatut { get; set; }

        public string Description { get; set; }


        public DateTime DateCreation { get; set; }
        public string Auteur { get; set; }

        public ProjetsForeCastEntitity()
        {

        }
        public ProjetsForeCastEntitity(int IdProjet, int IdStatut, string Description, DateTime DateCreation, string CreerPar)
        {
            this.IdProjet = IdProjet;
            this.IdStatut = IdStatut;
            this.Description = Description;
            this.DateCreation = DateCreation;
            this.Auteur = CreerPar;


        }
    }
}
