using System;


namespace Zadaca1
{
    internal class Kandidat
    {
        private string ime, prezime;
        private Stranka stranka;

        public Kandidat(string ime, string prezime, Stranka stranka)
        {
            this.ime = ime;
            this.prezime = prezime;
            this.stranka = stranka;
        }
    }
}
