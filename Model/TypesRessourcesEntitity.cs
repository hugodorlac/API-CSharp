namespace XefiAcademyAPI.Model
{
    public class TypesRessourcesEntitity
    {
        public int IdTypeRessource { get; set; }

        public string LienImage { get; set; }

        public TypesRessourcesEntitity()
        {

        }

        public TypesRessourcesEntitity(int IdTypeRessource, string LienImage)
        {
            this.IdTypeRessource = IdTypeRessource;
            this.LienImage = LienImage;
        }
    }
}
