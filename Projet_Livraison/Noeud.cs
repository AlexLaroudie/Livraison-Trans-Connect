using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanotte_Laroudie
{
    /// <summary>
    /// Cette classe permet de creer un arbre n-aire. On a choisi de définir chaque noeud avec un fils et un frère
    /// Chaque parent ne connait donc qu'un seul de ses fils même si ils en ont plusieurs
    /// On a défini des méthodes pour pouvoir utiliser et manipuler l'arbre ainsi creé
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Noeud<T> where T : Salarie
    {
        public T Employe { get; set; }
        public Noeud<T> Fils { get; set; } 
        public Noeud<T> Frere { get; set; } 

        public Noeud(T valeur)
        {
            Employe = valeur;
            Fils = null;
            Frere = null;
        }



        public void AjouterSalarié(Noeud<T> fils)
        {
            if (Fils == null)
            {
                Fils = fils;
            }
            else
            {
                Noeud<T> dernier_frere = Fils;
                while (dernier_frere.Frere != null)
                {
                    dernier_frere = dernier_frere.Frere;
                }
                dernier_frere.Frere = fils;
            }
        }

        public bool LicencierEtPromouvoir(T salarieLicencie)
        {
            if (Fils != null)
            {
                if (Fils.Employe.Equals(salarieLicencie)) // Si le fils est le salarié licencié
                {
                    
                    Fils = Fils.Frere;
                    return true;
                }
                else
                {
                    
                    if (Fils.LicencierEtPromouvoir(salarieLicencie))
                        return true;
                }
            }

            
            if (Frere != null)
            {
                if (Frere.Employe.Equals(salarieLicencie)) 
                {
                    
                    Frere = Frere.Frere;
                    return true;
                }
                else
                {
                    
                    if (Frere.LicencierEtPromouvoir(salarieLicencie))
                        return true;
                }
            }

            return false;
        }
        public Noeud<T> RechercherNoeud(T salarie)
        {
            if (Employe.Equals(salarie))
            {
                return this;
            }

            if (Fils != null)
            {
                var noeudTrouve = Fils.RechercherNoeud(salarie);
                if (noeudTrouve != null)
                {
                    return noeudTrouve;
                }
            }

            if (Frere != null)
            {
                var noeudTrouve = Frere.RechercherNoeud(salarie);
                if (noeudTrouve != null)
                {
                    return noeudTrouve;
                }
            }

            return null;
        }
        public Noeud<T> TrouverParent(Noeud<T> noeudRecherche)
        {
            if (Fils == noeudRecherche || Frere == noeudRecherche)
            {
                return this; // Si le nœud à rechercher est le fils ou le frère de ce nœud, alors ce nœud est le parent
            }

            if (Fils != null)
            {
                var parentDansFils = Fils.TrouverParent(noeudRecherche);
                if (parentDansFils != null)
                {
                    return parentDansFils;
                }
            }

            if (Frere != null)
            {
                var parentDansFrere = Frere.TrouverParent(noeudRecherche);
                if (parentDansFrere != null)
                {
                    return parentDansFrere;
                }
            }

            return null; 
        }
        public Noeud<Salarie> TrouverNoeudParNom(Noeud<Salarie> noeudCourant, string nom)
        {
            
            if (noeudCourant != null && noeudCourant.Employe != null && noeudCourant.Employe.Nom.Equals(nom))
            {
                return noeudCourant;
            }

            
            if (noeudCourant.Fils != null)
            {
                Noeud<Salarie> noeudTrouve = TrouverNoeudParNom(noeudCourant.Fils, nom);
                if (noeudTrouve != null)
                {
                    return noeudTrouve;
                }
            }

            
            if (noeudCourant.Frere != null)
            {
                Noeud<Salarie> noeudTrouve = TrouverNoeudParNom(noeudCourant.Frere, nom);
                if (noeudTrouve != null)
                {
                    return noeudTrouve;
                }
            }

            
            return null;
        }

    }
}

