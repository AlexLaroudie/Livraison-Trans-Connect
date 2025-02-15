using iTextSharp.text.log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanotte_Laroudie
{
    /// <summary>
    /// Méthode pour pouvoir obtenir les chemins les plus courts à partir de l'algorithme de Dijsktra. 
    /// 
    /// </summary>
    internal class GrapheDjistkra
    {
        public Dictionary<string, List<Tuple<string, int>>> listeAdjacence;


        private void AjouterArete(string source, string destination, int poids)
        {
            if (!listeAdjacence.ContainsKey(source))
            {
                listeAdjacence[source] = new List<Tuple<string, int>>();
            }
            listeAdjacence[source].Add(new Tuple<string, int>(destination, poids));
        }

        // On veut implémenter une liste d'adjacence à partir du dictionaire
        // La clé du premier argument est la ville de départ suivi d'un tuple (ville d'arrivée, distance pour y arriver)
        public GrapheDjistkra (Dictionary<string, Tuple<int, TimeSpan>> distancesVilles)  
        {
            listeAdjacence = new Dictionary<string, List<Tuple<string, int>>>();

            
            foreach (var entree in distancesVilles)
            {
                string[] villes = entree.Key.Split('-');
                string source = villes[0];
                string destination = villes[1];
                int distance = entree.Value.Item1;

                
                AjouterArete(source, destination, distance);     // on va creer ici un graphe non orienté 
                AjouterArete(destination, source, distance);


            }
        }

        
       




        // Méthode pour utiliser l'algorithme
        public Dictionary<string, int> Dijkstra(string noeudDepart)
        {
            
            Dictionary<string, int> distances = new Dictionary<string, int>();
            foreach (var noeud in listeAdjacence.Keys)
            {
                distances[noeud] = int.MaxValue;   // on crée la distance max qui équivaut à notre infini sur papier
            }
            distances[noeudDepart] = 0;

            // On crée une liste unique (HashSet) pour tous les noeuds ou on est pass encore allé
            HashSet<string> noeudsNonVisites = new HashSet<string>(listeAdjacence.Keys);

            
            while (noeudsNonVisites.Count > 0)
            {
                
                string noeudActuel = null;
                foreach (var noeud in noeudsNonVisites)
                {
                    if (noeudActuel == null || distances[noeud] < distances[noeudActuel])
                    {
                        noeudActuel = noeud;
                    }
                }

                
                noeudsNonVisites.Remove(noeudActuel); // Le noeud est dorénavant visité

                
                if (listeAdjacence.ContainsKey(noeudActuel))
                {
                    foreach (var arete in listeAdjacence[noeudActuel])
                    {
                        if (distances.ContainsKey(arete.Item1))
                        {
                            int nouvelleDistance = distances[noeudActuel] + arete.Item2;
                            if (nouvelleDistance < distances[arete.Item1])
                            {
                                distances[arete.Item1] = nouvelleDistance;
                            }
                        }
                        
                        
                    }
                }
            }
            foreach (var pair in distances)
            {
                //¨Pour l'affichage de notre résultat soit propre
                string keyAligned = pair.Key.PadRight(20);
                Console.WriteLine($"{keyAligned} {pair.Value} km");
            }
            return distances;
        }


        // Methode pour afficher le graphe final
        public void AfficherGraphe(Dictionary<string, List<Tuple<string, int>>> listeAdjacence)
        {
            Console.WriteLine("Graphe :");
            foreach (var pair in listeAdjacence)
            {
                string noeud = pair.Key;
                List<Tuple<string, int>> aretes = pair.Value;

                Console.Write($"Noeud {noeud.PadRight(15)} : ");

                foreach (var arete in aretes)
                {
                    string destination = arete.Item1;
                    int poids = arete.Item2;

                    Console.Write($"({destination.PadRight(12)}, distance : {poids} km) ");
                }

                Console.WriteLine();
            }
        }



    }
}
