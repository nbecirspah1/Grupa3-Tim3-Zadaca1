using CsvHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using Zadaca1;

namespace UnitTestovi
{
    //Ermin Jamaković
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
            Assert.AreEqual("Ukupan broj glasova: 0; Kandidati: Identifikacioni broj: " + kandidat.IdentifikacioniBroj,stranka.ispisiInformacijeRukovodstva());
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
    //Nejla Bečirspahić
    [TestClass]
    public class UnitTest1
    {
        #region Inline Testovi

        static IEnumerable<object[]> Stranke
        {
            get
            {
                return new[] { new object[] { "", "" } };
            }
        }

        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(true);
        }
        #endregion

        #region Testovi CSV
        static IEnumerable<object[]> StrankeCSV
        {
            get
            {
                return UčitajPodatkeCSV();
            }
        }

        public static IEnumerable<object[]> UčitajPodatkeCSV()
        {
            using (var reader = new StreamReader("Stranke.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var rows = csv.GetRecords<dynamic>();
                foreach (var row in rows)
                {
                    var values = ((IDictionary<String, Object>)row).Values;
                    var elements = values.Select(elem => elem.ToString()).ToList();
                    yield return new object[] { elements[0], elements[1], DateTime.Parse(elements[2]), elements[3], elements[4], elements[5] };
                }
            }
        }

        [TestMethod]
        public void TestMethod2()
        {
        }
        #endregion
    }


}