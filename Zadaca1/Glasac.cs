using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Zadaca1
{
    public class Glasac
    {

        #region Atributi 
        string ime;
        string prezime;
        string adresaStanovanja;
        DateTime datumRodjenja;
        string brojLicneKarte;
        string JMBG;
        string identifikacioniBroj;
        bool glasaoZaStranku;
        bool glasaoZaNezavisnogKandidata;
        List<Kandidat> stranackiKandidati;
        Stranka stranka;
        Kandidat nezavisniKandidat;
        #endregion

        #region Konstruktor
        public Glasac(string ime, string prezime, string adresaStanovanja, DateTime datumRodjenja, string brojLicneKarte, string JMBG)
        {
            ValidacijaPodataka(ime, prezime, adresaStanovanja, datumRodjenja, brojLicneKarte, JMBG);
            this.ime = ime;
            this.prezime = prezime;
            this.adresaStanovanja = adresaStanovanja;
            this.datumRodjenja = datumRodjenja;
            this.brojLicneKarte = brojLicneKarte;
            this.JMBG = JMBG;
            identifikacioniBroj = ime.Substring(0, 2) + prezime.Substring(0, 2) +
                                adresaStanovanja.Substring(0, 2) + datumRodjenja.ToString("dd.MM.yyyy.").Substring(0, 2)
                                + brojLicneKarte.Substring(0, 2) + JMBG.Substring(0, 2);
            glasaoZaStranku = false;
            glasaoZaNezavisnogKandidata = false;
            stranackiKandidati = new List<Kandidat>();
        }
        #endregion

        #region Properties
        public string IdentifikacioniBroj { get => identifikacioniBroj; }
        public string Ime { get => ime; set => ime = value; }
        public string Prezime { get => prezime; set => prezime = value; }
        #endregion

        #region Metode
        public bool VjerodostojnostGlasaca(IProvjera sigurnosnaProvjera)
        {
            if (sigurnosnaProvjera.DaLiJeVecGlasao(identifikacioniBroj))
                throw new Exception("Glasač je već izvršio glasanje!");
            return true;
        }
        public void GlasajZaStranku(Stranka stranka, List<Kandidat> kandidati)
        {
            bool imaPravoGlasa = glasaoZaNezavisnogKandidata == false && glasaoZaStranku == false;
            if (!imaPravoGlasa)
            {
                throw new Exception("Već ste glasali!");
            }
            if (kandidati.Count() != 0)
            {
                foreach (Kandidat k in kandidati)
                {
                    if (!stranka.Kandidati.Contains(k))
                    {
                        throw new Exception("Morate glasati za kandidate iz izabrane stranke!");
                    }
                    
                }
                foreach (Kandidat k in kandidati)
                {
                    stranka.Kandidati[stranka.Kandidati.IndexOf(k)].PovecajBrojGlasova();
                    stranackiKandidati.Add(k);
                }
            }
            glasaoZaStranku = true;
            this.stranka = stranka;
            stranka.PovecajGlasove();
        }
        public void GlasajZaNezavisnogKandidata(Kandidat nezavisniKandidat)
        {
            bool imaPravoGlasa = glasaoZaNezavisnogKandidata == false && glasaoZaStranku == false;
            if (!imaPravoGlasa)
            {
                throw new Exception("Već ste glasali!");
            }
            glasaoZaNezavisnogKandidata = true;
            this.nezavisniKandidat = nezavisniKandidat;
            nezavisniKandidat.PovecajBrojGlasova();
        }

        public List<Kandidat> DajSveStranackeKandidate() { return stranackiKandidati; }
        public Stranka DajStranku() { return stranka; }
        public Kandidat DajNezavisnogKandidata() { return nezavisniKandidat; }


        private void Provjeri(bool uslov, string poruka) {
            if(uslov)
            {
                throw new Exception(poruka);
            }
        }

        //Amina Pandžić
        public void ValidacijaPodataka(string ime, string prezime, string adresaStanovanja, DateTime datumRodjenja, string brojLicneKarte, string JMBG){
            Regex regex = new(@"[^-a-zA-ZčćžšđČĆŽŠĐ]");
            Regex licnaKarta = new(@"[0-9][0-9][0-9][EJKMT][0-9][0-9][0-9]");
            var trenutnaGodina = Convert.ToInt32(DateTime.Now.ToString("yyyy"));

            bool uslov1 = ime == "" || prezime == "" || adresaStanovanja == "";
            string poruka1 = "Niste unijeli ime, prezime ili adresu stanovanja!";

            bool uslov2 = regex.IsMatch(ime) || regex.IsMatch(prezime);
            string poruka2 = "Ime i prezime smiju sadržavati samo slova i crticu!";

            bool uslov3 = ime.Length < 2 || ime.Length > 40;
            string poruka3 = "Ime se sastoji od minimalno 2, a maksimalno 40 slova!";

            bool uslov4 = prezime.Length < 3 || prezime.Length > 50;
            string poruka4 = "Prezime se sastoji od minimalno 3, a maksimalno 50 slova!";

            bool uslov5 = datumRodjenja > DateTime.Now;
            string poruka5 = "Datum rođenja je u budućnosti!";

            bool uslov6 = trenutnaGodina - Convert.ToInt32(datumRodjenja.ToString("yyyy")) < 18;
            string poruka6 = "Glasač mora biti punoljetan!";

            bool uslov7 = !licnaKarta.IsMatch(brojLicneKarte) || brojLicneKarte.Length != 7;
            string poruka7 = "Pogrešan unos broja lične karte!";

            bool uslov8 = JMBG.Length != 13 || string.Compare(JMBG.Substring(0, 2), datumRodjenja.ToString("dd")) != 0
                    || string.Compare(JMBG.Substring(2, 2), datumRodjenja.ToString("MM")) != 0
                    || string.Compare(JMBG.Substring(4, 3), datumRodjenja.ToString("yyy").Substring(1, 3)) != 0;
            string poruka8 = "JMBG nije validan!";

            Provjeri(uslov1, poruka1);
            Provjeri(uslov2, poruka2);
            Provjeri(uslov3, poruka3);
            Provjeri(uslov4, poruka4);
            Provjeri(uslov5, poruka5);
            Provjeri(uslov6, poruka6);
            Provjeri(uslov7, poruka7);
            Provjeri(uslov8, poruka8);
        }
        #endregion
    }
}