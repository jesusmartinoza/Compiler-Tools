using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers
{
    class Grammar
    {
        List<Production> productions;

        public Grammar()
        {
            productions = new List<Production>();
        }

        /**
         * Adds production to list
         **/
        public void AddProduction(Production p)
        {
            productions.Add(p);
        }

        public string GetFormalType()
        {
            string type = "No grammar constraints";
            int maxAlphaLength = 0;
            int maxBetaLength = 0;
            bool regularGrammar = true;

            foreach (var p in productions)
            {
                if (p.Alpha.Count > maxAlphaLength)
                    maxAlphaLength = p.Alpha.Count;

                if (p.Symbols.Count > maxBetaLength)
                    maxBetaLength = p.Alpha.Count;

                // If it is Terminal pr nonTerminal-Terminal
                if (regularGrammar && (p.Symbols.Count == 1 && p.Symbols.ElementAt(0).IsTerminal())
                    || (p.Symbols.Count == 2 && !p.Symbols.ElementAt(1).IsTerminal()) )
                {
                    regularGrammar = true;
                } else {
                    regularGrammar = false;
                }
            }

            if (maxAlphaLength == 1)
                type = "Context-free grammar";
            else if (maxAlphaLength <= maxBetaLength)
                type = "Context-sensitive grammar";

            if (regularGrammar && maxAlphaLength == 1)
                type = "Regular grammar";

            return type;
        }

        public string GetTerminals()
        {
            string result = "";

            foreach (var p in productions)
            {
                foreach (var a in p.Alpha)
                {
                    if (!result.Contains(a.Coef) && a.IsTerminal())
                        result += " " + a.Coef;
                }

                foreach (var s in p.Symbols)
                {
                    if (!result.Contains(s.Coef) && s.IsTerminal())
                        result += " " + s.Coef;
                }
            }

            return result;
        }

        public string GetNonTerminals()
        {
            string result = "";

            foreach (var p in productions)
            {
                foreach (var a in p.Alpha)
                {
                    if (!result.Contains(a.Coef) && !a.IsTerminal())
                        result += " " + a.Coef;
                }
                foreach (var s in p.Symbols)
                {
                    if (!result.Contains(s.Coef) && !s.IsTerminal())
                        result += " " + s.Coef;
                }
            }

            return result;
        }
    }
}
