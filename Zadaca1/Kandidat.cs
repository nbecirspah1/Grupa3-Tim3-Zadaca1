using System;
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
        int brojGlasova;
        Stranka stranka = null;

        public Kandidat
            (string ime, string prezime, Stranka stranka = null)
        {
      this.ime = ime;
        this.prezime = prezime;
     brojGlasova = 0;      informacije = "";
            this.stranka = stranka;
        }

        public string Informacije { set => informacije = value; get => informacije;}
        public int BrojGlasova { set => brojGlasova=value; get => brojGlasova; }
        public string NazivKandidata { get => ime + " " + prezime; }
        public string Ime { set => ime=value; get => ime; }
        public string Prezime { set => prezime=value; get => prezime; }
        public Stranka Stranka { set => stranka=value; get => stranka; }
        

        public override bool Equals(object obj)
        {
            return obj is Kandidat kandidat &&
                                             ime == kandidat.ime &&
                   prezime == kandidat.prezime &&
                                                  informacije == kandidat.informacije &&
                   brojGlasova == kandidat.brojGlasova && stranka == kandidat.stranka;
}


         public void povecajBrojGlasova() { brojGlasova++; }

        public override int GetHashCode(){ return HashCode.Combine(ime,prezime,informacije,brojGlasova,stranka);  }
        
    }
}
