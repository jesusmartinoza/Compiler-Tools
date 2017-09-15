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
        List<List<Symbol>> symbols;

        public Production(List<Symbol> alpha, List<Symbol> symbols)
        {
            this.Alpha = alpha;
            this.symbols = new List<List<Symbol>>();
            this.symbols.Add(symbols);
        }

        /**
         * Print Production in human way.
         * */
        public override string ToString()
        {
            string str = "";

            foreach(Symbol a in alpha)
                str += a.Coef;

            str += " -> ";

            int i = symbols.Count;
            foreach (var list in symbols)
            {
                foreach(Symbol s in list)
                {
                    str += s.Coef;
                }

                // Avoid print "or" symbol in last list
                if (i-- != 0)
                    str += " | ";
            }

            return str;
        }

        internal List<List<Symbol>> Symbols { get => symbols; set => symbols = value; }
        internal List<Symbol> Alpha { get => alpha; set => alpha = value; }
    }
}
