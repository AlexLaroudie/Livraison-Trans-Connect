using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanotte_Laroudie
{
    /// <summary>
    /// Une classe véhicule pour définir les différents posséessons de l'entreprise en fonction du type de marchandise à déplacer
    /// On a choisi de creer une classe enum au lieu de creer une classe pour chaque type de véhicule parceque cela n'apportait rien
    /// dans notre code on a juste besoin de d'accéder au type de véhicule
    /// </summary>
    public  class Vehicule
    {
        public int imatriculation { get; set; }
        public int kilométrage { get; set; }
        public DateTime controle_technique { get; set; }

        public string type_carburant { get; set; }
        public Typevehicule typevehicule { get; set; }

        public HashSet<DateTime> agenda { get; set; }

        public Vehicule(int imatriculation, int kilométrage, DateTime controle_technique, string type_carburant, Typevehicule typevehicule)
        {
            this.imatriculation = imatriculation;
            this.kilométrage = kilométrage;
            this.controle_technique = controle_technique;
            this.type_carburant = type_carburant;
            this.typevehicule = typevehicule;
            agenda = new HashSet<DateTime>();
        }
    }
}
