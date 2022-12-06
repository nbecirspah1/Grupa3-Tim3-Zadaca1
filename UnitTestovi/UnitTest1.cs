using CsvHelper;
using System.Globalization;

namespace UnitTestovi
{
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