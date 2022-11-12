using System;
using System.Collections.Generic;
using System.Linq;

namespace Zadaca1
{
    internal class Program
    {
        static void Main(string[] args)
        {


            List<Stranka> stranke = new List<Stranka>();
            stranke.Add(new Stranka("SDA", "Izetbegović za predsjednika!",
                                   new List<Kandidat>() { new Kandidat("Bakir", "Izetbegović", false),
                                      new Kandidat("Sebija", "Izetbegović", false),
                                      new Kandidat("Šefik", "Džaferagić", false)}));
            stranke.Add(new Stranka("HDZ", "Krišto za predsjednicu!",
                                    new List<Kandidat>()  {new Kandidat("Borjana", "Krišto", false),
                                                           new Kandidat("Dragan", "Čović", false),
                                                           new Kandidat("Neko", "Nekić", false)}));

            stranke.Add(new Stranka("SNSD", "Dodik za predsjednika!",
                                new List<Kandidat>() { new Kandidat("Milorad", "Dodik", false),
                                  new Kandidat("Željka", "Cvijanović", false),
                                  new Kandidat("Dragutin", "Dragutinić", false)}));

            var k1 = new List<Kandidat>() { new Kandidat("Milorad", "Dodik", false),
                                  new Kandidat("Željka", "Cvijanović", false),
                                  new Kandidat("Dragutin", "Dragutinić", false)};

            List<Kandidat> nezKandidati = new();
            nezKandidati.Add(new("Nezavisni", "Kandidat1", true));
            nezKandidati.Add(new("Nezavisni", "Kandidat2", true));
            nezKandidati.Add(new("Nezavisni", "Kandidat3", true));



            Izbori izbori = new(stranke, k1, 100);

            List<string> glasaci = new();

            for (; ; )
            {
                Console.WriteLine("Unesite broj koji odgovara opciji: \n" +
                    "1 - Glasaj\n" +
                    "2 - Prikaži trenutno stanje izbora\n" +
                    "3 - Kraj");
                int broj = 0;
                broj = Convert.ToInt32(Console.ReadLine());
                if (broj == 1)
                {
                    Console.WriteLine("Ime:");
                    string ime = Console.ReadLine();
                    Console.WriteLine("Prezime:");
                    string prezime = Console.ReadLine();
                    Console.WriteLine("Adresa stanovanja:");
                    string adresaStanovanja = Console.ReadLine();
                    Console.WriteLine("Datum rođenja (mm/dd/gggg):");
                    DateTime datum = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Broj lične karte:");
                    string brojLicneKarte = Console.ReadLine();
                    Console.WriteLine("JMBG:");
                    string JMBG = Console.ReadLine();

                    Glasac glasac = new Glasac(ime, prezime, adresaStanovanja, datum, brojLicneKarte, JMBG);
                    if (glasaci.Contains(glasac.IdentifikacioniBroj))
                    {
                        Console.WriteLine("Već ste glasali!");
                        continue;
                    }

                    Console.WriteLine("Unesite broj koji odgovara opciji: \n" +
                   "1 - Glasaj za stranku\n" +
                   "2 - Glasaj za nezavisnog kandidata\n" +
                   "3 - Kraj glasanja");

                    int broj1 = 0;
                    broj1 = Convert.ToInt32(Console.ReadLine());
                    if (broj1 == 1)
                    {
                        int i = 1;
                        List<Kandidat> kandidati = new List<Kandidat>();
                        foreach (Stranka s in stranke)
                        {
                            Console.WriteLine("--- " + i + ". " + s.NazivStranke + "---");
                            i++;
                            kandidati = s.Kandidati;
                            int j = 1;
                            foreach (Kandidat k in kandidati)
                            {
                                Console.WriteLine(j + ". " + k.NazivKandidata);
                                j++;
                            }
                        }
                        Console.WriteLine("Unesite redni broj stranke za koju želite da glasate: ");
                        int redniBrojStranke = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Unesite redne brojeve kandidata za koje želite da glasate (unesite 0 ako samo za stranku glasate): ");
                        string kandidatiS = Console.ReadLine();
                        if (kandidatiS == "0")
                        {
                            glasac.glasajZaStranku(stranke[redniBrojStranke - 1], new List<Kandidat>());
                        }
                        else
                        {
                            var numbers = kandidatiS?.Split(',')?.Select(Int32.Parse)?.ToList();
                            List<Kandidat> izabraniKandidati = new List<Kandidat>();
                            foreach (int redniBroj in numbers)
                            {
                                izabraniKandidati.Add(stranke[redniBrojStranke - 1].Kandidati[redniBroj - 1]);
                            }
                            glasac.glasajZaStranku(stranke[redniBrojStranke - 1], izabraniKandidati);
                        }
                        glasaci.Add(glasac.IdentifikacioniBroj);
                    }
                    else if (broj1 == 2)
                    {
                        int i = 1;
                        foreach (Kandidat k in nezKandidati)
                        {
                            Console.WriteLine("--- " + i + ". " + k.NazivKandidata + "---");
                            i++;
                        }
                        Console.WriteLine("Unesite redni broj nezavisnog kandidata za kojeg želite da glasate: ");
                        int redniBrojNezavisnogKandidata = Convert.ToInt32(Console.ReadLine());
                        glasac.glasajZaNezavisnogKandidata(nezKandidati[redniBrojNezavisnogKandidata - 1]);
                        glasaci.Add(glasac.IdentifikacioniBroj);
                    }
                    else if (broj == 3) break;
                    else
                    {
                       Console.WriteLine("Neispravna opcija. Pokušajte ponovo!");
                    }
                    /*Radi veće pokrivenosti slučajeva pogrešnog unosa.*/
                }
                else if (broj == 2)
                {
                    Console.WriteLine(izbori.dajTrenutnoStanjeIzbornihRezultata());
                }
                else if (broj == 3)
                {
                    break;
                } else
                {
                    Console.WriteLine("Neispravna opcija. Pokušajte ponovo!");
                }
                /*Radi veće pokrivenosti slučajeva pogrešnog unosa.*/
            }
            for (int i=0; i<glasaci.size(); i++) {
                Console.WriteLine(glasaci[i]);
            }
             //Bespotrebna for petlja
        }
    }
}
