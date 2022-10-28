using System;

namespace Zadaca1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Glasac g = new Glasac("ermin","jamak","71300","44",1912);
            Stranka s = new Stranka("sda");
            Izbori i = new Izbori();
            i.dodajGlas(g.dajJedinstveniIndetifikacijskiKod());
            i.trenutnoStanje();
        }
    }
}
