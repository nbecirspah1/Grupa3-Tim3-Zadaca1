﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadaca1
{
    public class Kandidat
    {
        string ime;
        string prezime;
        string informacije;
        int number_of_votes;
        //Imenovanje varijabli nije konzistentno. Potrebno je koristiti CamelCase i imenovati na bosanskom jeziku atribute.
        Stranka stranka = null;

        public Kandidat
            (string ime, string prezime, Stranka stranka = null)
        {
      this.ime = ime;
        this.prezime = prezime;
     number_of_votes = 0;      informacije = "";
            this.stranka = stranka;
        }

        public string Informacije { set => informacije = value; get => informacije;}
        public int BrojGlasova { set => number_of_votes=value; get => number_of_votes; }
        public string NazivKandidata { get => ime + " " + prezime; }
        public string Ime { set => ime=value; get => ime; }
        public string Prezime { set => prezime=value; get => prezime; }
        public string Stranka { set => stranka=value; get => stranka; }
        /*Dodani seteri i geteri za sve atribute.
         Na taj način smo popravili kvalitetu koda 
        i omogućili lakše nadogradnje.*/
        

        public override bool Equals(object obj)
        {
            return obj is Kandidat kandidat &&
                                             ime == kandidat.ime &&
                   prezime == kandidat.prezime &&
                                                  informacije == kandidat.informacije &&
                   number_of_votes == kandidat.number_of_votes && stranka == kandidat.stranka;
}


         public void povecajBrojGlasova() { number_of_votes++; }

        public override int GetHashCode(){ return HashCode.Combine(ime,prezime,informacije,number_of_votes,stranka);  }
        
    }
}
