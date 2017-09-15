using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers
{
    class Production
    {
        string name;
        List<Symbol> alpha;
        List<List<Symbol>> symbols;

        internal string Name { get => name; set => name = value; }
        internal List<Symbol> Alpha { get => alpha; set => alpha = value; }
        internal List<List<Symbol>> Symbols { get => symbols; set => symbols = value; }

        public Production(string name, List<Symbol> alpha, List<Symbol> symbols)
        {
            this.name = name;
            this.alpha = alpha;
            this.symbols = new List<List<Symbol>>();
            this.symbols.Add(symbols);
        }

        /**
         * Print Production in human way.
         * For example.
         * 
         * S -> aA | aB | C
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
                if (i-- > 1)
                    str += " | ";
            }

            return str;
        }
    }
}
