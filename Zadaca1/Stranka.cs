using System;
using System.Collections.Generic;

namespace Zadaca1
{
    internal class Stranka
    {
        private string naziv;
        private List<Kandidat> kandidati;

        public Stranka(string naziv)
        {
            this.naziv = naziv;
        }
        public Stranka(string naziv, List<Kandidat> kandidatList)
        {
            this.naziv = naziv;
            this.kandidati = kandidatList;
        }

        public override bool Equals(object obj)
        {
            return obj is Stranka stranka &&
                   naziv == stranka.naziv;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(naziv, kandidati);
        }
    }
}
