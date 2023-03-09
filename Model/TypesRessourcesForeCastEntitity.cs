namespace XefiAcademyAPI.Model
{
    public class TypesRessourcesForeCastEntitity
    {
        public int IdTypeRessource { get; set; }

        public string LienImage { get; set; }

        public TypesRessourcesForeCastEntitity()
        {

        }

        public TypesRessourcesForeCastEntitity(int IdTypeRessource, string LienImage)
        {
            this.IdTypeRessource = IdTypeRessource;
            this.LienImage = LienImage;
        }
    }
}
