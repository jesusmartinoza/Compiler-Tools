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
        List<List<Symbol>> beta;
        HashSet<String> first;
        HashSet<String> next;

        internal string Name { get => name; set => name = value; }
        internal List<Symbol> Alpha { get => alpha; set => alpha = value; }
        internal List<List<Symbol>> Beta { get => beta; set => beta = value; }
        internal HashSet<String> Next { get => next; set => next = value; }
        internal HashSet<String> First { get => first; set => first = value; }

        public Production(string name, List<Symbol> alpha, List<Symbol> beta)
        {
            this.name = name;
            this.alpha = alpha;
            this.beta = new List<List<Symbol>>();
            this.beta.Add(beta);
            this.first = new HashSet<String>();
            this.next = new HashSet<String>();
        }

        /**
         * Search for a symbol with the same prod.name and replace it.
         * */
        public Boolean ReplaceInBeta(Production prod)
        {
            var selectedList = new List<Symbol>();
            var replaced = false;

            foreach(var list in beta)
            {
                for(var i = list.Count - 1; i >= 0 && !replaced; i--)
                {
                    var symbol = list[i];
                    if (symbol.Coef.Equals(prod.name))
                    {
                        selectedList = list;
                        replaced = true;
                        list.RemoveAt(i);
                    }
                }
            }

            if(replaced)
            {
                var added = false;
                var index = beta.IndexOf(selectedList);

                // If prod has more than one item replace the first one in the 
                // selected list and the rest add to main beta :)
                foreach (var prodList in prod.Beta)
                {
                    prodList.InsertRange(0, selectedList);

                    if (!added)
                    {
                        beta[index] = prodList;
                        added = true;
                    }
                    else
                    {
                        beta.Insert(index + 1, prodList);
                    }
                }
            }

            return replaced;
        }

        /**
         * Remove recursion changing the production
         * From
         *     X = αX | Ψ 
         * To
         *     X = {α} Ψ
         *  
         *  Where
         *      - X: Name of production
         *      - α: symbols before X
         *      - Ψ: Others symbols after X
         *  
         **/
        public Boolean RemoveRecursion()
        {
            List<Symbol> beforeX  = new List<Symbol>(); // Items before X
            List<List<Symbol>> newBeta = new List<List<Symbol>>();
            var recursionFound = false;
            var listPos = 0;

            // Find beta before X
            for(listPos = 0; listPos < beta.Count && !recursionFound; listPos++)
            {
                var list = beta[listPos];
                for (int j = 0; j < list.Count && !recursionFound; j++)
                {
                    var symbol = list[j];

                    if(symbol.Coef.Equals(name))
                        recursionFound = true;
                    else
                        beforeX.Add(symbol);
                }
            }

            // Cover alpha in brackets and append to every other symbol list.
            if(recursionFound)
            {
                var coef = "{";
                listPos--;

                foreach (var s in beforeX)
                    coef += s.Coef;

                coef += "}";

                foreach(var list in beta)
                {
                    if (beta.IndexOf(list) != listPos)
                    {
                        list.Insert(0, new Symbol(coef));
                        newBeta.Add(list);
                    }
                }
                
                beta = newBeta;
            }

            return recursionFound;
        }

        public string GetAlphaAsString()
        {
            string str = "";

            foreach (Symbol a in alpha)
                str += a.Coef;

            return str;
        }

        /**
         * Return if first symbol of first list equals to Alpha
         */
        public Boolean IsLeftRecursive()
        {
            return GetAlphaAsString() == Beta[0][0].Coef;
        }


        /**
         * Print first list separated by commas
         */
        public string GetFirstAsString()
        {
            string str = "";

            int i = first.Count;
            foreach (var s in first)
            {
                str += s;
                if (i-- > 1)
                    str += "  ";
            }

            return str;
        }

        /**
         * Print next list separated by commas
         */
        public string GetNextAsString()
        {
            string str = "";

            int i = next.Count;
            foreach (var s in next)
            {
                str += s;
                if (i-- > 1)
                    str += "  ";
            }

            return str;
        }

        /**
         * Print beta list in human way.
         * For example.
         * 
         * aA | aB | C
         */
        public string GetBetaAsString()
        {
            string str = "";

            int i = beta.Count;
            foreach (var list in beta)
            {
                foreach (Symbol s in list)
                {
                    str += s.Coef;
                }

                // Avoid print "or" symbol in last list
                if (i-- > 1)
                    str += " | ";
            }

            return str;
        }

        public string ToOperators()
        {
            // Try to convert every symbol to operator
            foreach(var list in beta)
                foreach(var s in list)
                    s.ConvertToOperator();

            // Convert aa* to a+
            // VERY VERY DIRTY IMPLEMENTATION. ITS ALL in ONE HOUR TEST.
            Symbol prevSymb = new Symbol("");

            foreach (var list in beta)
            {
                List<Symbol> deleteIndex = new List<Symbol>();
                foreach (var s in list)
                {
                    string cleanCoef = s.GetCleanCoef();

                    if (prevSymb.Coef.Contains(cleanCoef) && s.Coef.Contains("*"))
                    {
                        s.Coef = s.Coef.Remove(s.Coef.Length - 1);
                        s.Coef += "+";
                        deleteIndex.Add(prevSymb);
                    }
                    prevSymb = s;
                }

                foreach(var s in deleteIndex)
                {
                    list.Remove(s);
                }
            }

            return GetBetaAsString();
        }

        /**
         * Print Production in human way.
         * For example.
         * 
         * S -> aA | aB | C
         * */
        public override string ToString()
        {
            return GetAlphaAsString() + " -> " + GetBetaAsString();
        }
    }
}
