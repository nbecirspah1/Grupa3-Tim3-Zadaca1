using System;
using System.Collections.Generic;
using System.Text;

namespace Zadaca1
{
    public class Izbori
    {
        #region Atributi
        List<Stranka> stranke;
        List<Kandidat> nezavisniKandidati;
        List<Glasac> glasaci;
        int brojMogucihGlasaca;
        #endregion

        #region Konstruktor
        public Izbori(List<Stranka> stranke, List<Kandidat> nezavisniKandidati, int brojMogucihGlasaca)
        {
            this.stranke = stranke;
            this.nezavisniKandidati = nezavisniKandidati;
            this.glasaci = new List<Glasac>();
            this.brojMogucihGlasaca = brojMogucihGlasaca;
        }
        #endregion

        #region Metode
        private int BrojGlasaca()
        {
            return glasaci.Count;
        }
        public void DodajGlasaca(Glasac glasac) { if(glasaci.Count < brojMogucihGlasaca) glasaci.Add(glasac); }
        public void UkloniGlasaca(Glasac glasac) {
            List<Kandidat> sviStranackiKandidati = glasac.DajSveStranackeKandidate();
            Stranka stranka = glasac.DajStranku();
            Kandidat nezavisniKandidat = glasac.DajNezavisnogKandidata();
            if(sviStranackiKandidati != null)
            {
                foreach(Kandidat stranakciKandidat in sviStranackiKandidati)
                {
                    stranakciKandidat.SmanjiBrojGlasova();
                }
            }
            if(stranka != null)
            {
                stranka.SmanjiGlasove();
            }
            if(nezavisniKandidat!= null)
            {
                nezavisniKandidat.SmanjiBrojGlasova();
            }
            glasaci.Remove(glasac); 
        }
        public Glasac DajKonkretnogGlasaca(string identifikacioniBroj) { return glasaci.Find(glasac => glasac.IdentifikacioniBroj.Equals(identifikacioniBroj)); }
        public List<Glasac> DajSveGlasace() { return glasaci; }
        public void DodajStranku(Stranka stranka) { stranke.Add(stranka); }
        public void IzbrisiStranku(Stranka stranka) { stranke.Remove(stranka); }
        public void DodajNezavisnogKandidata(Kandidat kandidat) { nezavisniKandidati.Add(kandidat); }
        public void IzbrisiNezavisnogKandidata(Kandidat kandidat) { nezavisniKandidati.Remove(kandidat); }
        public double DajIzlaznostNaIzbore()
        {
            return (100 * (BrojGlasaca() / (double)brojMogucihGlasaca));
        }

        public Dictionary<Stranka, int> DajMandatskeStranke()
        {
            Dictionary<Stranka, int> stranke1 = new();
            foreach (Stranka s in stranke)
            {
                double postotak = 0;
                if(glasaci.Count > 0) postotak = (double)s.BrojGlasova / glasaci.Count;
                if (postotak > 0.02)
                {
                    stranke1.Add(s, (int)(100 * postotak));
                }
            }
            return stranke1;
        }
        public Dictionary<Kandidat, int> DajMandatskeNezavisneKandidate()
        {
            Dictionary<Kandidat, int> kandidati1 = new();
            foreach (Kandidat k in nezavisniKandidati)
            {
                double postotak = 0;
                if(glasaci.Count > 0) postotak = (double)k.BrojGlasova / glasaci.Count;
                if (postotak > 0.02)
                {
                    kandidati1.Add(k, (int)(100 * postotak));
                }
            }
            return kandidati1;
        }

        // Ova metoda vraća listu kandidata koji su osvojili 20% glasova od ukupnih glasova stranke kojoj pripadaju
        public List<Kandidat> DajMandatskeKandidate(Stranka s)
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
        public string DajTrenutnoStanjeIzbornihRezultata()
        {
            StringBuilder s = new();
            s.Append("Trenutno stanje izbornih rezultata:\n\n");
            s.Append("Izlaznost na izbore: ").Append(DajIzlaznostNaIzbore().ToString()).Append("%\n\n");
            s.Append("Mandatske stranke i njihovi rezultati:\n");
            foreach (var str in DajMandatskeStranke())
            {
                s.Append(str.Key.NazivStranke).Append(" - ").Append(str.Value).Append("%\n");
                s.Append("\nKandidati koji su osvojili mandate unutar " + str.Key.NazivStranke + " stranke:\n");
                foreach (Kandidat k in DajMandatskeKandidate(str.Key))
                {
                    s.Append(k.NazivKandidata).Append("\n");
                }
                s.Append("\n");
            }
            s.Append("Mandatski nezavisni kandidati i njihovi rezultati:\n");
            foreach (var str in DajMandatskeNezavisneKandidate())
            {
                s.Append(str.Key.NazivKandidata).Append(" - ").Append(str.Value).Append("%\n");
            }
            return s.ToString();
        }

        //Din Švraka
        public void IspisiRezultate()
        {
            StringBuilder informacije = new();
            foreach (var s in stranke)
            {
                informacije.Append(s.NazivStranke).Append(":\n");
                informacije.Append("Ukupan broj glasova: ").Append(s.BrojGlasova);
                var broj = (double)s.BrojGlasova * 100 / BrojGlasaca();
                broj = (int)broj;
                informacije.Append("\nPostotak osvojenih glasova: ").Append(broj).Append('%');
                informacije.Append('\n');

                var mandatskiKandidati = DajMandatskeKandidate(s);
                informacije.Append("Broj kandidata koji su osvojili mandate: ").Append(mandatskiKandidati.Count);
                informacije.Append("\nMandatski kandidati:\n");

                foreach (var k in mandatskiKandidati)
                {
                    informacije.Append(k.Ime).Append(' ').Append(k.Prezime).Append('\n');
                    informacije.Append("Broj glasova kandidata: ").Append(k.BrojGlasova).Append('\n');
                    broj = (double)k.BrojGlasova * 100 / s.BrojGlasova;
                    informacije.Append("Postotak osvojenih glasova kandidata: ").Append((int)broj).Append("%\n");
                }
            }
            informacije.Append('\n');
            Console.WriteLine(informacije.ToString());
        }
        #endregion


    }
}
