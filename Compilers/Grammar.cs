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

        /**
         * Get type of the grammar as string.
         * */
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

                // If it is Terminal or nonTerminal-Terminal
                foreach (var list in p.Symbols)
                {
                    if (list.Count > maxBetaLength)
                        maxBetaLength = list.Count;

                    if (regularGrammar && (list.Count == 1 && list.ElementAt(0).IsTerminal())
                        || (list.Count == 2 && !list.ElementAt(1).IsTerminal()))
                    {
                        regularGrammar = true;
                    }
                    else
                    {
                        regularGrammar = false;
                    }
                }
            }

            if (maxAlphaLength == 1)
                type = "Context-free grammar";
            else if (maxAlphaLength <= maxBetaLength)
                type = "Context-sensitive grammar";

            if (regularGrammar && maxAlphaLength == 1)
                type = "Regular grammar";

            if (productions.Count == 0)
                type = "";

            return type;
        }

        /**
         * Get terminals symbols in productions
         * */
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

                foreach (var list in p.Symbols)
                {
                    foreach(Symbol s in list)
                        if (!result.Contains(s.Coef) && s.IsTerminal())
                            result += " " + s.Coef;
                }
            }

            return result;
        }

        /**
         * Get non terminals symbols in productions
         * */
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
                foreach (var list in p.Symbols)
                {
                    foreach (Symbol s in list)
                        if (!result.Contains(s.Coef) && !s.IsTerminal())
                            result += " " + s.Coef;
                }
            }

            return result;
        }

        public void Simplify()
        {

        }
    }
}
