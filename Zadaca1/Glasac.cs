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

        //Amina Pandžić
        public void ValidacijaPodataka(string ime, string prezime, string adresaStanovanja, DateTime datumRodjenja, string brojLicneKarte, string JMBG){
            Regex regex = new(@"[^-a-zA-ZčćžšđČĆŽŠĐ]");
            Regex licnaKarta = new(@"[0-9][0-9][0-9][EJKMT][0-9][0-9][0-9]");
            var trenutniDatum = DateTime.Now;
            var trenutnaGodina = Convert.ToInt32(trenutniDatum.ToString("yyyy"));
            var duzinaImena = ime.Length;
            var duzinaPrezimena = prezime.Length;

            if (ime == "" || prezime == "" || adresaStanovanja == "")
            {
                throw new Exception("Niste unijeli ime, prezime ili adresu stanovanja!");
            }
            else if (regex.IsMatch(ime) || regex.IsMatch(prezime))
            {
                throw new Exception("Ime i prezime smiju sadržavati samo slova i crticu!");
            }
            else if ( duzinaImena < 2 || duzinaImena > 40)
            {
                throw new Exception("Ime se sastoji od minimalno 2, a maksimalno 40 slova!");
            }
            else if (duzinaPrezimena < 3 || duzinaPrezimena > 50)
            {
                throw new Exception("Prezime se sastoji od minimalno 3, a maksimalno 50 slova!");
            }
            else if (datumRodjenja > trenutniDatum)
            {
                throw new Exception("Datum rođenja je u budućnosti!");
            }
            else if (trenutnaGodina - Convert.ToInt32(datumRodjenja.ToString("yyyy")) < 18)
            {
                throw new Exception("Glasač mora biti punoljetan!");
            }
            else if (!licnaKarta.IsMatch(brojLicneKarte) || brojLicneKarte.Length != 7)
            {
                throw new Exception("Pogrešan unos broja lične karte!");
            }
            else if (JMBG.Length != 13 || string.Compare(JMBG.Substring(0, 2), datumRodjenja.ToString("dd")) != 0
                    || string.Compare(JMBG.Substring(2, 2), datumRodjenja.ToString("MM")) != 0
                    || string.Compare(JMBG.Substring(4, 3), datumRodjenja.ToString("yyy").Substring(1, 3)) != 0)
            {
                throw new Exception("JMBG nije validan!");
            }
        }
        #endregion
    }
}