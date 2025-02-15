using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanotte_Laroudie
{
    /// <summary>
    /// Classe abstract qui permettra de définir tous les individus de notre projet comme les employés ou les client
    /// </summary>
    public abstract class Personne
    {
        public int NumSS { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime DateNaissance { get; set; }
        public string AdressePostale { get; set; }
        public string AdresseMail { get; set; }
        public string Telephone { get; set; }

        public Personne(int numSS, string nom, string prenom, DateTime dateNaissance, string adressePostale, string adresseMail, string telephone)
        {
            NumSS = numSS;
            Nom = nom;
            Prenom = prenom;
            DateNaissance = dateNaissance;
            AdressePostale = adressePostale;
            AdresseMail = adresseMail;
            Telephone = telephone;
        }

        public void ModifierAdresse(string nouvelleAdresse)
        {
            AdressePostale = nouvelleAdresse;
        }

        public void ModifierMail(string nouveauMail)
        {
            AdresseMail = nouveauMail;
        }

        public void ModifierTelephone(string nouveauTelephone)
        {
            Telephone = nouveauTelephone;
        }
    }
}
