using System;
namespace Zadaca1
{
    internal class Glasac
    {
        private string ime, prezime, adresa, brojLicneKarte, jedinstveniIndetifikacijskiKod;
        private int maticniBroj;


        public Glasac(string ime, string prezime, string adresa, string brojLicneKarte, int maticniBroj)
        {
            this.ime = ime;
            this.prezime = prezime;
            this.adresa = adresa;
            this.brojLicneKarte = brojLicneKarte;
            this.maticniBroj = maticniBroj;

            jedinstveniIndetifikacijskiKod = ime.Substring(0, 2) + prezime.Substring(0, 2) + adresa.Substring(0, 2) + brojLicneKarte.Substring(0, 2) + maticniBroj.ToString().Substring(0, 2);
        }
        public String dajJedinstveniIndetifikacijskiKod()
        {
            return jedinstveniIndetifikacijskiKod;
        }
    }
}