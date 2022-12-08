using CsvHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
                 new object[] { "", "Prezime", "Adresa", DateTime.Parse("01/01/1996"), "123E123", "0101996170001"},
                 new object[] { "Din", "", "Adresa", DateTime.Parse("01/01/1996"), "123E123", "0101996170001"},
                 new object[] { "Din", "Prezime", "", DateTime.Parse("01/01/1996"), "123E123", "0101996170001"},
                 new object[] { "Din1", "Prezime", "Adresa", DateTime.Parse("01/01/1996"), "123E123", "0101996170001"},
                 new object[] { "Din", "Prezime1", "Adresa", DateTime.Parse("01/01/1996"), "123E123", "0101996170001"},
                 new object[] { "D", "Prezime", "Adresa", DateTime.Parse("01/01/1996"), "123E123", "0101996170001"},
                 new object[] { "Din", "Pr", "Adresa", DateTime.Parse("01/01/1996"), "123E123", "0101996170001"},
                 new object[] { "Din", "Prezime", "Adresa", DateTime.Parse("01/01/2024"), "123E123", "0101024170001"},
                 new object[] { "Din", "Prezime", "Adresa", DateTime.Parse("01/01/2009"), "123E123", "0101009170001"},
                 new object[] { "Din", "Prezime", "Adresa", DateTime.Parse("01/01/1996"), "123A123", "0101996170001"},
                 new object[] { "Din", "Prezime", "Adresa", DateTime.Parse("01/01/1996"), "123E123", "01019171701"}
                };
            }
        }

        static IEnumerable<object[]> Glasači2
        {
            get
            {
                return new[]
                {
                 new object[] { "Din", "Prezime", "Adresa", DateTime.Parse("01/01/1996"), "123E123", "0101996170001"}
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
                    yield return new object[] { elements[0], elements[1], elements[2], DateTime.Parse(elements[3]), elements[4], elements[5] };
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
                 new object[] { "Din", "Prezime", "Adresa", DateTime.Parse("01/01/1996"), "123E123",
                 "0101996170001", "Kandidat je bio član stranke SDA od 17/10/2010 do 19/11/2011, član stranke SDP od 12/12/2012 do 13/12/2015.",
                 "Stranka: SDA, Članstvo od: 17/10/2010, Članstvo do: 19/11/2011\nStranka: SDP, Članstvo od: 12/12/2012, Članstvo do: 13/12/2015\n"},
                 new object[] { "Amina", "Prezime", "Adresa", DateTime.Parse("01/01/1996"), "123E123",
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
                    yield return new object[] { elements[0], elements[1], elements[2], DateTime.Parse(elements[3]), elements[4], elements[5], elements[6], elements[7] };
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
        static IEnumerable<object[]> Kandidati
        {
            get
            {
                return new[]
                {
                 new object[] { "Din", "Prezime", "Adresa", DateTime.Parse("01/01/1996"), "123E123",
                 "0101996170001", "Kandidat je bio član stranke SDA od 17/10/2010 do 19/11/2011, član stranke SDP od 12/12/2012 do 13/12/2015.",
                 "Stranka: SDA, Članstvo od: 17/10/2010, Članstvo do: 19/11/2011\nStranka: SDP, Članstvo od: 12/12/2012, Članstvo do: 13/12/2015\n"},
                 new object[] { "Amina", "Prezime", "Adresa", DateTime.Parse("01/01/1996"), "123E123",
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
                    yield return new object[] { elements[0], elements[1], elements[2], DateTime.Parse(elements[3]), elements[4], elements[5], elements[6], elements[7] };
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
                stranka.dodajKandidata(kandidat);
                stranka.dodajClanaRukovodstva(kandidat);
                Assert.AreEqual("Ukupan broj glasova: 0; Kandidati: Identifikacioni broj: " + kandidat.IdentifikacioniBroj, stranka.ispisiInformacijeRukovodstva());
            }
            [TestMethod]
            public void TestIspisaInformacijaRukovodstva2()
            {
                Kandidat kandidat2 = new Kandidat("Bake", "Baki", "22", Convert.ToDateTime("22/3/2000"), "111E111", "2203000222222");
                stranka.dodajKandidata(kandidat);
                stranka.dodajClanaRukovodstva(kandidat);
                kandidat2.BrojGlasova = 10;
                stranka.dodajClanaRukovodstva(kandidat2);
                stranka.dodajKandidata(kandidat2);
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
                 new object[] { "Stranka", "BiH", new List<Kandidat>(){new Kandidat("Ime", "Prezime", "Adresa", DateTime.Parse("11/11/2000"),"222E222","1111000222222"), new Kandidat("Ime", "Prezime", "Adresa", DateTime.Parse("11/11/1999"), "222E222", "1111999222222")}
                 , "Rukovodstvo", new List<Kandidat>(){ new Kandidat("Ime", "Prezime", "Adresa", DateTime.Parse("11/11/2000"), "222E222", "1111000222222")}, "Ukupan broj glasova: 0; Kandidati: Identifikacioni broj: ImPrAd112211" }
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
            doc.Load("C:\\Users\\DT User2\\source\\repos\\nbecirspah1\\Grupa3-Tim3-Zadaca1\\UnitTestovi\\Stranka.xml");
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                List<string> elements = new List<string>();
                foreach (XmlNode innerNode in node)
                {
                    elements.Add(innerNode.InnerText);
                }
                Console.WriteLine(elements[2]);
                yield return new object[] { elements[0], elements[1],
elements[2], elements[3], elements[4]};
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
        public void Test2XML()
        {

        }

        #endregion

    }
}
