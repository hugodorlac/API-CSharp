﻿namespace XefiAcademyAPI.Model
{
    public class RessourcesForeCastEntitity
    {
        public int IdRessource { get; set; }

        public int IdConnaissance { get; set; }

        public int IdTypeRessource { get; set; }

        public string Auteur { get; set; }
        public string Contenu { get; set; }

        public RessourcesForeCastEntitity()
        {

        }

        public RessourcesForeCastEntitity(int IdRessource, int IdConnaissance, int IdTypeRessource,string Auteur, string Contenu)
        {
            this.IdRessource = IdRessource;
            this.IdConnaissance = IdConnaissance;
            this.IdTypeRessource = IdTypeRessource;
            this.Auteur = Auteur;
            this.Contenu = Contenu;
        }
    }
}
