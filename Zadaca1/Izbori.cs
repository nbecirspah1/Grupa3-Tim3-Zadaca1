using System;
using System.Collections.Generic;

namespace Zadaca1
{
    internal class Izbori
    {   
        private Dictionary<Stranka,int> stranke = new Dictionary<Stranka, int>();
        private Dictionary<Kandidat,int> kandidati = new Dictionary<Kandidat, int>();
        
        // max broj glasaca koji imaju pravo glasati
        private long brojGlasaca = 5; 
        
        private List<String> glasali = new List<string>();

        public Izbori() { }

        public void dodajStranku(Stranka stranka)
        {
            if(!stranke.ContainsKey(stranka)) stranke.Add(stranka, 0);
        }

        public void dodajKandidata(Kandidat kandidat)
        {
            if (!kandidati.ContainsKey(kandidat)) kandidati.Add(kandidat, 0);
        }

        public void dodajGlas(string glasac)
        {
            if (!glasali.Contains(glasac))
            {
                glasali.Add(glasac);
                
            }
            else Console.WriteLine("Ne moze isti glasac da glasa dva puta");
        }
        public void trenutnoStanje()
        {
            Console.WriteLine("Izlaznost na izborima je " + glasali.Count * 100/ brojGlasaca + "%");
        }
    }
}
