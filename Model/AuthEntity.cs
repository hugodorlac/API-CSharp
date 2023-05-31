namespace XefiAcademyAPI.Model
{
    public class AuthEntitity
    {

        public int IdUtilisateur { get; set; }

        public string Email { get; set; }
        public string MotDePasse { get; set; }

        public AuthEntitity()
        {

        }
        public AuthEntitity(int Id, string Email, string MotDePasse)
        {
            this.IdUtilisateur = Id;
            this.Email = Email;
            this.MotDePasse = MotDePasse;
        }
    }
}
