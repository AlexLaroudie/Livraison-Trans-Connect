using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Lanotte_Laroudie
{
    /// <summary>
    /// Les factures sont des vitrines des commandes que l'on va venir affiché et donner au clients dans une liste
    /// Chaque client possède donc une liste de facture plutot qu'une liste de commande
    /// Comme les données inutiles pour lui comme les chauffeurs ne sont pas dedans c'est plus facile de manipulation
    /// </summary>
    public class Facture
    {
        public double montant { get; set; }
        public Marchandise colis { get; set; }
        public DateTime date { get; set; }
        public int Num_livreur { get; set; }
        public string lieu_départ { get; set; }
        public string lieu_arrivée { get; set; }
        public int Idclient { get; set; }

        public Facture(double montant,Marchandise colis,DateTime date,int Num_livreur,string lieu_départ,string lieu_arrivée,int Idclient) 
        {
            this.montant = montant;
            this.colis = colis;
            this.date = date;
            this.Num_livreur= Num_livreur;
            this.lieu_arrivée = lieu_arrivée; this.Idclient = Idclient;
            this.lieu_départ = lieu_départ;
        }

        public string ToString()
        {
            
            return "Montant : " + montant+" euros ;   "+colis.ToString()+" ;\n  Date : "+ date.ToShortDateString() +" ; Identifiant du livreur : "+Num_livreur+" ;\n Lieu de départ : "+lieu_départ+" ; Lieu d'arrivée : "+lieu_arrivée+" ; Identifiant du client : "+ Idclient;
        }
        public string GetFactureText()
        {
            return $"Montant : {montant} euros\n{colis}\nDate : {date.ToShortDateString()}\nIdentifiant du livreur : {Num_livreur}\nLieu de départ : {lieu_départ}\nLieu d'arrivée : {lieu_arrivée}\nIdentifiant du client : {Idclient}";
        }

    }
}
