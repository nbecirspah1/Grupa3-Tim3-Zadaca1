using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private void ValidirajDuzinuStringa(string str, int minDuzina, int maxDuzina)
        {
            if (str.Length < minDuzina || str.Length > maxDuzina)
            {
                throw new Exception($"Unos se sastoji od minimalno {minDuzina}, a maksimalno {maxDuzina} slova!");
            }
        }

        //Amina Pandžić
        public void ValidacijaPodataka(string ime, string prezime, string adresaStanovanja, DateTime datumRodjenja, string brojLicneKarte, string JMBG)
        {
            Dictionary<string, string> izuzeci = new Dictionary<string, string>
            {
                {"praznaAdresa", "Niste unijeli adresu stanovanja!"},
                {"neispravniKarakteri", "Ime i prezime smiju sadržavati samo slova, brojeve i crticu!"},
                {"datumUBuducnosti", "Datum rođenja je u budućnosti!"},
                {"maloljetan", "Glasač mora biti punoljetan!"},
                {"neispravanBrojLK", "Pogrešan unos broja lične karte!"},
                {"neispravanJMBG", "JMBG nije validan!"}
            };

            Regex regex = new(@"[^-a-zA-ZčćžšđČĆŽŠĐ]");
            Regex licnaKarta = new(@"[0-9][0-9][0-9][EJKMT][0-9][0-9][0-9]");
            var trenutniDatum = DateTime.Now;
            var trenutnaGodina = Convert.ToInt32(trenutniDatum.ToString("yyyy"));

            ValidirajDuzinuStringa(ime.Trim(), 2, 40);
            ValidirajDuzinuStringa(prezime.Trim(), 3, 50);

            var sb = new StringBuilder();

            sb.Append(datumRodjenja.ToString("dd"));
            sb.Append(datumRodjenja.ToString("MM"));
            sb.Append(datumRodjenja.ToString("yyy").Substring(1, 3));

            if (adresaStanovanja == "")
            {
                throw new Exception(izuzeci["praznaAdresa"]);
            }
            else if (regex.IsMatch(ime) || regex.IsMatch(prezime))
            {
                throw new Exception(izuzeci["neispravniKarakteri"]);
            }
            else if (datumRodjenja > trenutniDatum)
            {
                throw new Exception(izuzeci["datumUBuducnosti"]);
            }
            else if (trenutnaGodina - Convert.ToInt32(datumRodjenja.ToString("yyyy")) < 18)
            {
                throw new Exception(izuzeci["maloljetan"]);
            }
            else if (!licnaKarta.IsMatch(brojLicneKarte) || brojLicneKarte.Length != 7)
            {
                throw new Exception(izuzeci["neispravanBrojLK"]);
            }
            else if (JMBG.Length != 13 || string.Compare(JMBG.Substring(0, 7), sb.ToString()) != 0)
            {
                throw new Exception(izuzeci["neispravanJMBG"]);
            }
        }
        #endregion
    }
}