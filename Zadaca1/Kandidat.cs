using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadaca1
{
    public class Kandidat
    {
        #region Atributi
        string ime;
        string prezime;
        string informacije;
        int brojGlasova;
        Stranka stranka = null;
        #endregion

        #region Konstruktor
        public Kandidat(string ime, string prezime, Stranka stranka = null)
        {
            this.ime = ime;
            this.prezime = prezime;
            brojGlasova = 0;
            informacije = "";
            this.stranka = stranka;
        }
        #endregion

        #region Properties
        public string Informacije { set => informacije = value; }
        public int BrojGlasova { get => brojGlasova; }
        public string NazivKandidata { get => ime + " " + prezime; }

        public override bool Equals(object obj)
        {
            return obj is Kandidat kandidat &&
                   ime == kandidat.ime &&
                   prezime == kandidat.prezime &&
                   informacije == kandidat.informacije &&
                   brojGlasova == kandidat.brojGlasova &&
                   stranka == kandidat.stranka;
        }
        #endregion

        #region Metode
        public void povecajBrojGlasova() { brojGlasova++; }

        public override int GetHashCode()
        {
            return HashCode.Combine(ime,prezime,informacije,brojGlasova,stranka);
        }
        #endregion
    }
}
