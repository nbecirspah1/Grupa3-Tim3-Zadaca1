using CsvHelper;
using System.Globalization;
using System.Text.RegularExpressions;
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
                    yield return new object[] { elements[0], elements[1], elements[2], DateTime.Parse(elements[3]), elements[4], elements[5]};
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
}
