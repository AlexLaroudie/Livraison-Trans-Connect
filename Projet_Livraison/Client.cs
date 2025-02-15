using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanotte_Laroudie
{
    /// <summary>
    /// Herite de personne permet de définir des clients qui vont pouvoir passer commande dans l'entrprise
    /// Cette classe utilise l'interface Imoyenne pour pouvoir calculer la moyenne des dépenses du client
    /// </summary>
    public class Client : Personne, Imoyenne
    {
       
        public List<Facture> factures { get; set; }
        public Client(int numSS, string nom, string prenom, DateTime dateNaissance, string adressePostale, string adresseMail, string telephone)
                   : base(numSS, nom, prenom, dateNaissance, adressePostale, adresseMail, telephone)
        {
            
            factures= new List<Facture>();
        }
        public double Total_depense()
        {
            double total = 0;
            foreach (Facture facture in factures)
            {
                total = total + facture.montant;
            }
            return total;
        }
        public double Moyenne()
        {
            double somme =0;
            foreach(Facture facture in factures)
            {
                somme = somme + facture.montant;
            }
            return somme/factures.Count;
        }
        public double TotalDepense()
        {
            return factures.Sum(facture => facture.montant);
        }
        public static void AfficherClientsParNomAlphabetique(List<Client> clients)
        {
            var clientsTriés = clients.OrderBy(client => client.Nom).ToList();
            foreach (var client in clientsTriés)
            {
                Console.WriteLine($"Nom: {client.Nom}, Prénom: {client.Prenom}");
            }
        }

        public static void AfficherClientsParMontantTotal(List<Client> clients)
        {
            var clientsTriés = clients.OrderByDescending(client => client.TotalDepense()).ToList();
            foreach (var client in clientsTriés)
            {
                Console.WriteLine($"Nom: {client.Nom}, Prénom: {client.Prenom}, Montant total des factures: {client.TotalDepense()}");
            }
        }
    }
}
