using System.Security.Cryptography.X509Certificates;

namespace Lanotte_Laroudie
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestEstDisponiblePourLivraison()
        {
            Chauffeur romu = new Chauffeur(012345678, "Romu", "Jean", new DateTime(1995, 9, 28), "10 rue de la Liberté", "jean.romu@example.com", "2109876543", new DateTime(2021, 1, 5), "Chauffeur", 2500, "Transport", "B");
            bool b = romu.EstDisponiblePourLivraison(new DateTime(2028,12,12));
            Assert.IsTrue(b);
            
            
        }
        [TestMethod]
        public void TestModifierAdresse()
        {
            Chauffeur romu = new Chauffeur(012345678, "Romu", "Jean", new DateTime(1995, 9, 28), "10 rue de la Liberté", "jean.romu@example.com", "2109876543", new DateTime(2021, 1, 5), "Chauffeur", 2500, "Transport", "B");
            romu.ModifierAdresse("5 rue de la rose");
            Assert.AreEqual("5 rue de la rose", romu.AdressePostale);
        }
        [TestMethod]
        public void TestMassesalariale()
        {
            Salarie fiesta = new Salarie(234567890, "Fiesta", "Marie", new DateTime(1985, 10, 20), "2 avenue des Champs-Élysées", "marie.fiesta@example.com", "0987654321", new DateTime(2012, 3, 15), "Directrice Commerciale", 8000, "Commercial");
            Salarie fetard = new Salarie(345678901, "Fetard", "Jean", new DateTime(1978, 3, 8), "3 place de la République", "jean.fetard@example.com", "9876543210", new DateTime(2013, 7, 20), "Directeur des Opérations", 8500, "Transport");
            List<Salarie> list = new List<Salarie> { fiesta,fetard};
            double somme = Statistiques.Massesalariale(list);
            Assert.AreEqual(somme,16500);
        }
        [TestMethod]
        public void TestTotalCa()
        {
            Client client1 = new Client(369147258, "Roux", "Inès", new DateTime(1992, 11, 28), "40 avenue de la Liberté", "ines.roux@example.com", "0369147258");
            Client client2 = new Client(951753852, "Leroy", "Julie", new DateTime(1988, 7, 5), "45 rue Victor Hugo", "julie.leroy@example.com", "0951753852");

            List<Client> clients = new List<Client> { client1, client2 };
            Facture facture1 = new Facture(200.0, new Marchandise(20.0, TypeMarchandise.Autre), new DateTime(2024, 5, 12), 123456789, "Toulouse", "Bordeaux", client2.NumSS);
            Facture facture2 = new Facture(120.0, new Marchandise(12.0, TypeMarchandise.Passager), new DateTime(2024, 5, 13), 012345678, "Lille", "Strasbourg", client2.NumSS);

            
            client1.factures.Add(facture1);
            client2.factures.Add(facture2);

            double ca = Statistiques.TotalCA(clients);
            Assert.AreEqual(320, ca);
        }
    }
}