using Microsoft.CodeAnalysis;

namespace XefiAcademyAPI.Model
{
    public class CategoriesEntitity
    {
        public int IdCategorie { get; set; }

        public string Libelle { get; set; }

        public CategoriesEntitity()
        {

        }

        public CategoriesEntitity(int IdCategorie, string Libelle)
        {
            this.IdCategorie = IdCategorie;
            this.Libelle = Libelle;
        }
    }
}
