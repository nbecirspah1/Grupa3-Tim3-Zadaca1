using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
public Glasac(string ime, string prezime, string adresaStanovanja, DateTime datumRodjenja, string brojLicneKarte)
{
this.ime = ime;
this.prezime = prezime;
this.adresaStanovanja = adresaStanovanja;
this.datumRodjenja = datumRodjenja;
this.brojLicneKarte = brojLicneKarte;
identifikacioniBroj = ime.Substring(0, 2) + prezime.Substring(0, 2) +
                    adresaStanovanja.Substring(0, 2) + datumRodjenja.ToString("dd.MM.yyyy.").Substring(0, 2)
                    + brojLicneKarte.Substring(0, 2);
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
if(!glasaoZaStranku)
{
throw new Exception("Već ste glasali!");
}
stranka.povecajGlasove();
if(kandidati.Count()!=0)
{
foreach (Kandidat k in kandidati)
{
if (!stranka.Kandidati.Contains(k)) {
throw new Exception("Morate glasati za kandidate iz izabrane stranke!");
}
stranka.Kandidati[stranka.Kandidati.IndexOf(k)].povecajBrojGlasova();
}
}
}
public void glasajZaNezavisnogKandidata(Kandidat nezavisniKandidat)
{
glasaoZaNezavisnogKandidata = glasaoZaNezavisnogKandidata == false && glasaoZaStranku == false;
if (glasaoZaNezavisnogKandidata)
{
throw new Exception("Već ste glasali!");
}
nezavisniKandidat.povecajBrojGlasova();
}
    #endregion
}
}