using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadaca1
{
    public class Stranka
    {
    //pOSTAVLJENI ogovarajući atributI
        #region Atributi
        string nazivStranke;
        string informacije;
        List<Kandidat> kandidati;
        int brojGlasova;
        #endregion

        
        #region KONSTRUKTOR
        public Stranka(string nazivStranke, string informacije, List<Kandidat> kandidati)
        {
            this.nazivStranke = nazivStranke;
            this.informacije = informacije;
            this.kandidati = kandidati;
            brojGlasova = 0;
        }
        #endregion

        #region Metode
        /*Metoda dodaje novog kandidata u atribut kandidati, koji je tipa List<Kandidat>*/
        public void dodajKandidata(Kandidat kandidat) { kandidati.Add(kandidat); }
        //Method removes candidate which is an argument of this function
        public void izbrisiKandidata(Kandidat kandidat) { 
            if(kandidati.size()==0) return;
            kandidati.Remove(kandidat); 
        }
        //Method for votes
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

        //odgovarajući geteri
        #region Properties
        public List<Kandidat> Kandidati { get => kandidati; }
        public int BrojGlasova { get => brojGlasova;  }
        public string NazivStranke { get => nazivStranke; }
        #endregion
    }
}
