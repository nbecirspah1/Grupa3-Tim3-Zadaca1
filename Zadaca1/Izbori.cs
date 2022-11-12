using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadaca1
{
    public class Izbori
    {   
        #region Atributi
        List<Stranka> stranke;
        List<Kandidat> nezavisniKandidati;
        int brojMogucihGlasaca;
        #endregion
        
        #region Konstruktor
        public Izbori(List<Stranka> stranke, List<Kandidat> nezavisniKandidati, int brojMogucihGlasaca)
        {
            this.stranke = stranke;
            this.nezavisniKandidati = nezavisniKandidati;
            this.brojMogucihGlasaca = brojMogucihGlasaca;
        }
        #endregion
        
        #region Metode
        public void dodajStranku(Stranka stranka) { stranke.Add(stranka); }
        public void izbrisiStranku(Stranka stranka) { stranke.Remove(stranka); }
        public void dodajNezavisnogKandidata(Kandidat kandidat) { nezavisniKandidati.Add(kandidat); }
        public void izbrisiNezavisnogKandidata(Kandidat kandidat) { nezavisniKandidati.Remove(kandidat); }
        public int dajIzlaznostNaIzbore()
        {
            int izlaznostNaIzbore = 0;
            foreach (Stranka s in stranke)
            {
                izlaznostNaIzbore += s.BrojGlasova;
            }
            foreach (Kandidat k in nezavisniKandidati)
            {
                izlaznostNaIzbore += k.BrojGlasova;
            }
            izlaznostNaIzbore = (int)(100 * (izlaznostNaIzbore / (double)brojMogucihGlasaca));
            return izlaznostNaIzbore;
        }
        
        public Dictionary<Stranka, int> dajMandatskeStranke()
        {
            Dictionary<Stranka,int> stranke1 = new();
            int brojGlasaca = 0;
            foreach (Stranka s in stranke)
            {
                brojGlasaca += s.BrojGlasova;
            }
            foreach (Kandidat k in nezavisniKandidati)
            {
                brojGlasaca += k.BrojGlasova;
            }
            foreach(Stranka s in stranke)
            {
                double postotak = (double)s.BrojGlasova / (double)brojGlasaca;
                if (postotak>0.02)
                {
                    stranke1.Add(s,(int)(100*postotak));
                }
            }
            return stranke1;
        }
        public Dictionary<Kandidat,int> dajMandatskeNezavisneKandidate()
        {
            Dictionary<Kandidat,int> kandidati1 = new();
            int brojGlasaca = 0;
            foreach (Stranka s in stranke)
            {
                brojGlasaca += s.BrojGlasova;
            }
            foreach (Kandidat k in nezavisniKandidati)
            {
                brojGlasaca += k.BrojGlasova;
            }
            foreach (Kandidat k in nezavisniKandidati)
            {
                double postotak = (double)k.BrojGlasova / (double)brojGlasaca;
                if (postotak > 0.02)
        {
                    kandidati1.Add(k, (int)(100 * postotak));
                }
            }
            return kandidati1;
        }

        // Ova metoda vraća listu kandidata koji su osvojili 20% glasova od ukupnih glasova stranke kojoj pripadaju
        public List<Kandidat> dajMandatskeKandidate(Stranka s)
        {
            List<Kandidat> kandidati1 = new();
            foreach (Kandidat k in s.Kandidati)
            {
                if ((double)k.BrojGlasova / (double)s.BrojGlasova > 0.2)
        {
                    kandidati1.Add(k);
                }
            }
            return kandidati1;
        }
        public string dajTrenutnoStanjeIzbornihRezultata()
        {
            StringBuilder s = new();
            s.Append("Trenutno stanje izbornih rezultata:\n\n");
            s.Append("Izlaznost na izbore: ").Append(dajIzlaznostNaIzbore().ToString()).Append("%\n\n");
            s.Append("Mandatske stranke i njihovi rezultati:\n");
            foreach(var str in dajMandatskeStranke())
        {
                s.Append(str.Key.NazivStranke).Append(" ").Append(str.Value).Append("%\n");
                s.Append("Kandidati koji su osvojili mandate unutar ove stranke:\n");
                foreach(Kandidat k in dajMandatskeKandidate(str.Key))
            {
                    s.Append(k.NazivKandidata).Append("\n");
            }
        }
            s.Append("\n");
            s.Append("Mandatski nezavisni kandidati i njihovi rezultati:\n");
            foreach (var str in dajMandatskeNezavisneKandidate())
        {
                s.Append(str.Key.NazivKandidata).Append(" ").Append(str.Value).Append("%\n");
            }
            return s.ToString();
        }
        #endregion


    }
}
