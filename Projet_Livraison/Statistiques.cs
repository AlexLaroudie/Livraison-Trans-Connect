using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lanotte_Laroudie
{
    /// <summary>
    /// classe statistiques pour toutes les méthodes du module
    /// On avait également mis notre méthode pour extraire les fichiers 
    /// On aurait pu glisser cette méthode dans n'importe quelle autre classe
    /// </summary>
    public class Statistiques
    {

        public Dictionary<string, Tuple<int, TimeSpan>> ObtenirDistancefichier()
        {
            string filePath = "Distances.csv"; 
            char separator = ';'; 

            Dictionary<string, Tuple<int, TimeSpan>> cityDistances = new Dictionary<string, Tuple<int, TimeSpan>>();

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split(separator);
                if (parts.Length >= 4) 
                {
                    string key = $"{parts[0]}-{parts[1]}"; 

                    if (!int.TryParse(parts[2], out int distance))
                    {
                        Console.WriteLine($"Erreur de conversion de la distance pour la ligne : {line}");
                        continue;
                    }

                    if (!TimeSpan.TryParse(parts[3], out TimeSpan duration))
                    {
                        Console.WriteLine($"Erreur de conversion de la durée pour la ligne : {line}");
                        continue;
                    }

                    
                    Tuple<int, TimeSpan> value = Tuple.Create(distance, duration);

                   
                    cityDistances[key] = value;
                }
                else
                {
                    Console.WriteLine("Erreur de format de ligne dans le fichier CSV.");
                }
            }

            return cityDistances;
        }
        public  static double TotalCA(List<Client> clients)
        {
            double resultat = 0.0;
            foreach (Client client in clients)
            {
                for (int i =0;i<client.factures.Count;i++)
                {
                    resultat = resultat + client.factures[i].montant;
                }
            }
            return resultat;
        }

         public static void CommandeParDate (DateTime datedébut, List<Client> clients, DateTime datefin)
        {
            List<Facture> facturedate = new List<Facture>();
            foreach (Client client in clients)
            {
                for (int i =0;i< client.factures.Count;i++)
                {
                    if (client.factures[i].date >= datedébut && client.factures[i].date<= datefin)
                    {
                        facturedate.Add(client.factures[i]);
                    }
                }
                
            }
            if (facturedate.Count > 0)
            {
                foreach (Facture facture in facturedate)
                {
                    Console.WriteLine("Voici la liste des commandes dans l'intervalle demandées : \n");
                    Console.WriteLine("-   "+facture.ToString());
                    Console.WriteLine(" \n");
                }
            }
            else
            {
                Console.WriteLine("Aucune commande trouvées dans l'intervalle");
            }
           
        }

        /// <summary>
        /// Liste toutes les dates ou le chauffeur a effectués une livraison
        /// </summary>
        /// <param name="chauffeurs"></param>
         public static void Commandeeffectuéschauffeur (List<Chauffeur> chauffeurs)
        {
            foreach (var chauffeur in chauffeurs)
            {
                Console.WriteLine($"Commandes effectuées par {chauffeur.Nom} {chauffeur.Prenom} :\n");
                foreach (var dateLivraison in chauffeur.emploiDuTemps)
                {
                    if (dateLivraison < DateTime.Today) 
                    {
                        Console.WriteLine($"- Date de livraison : {dateLivraison.ToShortDateString()}");
                        
                    }
                }
                Console.WriteLine("\n"); 
            }
            Console.ReadLine();
        }
        public static  void PrixMoyenParClient(List<Client> clients)
        {
            Console.WriteLine("Prix moyen payé par chaque clients : \n\n");
            foreach (Client client in clients)
            {

                Console.WriteLine("- Client : " + client.Nom + "    " + client.Moyenne() + " euros");
            }
            Console.ReadLine();
        }
        
         public static void PrixMoyenCommande(List<Client> clients)
        {
            double somme = 0;
            int clientsAvecCommandes = 0;

            Console.WriteLine("Prix moyen pour toutes les commandes :\n\n ");
            foreach (Client client in clients)
            {
                if (client.factures.Count > 0)
                {
                    somme += client.Moyenne();
                    clientsAvecCommandes++;
                }
            }

            if (clientsAvecCommandes > 0)
            {
                Console.WriteLine($"-  {somme / clientsAvecCommandes} euros ");
            }
            else
            {
                Console.WriteLine("Aucune commande n'a été trouvée chez les clients.");
            }

            Console.ReadLine();
        }

         public static void MeilleurClient(List<Client> clients)
        {
            Console.Write("Le meilleur client de TransConnect est  ");
            double max = 0;
            int indexmeilleur = -1;

            for (int i = 0;i< clients.Count;i++)
            {
                if (clients[i].Moyenne() > max)
                {
                    max = clients[i].Moyenne();
                    indexmeilleur = i;
                }
            }
            Console.WriteLine(clients[indexmeilleur].Nom+ " avec un montant moyen de " + clients[indexmeilleur].Moyenne()+" euros");

        }
        public static void KilometreparChaufeur (List<Chauffeur> chauffeurs)
        {
            Console.WriteLine("Distance parcourues par chauffeur : \n");
            foreach(Chauffeur chauffeur in chauffeurs)
            {
                double somme = 0;
                    for (int i =0;i<chauffeur.distanceparcourues.Count;i++)
                {
                    somme = somme + chauffeur.distanceparcourues[i];
                }
                Console.WriteLine("- Chauffeur : " + chauffeur.Nom + "    " + somme + " km");
            }
        }
        public static double Massesalariale (List<Salarie> liste)
        {
            double somme = 0;
            foreach(Salarie salarie in liste)
            {
                somme = somme + salarie.Salaire;
            }
            Console.WriteLine($"Le total des salaires payés est de {somme} euros par an");
            return somme;
            
        }
        public static void Detailsalaire(List<Salarie> liste)
        {
            foreach (Salarie salarie in liste)
            {

                Console.WriteLine("- Salrié : " + salarie.Nom + "    " + salarie.Salaire + " euros");
            }
            Console.ReadLine();
        }
    }
}
