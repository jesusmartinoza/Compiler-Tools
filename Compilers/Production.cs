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
        List<Symbol> alphaList;
        List<List<Symbol>> betaList;

        internal string Name { get => name; set => name = value; }
        internal List<Symbol> AlphaList { get => alphaList; set => alphaList = value; }
        internal List<List<Symbol>> BetaList { get => betaList; set => betaList = value; }

        public Production(string name, List<Symbol> alphaList, List<Symbol> betaList)
        {
            this.name = name;
            this.alphaList = alphaList;
            this.betaList = new List<List<Symbol>>();
            this.betaList.Add(betaList);
        }

        public Boolean ReplaceInBeta(Production prod)
        {
            var replaced = false;

            foreach(var list in betaList)
            {
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
            List<List<Symbol>> newBetaList = new List<List<Symbol>>();
            var recursionFound = false;
            var listPos = 0;

            // Find betaList before X
            for(listPos = 0; listPos < betaList.Count && !recursionFound; listPos++)
            {
                var list = betaList[listPos];
                for (int j = 0; j < list.Count && !recursionFound; j++)
                {
                    var symbol = list[j];

                    if(symbol.Coef.Equals(name))
                        recursionFound = true;
                    else
                        beforeX.Add(symbol);
                }
            }

            // Cover alphaList in brackets and append to every other symbol list.
            if(beforeX.Count > 0)
            {
                var coef = "{";
                listPos--;

                foreach (var s in beforeX)
                    coef += s.Coef;

                coef += "}";

                foreach(var list in betaList)
                {
                    if (betaList.IndexOf(list) != listPos)
                    {
                        list.Insert(0, new Symbol(coef));
                        newBetaList.Add(list);
                    }
                }
                
                betaList = newBetaList;
            }

            return recursionFound;
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

            foreach(Symbol a in alphaList)
                str += a.Coef;

            str += " -> ";

            int i = betaList.Count;
            foreach (var list in betaList)
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
