using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanotte_Laroudie
{
    /// <summary>
    /// Classe pour charger et sauvegarder les document
    /// on a cree des délégués et des méthodes de types T pour pouvoir les utiliser en fonction de nso differents types
    /// La sauvegarde se fait dans des fichiers JSON avec des commandes de serialisation 
    /// </summary>
    internal class Sauvegarde
    {
        public static string cheminFichierClients = "clients.json"; 
        public static string cheminFichierSalaries = "salaries.json";
        public static string cheminFichierChauffeurs = "chauffeurs.json";
        public static string cheminFichierVehicules = "Vehicules.json";


        public delegate List<T> DelegueChargementDonnees<T>();


        public delegate void DelegueSauvegardeDonnees<T>(List<T> donnees);

        
        public static List<T> ChargerDonnees<T>(string cheminFichier, DelegueChargementDonnees<T> chargerDonnees)
        {
            if (File.Exists(cheminFichier))
            {
                string jsonData = File.ReadAllText(cheminFichier);
                return chargerDonnees();
            }
            else
            {
                
                return new List<T>();
            }
        }

        
        public static void SauvegarderDonnees<T>(string cheminFichier, List<T> donnees, DelegueSauvegardeDonnees<T> sauvegarderDonnees)
        {
            string jsonData = JsonConvert.SerializeObject(donnees, Formatting.Indented);
            File.WriteAllText(cheminFichier, jsonData);
        }

        
        public static List<Client> ChargerClients(string jsonData)
        {
            return JsonConvert.DeserializeObject<List<Client>>(jsonData);
        }

        
        public static void SauvegarderClients(List<Client> clients)
        {
            SauvegarderDonnees(cheminFichierClients, clients, SauvegarderClients);
        }

        
        public static List<Salarie> ChargerSalaries(string jsonData)
        {
            return JsonConvert.DeserializeObject<List<Salarie>>(jsonData);
        }

        
        public static void SauvegarderSalaries(List<Salarie> salaries)
        {
            SauvegarderDonnees(cheminFichierSalaries, salaries, SauvegarderSalaries);
        }

        
        public static List<Chauffeur> ChargerChauffeurs(string jsonData)
        {
            return JsonConvert.DeserializeObject<List<Chauffeur>>(jsonData);
        }

        
        public static void SauvegarderChauffeurs(List<Chauffeur> chauffeurs)
        {
            SauvegarderDonnees(cheminFichierChauffeurs, chauffeurs, SauvegarderChauffeurs);
        }


        public static List<Vehicule> ChargerVehicule(string jsonData)
        {
            return JsonConvert.DeserializeObject<List<Vehicule>>(jsonData);
        }

        
        public static void SauvegarderVehicule(List<Vehicule> vehicules)
        {
            SauvegarderDonnees(cheminFichierChauffeurs, vehicules, SauvegarderVehicule);
        }

    }
}
