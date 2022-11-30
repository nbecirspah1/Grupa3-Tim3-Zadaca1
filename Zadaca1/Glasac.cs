﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
        }
        #endregion

        #region Properties
        public string IdentifikacioniBroj { get => identifikacioniBroj; }
        #endregion

        #region Metode
        public void glasajZaStranku(Stranka stranka, List<Kandidat> kandidati)
        {
            glasaoZaStranku = glasaoZaNezavisnogKandidata == false && glasaoZaStranku == false;
            if (!glasaoZaStranku)
            {
                throw new Exception("Već ste glasali!");
            }
            stranka.povecajGlasove();
            if (kandidati.Count() != 0)
            {
                foreach (Kandidat k in kandidati)
                {
                    if (!stranka.Kandidati.Contains(k))
                    {
                        throw new Exception("Morate glasati za kandidate iz izabrane stranke!");
                    }
                    stranka.Kandidati[stranka.Kandidati.IndexOf(k)].povecajBrojGlasova();
                }
            }
        }
        public void glasajZaNezavisnogKandidata(Kandidat nezavisniKandidat)
        {
            glasaoZaNezavisnogKandidata = glasaoZaNezavisnogKandidata == false && glasaoZaStranku == false;
            if (!glasaoZaNezavisnogKandidata)
            {
                throw new Exception("Već ste glasali!");
            }
            nezavisniKandidat.povecajBrojGlasova();
        }

        public void ValidacijaPodataka(string ime, string prezime, string adresaStanovanja, DateTime datumRodjenja, string brojLicneKarte, string JMBG){
            Regex regex = new(@"[^-a-zA-ZčćžšđČĆŽŠĐ]");
            Regex licnaKarta = new(@"[0-9][0-9][0-9][EJKMT][0-9][0-9][0-9]");
            var trenutnaGodina = Convert.ToInt32(DateTime.Now.ToString("yyyy"));

            if(ime == "" || prezime == "" || adresaStanovanja == ""){ 
                throw new Exception("Niste unijeli ime, prezime ili adresu stanovanja!");
            }
            else if(regex.IsMatch(ime) || regex.IsMatch(prezime)){
                throw new Exception("Ime i prezime smiju sadržavati samo slova i crticu!");
            }
            else if(ime.Length < 2 || ime.Length > 40){
                throw new Exception("Ime se sastoji od minimalno 2, a maksimalno 40 slova!");
            }
            else if(prezime.Length < 3 || prezime.Length > 50){
                throw new Exception("Prezime se sastoji od minimalno 3, a maksimalno 50 slova!");
            }
            else if(datumRodjenja > DateTime.Now){
                throw new Exception("Datum rođenja je u budućnosti!");
            }
            else if(trenutnaGodina - Convert.ToInt32(datumRodjenja.ToString("yyyy")) < 18){
                throw new Exception("Glasač mora biti punoljetan!");
            }
            else if(!licnaKarta.IsMatch(brojLicneKarte) || brojLicneKarte.Length != 7){ 
                throw new Exception("Pogrešan unos broja lične karte!");   
            }
            else if(JMBG.Length != 13 || string.Compare(JMBG.Substring(0,2), datumRodjenja.ToString("dd")) != 0
                    || string.Compare(JMBG.Substring(2,2), datumRodjenja.ToString("MM")) != 0
                    || string.Compare(JMBG.Substring(4,3), datumRodjenja.ToString("yyy").Substring(1,3)) != 0){
                throw new Exception("JMBG nije validan!");   
            }
        }


        #endregion
    }
}