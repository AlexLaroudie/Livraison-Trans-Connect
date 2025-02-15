using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanotte_Laroudie
{
    /// <summary>
    /// Classe hérité de salrié permettant de manipuler des chaufeurs car ils ont des roles spécifiques
    /// </summary>
    public class Chauffeur : Salarie
    {

        public HashSet<DateTime> emploiDuTemps;
        public List<double> distanceparcourues;
        public string PermisConduire { get;  set; }

        public Chauffeur(int numSS, string nom, string prenom, DateTime dateNaissance, string adressePostale, string adresseMail, string telephone,
                         DateTime dateEntree, string poste, double salaire, string departement,string permisConduire)
                         : base(numSS, nom, prenom, dateNaissance, adressePostale, adresseMail, telephone, dateEntree, poste, salaire,departement)
        {
            PermisConduire = permisConduire;
            emploiDuTemps = new HashSet<DateTime>();
            distanceparcourues = new List<double>();
        }
        public bool AjouterLivraison(DateTime dateLivraison) // cette méthode sert à ajouter la livraison dans l'emploi du temps du chauffeur
        {
            if (emploiDuTemps.Contains(dateLivraison))
            {
                return false; 
            }

            emploiDuTemps.Add(dateLivraison);
            return true; 
        }

        public bool EstDisponiblePourLivraison(DateTime dateLivraison) // cette méthode nous sert à vérifier si un chauffeur est disponible à une certaine date pour choisir quel chauffeur
        {
            return !emploiDuTemps.Contains(dateLivraison);
        }
        public void AjouterDistance(double distance)
        {
            distanceparcourues.Add(distance);
        }
    }
}
