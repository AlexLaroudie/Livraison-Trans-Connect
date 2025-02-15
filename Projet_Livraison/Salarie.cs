using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanotte_Laroudie
{
    /// <summary>
    /// Herité de la classe Personne permet de définie les employés de notre entreprise 
    /// </summary>
    public class Salarie : Personne
    {
        public DateTime DateEntree { get;  set; }
        public string Poste { get;  set; } 
        public string Departement { get; set; }
        public double Salaire { get;  set; }


        
        public Salarie(int numSS, string nom, string prenom, DateTime dateNaissance, string adressePostale, string adresseMail, string telephone,
                       DateTime dateEntree, string poste, double salaire, string departement,string permisConduire = null)
                       : base(numSS, nom, prenom, dateNaissance, adressePostale, adresseMail, telephone)
        {
            DateEntree = dateEntree;
            Poste = poste;
            Salaire = salaire;
            Departement = departement;

            
        }

        public void ModifierPoste(string nouveauPoste)
        {
            Poste = nouveauPoste;
        }

        public void ModifierSalaire(double nouveauSalaire)
        {
            Salaire = nouveauSalaire;
        }
    }
}
