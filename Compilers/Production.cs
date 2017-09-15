using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers
{
    class Production
    {
        List<Symbol> alpha;
        List<Symbol> symbols;

        public Production(List<Symbol> alpha, List<Symbol> symbols)
        {
            this.Alpha = alpha;
            this.symbols = symbols;
        }

        internal List<Symbol> Symbols { get => symbols; set => symbols = value; }
        internal List<Symbol> Alpha { get => alpha; set => alpha = value; }
    }
}
