using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lanotte_Laroudie
{
    /// <summary>
    /// Classe Commande pour lier les differentes instances et pouvoir passer commande 
    /// Dans cette calsse on a défini de sméthode choisir et pour vérifier la disponibilité des véhicules et des chauffeurs pour valider la commande
    /// Il y a également une méthode pour obtenir une distance en utilisant l'API Google map
    /// </summary>
    internal class Commande
    {
       public Client client { get; set; }
        public Chauffeur chauffeur { get; set; }
        public DateTime dateLivraison { get; set; }
        public Vehicule vehicule { get; set; }
        public Marchandise marchandise { get; set; }
        public double prix { get; set; }

        public Commande(Client client,Chauffeur chauffeur, DateTime datelivraison, Vehicule vehicule, Marchandise marchandise, double prix)
        {
            this.client = client;
            this.chauffeur = chauffeur;
            this.dateLivraison = datelivraison;
            this.vehicule = vehicule;
            this.marchandise = marchandise;
            this.prix = prix;
        }
        /// <summary>
        /// On chosiit quel type de vehicule a partir des marchandise transportées et en quelle quantité
        /// </summary>
        /// <param name="type"></param>
        /// <param name="taille"></param>
        /// <returns></returns>
        public static Vehicule Choixtypevehicule(TypeMarchandise type,double taille)
        {
            Vehicule vehicule1 = new Vehicule(3,0,DateTime.Now,"c",Typevehicule.voiture);
            
            
            switch (type)
            {
                case TypeMarchandise.Liquide:
                    vehicule1.typevehicule = Typevehicule.camion_citerne;
                    break;
                case TypeMarchandise.Passager:
                    if (taille <= 4)
                    {
                        vehicule1.typevehicule = Typevehicule.voiture;
                    }
                    else
                    {
                        if (taille <= 9)
                        {
                            vehicule1.typevehicule = Typevehicule.minivan;
                        }
                    }
                    break;
                case TypeMarchandise.Perissable:
                    vehicule1.typevehicule = Typevehicule.camion_frigorifique;
                    break;
                case TypeMarchandise.Autre:
                  if (taille <= 5)
                    {
                        vehicule1.typevehicule = Typevehicule.camionette;
                    }
                    else
                    {
                        vehicule1.typevehicule = Typevehicule.camion_benne;
                    }
                    break;
            }
            return vehicule1;
        }

        /// <summary>
        /// Test a partir du type de vehicule si un est dispo pour la date de livraison
        /// </summary>
        /// <param name="t"></param>
        /// <param name="flotte"></param>
        /// <param name="dateLivraison"></param>
        /// <returns></returns>
        public static Vehicule TrouverVehiculeDisponible(Typevehicule t, List<Vehicule> flotte, DateTime dateLivraison)
        {
            foreach (var vehicule in flotte)
            {
                if (vehicule.typevehicule == t && !vehicule.agenda.Contains(dateLivraison))
                {
                    vehicule.agenda.Add(dateLivraison);
                    return vehicule;
                }
            }
            Console.WriteLine("Aucun véhicule approprié disponible à cette date");
            return null;
        }

        /// <summary>
        /// A partir de l'api google map
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        public static double CalculDistance(string origin, string destination) // Cette méthode permet d'obtenir la distance à partir de l'API Google Map
        {
            string apiKey = Environment.GetEnvironmentVariable("GOOGLE_MAPS_API_KEY");
            string url = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={origin}&destinations={destination}&key={apiKey}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    dynamic data = JObject.Parse(json);
                    string distanceString = data.rows[0].elements[0].distance.text;
                    Console.WriteLine("\n\nLa distance  de votre trajet est de : "+distanceString);
                    Console.ReadLine();
                    
                    Match match = Regex.Match(distanceString, @"[\d\.]+");         // retire tous les caractères superflux autre que des chiffres ou des points
                    if (match.Success)
                    {
                        
                        
                        double distance = double.Parse(match.Value, CultureInfo.InvariantCulture);
                        return distance;
                    }
                    else
                    {
                        throw new FormatException("Format de distance non valide.");
                    }

                    
                }
                else
                {
                    throw new HttpRequestException($"Erreur lors de la requête : {response.StatusCode}");
                }
            }
        }
        public static double Prixlivraison(Typevehicule t,double distance)
        {
           
           switch (distance)
            {
                
                case < 250:
                    switch(t)
                    {
                        case Typevehicule.voiture:
                            return 75;
                        case Typevehicule.camionette:
                            return 150;
                        case Typevehicule.camion_citerne:
                            return 500;
                            
                        case Typevehicule.camion_frigorifique:
                            return 500;
                            
                        case Typevehicule.minivan:
                            return 120;
                            
                        case Typevehicule.camion_benne:
                            return 500;
                            
                    }
                    break;
                case < 500:
                    switch (t)
                    {
                        case Typevehicule.voiture:
                            return 120;
                        case Typevehicule.camionette:
                            return 200;
                        case Typevehicule.camion_citerne:
                            return 750;

                        case Typevehicule.camion_frigorifique:
                            return 750;

                        case Typevehicule.minivan:
                            return 150;

                        case Typevehicule.camion_benne:
                            return 750;

                    }
                    break;
                case < 1000:
                    switch (t)
                    {
                        case Typevehicule.voiture:
                            return 200;
                        case Typevehicule.camionette:
                            return 250;
                        case Typevehicule.camion_citerne:
                            return 1000;

                        case Typevehicule.camion_frigorifique:
                            return 1000;

                        case Typevehicule.minivan:
                            return 250;

                        case Typevehicule.camion_benne:
                            return 1000;

                    }
                    break;
                case > 1000:
                    switch (t)
                    {
                        case Typevehicule.voiture:
                            return 300;
                        case Typevehicule.camionette:
                            return 500;
                        case Typevehicule.camion_citerne:
                            return 2000;

                        case Typevehicule.camion_frigorifique:
                            return 2000;

                        case Typevehicule.minivan:
                            return 500;

                        case Typevehicule.camion_benne:
                            return 2000;

                    }
                    break;
            }
            return 0.0; 

        }

    }
}
