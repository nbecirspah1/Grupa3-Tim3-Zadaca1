using System;

namespace Zadaca1
{
    public class Administrator
    {
        #region Atributi
        Izbori izbori;
        string tajnaSifra = "VVS20222023";
        #endregion

        #region Konstruktor
        public Administrator(Izbori izbori)
        {
            this.izbori = izbori;
        }
        #endregion

        // Tarik Đogić
        #region Metode
        private bool UnosSifre()
        {
            Console.WriteLine("\nIz sigurnosnih razloga ukucajte tajnu šifru (imate 3 pokušaja):");
            for (int i = 0; i < 3; i++)
            {
                string sifra = Console.ReadLine();
                if (sifra.Equals(tajnaSifra)) return true;
                Console.WriteLine("Ostalo vam je još " + (2 - i) + " pokušaja.");
            }
            return false;
        }
        public void PonistiGlas()
        {
            Console.WriteLine("Ukucajte indetifikacioni broj glasača čiji glas želite poništiti:");
            string identifikacioniBroj = Console.ReadLine();
            Glasac glasac = izbori.DajKonkretnogGlasaca(identifikacioniBroj);
            if (glasac != null)
            {
                bool ispravnaSifra = UnosSifre();
                if (ispravnaSifra)
                {
                    izbori.UkloniGlasaca(glasac);
                    Console.WriteLine();
                }
                else
                {
                    throw new Exception("\nNemate dopuštenje da poništite nečiji glas.\nPokušaj proboja. Gasimo sistem...");
                }
            }
            else
            {
                Console.WriteLine("\nNe postoji glasač sa tim identifikacionim brojem.\n");
            }
        }
        #endregion
    }
}
