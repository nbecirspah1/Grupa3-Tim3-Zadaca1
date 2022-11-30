using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadaca1
{
    public class Stranka
    {
    //Postavljeni ogovarajući atributi
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
        //Metoda dodaje novog kandidata u atribut kandidati, koji je tipa List<Kandidat>
        public void dodajKandidata(Kandidat kandidat) { kandidati.Add(kandidat); }
        //Metoda uklanja kandidata koji je proslijedjen kao argument ove funkcije
        public void izbrisiKandidata(Kandidat kandidat) { 
            kandidati.Remove(kandidat); 
        }
        //Metoda za povecavanje glasova
        public void povecajGlasove() { brojGlasova++; }

        public override bool Equals(object obj)
        {
            return obj is Stranka stranka &&
                   nazivStranke == stranka.nazivStranke &&
                   informacije == stranka.informacije &&
                   EqualityComparer<List<Kandidat>>.Default.Equals(kandidati, stranka.kandidati) &&
                   brojGlasova == stranka.brojGlasova;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(nazivStranke, informacije, kandidati, brojGlasova);
        }
        #endregion

        //Odgovarajući geteri
        #region Properties
        public List<Kandidat> Kandidati { get => kandidati; set => kandidati = value; }
        public int BrojGlasova { get => brojGlasova; set => brojGlasova = value; }
        public string NazivStranke { get => nazivStranke; set => nazivStranke = value; }
        #endregion
    }
}
