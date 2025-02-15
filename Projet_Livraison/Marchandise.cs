using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanotte_Laroudie
{
    /// <summary>
    /// Pour notre projet on a choisi de creer des marchandises qui seront livrées cela sera utilisé pour choisir le véhicule pour la commabde
    /// C'est défini en fonction du type de marchandise avec une classe enum et de la taille/quantité du produit
    /// </summary>
    public class Marchandise
    {
        public double Taille { get;  set; }
        public TypeMarchandise Type { get;  set; }

        public Marchandise(double taille, TypeMarchandise type)
        {
            Taille = taille;
            Type = type;
        }

        public string ToString()
        {
            return "Type de Marchandise : " + Type + "  ; Quantité : " + Taille;
        }
        
    }
}
