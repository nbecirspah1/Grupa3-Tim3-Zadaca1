using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Zadaca1
{
    public class Kandidat : Glasac
    {
        #region Atributi
        string informacije;
        int brojGlasova;
        Stranka stranka = null;
        #endregion

        #region Konstruktor
        public Kandidat(string ime, string prezime, string adresaStanovanja, DateTime datumRodjenja, string brojLicneKarte, string JMBG, Stranka stranka = null) : base(ime, prezime, adresaStanovanja, datumRodjenja, brojLicneKarte, JMBG)
        {
            brojGlasova = 0;
            informacije = "";
            this.stranka = stranka;
        }
        #endregion

        #region Properties
        public string Informacije { set => informacije = value; get => informacije; }
        public int BrojGlasova { set => brojGlasova = value; get => brojGlasova; }
        public string NazivKandidata { get => base.Ime + " " + base.Prezime; }
        public Stranka Stranka { set => stranka = value; get => stranka; }
        #endregion

        #region Metode
        public override bool Equals(object obj)
        {
            return obj is Kandidat kandidat &&
                                             IdentifikacioniBroj == (obj as Kandidat).IdentifikacioniBroj;
        }

        public void povecajBrojGlasova() { brojGlasova++; }

        public override int GetHashCode() { return HashCode.Combine(base.IdentifikacioniBroj); }

        public void ispisiDetaljneInformacije()
        {
            string informacije1 = informacije;
            Regex regex = new(@"Kandidat je bio ");
            informacije1 = regex.Replace(informacije1, "");
            regex = new(@"član stranke ");
            informacije1 = regex.Replace(informacije1, "");
            var lista = informacije1.Split(", ");
            StringBuilder informacije2 = new();
            foreach (var l in lista)
            {
                var groups = Regex.Match(l, @"(.*) od (.*) do (.*)").Groups;
                var datum2 = groups[3].Value;
                datum2 = Regex.Replace(datum2, @"\.", "");
                informacije2.Append("Stranka: ").Append(groups[1].Value).Append(", Članstvo od: ").Append(groups[2].Value)
                            .Append(", Članstvo do: ").Append(datum2).Append("\n");
            }
            Console.WriteLine(informacije2.ToString());
        }
        #endregion
    }
}
