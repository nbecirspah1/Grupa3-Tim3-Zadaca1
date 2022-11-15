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
        public List<Kandidat> Kandidati { get => kandidati; set => kandidati = value; }
        public int BrojGlasova { get => brojGlasova; set => brojGlasova = value; }
        public string NazivStranke { get => nazivStranke; set => nazivStranke = value; }
        /*Dodani seteri za sve atribute. Ovim putem je popravljena
        kvaliteta koda i omogućena nadogradnja istog u budućnosti.*/
        #endregion
    }
}
