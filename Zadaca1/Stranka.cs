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
        string informacijeRukovodstva = "";
        List<Kandidat> rukovodstvo = new List<Kandidat>();
        #endregion

        
        #region Konstruktor
        public Stranka(string nazivStranke, string informacije, List<Kandidat> kandidati)
        {
            this.nazivStranke = nazivStranke;
            this.informacije = informacije;
            this.kandidati = kandidati;
            brojGlasova = 0;
        }
        public Stranka(string nazivStranke, string informacije, List<Kandidat> kandidati, string informacijeRukovodstva, List<Kandidat> rukovodstvo)
        {
            this.nazivStranke = nazivStranke;
            this.informacije = informacije;
            this.kandidati = kandidati;
            this.rukovodstvo = rukovodstvo;
            this.informacijeRukovodstva = informacijeRukovodstva;
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
        //Metoda dodaje novog člana rukovodstva
        public void dodajClanaRukovodstva(Kandidat clan)
        { rukovodstvo.Add(clan); }
        //Metoda uklanja člana rukovodstva
        public void izbrisiClanaRukovodstva(Kandidat clan)
        {
            rukovodstvo.Remove(clan);
        }
        //Metoda za povecavanje glasova

        public void povecajGlasove() { brojGlasova++; }

        //Ermin Jamaković - Funkcionalnost 4
        public void ispisiInformacijeRukovodstva()
        {
            int brojGlasova = 0;
            kandidati.ForEach(kandidat =>
            {
                if (rukovodstvo.Contains(kandidat))
                    brojGlasova += kandidat.BrojGlasova;

            });
            string ispis = "Ukupan broj glasova: " + brojGlasova + "; Kandidati: ";
            rukovodstvo.ForEach(clan => {
                if (!clan.Equals(rukovodstvo[rukovodstvo.Count - 1]))
                    ispis += "Identifikacioni broj: " + clan.IdentifikacioniBroj + ",";
            });
            ispis += "Identifikacioni broj: " + rukovodstvo[rukovodstvo.Count - 1].IdentifikacioniBroj;
            Console.WriteLine(ispis);
        }

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
        public string InformacijeRukovostva { get => informacijeRukovodstva; set => informacijeRukovodstva = value; }
        public List<Kandidat> Rukovodstvo { get => rukovodstvo; set => rukovodstvo = value; }
        public List<Kandidat> Kandidati { get => kandidati; set => kandidati = value; }
        public int BrojGlasova { get => brojGlasova; set => brojGlasova = value; }
        public string NazivStranke { get => nazivStranke; set => nazivStranke = value; }
        #endregion
    }
}
