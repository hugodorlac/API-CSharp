using Microsoft.CodeAnalysis;

namespace XefiAcademyAPI.Model
{
    public class CategoriesForeCastEntitity
    {
        public int IdCategorie { get; set; }

        public string Libelle { get; set; }

        public CategoriesForeCastEntitity()
        {

        }

        public CategoriesForeCastEntitity(int IdCategorie, string Libelle)
        {
            this.IdCategorie = IdCategorie;
            this.Libelle = Libelle;
        }
    }
}
