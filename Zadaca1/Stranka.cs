using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadaca1
{
    public class Stranka
    {
        #region Atributi
        string nazivStranke;
        string informacije;
        List<Kandidat> kandidati;
        int brojGlasova;
        #endregion

        #region Konstruktor
        public Stranka(string nazivStranke, string informacije, List<Kandidat> kandidati)
        {
            this.nazivStranke = nazivStranke;
            this.informacije = informacije;
            this.kandidati = kandidati;
            brojGlasova = 0;
        }
        #endregion

        #region Metode
        public void dodajKandidata(Kandidat kandidat) { kandidati.Add(kandidat); }
        public void izbrisiKandidata(Kandidat kandidat) { kandidati.Remove(kandidat); }
        public void povecajGlasove() { brojGlasova++; }
        #endregion

        #region Properties
        public List<Kandidat> Kandidati { get => kandidati; }
        public int BrojGlasova { get => brojGlasova;  }
        public string NazivStranke { get => nazivStranke; }
        #endregion
    }
}
