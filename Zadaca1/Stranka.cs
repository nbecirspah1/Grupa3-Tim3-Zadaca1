using System;
using System.Collections.Generic;

namespace Zadaca1
{
    public class Stranka
    {
    //Postavljeni ogovarajući atributi
        #region Atributi
        string nazivStranke;
        string informacije;
        List<Kandidat> kandidati = new List<Kandidat>();
        int brojGlasova;
        string informacijeRukovodstva = "";
        List<Kandidat> rukovodstvo = new List<Kandidat>();
        #endregion

        
        #region Konstruktor
        public Stranka(string nazivStranke, string informacije, List<Kandidat> kandidati = null)
        {
            this.nazivStranke = nazivStranke;
            this.informacije = informacije;
            if(kandidati!=null)
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
        public void DodajKandidata(Kandidat kandidat) 
        { 
            kandidati.Add(kandidat);
            kandidat.Stranka = this;
        }
        //Metoda uklanja kandidata koji je proslijedjen kao argument ove funkcije
        public void izbrisiKandidata(Kandidat kandidat) 
        { 
            kandidati.Remove(kandidat);
            kandidat.Stranka = null;
        }
        //Metoda dodaje novog člana rukovodstva
        public void dodajClanaRukovodstva(Kandidat clan)
        { 
            rukovodstvo.Add(clan);
            clan.Stranka = this;
        }
        //Metoda uklanja člana rukovodstva
        public void izbrisiClanaRukovodstva(Kandidat clan)
        {
            rukovodstvo.Remove(clan);
            clan.Stranka = null;
        }
        //Metoda za povecavanje glasova

        public void PovecajGlasove() { brojGlasova++; }
        public void SmanjiGlasove() { brojGlasova--; }

        //Ermin Jamaković - Funkcionalnost 4
        public string ispisiInformacijeRukovodstva()
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
            return ispis;
        }

        public override bool Equals(object obj)
        {
            return obj is Stranka stranka &&
                   nazivStranke == stranka.nazivStranke &&
                   informacije == stranka.informacije &&
                   EqualityComparer<List<Kandidat>>.Default.Equals(kandidati, stranka.kandidati) &&
                   EqualityComparer<List<Kandidat>>.Default.Equals(rukovodstvo, stranka.rukovodstvo) &&
                   informacijeRukovodstva == stranka.informacijeRukovodstva &&
                   brojGlasova == stranka.brojGlasova;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(nazivStranke, informacije, kandidati, brojGlasova, rukovodstvo, informacijeRukovodstva);
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
