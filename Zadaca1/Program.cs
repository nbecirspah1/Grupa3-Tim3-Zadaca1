using System;
using System.Collections.Generic;
using System.Linq;

namespace Zadaca1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Stranka> stranke = new();
            Stranka stranka1 = new("SDA", "Izetbegović za predsjednika!", new List<Kandidat>());
            stranka1.DodajKandidata(new Kandidat("Bakir", "Izetbegović", "Adresa1", new DateTime(1955, 1, 1), "111E111", "0101955111111", stranka1));
            stranka1.DodajKandidata(new Kandidat("Sebija", "Izetbegović", "Adresa2", new DateTime(1955, 1, 2), "222E222", "0201955222222", stranka1));
            stranka1.DodajKandidata(new Kandidat("Šefik", "Džaferagić", "Adresa3", new DateTime(1955, 1, 3), "333E333", "0301955333333", stranka1));
            stranke.Add(stranka1);

            Stranka stranka2 = new("SNSD", "Krišto za predsjednicu!", new List<Kandidat>());
            stranka2.DodajKandidata(new Kandidat("Milorad", "Dodik", "Adresa4", new DateTime(1955, 1, 4), "444E444", "0401955444444", stranka2));
            stranka2.DodajKandidata(new Kandidat("Željka", "Cvijanović", "Adresa5", new DateTime(1955, 1, 5), "555E555", "0501955555555", stranka2));
            stranka2.DodajKandidata(new Kandidat("Dragutin", "Dragutinić", "Adresa6", new DateTime(1955, 1, 6), "666E666", "0601955666666", stranka2));
            stranke.Add(stranka2);

            List<Kandidat> nezavisniKandidati = new()
            {
                new Kandidat("Meho", "Mehić", "Adresa7", new DateTime(1955, 1, 7), "777E777", "0701955777777"),
                new Kandidat("Huse", "Husić", "Adresa8", new DateTime(1955, 1, 8), "888E888", "0801955888888"),
                new Kandidat("Neko", "Nekić", "Adresa9", new DateTime(1955, 1, 9), "999E999", "0901955999999")
            };

            Izbori izbori = new(stranke, nezavisniKandidati, 100);

            for (; ; )
            {
                Console.WriteLine("Da li ste glasac ili administrator (pritisnite 0 za kraj programa):\n1. Glasac\n2. Administrator\n");
                int role = Convert.ToInt32(Console.ReadLine());
                if (role == 1)
                {
                    for (; ; )
                    {
                        Console.WriteLine("Unesite broj koji odgovara opciji:\n" +
                            "1. Glasaj\n" +
                            "2. Prikaži trenutno stanje izbora\n" +
                            "3. Odustani\n");
                        int broj = 0;
                        broj = Convert.ToInt32(Console.ReadLine());
                        if (broj == 1)
                        {
                            Console.WriteLine("Izabrali ste opciju glasanja.\nMolimo vas da popunite slijedeće podatke.");
                            Console.WriteLine("\nIme:");
                            string ime = Console.ReadLine();
                            Console.WriteLine("\nPrezime:");
                            string prezime = Console.ReadLine();
                            Console.WriteLine("\nAdresa stanovanja:");
                            string adresaStanovanja = Console.ReadLine();
                            Console.WriteLine("\nDatum rođenja (mm/dd/gggg):");
                            DateTime datum = Convert.ToDateTime(Console.ReadLine());
                            Console.WriteLine("\nBroj lične karte:");
                            string brojLicneKarte = Console.ReadLine();
                            Console.WriteLine("\nJMBG:");
                            string JMBG = Console.ReadLine();

                            try
                            {
                                Glasac glasac = new(ime, prezime, adresaStanovanja, datum, brojLicneKarte, JMBG);
                                string identifikacioniBroj = glasac.IdentifikacioniBroj;
                                if (izbori.DajKonkretnogGlasaca(identifikacioniBroj) != null)
                                {
                                    Console.WriteLine("\nVeć ste glasali!\n");
                                    continue;
                                }

                                Console.WriteLine("\nUnesite broj koji odgovara opciji:\n" +
                               "1. Glasaj za stranku\n" +
                               "2. Glasaj za nezavisnog kandidata\n" +
                               "3. Odustani od glasanja\n");

                                int broj1 = 0;
                                broj1 = Convert.ToInt32(Console.ReadLine());
                                if (broj1 == 1)
                                {
                                    Console.WriteLine("Izabrali ste opciju glasanja za stranku.\nMolimo vas da izaberete jednu od ponuđenih:\n");
                                    int i = 1;
                                    List<Kandidat> kandidati = new();
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
                                        Console.WriteLine();
                                    }
                                    Console.WriteLine("\nUnesite redni broj stranke za koju želite da glasate:");
                                    int redniBrojStranke = Convert.ToInt32(Console.ReadLine());
                                    if (redniBrojStranke < 1 || redniBrojStranke > stranke.Count)
                                    {
                                        Console.WriteLine("\nOpcija ne postoji. Pokušajte ponovo!\n");
                                        continue;
                                    }
                                    Console.WriteLine("\nUnesite redne brojeve kandidata za koje želite da glasate odvojenih zarezom (izaberite 0 ako samo za stranku glasate):");
                                    string kandidatiS = Console.ReadLine();
                                    Console.WriteLine();
                                    if (kandidatiS == "0")
                                    {
                                        glasac.GlasajZaStranku(stranke[redniBrojStranke - 1], new List<Kandidat>());
                                    }
                                    else
                                    {
                                        var numbers = kandidatiS?.Split(',')?.Select(Int32.Parse)?.ToList();
                                        List<Kandidat> izabraniKandidati = new();
                                        foreach (int redniBroj in numbers)
                                        {
                                            izabraniKandidati.Add(stranke[redniBrojStranke - 1].Kandidati[redniBroj - 1]);
                                        }
                                        glasac.GlasajZaStranku(stranke[redniBrojStranke - 1], izabraniKandidati);
                                    }
                                    izbori.DodajGlasaca(glasac);
                                }
                                else if (broj1 == 2)
                                {
                                    int i = 1;
                                    Console.WriteLine("Izabrali ste opciju glasanja za nezavisnog kandidata.\nMolimo vas da izaberete jednog od ponuđenih:\n");
                                    foreach (Kandidat k in nezavisniKandidati)
                                    {
                                        Console.WriteLine("--- " + i + ". " + k.NazivKandidata + "---");
                                        i++;
                                    }
                                    Console.WriteLine("\nUnesite redni broj nezavisnog kandidata za kojeg želite da glasate:");
                                    int redniBrojNezavisnogKandidata = Convert.ToInt32(Console.ReadLine());
                                    if (redniBrojNezavisnogKandidata < 1 || redniBrojNezavisnogKandidata > nezavisniKandidati.Count)
                                    {
                                        Console.WriteLine("\nOpcija ne postoji. Pokušajte ponovo.\n");
                                        continue;
                                    }
                                    glasac.GlasajZaNezavisnogKandidata(nezavisniKandidati[redniBrojNezavisnogKandidata - 1]);
                                    izbori.DodajGlasaca(glasac);
                                    Console.WriteLine();
                                }
                                else if (broj1 == 3) break;
                                else
                                {
                                    Console.WriteLine("\nOpcija ne postoji. Pokušajte ponovo!\n");
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("\nGreška! " + e.Message + "\nPokušajte ponovo.\n");
                            }
                        }
                        else if (broj == 2)
                        {
                            Console.WriteLine(izbori.DajTrenutnoStanjeIzbornihRezultata());
                        }
                        else if (broj == 3) break;
                        else
                        {
                            Console.WriteLine("\nOpcija ne postoji. Pokušajte ponovo!\n");
                        }
                    }
                }
                else if(role == 2)
                {
                    Console.WriteLine("Pozdrav Admin. Da li želite poništiti nečiji glas:");
                    Console.WriteLine("1. DA\n2. NE\n");
                    int ponistiGlas = Convert.ToInt32(Console.ReadLine());
                    if(ponistiGlas == 1)
                    {
                        Administrator admin = new(izbori);
                        admin.PonistiGlas();
                    }
                    else if(ponistiGlas == 2)
                    {
                        Console.WriteLine("Odustali ste od poništavanja glasa.\n");
                    }
                    else
                    {
                        Console.WriteLine("\nOpcija ne postoji. Pokušajte ponovo!\n");
                    }
                }
                else if(role == 0)
                {
                    Console.WriteLine("\nGasimo sistem...");
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("\nOpcija ne postoji. Pokušajte ponovo!\n");
                }
            }
        }
    }
}
