using System;
using System.Globalization;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

using System.Net.Mail;
using iTextSharp.text;
using iTextSharp.text.pdf;

using Lanotte_Laroudie;
using System.Text.RegularExpressions;

public class Program
{
    #region méthodes_affichage
    
    void AfficherArbre(Noeud<Salarie> racine, string indentation = "")
    {
        if (racine == null)
            return;

        Console.WriteLine($"{indentation}- {racine.Employe.Nom} ({racine.Employe.Poste})");

        // Afficher les enfants de manière récursive avec une indentation supplémentaire
        foreach (var enfant in GetEnfants(racine))
        {
            AfficherArbre(enfant, indentation + "   ");
        }
    }

    List<Noeud<Salarie>> GetEnfants(Noeud<Salarie> parent)
    {
        var enfants = new List<Noeud<Salarie>>();

        if (parent.Fils != null)
        {
            enfants.Add(parent.Fils);
            var frere = parent.Fils.Frere;
            while (frere != null)
            {
                enfants.Add(frere);
                frere = frere.Frere;
            }
        }

        return enfants;
    }






  


    #endregion
    /// <summary>
    /// Le programme principal 
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>

    static async System.Threading.Tasks.Task Main(string[] args)
    {
        
        #region Initialisation

        
        

        Statistiques statistiques = new Statistiques();
        Dictionary<string, Tuple<int, TimeSpan>>  distancesVilles = statistiques.ObtenirDistancefichier();



        Sauvegarde.DelegueChargementDonnees<Client> chargerclients;

        List<Client> listeClients = Sauvegarde.ChargerDonnees<Client>(Sauvegarde.cheminFichierClients, () => Sauvegarde.ChargerClients(File.ReadAllText(Sauvegarde.cheminFichierClients)));

        List<Salarie> listesalarie = Sauvegarde.ChargerDonnees<Salarie>(Sauvegarde.cheminFichierSalaries, () => Sauvegarde.ChargerSalaries(File.ReadAllText(Sauvegarde.cheminFichierSalaries)));

        
        List<Chauffeur> listechauffeurs = Sauvegarde.ChargerDonnees<Chauffeur>(Sauvegarde.cheminFichierChauffeurs, () => Sauvegarde.ChargerChauffeurs(File.ReadAllText(Sauvegarde.cheminFichierChauffeurs)));

        List<Vehicule> flotte = Sauvegarde.ChargerDonnees<Vehicule>(Sauvegarde.cheminFichierVehicules, () => Sauvegarde.ChargerVehicule(File.ReadAllText(Sauvegarde.cheminFichierVehicules)));



        if (listeClients == null)
        {
            listeClients = new List<Client>(); 
        }
        if (listesalarie == null)
        {
            listesalarie = new List<Salarie>(); 
        }
        if (listechauffeurs == null)
        {
            listechauffeurs = new List<Chauffeur>(); 
        }
        if (flotte == null)
        {
            flotte = new List<Vehicule>(); 
        }










        

        Vehicule voiture1 = new Vehicule(1001, 50000, DateTime.Now, "Essence", Typevehicule.voiture);
        Vehicule camion_citerne1 = new Vehicule(1002, 60000, DateTime.Now, "Diesel", Typevehicule.camion_citerne);
        Vehicule voiture2 = new Vehicule(1003, 70000, DateTime.Now, "Hybride", Typevehicule.voiture);
        Vehicule voiture3 = new Vehicule(1004, 80000, DateTime.Now, "Electrique", Typevehicule.voiture);
        Vehicule camionette1 = new Vehicule(1005, 90000, DateTime.Now, "Essence", Typevehicule.camionette);
        Vehicule camion_benne1 = new Vehicule(1006, 100000, DateTime.Now, "Diesel", Typevehicule.camion_benne);
        Vehicule camion_frigorifique1 = new Vehicule(1007, 110000, DateTime.Now, "Hybride", Typevehicule.camion_frigorifique);
        Vehicule minivan1 = new Vehicule(1008, 120000, DateTime.Now, "Electrique", Typevehicule.minivan);

        flotte.Clear();
       
        flotte.Add(voiture1);
        flotte.Add(voiture2);
        flotte.Add(voiture3);
        flotte.Add(camion_citerne1);
        flotte.Add(camion_benne1);
        flotte.Add(camionette1);
        flotte.Add(minivan1);
        flotte.Add(camion_frigorifique1);
        

        Salarie pdg = new Salarie(123456789, "Dupont", "Pierre", new DateTime(1990, 5, 15), "1 rue de la Paix", "pierre.dupont@example.com", "0123456789", new DateTime(2010, 1, 1), "PDG", 10000,"PDG");
        Salarie fiesta = new Salarie(234567890, "Fiesta", "Marie", new DateTime(1985, 10, 20), "2 avenue des Champs-Élysées", "marie.fiesta@example.com", "0987654321", new DateTime(2012, 3, 15), "Directrice Commerciale", 8000,"Commercial");
        Salarie fetard = new Salarie(345678901, "Fetard", "Jean", new DateTime(1978, 3, 8), "3 place de la République", "jean.fetard@example.com", "9876543210", new DateTime(2013, 7, 20), "Directeur des Opérations", 8500,"Transport");
        Salarie joyeuse = new Salarie(456789012, "Joyeuse", "Sophie", new DateTime(1982, 12, 12), "4 boulevard Voltaire", "sophie.joyeuse@example.com", "8765432109", new DateTime(2015, 5, 10), "DRH", 9000,"Ressources Humaines");
        Salarie gripsou = new Salarie(567890123, "Gripsou", "Paul", new DateTime(1975, 7, 30), "5 rue du Commerce", "paul.gripsou@example.com", "7654321098", new DateTime(2014, 9, 5), "Directeur Financier", 9500,"Financier");
        Salarie forge = new Salarie(678901234, "Forge", "Jean", new DateTime(1992, 8, 25), "6 rue des Lilas", "jean.forge@example.com", "6543210987", new DateTime(2017, 2, 28), "Commercial", 6000,"Commercial");
        Salarie fermi = new Salarie(789012345, "Fermi", "Marie", new DateTime(1990, 4, 18), "7 rue Saint-Michel", "marie.fermi@example.com", "5432109876", new DateTime(2018, 10, 15), "Commercial", 5800,"Commercial");
        Salarie royal = new Salarie(890123456, "Royal", "Paul", new DateTime(1988, 6, 5), "8 avenue Foch", "paul.royal@example.com", "4321098765", new DateTime(2019, 6, 20), "Chef d'équipe", 7000,"Transport");
        Salarie prince = new Salarie(901234567, "Prince", "Marie", new DateTime(1986, 11, 11), "9 rue Royale", "marie.prince@example.com", "3210987654", new DateTime(2020, 4, 10), "Chef d'équipe", 7200,"Transport");
        Chauffeur romu = new Chauffeur(012345678, "Romu", "Jean", new DateTime(1995, 9, 28), "10 rue de la Liberté", "jean.romu@example.com", "2109876543", new DateTime(2021, 1, 5), "Chauffeur", 2500,"Transport", "B");
        Chauffeur romi = new Chauffeur(123456789, "Romi", "Marie", new DateTime(1993, 7, 15), "11 rue Victor Hugo", "marie.romi@example.com", "1098765432", new DateTime(2021, 3, 20), "Chauffeur", 2600, "Transport", "B");
        Chauffeur romal = new Chauffeur(234567890, "Romal", "Paul", new DateTime(1990, 5, 30), "12 rue de la Paix", "paul.romal@example.com", "0987654321", new DateTime(2021, 5, 15), "Chauffeur", 2550, "Transport","B");
        Chauffeur rome = new Chauffeur(345678901, "Rome", "Marie", new DateTime(1994, 4, 10), "13 rue des Roses", "marie.rome@example.com", "9876543210", new DateTime(2021, 7, 10), "Chauffeur", 2650, "Transport", "B");
        Chauffeur rimso = new Chauffeur(456789012, "Rimso", "Paul", new DateTime(1991, 3, 20), "14 avenue des Avenues", "paul.rimso@example.com", "8765432109", new DateTime(2021, 9, 5), "Chauffeur", 2700, "Transport", "B");
        Salarie couleur = new Salarie(567890123, "Couleur", "Marie", new DateTime(1998, 10, 15), "15 rue du Soleil", "marie.couleur@example.com", "7654321098", new DateTime(2022, 11, 5), "Stagiaire", 1500,"Ressources Humaines");
        Salarie toutlemonde = new Salarie(678901234, "Toutlemonde", "Jean", new DateTime(1996, 11, 20), "16 rue des Fées", "jean.toutlemonde@example.com", "6543210987", new DateTime(2023, 2, 10), "Employé", 2000,"Ressources Humaines");
        Salarie picsou = new Salarie(789012345, "Picsou", "Pierre", new DateTime(1980, 4, 25), "17 rue du Trésor", "pierre.picsou@example.com", "5432109876", new DateTime(2020, 8, 15), "Directeur Comptable", 8500,"Financier");
        Salarie grosous = new Salarie(890123456, "Grosous", "Jean", new DateTime(1976, 12, 1), "18 rue de la Finance", "jean.grosous@example.com", "4321098765", new DateTime(2021, 12, 10), "Contrôleur de Gestion", 8200,"Financier");
        Salarie fourier = new Salarie(901234567, "Fourier", "Marie", new DateTime(1997, 5, 10), "19 avenue des Mathématiques", "marie.fourier@example.com", "3210987654", new DateTime(2022, 3, 5), "Comptable", 6000,"Financier");
        Salarie gauthier = new Salarie(012345678, "Gauthier", "Paul", new DateTime(1995, 8, 28), "20 rue de la Statistique", "paul.gauthier@example.com", "2109876543", new DateTime(2022, 7, 20), "Comptable", 5800,"Financier");

        List<string> doublonschauffeurs = new List<string> { "Romu", "Rome", "Rimso", "Romi", "Romal" };
        listechauffeurs.RemoveAll(s => doublonschauffeurs.Contains(s.Nom));

        
        listechauffeurs.Add(romu);
        listechauffeurs.Add(romi);
        listechauffeurs.Add(romal);
        listechauffeurs.Add(rome);
        listechauffeurs.Add(rimso);


        
        List<string> doublonssalarie = new List<string> { "Gauthier", "Fourier", "Grosous", "Picsou", "Toutlemonde", "Couleur", "Rimso", "Rome", "Romal", "Romi", "Romu", "Prince", "Royal", "Fermi", "Forge", "Gripsou", "Joyeuse", "Fetard", "Fiesta", "Dupont" };
       
        listesalarie.RemoveAll(s => doublonssalarie.Contains(s.Nom));

        listesalarie.Add(pdg);
        listesalarie.Add(fiesta);
        listesalarie.Add(fetard);
        listesalarie.Add(joyeuse);
        listesalarie.Add(gripsou);
        listesalarie.Add(forge);
        listesalarie.Add(fermi);
        listesalarie.Add(royal);
        listesalarie.Add(prince);
        listesalarie.Add(romu);
        listesalarie.Add(romi);
        listesalarie.Add(romal);
        listesalarie.Add(rome);
        listesalarie.Add(rimso);
        listesalarie.Add(couleur);
        listesalarie.Add(toutlemonde);
        listesalarie.Add(picsou);
        listesalarie.Add(grosous);
        listesalarie.Add(fourier);
        listesalarie.Add(gauthier);

       

        

        romi.distanceparcourues.Add(465.0);
        romi.distanceparcourues.Add(245.3);
        rimso.distanceparcourues.Add(197.3);
        romu.distanceparcourues.Add(525.2);
        romi.AjouterLivraison(new DateTime(2024, 5, 10));
        romi.AjouterLivraison(new DateTime(2023, 6, 12));
        rimso.AjouterLivraison(new DateTime(2022, 5, 11));
        romu.AjouterLivraison(new DateTime(2023, 5, 13));


        // Initialisation de l'arbre avec les salariés
        Noeud<Salarie> pdgNode = new Noeud<Salarie>(pdg);
        Noeud<Salarie> fiestaNode = new Noeud<Salarie>(fiesta);
        Noeud<Salarie> fetardNode = new Noeud<Salarie>(fetard);
        Noeud<Salarie> joyeuseNode = new Noeud<Salarie>(joyeuse);
        Noeud<Salarie> gripsouNode = new Noeud<Salarie>(gripsou);
        Noeud<Salarie> forgeNode = new Noeud<Salarie>(forge);
        Noeud<Salarie> fermiNode = new Noeud<Salarie>(fermi);
        Noeud<Salarie> royalNode = new Noeud<Salarie>(royal);
        Noeud<Salarie> princeNode = new Noeud<Salarie>(prince);
        Noeud<Salarie> romuNode = new Noeud<Salarie>(romu);
        Noeud<Salarie> romiNode = new Noeud<Salarie>(romi);
        Noeud<Salarie> romalNode = new Noeud<Salarie>(romal);
        Noeud<Salarie> romeNode = new Noeud<Salarie>(rome);
        Noeud<Salarie> rimsoNode = new Noeud<Salarie>(rimso);
        Noeud<Salarie> couleurNode = new Noeud<Salarie>(couleur);
        Noeud<Salarie> toutlemondeNode = new Noeud<Salarie>(toutlemonde);
        Noeud<Salarie> picsouNode = new Noeud<Salarie>(picsou);
        Noeud<Salarie> grosousNode = new Noeud<Salarie>(grosous);
        Noeud<Salarie> fourierNode = new Noeud<Salarie>(fourier);
        Noeud<Salarie> gauthierNode = new Noeud<Salarie>(gauthier);


        // Ajout des fils et frères
        pdgNode.AjouterSalarié( fiestaNode);
        pdgNode.AjouterSalarié( fetardNode);
        pdgNode.AjouterSalarié(joyeuseNode);
        pdgNode.AjouterSalarié(gripsouNode);
        fetardNode.AjouterSalarié(royalNode);
        fetardNode.AjouterSalarié(princeNode);
        royalNode.AjouterSalarié( romuNode);
        royalNode.AjouterSalarié(romiNode);
        royalNode.AjouterSalarié(romalNode);
        princeNode.AjouterSalarié(romeNode);
        princeNode.AjouterSalarié(rimsoNode);
        fiestaNode.AjouterSalarié(forgeNode);
        fiestaNode.AjouterSalarié(fermiNode);
        joyeuseNode.AjouterSalarié(couleurNode);
        joyeuseNode.AjouterSalarié(toutlemondeNode);
        gripsouNode.AjouterSalarié(picsouNode);
        gripsouNode.AjouterSalarié(grosousNode);
        picsouNode.AjouterSalarié(fourierNode);
        picsouNode.AjouterSalarié(gauthierNode);

        #endregion

        int answer = 0;
        while (answer != 4)
        {
            Console.Clear();
            Console.WriteLine(" ****    Bienvenue dans la commande de gestion de Transport TransConnect **** ");
            Console.WriteLine("\n\n\n Voici la liste des modules disponibles : \n\n- (1) Module Client\n- (2) Module Statistiques\n- (3) Module Entreprise\n- (4) Quitter le logiciel");
            Console.WriteLine("\n\n Veuillez entrer le numéro associé à votre réponse");
            
            string sortie = Console.ReadLine();
            if (!int.TryParse(sortie, out answer))
            {
                Console.WriteLine("Erreur dans la saisie, Veuillez réessayer");
                Console.WriteLine("Appuyez sur une touche pour continuer...");
                Console.ReadKey();
            }
            else if (answer != 1 && answer != 2 && answer != 3 && answer !=4)
            {
                Console.WriteLine("Erreur dans la saisie,  Veuillez rééssayer");
            }
            else
            {
                
                
                switch (answer)
                {
                    case 1:
                        int a2 = 0;
                        while(a2 != 6)
                        {
                            Console.Clear();
                            Console.WriteLine("Module Client : \n\n\nVoici les fonctionalitées actuelles :\n\n- (1) Création d'un nouveau Client\n\n- (2) Historique de commande du client\n\n- (3) Passer une nouvelle Commande \n\n- (4) Affichage des clients par odre alphabétique\n\n- (5) Affichage des clients selon leurs dépenses\n\n- (6) Quitter");
                            Console.WriteLine("\n\n Veuillez entrer le numéro associé à votre réponse");
                            string sortie2 = Console.ReadLine();
                            if (!int.TryParse(sortie2, out a2))
                            {
                                Console.WriteLine("Erreur dans la saisie, Veuillez réessayer");
                                Console.WriteLine("Appuyez sur une touche pour continuer...");
                                Console.ReadKey();
                            }
                            else if (a2 != 1 && a2 != 2 && a2 != 3 && a2 != 4 && a2 != 5 && a2 != 6)
                            {
                                Console.WriteLine("Erreur dans la saisie,  Veuillez rééssayer");
                            }

                            else
                            {
                              
                                switch (a2)
                                {
                                    case 1:
                                        Console.Clear();
                                        Random random = new Random();
                                        int numSS = random.Next(100000000, 999999999);
                                        Console.Write("Quel est le nom du client : ");
                                        string nom = Console.ReadLine();
                                        Console.Write("\nQuel est le prenom du client : ");
                                        string prenom = Console.ReadLine();
                                        DateTime datecl = new DateTime();
                                        bool isValidD = false;
                                        Console.WriteLine("");
                                        while (!isValidD)
                                        {
                                            Console.WriteLine("Quel est la date de naissance de l'employé (YYYY/MM/DD) : ");
                                            string input = Console.ReadLine();

                                            if (DateTime.TryParseExact(input, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out datecl))
                                            {

                                                isValidD = true;
                                            }
                                            else
                                            {

                                                Console.WriteLine("Format de date incorrect. Veuillez réessayer.");
                                                Console.ReadLine();
                                                Console.Clear();
                                            }
                                        }
                                        Console.Write("\nQuel est l'adresse du client : ");
                                        string adresse = Console.ReadLine();
                                        Console.Write("\nQuel est l'adresse mail du client : ");
                                        string mail = Console.ReadLine();
                                        Console.Write("\nQuel est le numéro de téléphone du client : ");
                                        string telephone = Console.ReadLine();
                                        List<Facture> facturenouveau = new List<Facture>();
                                        Client nouveau = new Client(numSS, nom, prenom, datecl, adresse, mail, telephone);
                                        listeClients.Add(nouveau);

                                        Console.WriteLine("\n\n\nLe client a bien été ajouté avec succès !");
                                        Console.ReadLine();

                                        break;
                                    case 2:
                                        Console.Clear();
                                        Console.WriteLine("Veuillez renseigner le nom du client dont vous voulez consulter l'historique de commande : ");
                                        string nomReponse = Console.ReadLine();

                                        List<Client> clientsTrouves = listeClients.FindAll(client => client.Nom.Equals(nomReponse, StringComparison.OrdinalIgnoreCase));

                                        if (clientsTrouves.Count > 0)
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Voici la liste des factures du client : " + nomReponse + "\n");
                                            clientsTrouves.ForEach(client =>
                                            {
                                                foreach (Facture fact in client.factures)
                                                {
                                                    Console.WriteLine("-  " + fact.ToString() + "\n");
                                                }
                                            });
                                        }
                                        else
                                        {
                                            Console.WriteLine("Aucun client trouvé avec le nom spécifié. Veuillez vérifier les majuscules.");
                                        }
                                        Console.ReadLine();

                                       
                                        break;
                                    case 3:
                                        Console.Clear();
                                        Console.Write("Avec quel compte client souhaitez vous passer commande, Veuillez renseignez son nom :  ");
                                        string nomreponse1 = Console.ReadLine();
                                        Client clientTrouve1 = null;
                                        int indexClientTrouve = -1;

                                        for (int i = 0; i < listeClients.Count; i++)
                                        {
                                            if (listeClients[i].Nom.Equals(nomreponse1, StringComparison.OrdinalIgnoreCase))
                                            {
                                                clientTrouve1 = listeClients[i];
                                                indexClientTrouve = i;
                                                break;
                                            }
                                        }
                                        if (clientTrouve1 != null)
                                        {
                                            Console.Clear();
                                            int t = 0;
                                            while (t != 5)
                                            {
                                                Console.WriteLine("Quel type de marchandise souhaitez vous faire livrer parmi : \n\n- (1) Passager\n- (2) Perissable\n- (3) Liquide\n- (4) Autre");
                                                Console.WriteLine("\n\n Veuillez entrer le numéro associé à votre réponse");
                                                string sortie3 = Console.ReadLine();
                                                if (!int.TryParse(sortie3, out t))
                                                {
                                                    Console.WriteLine("Erreur dans la saisie, Veuillez réessayer");
                                                    Console.WriteLine("Appuyez sur une touche pour continuer...");
                                                    Console.ReadKey();
                                                }
                                                else if (t != 1 && t != 2 && t != 3 && t != 4)
                                                {
                                                    Console.WriteLine("Erreur dans la saisie,  Veuillez rééssayer");
                                                }

                                                else
                                                {
                                                    TypeMarchandise typemarch = TypeMarchandise.Passager;
                                                    switch (t)
                                                    {
                                                        case 1:
                                                            typemarch = TypeMarchandise.Passager;
                                                            
                                                            break;
                                                        case 2:
                                                            typemarch = TypeMarchandise.Perissable;
                                                            break;
                                                        case 3:
                                                            typemarch = TypeMarchandise.Liquide;
                                                            break;
                                                        case 4:
                                                            typemarch = TypeMarchandise.Autre;
                                                            break;
                                                    }
                                                    Console.Clear();

                                                    int nb = 0; ;
                                                    bool isValidNumber = false;

                                                    while (!isValidNumber)
                                                    {
                                                        Console.WriteLine("Quelle quantité voulez vous de marchandises ");
                                                        string input = Console.ReadLine();

                                                        if (int.TryParse(input, out nb))
                                                        {
                                                            
                                                            isValidNumber = true; 
                                                        }
                                                        else
                                                        {
                                                            
                                                            Console.WriteLine("Format de nombre incorrect. Veuillez réessayer.");
                                                        }
                                                    }

                                                    Vehicule vehiculetest = Commande.Choixtypevehicule(typemarch, nb);
                                                    
                                                    
                                                    Console.Clear();
                                                    DateTime datelivraison = new DateTime();
                                                    bool isValidDatli = false;
                                                    Console.WriteLine("");
                                                    while (!isValidDatli)
                                                    {
                                                        Console.WriteLine("Quel est la date de livraison souhaitée (YYYY/MM/DD) : ");
                                                        string input = Console.ReadLine();

                                                        if (DateTime.TryParseExact(input, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out datelivraison))
                                                        {

                                                            isValidDatli = true;
                                                        }
                                                        else
                                                        {

                                                            Console.WriteLine("Format de date incorrect. Veuillez réessayer.");
                                                            Console.ReadLine();
                                                            Console.Clear();
                                                        }
                                                    }
                                                    Vehicule dispo = Commande.TrouverVehiculeDisponible(vehiculetest.typevehicule, flotte, datelivraison);
                                                   
                                                    Chauffeur dispochauf = new Chauffeur(00, "paul", "", new DateTime(2003, 12,12), "", "", "", new DateTime(2023 , 02 , 03), "", 3, "","") ;
                                                   
                                                    int indexchauffeur = -1;
                                                    for (int j =0;j< listechauffeurs.Count;j++)
                                                    {
                                                        if (listechauffeurs[j].EstDisponiblePourLivraison(datelivraison))
                                                        {
                                                             dispochauf = listechauffeurs[j];
                                                            indexchauffeur = j;
                                                            break;
                                                        }
                                                    }
                                                    
                                                    if (dispo != null && dispochauf.NumSS != 00)
                                                    {
                                                        int ba = 0;
                                                        while (ba != 3)
                                                        {
                                                            Console.Clear();

                                                            Console.WriteLine("Pour la suite nous avons 2 méthode pour calculer la distance de votre commande :\n\n- (1) Utilisation de l'API Google Map\n- (2) Utilisation de l'algorythme de Djistrkra");
                                                            Console.WriteLine("\n\n Veuillez entrer le numéro associé à votre réponse");
                                                            string sortie23 = Console.ReadLine();
                                                            if (!int.TryParse(sortie23, out ba))
                                                            {
                                                                Console.WriteLine("Erreur dans la saisie, Veuillez réessayer");
                                                                Console.WriteLine("Appuyez sur une touche pour continuer...");
                                                                Console.ReadKey();
                                                            }
                                                            else if (ba != 1 && ba != 2 )
                                                            {
                                                                Console.WriteLine("Erreur dans la saisie,  Veuillez rééssayer");
                                                            }

                                                            else
                                                            {
                                                                if (ba == 1)
                                                                {
                                                                    Console.Clear();
                                                                    Console.WriteLine("Méthode de l'API Google Map\n\n\n");
                                                                    Console.WriteLine("Veuillez renseigner l'adresse exacte de départ");
                                                                    string addépart = Console.ReadLine();
                                                                    Console.Clear();
                                                                    Console.WriteLine("Méthode de l'API Google Map\n\n\n");
                                                                    Console.WriteLine("Veuillez renseigner l'adresse exacte d'arrivée");
                                                                    string adarrivee = Console.ReadLine();
                                                                    Console.WriteLine(" \n\n");
                                                                    double distance = Commande.CalculDistance(addépart,adarrivee);

                                                                    
                                                                    Console.Clear();
                                                                    double prix = Commande.Prixlivraison(dispo.typevehicule, distance);
                                                                    Console.WriteLine("Après calcul le montant de votre commande s'élève à : " + prix + " euros\n\n\n");
                                                                    Console.WriteLine("Souhaitez vous valider votre commande (O)   ou l'annuler (N) :");
                                                                    string reponse = Console.ReadLine();
                                                                    if (reponse.ToLower() == "o")
                                                                    {
                                                                        Marchandise marchandise = new Marchandise(nb, typemarch);
                                                                        Commande lacommande = new Commande(listeClients[indexClientTrouve], dispochauf, datelivraison, dispo, marchandise, prix);
                                                                        Facture lafacture = new Facture(prix, marchandise, datelivraison, listechauffeurs[indexchauffeur].NumSS,addépart, adarrivee, listeClients[indexClientTrouve].NumSS);
                                                                        listeClients[indexClientTrouve].factures.Add(lafacture);
                                                                        listechauffeurs[indexchauffeur].emploiDuTemps.Add(datelivraison);
                                                                        listechauffeurs[indexchauffeur].AjouterLivraison(datelivraison);
                                                                        listechauffeurs[indexchauffeur].AjouterDistance(distance);
                                                                        Console.Clear();
                                                                        Console.WriteLine("Votre commande a été enregistré avec succès");
                                                                        string pdfPath = Mailing.GenererPDF("facture.pdf", lafacture.GetFactureText());
                                                                        Mailing.EnvoyerMail(listeClients[indexClientTrouve].AdresseMail, "Facture", "Veuillez trouver ci-joint la facture.", pdfPath);

                                                                        ba = 3;
                                                                        t = 5;

                                                                    }
                                                                    else
                                                                    {
                                                                        Console.Clear();
                                                                        Console.WriteLine("Annulation de la commande");
                                                                        
                                                                        ba = 3;
                                                                        t = 5;
                                                                    }


                                                                }
                                                                else
                                                                {
                                                                    // partie djistrkra
                                                                    int abs = 0;
                                                                    while (abs != 8)
                                                                    {

                                                                        Console.Clear();
                                                                        GrapheDjistkra graphe = new GrapheDjistkra(statistiques.ObtenirDistancefichier());
                                                                        
                                                                        Console.WriteLine("Parmi les Villes suivantes , Choisissez la ville de départ : \n\nParis, Lyon, Marseille, Bordeaux, Rouen, Toulon, Monaco, Toulouse\nLa Rochelle, Angers, Pau, Nimes, Avignon, Montpellier, Biarritz\n");
                                                                        string villedepa = Console.ReadLine();
                                                                        Console.WriteLine("\n\n Voici les distances depuis votre ville de départ :\n ");
                                                                        Dictionary<string, int> distancesDepuisVilleDepart = graphe.Dijkstra(villedepa);
                                                                        Console.ReadLine();


                                                                    
                                                                        Console.Clear();
                                                                        Console.WriteLine("\n\n Parmi les Villes suivantes , Choisissez la ville d'arrivée : \n\nParis, Lyon, Marseille, Bordeaux, Rouen, Toulon, Monaco, Toulouse\nLa Rochelle, Angers, Pau, Nimes, Avignon, Montpellier, Biarritz\n");
                                                                        string villedarriv = Console.ReadLine();
                                                                        int distanceVilleArrive = -1;
                                                                        if (distancesDepuisVilleDepart.ContainsKey(villedarriv))
                                                                        {
                                                                            // On veut ici récuperer la distance entre la ville de départ et la ville d'arrivée
                                                                            distanceVilleArrive = distancesDepuisVilleDepart[villedarriv];
                                                                            

                                                                            Console.WriteLine($"La distance entre {villedepa} et {villedarriv} est de {distanceVilleArrive} km.");
                                                                            double distance = Convert.ToDouble(distanceVilleArrive);
                                                                            Console.ReadLine();
                                                                            Console.Clear();
                                                                            double prix = Commande.Prixlivraison(dispo.typevehicule, distance);
                                                                            Console.WriteLine("Après calcul le montant de votre commande s'élève à : " + prix + " euros\n\n\n");
                                                                            Console.WriteLine("Souhaitez vous valider votre commande (O)   ou l'annuler (N) :");
                                                                            string reponse = Console.ReadLine();
                                                                            if (reponse.ToLower() == "o")
                                                                            {
                                                                                Marchandise marchandise = new Marchandise(nb, typemarch);
                                                                                Commande lacommande = new Commande(listeClients[indexClientTrouve], dispochauf, datelivraison, dispo, marchandise, prix);
                                                                                Facture lafacture = new Facture(prix, marchandise, datelivraison, listechauffeurs[indexchauffeur].NumSS, villedepa, villedarriv, listeClients[indexClientTrouve].NumSS);
                                                                                listechauffeurs[indexchauffeur].emploiDuTemps.Add(datelivraison);
                                                                                listeClients[indexClientTrouve].factures.Add(lafacture);
                                                                                listechauffeurs[indexchauffeur].AjouterLivraison(datelivraison);
                                                                                listechauffeurs[indexchauffeur].AjouterDistance(distance);
                                                                                Console.Clear();
                                                                                Console.WriteLine("Votre commande a été enregistré avec succès");
                                                                                string pdfPath = Mailing.GenererPDF("facture.pdf", lafacture.GetFactureText());
                                                                                Mailing.EnvoyerMail(listeClients[indexClientTrouve].AdresseMail, "Facture", "Veuillez trouver ci-joint la facture.", pdfPath);

                                                                                ba = 3;
                                                                                t = 5;
                                                                                abs = 8;

                                                                            }
                                                                            else
                                                                            {
                                                                                Console.Clear();
                                                                                Console.WriteLine("Annulation de la commande");

                                                                                ba = 3;
                                                                                t = 5;
                                                                                abs = 8;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            Console.WriteLine($"La distance depuis {villedepa} jusqu'à {villedarriv} n'est pas prise en charge par notre entreprise.\nNous allons vous redemandez vos choix de villes");
                                                                            Console.ReadLine();
                                                                        }
                                                                    }
                                                                    
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine("Nous sommes navrés mais la date séléctionnée n'est pas disponible ");
                                                    }

                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Aucun client trouvé avec le nom spécifié. Verifier bien les majuscules.");
                                        }
                                        Console.ReadLine();
                                        break;
                                    case 4:
                                        Console.Clear();
                                        Console.Write("Voici tous les clients triés par ordre alphabétique \n");
                                        Client.AfficherClientsParNomAlphabetique(listeClients);
                                        Console.ReadLine();
                                        break;
                                        case 5:
                                        Console.Clear();
                                        Console.Write("Voici tous les clients triés par ordre croissant des dépenses totales \n");
                                        Client.AfficherClientsParMontantTotal(listeClients);
                                        Console.ReadLine();
                                        break;
                                }
                                


                                    
                                    

                              

                            }
                        }
                        break;
                    case 2:
                        int la = 0;
                        while (la != 12)
                        {
                            Console.Clear();
                            Console.WriteLine("Module Statistiques : \n\n\nVoici les différentes fonctionnalités disponibles :\n\n- (1) Affichage du meilleur client\n- (2) Filtrer les commandes selon une date\n- (3) Prix moyen par client\n- (4) Prix moyen global\n- (5) Total du chiffre d'affaires\n- (6) Masse salariale totale\n- (7) Commandes par chauffeur\n- (8) Distance parcourues par chauffeur\n- (9) Retour");

                            Console.WriteLine("\n\n Veuillez entrer le numéro associé à votre réponse");
                            string sortie21 = Console.ReadLine();
                            if (!int.TryParse(sortie21, out la))
                            {
                                Console.WriteLine("Erreur dans la saisie, Veuillez réessayer");
                                Console.WriteLine("Appuyez sur une touche pour continuer...");
                                Console.ReadKey();
                            }
                            else if (la != 1 && la != 2 && la != 3 && la != 4 && la != 5 && la != 6 && la != 7 && la != 8 && la != 9)
                            {
                                Console.WriteLine("Erreur dans la saisie,  Veuillez rééssayer");
                            }

                            else
                            {
                                switch (la)
                                {
                                    case 1:
                                        Console.Clear();
                                        Statistiques.MeilleurClient(listeClients );
                                        Console.ReadLine();
                                        break;
                                    case 2:
                                        Console.Clear();
                                       
                                        
                                        DateTime debut = new DateTime();
                                        bool isValidDate23 = false;
                                        Console.WriteLine("");
                                        while (!isValidDate23)
                                        {
                                            Console.WriteLine("Veuillez selectionner la date de début du filtre : (YYYY/MM/DD)\n");
                                            string input = Console.ReadLine();

                                            if (DateTime.TryParseExact(input, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out debut))
                                            {

                                                isValidDate23 = true;
                                            }
                                            else
                                            {

                                                Console.WriteLine("Format de date incorrect. Veuillez réessayer.");
                                                Console.ReadLine();
                                                Console.Clear();
                                            }
                                        }
                                        Console.Clear();
                                        DateTime fin = new DateTime();
                                        bool isValidDate234 = false;
                                        Console.WriteLine("");
                                        while (!isValidDate234)
                                        {
                                            Console.WriteLine("Veuillez selectionner la date de fin du filtre : (YYYY/MM/DD)\n");
                                            string input = Console.ReadLine();

                                            if (DateTime.TryParseExact(input, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fin))
                                            {

                                                isValidDate234 = true;
                                            }
                                            else
                                            {

                                                Console.WriteLine("Format de date incorrect. Veuillez réessayer.");
                                                Console.ReadLine();
                                                Console.Clear();
                                            }
                                        }
                                        Console.Clear();
                                        Statistiques.CommandeParDate(debut, listeClients, fin);
                                        Console.ReadLine();
                                        break;
                                    case 3:
                                        Console.Clear();
                                        Statistiques.PrixMoyenParClient(listeClients);
                                        break;
                                    case 4:
                                        Console.Clear();
                                        Statistiques.PrixMoyenCommande(listeClients);
                                        break;
                                    case 5:
                                        Console.Clear();
                                        double ca = Statistiques.TotalCA(listeClients);
                                        Console.WriteLine($" Le Chiffre d'affaire de l'entreprise est pour l'instant de : {ca} euros. ");
                                        Console.ReadLine();
                                        break;
                                    case 6:
                                        Console.Clear();
                                        Console.WriteLine("Voici le montant total payé pour les salaires : ");
                                        Statistiques.Massesalariale(listesalarie);
                                        Console.WriteLine("\n\n Voici le détail de ces salaires :\n");
                                        Statistiques.Detailsalaire(listesalarie);
                                        break;
                                    case 7:
                                        Console.Clear();
                                        Console.WriteLine("Voici les commandes effectuées par chaque chauffeurs : \n\n");
                                        Statistiques.Commandeeffectuéschauffeur(listechauffeurs);
                                        break;
                                    case 8:
                                        Console.Clear();
                                        Statistiques.KilometreparChaufeur(listechauffeurs);
                                        Console.ReadLine();
                                        break;
                                    case 9:
                                        la = 12;
                                        break;
                                }
                            }
                        }
                            break;
                    case 3:
                        int ans = 0;
                        while (ans != 4)
                        {
                            Console.Clear();
                            Console.WriteLine("Module Entreprise : \n\n\nVoici les fonctionalitées actuelles :\n\n- (1) Visualisation de l'oganigramme de l'entreprise\n\n- (2) Ajouter un employé\n\n- (3) Licensier un employé \n\n- (4) Quitter");
                            Console.WriteLine("\n\n Veuillez entrer le numéro associé à votre réponse");
                            string sortie2 = Console.ReadLine();
                            if (!int.TryParse(sortie2, out ans))
                            {
                                Console.WriteLine("Erreur dans la saisie, Veuillez réessayer");
                                Console.WriteLine("Appuyez sur une touche pour continuer...");
                                Console.ReadKey();
                            }
                            else if (ans != 1 && ans != 2 && ans != 3 && ans != 4)
                            {
                                Console.WriteLine("Erreur dans la saisie,  Veuillez rééssayer");
                            }

                            else
                            {
                                switch(ans)
                                {
                                    case 1:
                                        Console.Clear();
                                        Console.WriteLine("Arbre de l'entreprise :\n\n");
                                        string a = "";
                                        Program program = new Program();
                                        program.AfficherArbre(pdgNode);




                                        Console.Write("\n\n\n\nVoici la liste des employés avec ceux que vous auriez éventuellement rajoutés dans d'autres manipulations mais nous n'arrivons pas a les afficher dans l'arbre après le chargement de l'ancienne sauvegarde\n\n");
                                        foreach(Salarie salarie in listesalarie)
                                        {
                                            Console.Write(salarie.Nom + "  ");
                                        }

                                        Console.ReadLine();
                                        break;
                                    case 2:
                                        Console.Clear();
                                        Console.WriteLine("Quel est le département du nouvel employé : \n- (1) Financier\n- (2) Transport\n- (3) Ressources humaines\n- (4) Commercial");
                                        string sort = Console.ReadLine();
                                        int an = 0;
                                        while (an != 5)
                                        {
                                            if (!int.TryParse(sort, out an))
                                            {
                                                Console.WriteLine("Erreur dans la saisie, Veuillez réessayer");
                                                Console.WriteLine("Appuyez sur une touche pour continuer...");
                                                Console.ReadKey();
                                            }
                                            else if (an != 1 && an != 2 && an != 3 && an != 4)
                                            {
                                                Console.WriteLine("Erreur dans la saisie,  Veuillez rééssayer");
                                            }
                                            else
                                            {
                                                switch(an)
                                                {

                                                    case 1:
                                                        
                                                        
                                                            Console.Clear();
                                                            Random random = new Random();
                                                            int numSS = random.Next(100000000, 999999999);
                                                            Console.Write("Quel est le nom de l'employé : ");
                                                            string nom = Console.ReadLine();
                                                            Console.Write("\nQuel est le prenom de l'employé : ");
                                                            string prenom = Console.ReadLine();
                                                        DateTime date = new DateTime();
                                                        bool isValidDat = false;
                                                        Console.WriteLine("");
                                                        while (!isValidDat)
                                                        {
                                                            Console.WriteLine("Quel est la date de naissance de l'employé (YYYY/MM/DD) : ");
                                                            string input = Console.ReadLine();

                                                            if (DateTime.TryParseExact(input, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                                                            {

                                                                isValidDat = true;
                                                            }
                                                            else
                                                            {

                                                                Console.WriteLine("Format de date incorrect. Veuillez réessayer.");
                                                                Console.ReadLine();
                                                                Console.Clear();
                                                            }
                                                        }
                                                        Console.Write("\nQuel est l'adresse de l'employé : ");
                                                            string adresse = Console.ReadLine();
                                                            Console.Write("\nQuel est l'adresse mail de l'employé : ");
                                                            string mail = Console.ReadLine();
                                                            Console.Write("\nQuel est le numéro de téléphone de l'employé : ");
                                                            string telephone = Console.ReadLine();
                                                            Console.Write("\nQuel est le poste de l'employé : ");
                                                            string poste = Console.ReadLine();

                                                        bool salairegood = false;
                                                        double salaire = 0;
                                                        while (!salairegood)
                                                        {
                                                            Console.Write("\nQuel est le salaire de l'employé : ");
                                                            string output = Console.ReadLine();
                                                            if (double.TryParse(output,out salaire))
                                                            {
                                                                salairegood = true;
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("\nMauvais Format, Veuillez reessayer");
                                                                Console.ReadLine();
                                                                Console.Clear();
                                                            }
                                                        }
                                                            

                                                            Salarie nouveau = new Salarie(numSS, nom, prenom, date, adresse, mail, telephone, DateTime.Now, poste, salaire,"Financier");
                                                            Noeud<Salarie> nouveauNode = new Noeud<Salarie>(nouveau);
                                                            
                                                            picsouNode.AjouterSalarié(nouveauNode);
                                                            Console.Clear();
                                                            Console.WriteLine("Arbre de l'entreprise :\n\n");

                                                            Program programm = new Program();
                                                            programm.AfficherArbre(pdgNode);
                                                        listesalarie.Add(nouveau);
                                                        if (nouveau.Poste == "Chauffeur" || nouveau.Poste == "chauffeur")
                                                        {
                                                            Chauffeur na = new Chauffeur(numSS, nom, prenom, date, adresse, mail, telephone, DateTime.Now, poste, salaire,"Fianncier", "B");
                                                            listechauffeurs.Add(na);
                                                        }

                                                        Console.ReadLine();
                                                            Console.WriteLine("Voulez-vous ajouter un autre employé ? (O/N)");
                                                            string reponse = Console.ReadLine();
                                                            if (reponse.ToLower() != "o")
                                                            {
                                                                an = 5;
                                                            }
                                                                
                                                        

                                                        break;
                                                    case 2:
                                                        Console.Clear();
                                                        Random randome = new Random();
                                                        int numSSe = randome.Next(100000000, 999999999);
                                                        Console.Write("Quel est le nom de l'employé : ");
                                                        string nome = Console.ReadLine();
                                                        Console.Write("\nQuel est le prenom de l'employé : ");
                                                        string prenome = Console.ReadLine();

                                                        DateTime datee = new DateTime() ;
                                                        bool isValidDate = false;
                                                        Console.WriteLine("");
                                                        while (!isValidDate)
                                                        {
                                                            Console.WriteLine("Quel est la date de naissance de l'employé (YYYY/MM/DD) : ");
                                                            string input = Console.ReadLine();

                                                            if (DateTime.TryParseExact(input, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out datee))
                                                            {
                                                               
                                                                isValidDate = true; 
                                                            }
                                                            else
                                                            {
                                                                
                                                                Console.WriteLine("Format de date incorrect. Veuillez réessayer.");
                                                                Console.ReadLine();
                                                                Console.Clear();
                                                            }
                                                        }

                                                        
                                                        Console.Write("\nQuel est l'adresse de l'employé : ");
                                                        string adressee = Console.ReadLine();
                                                        Console.Write("\nQuel est l'adresse mail de l'employé : ");
                                                        string maile = Console.ReadLine();
                                                        Console.Write("\nQuel est le numéro de téléphone de l'employé : ");
                                                        string telephonee = Console.ReadLine();
                                                        Console.Write("\nQuel est le poste de l'employé : ");
                                                        string postee = Console.ReadLine();
                                                        bool salairegoo = false;
                                                        double salairee = 0;
                                                        while (!salairegoo)
                                                        {
                                                            Console.Write("\nQuel est le salaire de l'employé : ");
                                                            string output = Console.ReadLine();
                                                            if (double.TryParse(output, out salairee))
                                                            {
                                                                salairegoo = true;
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("\nMauvais Format, Veuillez reessayer");
                                                                Console.ReadLine();
                                                                Console.Clear();
                                                            }
                                                        }

                                                        Salarie nouveaue = new Chauffeur(numSSe, nome, prenome, datee, adressee, maile, telephonee, DateTime.Now, postee, salairee,"Transport","B");
                                                        Noeud<Salarie> nouveauNodee = new Noeud<Salarie>(nouveaue);
                                                        royalNode.AjouterSalarié(nouveauNodee);
                                                        Console.Clear();
                                                        Console.WriteLine("Arbre de l'entreprise :\n\n");

                                                        Program programme = new Program();
                                                        programme.AfficherArbre(pdgNode);
                                                        listesalarie.Add(nouveaue);
                                                        if (nouveaue.Poste == "Chauffeur" || nouveaue.Poste == "chauffeur")
                                                        {
                                                            Chauffeur n = new Chauffeur(numSSe, nome, prenome, datee, adressee, maile, telephonee, DateTime.Now, postee, salairee,"Transport", "B");
                                                            listechauffeurs.Add(n);
                                                        }
                                                        Console.ReadLine();
                                                        Console.WriteLine("Voulez-vous ajouter un autre employé ? (O/N)");
                                                        string reponsee = Console.ReadLine();
                                                        if (reponsee.ToLower() != "o")
                                                        {
                                                            an = 5;
                                                        }
                                                        break;

                                                    case 3:
                                                        Console.Clear();
                                                        Random random1 = new Random();
                                                        int numSS1 = random1.Next(100000000, 999999999);
                                                        Console.Write("Quel est le nom de l'employé : ");
                                                        string nom1 = Console.ReadLine();
                                                        Console.Write("\nQuel est le prenom de l'employé : ");
                                                        string prenom1 = Console.ReadLine();
                                                        DateTime dateee = new DateTime();
                                                        bool isValidDatee = false;
                                                        Console.WriteLine("");
                                                        while (!isValidDatee)
                                                        {
                                                            Console.WriteLine("Quel est la date de naissance de l'employé (YYYY/MM/DD) : ");
                                                            string input = Console.ReadLine();

                                                            if (DateTime.TryParseExact(input, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateee))
                                                            {

                                                                isValidDatee = true;
                                                            }
                                                            else
                                                            {

                                                                Console.WriteLine("Format de date incorrect. Veuillez réessayer.");
                                                                Console.ReadLine();
                                                                Console.Clear();
                                                            }
                                                        }
                                                        Console.Write("\nQuel est l'adresse de l'employé : ");
                                                        string adresse1 = Console.ReadLine();
                                                        Console.Write("\nQuel est l'adresse mail de l'employé : ");
                                                        string mail1 = Console.ReadLine();
                                                        Console.Write("\nQuel est le numéro de téléphone de l'employé : ");
                                                        string telephone1 = Console.ReadLine();
                                                        Console.Write("\nQuel est le poste de l'employé : ");
                                                        string poste1 = Console.ReadLine();
                                                        bool salairegoode = false;
                                                        double salaire1 = 0;
                                                        while (!salairegoode)
                                                        {
                                                            Console.Write("\nQuel est le salaire de l'employé : ");
                                                            string output = Console.ReadLine();
                                                            if (double.TryParse(output, out salaire1))
                                                            {
                                                                salairegoode = true;
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("\nMauvais Format, Veuillez reessayer");
                                                                Console.ReadLine();
                                                                Console.Clear();
                                                            }
                                                        }

                                                        Salarie nouveau1 = new Salarie(numSS1, nom1, prenom1, dateee, adresse1, mail1, telephone1, DateTime.Now, poste1, salaire1,"Ressources Humaines");
                                                        Noeud<Salarie> nouveauNode1 = new Noeud<Salarie>(nouveau1);
                                                        joyeuseNode.AjouterSalarié(nouveauNode1);
                                                        Console.Clear();
                                                        Console.WriteLine("Arbre de l'entreprise :\n\n");

                                                        Program programm1 = new Program();
                                                        programm1.AfficherArbre(pdgNode);

                                                        listesalarie.Add(nouveau1);
                                                        if (nouveau1.Poste == "Chauffeur" || nouveau1.Poste == "chauffeur")
                                                        {
                                                            Chauffeur nan = new Chauffeur(numSS1, nom1, prenom1, dateee, adresse1, mail1, telephone1, DateTime.Now, poste1, salaire1,"Ressources Humaines", "B");
                                                            listechauffeurs.Add(nan);
                                                        }
                                                        Console.ReadLine();
                                                        Console.WriteLine("Voulez-vous ajouter un autre employé ? (O/N)");
                                                        string reponse1 = Console.ReadLine();
                                                        if (reponse1.ToLower() != "o")
                                                        {
                                                            an = 5;
                                                        }
                                                        break;
                                                    case 4:
                                                        Console.Clear();
                                                        Random random2 = new Random();
                                                        int numSS2 = random2.Next(100000000, 999999999);
                                                        Console.Write("Quel est le nom de l'employé : ");
                                                        string nom2 = Console.ReadLine();
                                                        Console.Write("\nQuel est le prenom de l'employé : ");
                                                        string prenom2 = Console.ReadLine();
                                                        DateTime date2 = new DateTime();
                                                        bool isValidDate2 = false;
                                                        Console.WriteLine("");
                                                        while (!isValidDate2)
                                                        {
                                                            Console.WriteLine("Quel est la date de naissance de l'employé (YYYY/MM/DD) : ");
                                                            string input = Console.ReadLine();

                                                            if (DateTime.TryParseExact(input, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date2))
                                                            {

                                                                isValidDate2 = true;
                                                            }
                                                            else
                                                            {

                                                                Console.WriteLine("Format de date incorrect. Veuillez réessayer.");
                                                                Console.ReadLine();
                                                                Console.Clear();
                                                            }
                                                        }
                                                        Console.Write("\nQuel est l'adresse de l'employé : ");
                                                        string adresse2 = Console.ReadLine();
                                                        Console.Write("\nQuel est l'adresse mail de l'employé : ");
                                                        string mail2 = Console.ReadLine();
                                                        Console.Write("\nQuel est le numéro de téléphone de l'employé : ");
                                                        string telephone2 = Console.ReadLine();
                                                        Console.Write("\nQuel est le poste de l'employé : ");
                                                        string poste2 = Console.ReadLine();
                                                        bool salairego = false;
                                                        double salaire2 = 0;
                                                        while (!salairego)
                                                        {
                                                            Console.Write("\nQuel est le salaire de l'employé : ");
                                                            string output = Console.ReadLine();
                                                            if (double.TryParse(output, out salaire2))
                                                            {
                                                                salairego = true;
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("\nMauvais Format, Veuillez reessayer");
                                                                Console.ReadLine();
                                                                Console.Clear();
                                                            }
                                                        }

                                                        Salarie nouveau2 = new Salarie(numSS2, nom2, prenom2, date2, adresse2, mail2, telephone2, DateTime.Now, poste2, salaire2,"Comptable");
                                                        Noeud<Salarie> nouveauNode2 = new Noeud<Salarie>(nouveau2);
                                                        fiestaNode.AjouterSalarié(nouveauNode2);
                                                        Console.Clear();
                                                        Console.WriteLine("Arbre de l'entreprise :\n\n");

                                                        Program programm2 = new Program();
                                                        programm2.AfficherArbre(pdgNode);

                                                        listesalarie.Add(nouveau2);
                                                        if (nouveau2.Poste == "Chauffeur" || nouveau2.Poste == "chauffeur")
                                                        {
                                                            Chauffeur nani = new Chauffeur(numSS2, nom2, prenom2, date2, adresse2, mail2, telephone2, DateTime.Now, poste2, salaire2,"Comptable", "B");
                                                            listechauffeurs.Add(nani);
                                                        }
                                                        Console.ReadLine();
                                                        Console.WriteLine("Voulez-vous ajouter un autre employé ? (O/N)");
                                                        string reponse2 = Console.ReadLine();
                                                        if (reponse2.ToLower() != "o")
                                                        {
                                                            an = 5;
                                                        }
                                                        break;
                                                }
                                            }
                                        }
                                        break;
                                    case 3:
                                        Console.Clear();
                                        Console.Write("Attention si vous licensier un cadre cela supprimera tous ses subordonnées !\n\nVeuillez spécifier le nom de l'employé a licensier : ");
                                        string nomEmploye = Console.ReadLine();
                                        Noeud<Salarie> noeudEmploye = pdgNode.TrouverNoeudParNom(pdgNode,nomEmploye);
                                  

                                        if (noeudEmploye != null)
                                        {
                                            Noeud<Salarie> parent = pdgNode.TrouverParent(noeudEmploye);
                                            bool licensie;
                                            if (parent != null)
                                            {
                                                // Vous avez trouvé le parent du nœud employé
                                                 licensie = parent.LicencierEtPromouvoir(noeudEmploye.Employe);
                                            }
                                            else
                                            {
                                                // Le nœud employé n'a pas de parent
                                                licensie = false;
                                                Console.WriteLine("L'employe n'a pas de supérieur");
                                            }
                                            if (licensie)
                                            {
                                                Console.WriteLine("L'employé a été licencié avec succès.");
                                            }
                                            else
                                            {
                                                Console.WriteLine("L'employé n'a pas été trouvé dans l'arbre.");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Aucun employé trouvé avec le nom spécifié.");
                                        }
                                        Console.ReadLine();

                                        break;

                                }
                            }

                        }
                        break;
                }
            }
        }
        Sauvegarde.SauvegarderVehicule(flotte);
        Sauvegarde.SauvegarderClients(listeClients);
        Sauvegarde.SauvegarderSalaries(listesalarie);
        Sauvegarde.SauvegarderChauffeurs(listechauffeurs);
    }
}
