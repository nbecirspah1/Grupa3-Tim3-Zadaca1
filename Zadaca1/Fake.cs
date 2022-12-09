using System.Collections.Generic;

namespace Zadaca1
{
    public class Fake : IProvjera
    {
        public List<string> IDBrojevi { get; set; }
        public bool DaLiJeVecGlasao(string IDBroj)
        {
            return IDBrojevi.Contains(IDBroj);
        }
    }
}
