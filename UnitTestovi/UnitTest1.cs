using CsvHelper;
using System.Globalization;
using System.Xml;
using Zadaca1;

namespace UnitTestovi
{
    //Amina Pandžić
    [TestClass]
    public class UnitTest1
    {
        #region Inline testovi
        static IEnumerable<object[]> Glasači
        {
            get
            {
                return new[]
                {
                 new object[] { "", "Prezime", "Adresa", DateTime.ParseExact("01/01/1996","dd/MM/yyyy",CultureInfo.InvariantCulture), "123E123", "0101996170001"},
                 new object[] { "Din", "", "Adresa", DateTime.ParseExact("01/01/1996", "dd/MM/yyyy", CultureInfo.InvariantCulture), "123E123", "0101996170001"},
                 new object[] { "Din", "Prezime", "", DateTime.ParseExact("01/01/1996","dd/MM/yyyy",CultureInfo.InvariantCulture), "123E123", "0101996170001"},
                 new object[] { "Din1", "Prezime", "Adresa", DateTime.ParseExact("01/01/1996","dd/MM/yyyy",CultureInfo.InvariantCulture), "123E123", "0101996170001"},
                 new object[] { "Din", "Prezime1", "Adresa", DateTime.ParseExact("01/01/1996","dd/MM/yyyy",CultureInfo.InvariantCulture), "123E123", "0101996170001"},
                 new object[] { "D", "Prezime", "Adresa", DateTime.ParseExact("01/01/1996","dd/MM/yyyy",CultureInfo.InvariantCulture), "123E123", "0101996170001"},
                 new object[] { "Din", "Pr", "Adresa", DateTime.ParseExact("01/01/1996","dd/MM/yyyy",CultureInfo.InvariantCulture), "123E123", "0101996170001"},
                 new object[] { "Din", "Prezime", "Adresa", DateTime.ParseExact("01/01/2024","dd/MM/yyyy",CultureInfo.InvariantCulture), "123E123", "0101024170001"},
                 new object[] { "Din", "Prezime", "Adresa", DateTime.ParseExact("01/01/2009","dd/MM/yyyy",CultureInfo.InvariantCulture), "123E123", "0101009170001"},
                 new object[] { "Din", "Prezime", "Adresa", DateTime.ParseExact("01/01/1996","dd/MM/yyyy",CultureInfo.InvariantCulture), "123A123", "0101996170001"},
                 new object[] { "Din", "Prezime", "Adresa", DateTime.ParseExact("01/01/1996","dd/MM/yyyy",CultureInfo.InvariantCulture), "123E123", "01019171701"}
                };
            }
        }

        static IEnumerable<object[]> Glasači2
        {
            get
            {
                return new[]
                {
                 new object[] { "Din", "Prezime", "Adresa", DateTime.ParseExact("01/01/1996", "dd/MM/yyyy", CultureInfo.InvariantCulture), "123E123", "0101996170001"}
                };
            }
        }

        [TestMethod]
        [DynamicData("Glasači")]
        [ExpectedException(typeof(Exception))]
        public void Test1(string ime, string prezime, string adresa, DateTime
     datumRodjenja, string brojLK, string JMBG)
        {
            Glasac glasac = new(ime, prezime, adresa, datumRodjenja, brojLK, JMBG);
        }

        [TestMethod]
        [DynamicData("Glasači2")]
        public void Test2(string ime, string prezime, string adresa, DateTime
     datumRodjenja, string brojLK, string JMBG)
        {
            Glasac glasac = new(ime, prezime, adresa, datumRodjenja, brojLK, JMBG);
            Assert.AreEqual(ime, glasac.Ime);
            Assert.AreEqual(prezime, glasac.Prezime);
        }
        #endregion

        #region Testovi CSV
        static IEnumerable<object[]> GlasačiCSV
        {
            get
            {
                return UčitajPodatkeCSV("Glasaci.csv");
            }
        }

        static IEnumerable<object[]> Glasači2CSV
        {
            get
            {
                return UčitajPodatkeCSV("Glasaci2.csv");
            }
        }

        public static IEnumerable<object[]> UčitajPodatkeCSV(string datoteka)
        {
            using (var reader = new StreamReader(datoteka))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var rows = csv.GetRecords<dynamic>();
                foreach (var row in rows)
                {
                    var values = ((IDictionary<String, Object>)row).Values;
                    var elements = values.Select(elem => elem.ToString()).ToList();
                    yield return new object[] { elements[0], elements[1], elements[2], DateTime.ParseExact(elements[3], "dd/MM/yyyy", CultureInfo.InvariantCulture), elements[4], elements[5] };
                }
            }
        }

        [TestMethod]
        [DynamicData("GlasačiCSV")]
        [ExpectedException(typeof(Exception))]
        public void Test3(string ime, string prezime, string adresa, DateTime
     datumRodjenja, string brojLK, string JMBG)
        {
            Glasac glasac = new(ime, prezime, adresa, datumRodjenja, brojLK, JMBG);
        }

        [TestMethod]
        [DynamicData("Glasači2CSV")]
        public void Test4(string ime, string prezime, string adresa, DateTime
     datumRodjenja, string brojLK, string JMBG)
        {
            Glasac glasac = new(ime, prezime, adresa, datumRodjenja, brojLK, JMBG);
            Assert.AreEqual(ime, glasac.Ime);
            Assert.AreEqual(prezime, glasac.Prezime);
        }
        #endregion
    }

    //Nejla Bečirspahić
    [TestClass]
    public class UnitTest2
    {
        #region Inline testovi
        static IEnumerable<object[]> Kandidati
        {
            get
            {
                return new[]
                {
                 new object[] { "Din", "Prezime", "Adresa", DateTime.ParseExact("01/01/1996", "dd/MM/yyyy", CultureInfo.InvariantCulture), "123E123",
                 "0101996170001", "Kandidat je bio član stranke SDA od 17/10/2010 do 19/11/2011, član stranke SDP od 12/12/2012 do 13/12/2015.",
                 "Stranka: SDA, Članstvo od: 17/10/2010, Članstvo do: 19/11/2011\nStranka: SDP, Članstvo od: 12/12/2012, Članstvo do: 13/12/2015\n"},
                 new object[] { "Amina", "Prezime", "Adresa", DateTime.ParseExact("01/01/1996", "dd/MM/yyyy", CultureInfo.InvariantCulture), "123E123",
                 "0101996170001", "Kandidat je bio član stranke Stranka demokratske akcije od 18/10/2011 do 19/11/2013, član stranke Demokratska fronta od 12/12/2013 do 13/12/2016.",
                 "Stranka: Stranka demokratske akcije, Članstvo od: 18/10/2011, Članstvo do: 19/11/2013\nStranka: Demokratska fronta, Članstvo od: 12/12/2013, Članstvo do: 13/12/2016\n"}
                 };
            }
        }

        [TestMethod]
        [DynamicData("Kandidati")]
        public void Test1(string ime, string prezime, string adresa, DateTime
     datumRodjenja, string brojLK, string JMBG, string info, string ispis)
        {
            Kandidat kandidat = new(ime, prezime, adresa, datumRodjenja, brojLK, JMBG)
            {
                Informacije = info
            };
            Assert.AreEqual(kandidat.IspisiDetaljneInformacije(), ispis);
        }
        #endregion

        #region Testovi CSV
        static IEnumerable<object[]> KandidatiCSV
        {
            get
            {
                return UčitajPodatkeCSV();
            }
        }

        public static IEnumerable<object[]> UčitajPodatkeCSV()
        {
            using (var reader = new StreamReader("Kandidati.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var rows = csv.GetRecords<dynamic>();
                foreach (var row in rows)
                {
                    var values = ((IDictionary<String, Object>)row).Values;
                    var elements = values.Select(elem => elem.ToString()).ToList();
                    elements[7] = elements[7].Replace("$", "\n");
                    yield return new object[] { elements[0], elements[1], elements[2], DateTime.ParseExact(elements[3], "dd/MM/yyyy", CultureInfo.InvariantCulture), elements[4], elements[5], elements[6], elements[7] };
                }
            }
        }

        [TestMethod]
        [DynamicData("KandidatiCSV")]
        public void Test2(string ime, string prezime, string adresa, DateTime
     datumRodjenja, string brojLK, string JMBG, string info, string ispis)
        {
            Kandidat kandidat = new(ime, prezime, adresa, datumRodjenja, brojLK, JMBG)
            {
                Informacije = info
            };
            Assert.AreEqual(kandidat.IspisiDetaljneInformacije(), ispis);
        }
        #endregion
    }

    //Din Švraka
    [TestClass]
    public class UnitTest3
    {
        #region Inline testovi
        static IEnumerable<object[]> Izbori
        {
            get
            {
                return new[]
                {
                 new object[] { 0, "SDA", "broj glasova: 0", "glasova: 0%", "mandate: 0", "SNSD", "broj glasova: 0", "glasova: 0%", "mandate: 0" },
                 new object[] { 4, "SDA", "broj glasova: 3", "glasova: 75%", "mandate: 3", "SNSD", "broj glasova: 1", "glasova: 25%", "mandate: 2" }
                 };
            }
        }

        [TestMethod]
        [DynamicData("Izbori")]
        public void Test1(int brojGlasaca, string prvaStranka, string prviGlasovi, string prviPostotak, string prviBrojMandatskihKandidata, string drugaStranka, string drugiGlasovi, string drugiPostotak, string drugiBrojMandatskihKandidata)
        {
            List<Stranka> stranke = new();
            Stranka stranka1 = new("SDA", "Izetbegović za predsjednika!", new List<Kandidat>());
            stranka1.DodajKandidata(new Kandidat("Bakir", "Izetbegović", "Adresa1", new DateTime(1955, 1, 1), "111E111", "0101955111111", stranka1));
            stranka1.DodajKandidata(new Kandidat("Sebija", "Izetbegović", "Adresa2", new DateTime(1955, 1, 2), "222E222", "0201955222222", stranka1));
            stranka1.DodajKandidata(new Kandidat("Šefik", "Džaferagić", "Adresa3", new DateTime(1955, 1, 3), "333E333", "0301955333333", stranka1));
            stranke.Add(stranka1);

            Stranka stranka2 = new("SNSD", "Krišto za predsjednicu!", new List<Kandidat>());
            stranka2.DodajKandidata(new Kandidat("Milorad", "Dodik", "Adresa4", new DateTime(1955, 1, 4), "444E444", "0401955444444", stranka2));
            stranka2.DodajKandidata(new Kandidat("Željka", "Cvijanović", "Adresa5", new DateTime(1955, 1, 5), "555E555", "0501955555555", stranka2));
            stranka2.DodajKandidata(new Kandidat("Dragutin", "Dragutinić", "Adresa6", new DateTime(1955, 1, 6), "666E666", "0601955666666", stranka2));
            stranke.Add(stranka2);

            List<Glasac> glasaci = new()
            {
                new Glasac("Ime", "Prezime", "Adresa", DateTime.ParseExact("01/01/2001", "dd/MM/yyyy", CultureInfo.InvariantCulture),"111E111","0101001111111"),
                new Glasac("Ime", "Prezime", "Adresa", DateTime.ParseExact("02/02/2002", "dd/MM/yyyy", CultureInfo.InvariantCulture),"222E222","0202002222222"),
                new Glasac("Ime", "Prezime", "Adresa", DateTime.ParseExact("03/03/2003", "dd/MM/yyyy", CultureInfo.InvariantCulture),"333E333","0303003333333"),
                new Glasac("Ime", "Prezime", "Adresa", DateTime.ParseExact("04/04/2004", "dd/MM/yyyy", CultureInfo.InvariantCulture),"444E444","0404004444444")
            };

            Izbori izbori = new(stranke, new(), 100);

            for (int i = 0; i < brojGlasaca; i++)
            {
                if (i == 0)
                {
                    glasaci[0].GlasajZaStranku(stranke[0], stranke[0].Kandidati);
                }
                else if (i == 1)
                {
                    glasaci[1].GlasajZaStranku(stranke[0], new() { stranke[0].Kandidati[0], stranke[0].Kandidati[1] });
                }
                else if (i == 2)
                {
                    glasaci[2].GlasajZaStranku(stranke[0], new() { stranke[0].Kandidati[0] });
                }
                else if (i == 3)
                {
                    glasaci[3].GlasajZaStranku(stranke[1], new() { stranke[1].Kandidati[0], stranke[1].Kandidati[1] });
                }
                izbori.DodajGlasaca(glasaci[i]);
            }
            var ispis = izbori.IspisiRezultate();
            StringAssert.Contains(ispis, prvaStranka);
            StringAssert.Contains(ispis, prviGlasovi);
            StringAssert.Contains(ispis, prviPostotak);
            StringAssert.Contains(ispis, prviBrojMandatskihKandidata);
            StringAssert.Contains(ispis, drugaStranka);
            StringAssert.Contains(ispis, drugiGlasovi);
            StringAssert.Contains(ispis, drugiPostotak);
            StringAssert.Contains(ispis, drugiBrojMandatskihKandidata);
        }
        #endregion

        #region Testovi CSV
        static IEnumerable<object[]> IzboriCSV
        {
            get
            {
                return UčitajPodatkeCSV();
            }
        }

        public static IEnumerable<object[]> UčitajPodatkeCSV()
        {
            using (var reader = new StreamReader("Izbori.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var rows = csv.GetRecords<dynamic>();
                foreach (var row in rows)
                {
                    var values = ((IDictionary<String, Object>)row).Values;
                    var elements = values.Select(elem => elem.ToString()).ToList();
                    yield return new object[] { Int32.Parse(elements[0]), elements[1], elements[2], elements[3], elements[4], elements[5], elements[6], elements[7], elements[8] };
                }
            }
        }

        [TestMethod]
        [DynamicData("IzboriCSV")]
        public void Test2(int brojGlasaca, string prvaStranka, string prviGlasovi, string prviPostotak, string prviBrojMandatskihKandidata, string drugaStranka, string drugiGlasovi, string drugiPostotak, string drugiBrojMandatskihKandidata)
        {
            List<Stranka> stranke = new();
            Stranka stranka1 = new("SDA", "Izetbegović za predsjednika!", new List<Kandidat>());
            stranka1.DodajKandidata(new Kandidat("Bakir", "Izetbegović", "Adresa1", new DateTime(1955, 1, 1), "111E111", "0101955111111", stranka1));
            stranka1.DodajKandidata(new Kandidat("Sebija", "Izetbegović", "Adresa2", new DateTime(1955, 1, 2), "222E222", "0201955222222", stranka1));
            stranka1.DodajKandidata(new Kandidat("Šefik", "Džaferagić", "Adresa3", new DateTime(1955, 1, 3), "333E333", "0301955333333", stranka1));
            stranke.Add(stranka1);

            Stranka stranka2 = new("SNSD", "Krišto za predsjednicu!", new List<Kandidat>());
            stranka2.DodajKandidata(new Kandidat("Milorad", "Dodik", "Adresa4", new DateTime(1955, 1, 4), "444E444", "0401955444444", stranka2));
            stranka2.DodajKandidata(new Kandidat("Željka", "Cvijanović", "Adresa5", new DateTime(1955, 1, 5), "555E555", "0501955555555", stranka2));
            stranka2.DodajKandidata(new Kandidat("Dragutin", "Dragutinić", "Adresa6", new DateTime(1955, 1, 6), "666E666", "0601955666666", stranka2));
            stranke.Add(stranka2);

            List<Glasac> glasaci = new()
            {
                new Glasac("Ime", "Prezime", "Adresa", DateTime.ParseExact("01/01/2001", "dd/MM/yyyy", CultureInfo.InvariantCulture),"111E111","0101001111111"),
                new Glasac("Ime", "Prezime", "Adresa", DateTime.ParseExact("02/02/2002", "dd/MM/yyyy", CultureInfo.InvariantCulture),"222E222","0202002222222"),
                new Glasac("Ime", "Prezime", "Adresa", DateTime.ParseExact("03/03/2003", "dd/MM/yyyy", CultureInfo.InvariantCulture),"333E333","0303003333333"),
                new Glasac("Ime", "Prezime", "Adresa", DateTime.ParseExact("04/04/2004", "dd/MM/yyyy", CultureInfo.InvariantCulture),"444E444","0404004444444")
            };

            Izbori izbori = new(stranke, new(), 100);

            for (int i = 0; i < brojGlasaca; i++)
            {
                if (i == 0)
                {
                    glasaci[0].GlasajZaStranku(stranke[0], stranke[0].Kandidati);
                }
                else if (i == 1)
                {
                    glasaci[1].GlasajZaStranku(stranke[0], new() { stranke[0].Kandidati[0], stranke[0].Kandidati[1] });
                }
                else if (i == 2)
                {
                    glasaci[2].GlasajZaStranku(stranke[0], new() { stranke[0].Kandidati[0] });
                }
                else if (i == 3)
                {
                    glasaci[3].GlasajZaStranku(stranke[1], new() { stranke[1].Kandidati[0], stranke[1].Kandidati[1] });
                }
                izbori.DodajGlasaca(glasaci[i]);
            }
            var ispis = izbori.IspisiRezultate();
            StringAssert.Contains(ispis, prvaStranka);
            StringAssert.Contains(ispis, prviGlasovi);
            StringAssert.Contains(ispis, prviPostotak);
            StringAssert.Contains(ispis, prviBrojMandatskihKandidata);
            StringAssert.Contains(ispis, drugaStranka);
            StringAssert.Contains(ispis, drugiGlasovi);
            StringAssert.Contains(ispis, drugiPostotak);
            StringAssert.Contains(ispis, drugiBrojMandatskihKandidata);
        }
        #endregion
    }

    //Ermin Jamaković
    [TestClass]
    public class UnitTest4
    {
        #region Unit testovi
        [TestClass]
        public class TestiranjeFunkcionalnosti_4
        {
            static Stranka stranka;
            static Kandidat kandidat;

            [ClassInitialize]
            public static void PočetnaInicijalizacija(TestContext context)
            {
                kandidat = new Kandidat("bake", "bakir", "111", Convert.ToDateTime("11/11/1999"), "333E333", "1111999222222");
            }
            [TestInitialize]
            public void InicjalizacijaPrijeSvakogTesta()
            {
                stranka = new Stranka("Stranak", "Najbolja");
            }
            [TestMethod]
            public void TestDodavanjeClanaRukovodstva()
            {
                stranka.dodajClanaRukovodstva(kandidat);
                Assert.IsTrue(stranka.Rukovodstvo.Count == 1);
            }
            [TestMethod]
            public void TestBrisanjaClanaRukovodstva()
            {
                stranka.dodajClanaRukovodstva(kandidat);
                stranka.izbrisiClanaRukovodstva(kandidat);
                Assert.IsFalse(stranka.Rukovodstvo.Contains(kandidat));
            }
            [TestMethod]
            public void TestIspisaInformacijaRukovodstva1()
            {
                stranka.DodajKandidata(kandidat);
                stranka.dodajClanaRukovodstva(kandidat);
                Assert.AreEqual("Ukupan broj glasova: 0; Kandidati: Identifikacioni broj: " + kandidat.IdentifikacioniBroj, stranka.ispisiInformacijeRukovodstva());
            }
            [TestMethod]
            public void TestIspisaInformacijaRukovodstva2()
            {
                Kandidat kandidat2 = new Kandidat("Bake", "Baki", "22", DateTime.ParseExact("22/03/2000", "dd/MM/yyyy", CultureInfo.InvariantCulture), "111E111", "2203000222222");
                stranka.DodajKandidata(kandidat);
                stranka.dodajClanaRukovodstva(kandidat);
                kandidat2.BrojGlasova = 10;
                stranka.dodajClanaRukovodstva(kandidat2);
                stranka.DodajKandidata(kandidat2);
                Assert.AreEqual("Ukupan broj glasova: 10; Kandidati: Identifikacioni broj: " + kandidat.IdentifikacioniBroj + ",Identifikacioni broj: " + kandidat2.IdentifikacioniBroj, stranka.ispisiInformacijeRukovodstva());
            }
        }
        #endregion

        #region Inline testovi
        static IEnumerable<object[]> Stranka
        {
            get
            {
                return new[]
                {
                 new object[] { "Stranka", "BiH", new List<Kandidat>(){new Kandidat("Ime", "Prezime", "Adresa", DateTime.ParseExact("11/11/2000", "dd/MM/yyyy", CultureInfo.InvariantCulture),"222E222","1111000222222"), new Kandidat("Ime", "Prezime", "Adresa", DateTime.ParseExact("11/11/1999", "dd/MM/yyyy", CultureInfo.InvariantCulture), "222E222", "1111999222222")}
                 , "Rukovodstvo", new List<Kandidat>(){ new Kandidat("Ime", "Prezime", "Adresa", DateTime.ParseExact("11/11/2000", "dd/MM/yyyy", CultureInfo.InvariantCulture), "222E222", "1111000222222")}, "Ukupan broj glasova: 0; Kandidati: Identifikacioni broj: ImPrAd112211" }
                };
            }
        }

        [TestMethod]
        [DynamicData("Stranka")]
        public void Test1(string naziv, string info, List<Kandidat> kandidati, string infoRukovodstvo, List<Kandidat> rukovodstvo, string ispis)
        {
            Stranka stranka = new Stranka(naziv, info, kandidati, infoRukovodstvo, rukovodstvo);
            Assert.AreEqual(ispis, stranka.ispisiInformacijeRukovodstva());
        }
        #endregion

        #region XML testovi
        public static IEnumerable<object[]> UčitajPodatkeXML()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Stranka.xml");
            XmlNodeList strankeXML = doc.SelectNodes("//Stranke/Stranka");
            XmlNodeList kandidatiXML = doc.SelectNodes("//Stranke/Stranka/Kandidati");
            XmlNodeList rukovodstvoXML = doc.SelectNodes("//Stranke/Stranka/Rukovodstvo");

            for (int i = 0; i < strankeXML.Count; i++)
            {
                XmlNode s = strankeXML[i];
                XmlNode kan = kandidatiXML[i];
                XmlNode ruk = rukovodstvoXML[i];
                string naziv = s.ChildNodes.Item(0).InnerText.Trim();
                string info = s.ChildNodes.Item(1).InnerText.Trim();
                List<Kandidat> kandidati = new List<Kandidat>();
                XmlNodeList lista = kan.SelectNodes("Kandidat");
                foreach (XmlNode kanidat in lista)
                {
                    string ime = kanidat.SelectSingleNode("Ime").InnerText.Trim();
                    string prezime = kanidat.SelectSingleNode("Prezime").InnerText.Trim();
                    string adresa = kanidat.SelectSingleNode("Adresa").InnerText.Trim();
                    string datum = kanidat.SelectSingleNode("DatumRodenja").InnerText.Trim();
                    string bjk = kanidat.SelectSingleNode("BrojLicneKarte").InnerText.Trim();
                    string jmbg = kanidat.SelectSingleNode("JMBG").InnerText.Trim();
                    kandidati.Add(new Kandidat(ime, prezime, adresa, DateTime.ParseExact(datum, "dd/MM/yyyy", CultureInfo.InvariantCulture), bjk, jmbg));
                }
                List<Kandidat> rukovodstvo = new List<Kandidat>();
                XmlNodeList lista2 = ruk.SelectNodes("Kandidat");
                foreach (XmlNode kanidat in lista2)
                {
                    string ime = kanidat.SelectSingleNode("Ime").InnerText.Trim();
                    string prezime = kanidat.SelectSingleNode("Prezime").InnerText.Trim();
                    string adresa = kanidat.SelectSingleNode("Adresa").InnerText.Trim();
                    string datum = kanidat.SelectSingleNode("DatumRodenja").InnerText.Trim();
                    string bjk = kanidat.SelectSingleNode("BrojLicneKarte").InnerText.Trim();
                    string jmbg = kanidat.SelectSingleNode("JMBG").InnerText.Trim();
                    rukovodstvo.Add(new Kandidat(ime, prezime, adresa, DateTime.ParseExact(datum, "dd/MM/yyyy", CultureInfo.InvariantCulture), bjk, jmbg));
                }
                string infoR = s.ChildNodes.Item(3).InnerText.Trim();
                yield return new object[] { naziv, info, kandidati, infoR, rukovodstvo };
            }

        }
        static IEnumerable<object[]> StrankaXML
        {
            get
            {
                return UčitajPodatkeXML();
            }
        }
        [TestMethod]
        [DynamicData("StrankaXML")]
        public void Test2XML(string naziv, string info, List<Kandidat> kandidati, string infoR, List<Kandidat> rukovodstvo)
        {
            Stranka stranka = new Stranka(naziv, info, kandidati, infoR, rukovodstvo);
            Assert.AreEqual("Ukupan broj glasova: 0; Kandidati: Identifikacioni broj: ImPrAd112211,Identifikacioni broj: ImPrAd222222", stranka.ispisiInformacijeRukovodstva());
        }

        #endregion

    }

    // Tarik Đogić
    [TestClass]
    public class UnitTest6
    {
        #region Inicijalizacija
        static Administrator admin;

        [TestInitialize]
        public void InicjalizacijaPrijeSvakogTesta()
        {
            List<Stranka> stranke = new();
            Stranka strankaSDA = new("SDA", "Izetbegović za predsjednika!", new List<Kandidat>());
            Stranka strankaSNSD = new("SNSD", "Krišto za predsjednicu!", new List<Kandidat>());

            Kandidat kandidatSDA1 = new("Bakir", "Izetbegović", "Adresa1", new DateTime(1955, 1, 1), "111E111", "0101955111111", strankaSDA);
            Kandidat kandidatSDA2 = new("Sebija", "Izetbegović", "Adresa2", new DateTime(1955, 1, 2), "222E222", "0201955222222", strankaSDA);

            Kandidat kandidatSNSD1 = new("Milorad", "Dodik", "Adresa4", new DateTime(1955, 1, 4), "444E444", "0401955444444", strankaSNSD);
            Kandidat kandidatSNSD2 = new("Željka", "Cvijanović", "Adresa5", new DateTime(1955, 1, 5), "555E555", "0501955555555", strankaSNSD);

            List<Kandidat> nezavisniKandidati = new();
            Kandidat nezavisniKandidat1 = new("Edin", "Forto", "Adresa7", new DateTime(1955, 1, 7), "777E777", "0701955777777");
            Kandidat nezavisniKandidat2 = new("Nermin", "Nikšić", "Adresa8", new DateTime(1955, 1, 8), "888E888", "0801955888888");


            strankaSDA.DodajKandidata(kandidatSDA1);
            strankaSDA.DodajKandidata(kandidatSDA2);
            stranke.Add(strankaSDA);

            strankaSNSD.DodajKandidata(kandidatSNSD1);
            strankaSNSD.DodajKandidata(kandidatSNSD2);
            stranke.Add(strankaSNSD);

            nezavisniKandidati.Add(nezavisniKandidat1);
            nezavisniKandidati.Add(nezavisniKandidat2);

            Izbori izbori = new(stranke, nezavisniKandidati, 100);

            Glasac glasac = new("Huse", "Husić", "Adresa1", new DateTime(1993, 1, 1), "111J111", "0101993111111");

            glasac.GlasajZaStranku(strankaSDA, new List<Kandidat>() { kandidatSDA1, kandidatSDA2 });

            izbori.DodajGlasaca(glasac);

            admin = new Administrator(izbori);
        }
        #endregion

        #region Inline testovi
        static IEnumerable<object[]> Administrator
        {
            get
            {
                return new[] {
                   new object[] { "Ukucajte indetifikacioni broj glasača čiji glas želite poništiti:\r\n\r\nNe postoji glasač sa tim identifikacionim brojem.\r\n\r\n", "Pogresan identifikacioni broj", "Nije bitna šifra" , false},
                   new object[] { "Ukucajte indetifikacioni broj glasača čiji glas želite poništiti:\r\n\r\nIz sigurnosnih razloga ukucajte tajnu šifru (imate 3 pokušaja):\r\n\r\n", "HuHuAd011101", "VVS20222023" , false},
                   new object[] { "Ukucajte indetifikacioni broj glasača čiji glas želite poništiti:\r\n\r\nIz sigurnosnih razloga ukucajte tajnu šifru (imate 3 pokušaja):\r\nOstalo vam je još 2 pokušaja.\r\n\r\n", "HuHuAd011101", "Pogresna šifra\r\nVVS20222023" , false },
                   new object[] { "Ukucajte indetifikacioni broj glasača čiji glas želite poništiti:\r\n\r\nIz sigurnosnih razloga ukucajte tajnu šifru (imate 3 pokušaja):\r\nOstalo vam je još 2 pokušaja.\r\nOstalo vam je još 1 pokušaja.\r\n\r\n", "HuHuAd011101", "Pogresna šifra prvi put\r\nPogresna šifra drugi put\r\nVVS20222023" , false },
                   new object[] { "Ukucajte indetifikacioni broj glasača čiji glas želite poništiti:\r\n\r\nIz sigurnosnih razloga ukucajte tajnu šifru (imate 3 pokušaja):\r\nOstalo vam je još 2 pokušaja.\r\nOstalo vam je još 1 pokušaja.\r\nOstalo vam je još 0 pokušaja.\r\n\r\nNemate dopuštenje da poništite nečiji glas.\r\nPokušaj proboja. Gasimo sistem...\r\n", "HuHuAd011101", "Pogresna šifra prvi put\r\nPogresna šifra drugi put\r\nPogresna šifra treci put", true }
                };
            }
        }

        [TestMethod]
        [DynamicData("Administrator")]
        public void Test1(string poruka, string identifikacioniBroj, string sifra, bool daLiCeDociDoBacanjaIzuzetka)
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var input = identifikacioniBroj + "\r\n" + sifra;
            var stringReader = new StringReader(input);
            Console.SetIn(stringReader);

            if (!daLiCeDociDoBacanjaIzuzetka)
            {
                admin.PonistiGlas();

                var output = stringWriter.ToString();

                Assert.AreEqual(poruka, output);
            }
            else
            {
                Assert.ThrowsException<Exception>(() => admin.PonistiGlas());
            }
        }
        #endregion

        #region Testovi CSV
        static IEnumerable<object[]> AdministratorCSV
        {
            get
            {
                return UčitajPodatkeCSV();
            }
        }

        public static IEnumerable<object[]> UčitajPodatkeCSV()
        {
            using (var reader = new StreamReader("Administrator.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var rows = csv.GetRecords<dynamic>();
                foreach (var row in rows)
                {
                    var values = ((IDictionary<String, Object>)row).Values;
                    var elements = values.Select(elem => elem.ToString()).ToList();
                    yield return new object[] { elements[0], elements[1], elements[2], bool.Parse(elements[3]) };
                }
            }
        }

        [TestMethod]
        [DynamicData("AdministratorCSV")]
        public void Test2(string poruka, string identifikacioniBroj, string sifra, bool daLiCeDociDoBacanjaIzuzetka)
        {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var input = identifikacioniBroj + "\r\n" + sifra;
            var stringReader = new StringReader(input);
            Console.SetIn(stringReader);

            if (!daLiCeDociDoBacanjaIzuzetka)
            {
                admin.PonistiGlas();

                var output = stringWriter.ToString();

                Assert.AreEqual(poruka, output);
            }
            else
            {
                Assert.ThrowsException<Exception>(() => admin.PonistiGlas());
            }
        }
        #endregion
    }

    //Test-Driven Development
    [TestClass]
    public class UnitTest5
    {
        [TestMethod]
        public void TestZamjenskiObjekat()
        {
            Fake fake = new()
            {
                IDBrojevi = new()
                {
                    "AaBbCc112211","AaCcBb112211","AaBbCc221122"
                }
            };
            Glasac glasac1 = new("Aaa", "Bbb", "Ccc", DateTime.ParseExact("11/11/2001", "dd/MM/yyyy", CultureInfo.InvariantCulture), "221E221", "1111001123123");
            bool bacaIzuzetak = false;
            try
            {
                glasac1.VjerodostojnostGlasaca(fake);
            }
            catch (Exception)
            {
                bacaIzuzetak = true;
            }
            Assert.IsTrue(bacaIzuzetak);
            Glasac glasac2 = new("Ddd", "Bbb", "Ccc", DateTime.ParseExact("11/11/2001", "dd/MM/yyyy", CultureInfo.InvariantCulture), "221E221", "1111001123123");
            Assert.IsTrue(glasac2.VjerodostojnostGlasaca(fake));
        }
    }
}
