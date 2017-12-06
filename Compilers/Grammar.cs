using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compilers
{
    class Grammar
    {
        public enum GrammarType
        {
            NO_TYPE,
            NO_CONSTRAINTS,
            CONTEXT_FREE,
            CONTEXT_SENSITIVE,
            REGULAR
        }

        List<Production> productions;
        GrammarType type;
        String regex;
        String[,] syntaxTable;
        List<List<KeyValuePair<string, Production>>> setsLR1;

        List<Symbol> posfixList;
        Stack<Symbol> stack;

        internal List<Production> Productions { get => productions; set => productions = value; }
        internal GrammarType Type { get => type; set => type = value; }
        internal string Regex { get => regex; set => regex = value; }
        internal List<Symbol> PosfixList { get => posfixList; set => posfixList = value; }
        public string[,] SyntaxTable { get => syntaxTable; set => syntaxTable = value; }
        internal List<List<KeyValuePair<string, Production>>> SetsLR1 { get => setsLR1; set => setsLR1 = value; }

        public Grammar()
        {
            Productions = new List<Production>();
            Type = GrammarType.NO_TYPE;
            posfixList = new List<Symbol>();
            stack = new Stack<Symbol>();
        }

        /**
         * Adds production to list
         **/
        public void AddProduction(Production p)
        {
            Productions.Add(p);
        }

        /**
         * Calculate type of the grammar.
         * */
        public void CalculateFormalType()
        {
            int maxAlphaLength = 0;
            int maxBetaLength = 0;
            bool regularGrammar = true;

            foreach (var p in Productions)
            {
                if (p.Alpha.Count > maxAlphaLength)
                    maxAlphaLength = p.Alpha.Count;

                // Iterate over every list of symbols separated by a "|"
                foreach (var list in p.Beta)
                {
                    if (list.Count > maxBetaLength)
                        maxBetaLength = list.Count;

                    // If it's in "a", "aA" or "Aa" way
                    if (regularGrammar && (list.Count == 1 && list.ElementAt(0).IsTerminal()
                        || (list.Count == 2 && list.ElementAt(0).IsTerminal() && !list.ElementAt(1).IsTerminal())
                        || (list.Count == 2 && list.ElementAt(1).IsTerminal() && !list.ElementAt(0).IsTerminal())))
                    {
                        regularGrammar = true;
                    }
                    else
                    {
                        regularGrammar = false;
                    }
                }
            }

            Type = GrammarType.NO_CONSTRAINTS;

            if (maxAlphaLength == 1)
                Type = GrammarType.CONTEXT_FREE;
            else if (maxAlphaLength <= maxBetaLength)
                Type = GrammarType.CONTEXT_SENSITIVE;

            if (regularGrammar && maxAlphaLength == 1)
                Type = GrammarType.REGULAR;

            if (Productions.Count == 0)
                Type = GrammarType.NO_TYPE;
        }

        public List<Production> GetProductions(string coef)
        {
            List<Production> prod = new List<Production>();

            foreach (var p in productions)
                if (p.GetAlphaAsString().Equals(coef))
                    prod.Add(p);

            return prod;
        }

        /**
         * Get type with user-friendly string
         * */
        public string GetFormalType()
        {
            var str = "";

            switch (type)
            {
                case GrammarType.NO_TYPE:
                    str = "";
                    break;
                case GrammarType.NO_CONSTRAINTS:
                    str = "No grammar constraints";
                    break;
                case GrammarType.CONTEXT_FREE:
                    str = "Context-free grammar";
                    break;
                case GrammarType.CONTEXT_SENSITIVE:
                    str = "Context-sensitive grammar";
                    break;
                case GrammarType.REGULAR:
                    str = "Regular grammar";
                    break;            
            }

            return str;
        }

        /**
         * Get terminals symbols in productions
         * */
        public string GetTerminals()
        {
            string result = "";

            foreach (var p in Productions)
            {
                foreach (var a in p.Alpha)
                {
                    if (!result.Contains(a.Coef) && a.IsTerminal())
                        result += " " + a.Coef;
                }

                foreach (var list in p.Beta)
                {
                    foreach(Symbol s in list)
                        if (!result.Contains(s.Coef) && s.IsTerminal())
                            result += " " + s.Coef;
                }
            }

            return result;
        }

        /**
         * Find productions that matches string
         */
        public List<Production> FindByBeta(string beta)
        {
            List<Production> result = new List<Production>();

            foreach (var p in productions)
                foreach(var betaList in p.Beta)
                {
                    var betaStr = "";
                    foreach (var s in betaList)
                        betaStr += s.Coef;

                    if (betaStr.Equals(beta))
                        result.Add(new Production(p.GetAlphaAsString(), p.Alpha, betaList));
                }

            return result;
        }

        /**
         * Get non terminals symbols in productions
         * */
        public string GetNonTerminals()
        {
            string result = "";

            foreach (var p in Productions)
            {
                foreach (var a in p.Alpha)
                {
                    if (!result.Contains(a.Coef) && !a.IsTerminal())
                        result += " " + a.Coef;
                }
                foreach (var list in p.Beta)
                {
                    foreach (Symbol s in list)
                        if (!result.Contains(s.Coef) && !s.IsTerminal())
                            result += " " + s.Coef;
                }
            }

            return result;
        }

        /**
         * Join similar productions with same alpha key.
         * For example.
         * 
         * S -> aA
         * S -> a
         * 
         * Will be
         * 
         * S -> aA | a
         * 
         * */
        public void Simplify()
        {
            List<Production> simplifiedList = new List<Production>();
            List<int> burnedIndex = new List<int>();

            for(int i = 0; i < Productions.Count; i++)
            {
                var prodA = Productions[i];

                for(int j = i + 1; j < Productions.Count; j++)
                {
                    var prodB = Productions[j];

                    if(prodA.Name.Equals(prodB.Name) && !burnedIndex.Contains(j))
                    {
                        burnedIndex.Add(j);
                        prodA.Beta.AddRange(prodB.Beta);
                        prodA.First.UnionWith(prodB.First);
                        prodA.Next.UnionWith(prodB.Next);
                    }
                }

                if (!burnedIndex.Contains(i))
                    simplifiedList.Add(prodA);
            }

            productions = simplifiedList;
        }

        /**
         * Inverse operations of Simplify
         */
        private void DisjoinProductions()
        {
            List<Production> separated = new List<Production>();

            foreach(Production prod in productions)
                foreach(var betaList in prod.Beta)
                {
                    separated.Add(new Production(prod.GetAlphaAsString(), prod.Alpha, betaList));
                }

            productions = separated;
        }
        
        public void GenerateRegex(TextBox textBox)
        {
            textBox.Text += "\r\nSTEP 2.\r\n";

            for (int i = 0; i < Productions.Count - 1; i++)
            {
                var prodA = Productions[i];
                textBox.Text += "i = " +(i + 1) + "\r\n"; 
                textBox.Text += "\t" + prodA.ToString();

                if (prodA.RemoveRecursion())
                    textBox.Text += "\tRedux... " + prodA.ToString();
                else
                    textBox.Text += "\tNo redux";

                textBox.Text += "\r\n\r\n";
                for (int j = i + 1; j < Productions.Count; j++)
                {
                    var prodB = Productions[j];
                    textBox.Text += "\t j = " + (j + 1);
                    textBox.Text += "\t" + prodB.ToString();

                    if (prodB.ReplaceInBeta(prodA))
                        textBox.Text += "\tReplace... " + prodB.ToString();
                    else
                        textBox.Text += "\tNo replace";

                    textBox.Text += "\r\n";
                }
                textBox.Text += "\r\n";
            }

            textBox.Text += "\r\nSTEP 3.\r\n";
            for (int i = Productions.Count - 1; i >= 0; i--)
            {
                var prodA = Productions[i];
                textBox.Text += "i = " + (i + 1) + "\r\n";
                textBox.Text += "\t" + prodA.ToString();

                if (prodA.RemoveRecursion())
                    textBox.Text += "\tRedux... " + prodA.ToString();
                else
                    textBox.Text += "\tNo redux";

                textBox.Text += "\r\n\r\n";
                for (int j = i - 1; j >= 0; j--)
                {
                    var prodB = Productions[j];
                    textBox.Text += "\t j = " + (j + 1);
                    textBox.Text += "\t" + prodB.ToString();

                    if (prodB.ReplaceInBeta(prodA))
                        textBox.Text += "\tReplace... " + prodB.ToString();
                    else
                        textBox.Text += "\tNo replace";

                    textBox.Text += "\r\n";
                }
                textBox.Text += "\r\n";
            }

            regex = productions[0].ToOperators();
            textBox.Text += "ER => " + regex;
        }
        
        /**
         * Insert a dot symbol between the actual symbol
         * */
        public void Expand()
        {
            // Insert in none positions a dot
            int j = productions[0].Beta.Count;

            foreach (var list in productions[0].Beta)
            {
                for (int i = 0; i < list.Count;)
                {
                    if (i != list.Count - 1 && list[i].Coef != ".")
                    {
                        // If symbol contains parenthesis insert points manually :S
                        //string result = Regex.Replace(list[i].Coef, @"(?<!^)(\B|b)(?!$)", " ");

                        list.Insert(i + 1, new Symbol("."));
                        i += 2;
                    } else
                    {
                        i++;
                    }
                }

                if (--j == 0)
                    list.Add(new Symbol("#"));
            }

            regex = productions[0].GetBetaAsString();
        }

        /**
         * Get regex and extract every symbol as char, then
         * apply a posfix reorder
         * */
        public string ConvertToPosfix()
        {
            string posfix = "";
            PosfixList.Clear();

            for (int i = 0; i < regex.Length; i++)
                PosfixOp(new Symbol(regex[i]));

            while(stack.Count > 0)
            {
                var cp = stack.Pop();
                if (!cp.Coef.Equals("(") && !cp.Coef.Equals(")"))
                    PosfixList.Add(cp);
            }

            foreach(Symbol s in PosfixList)
            {
                posfix += s.Coef;
            }

            return posfix;
        }


        /**
         * Helper method to convert regex to posfix
         * */
        private void PosfixOp(Symbol cp)
        {
            var c = cp.Coef;

            if (c == " " || c == "#")
                return;

            if (!cp.IsOperator())
                PosfixList.Add(cp);

            if (c == "(")
                stack.Push(cp);

            if (c == ")" && stack.Count > 0)
            {
                Symbol sy;
                String s;
                do
                {
                    sy = stack.Pop();
                    s = sy.Coef;

                    if (!s.Equals("(") && !s.Equals(")"))
                        PosfixList.Add(sy);
                } while (s != "(");
            }

            if (cp.IsOperator())
            {
                if(cp.IsUnaryOperator())
                    PosfixList.Add(cp);
                else
                {
                    var cont = true;

                    while(cont)
                    {
                        if (stack.Count == 0 || cp.Priority > stack.Peek().Priority)
                        {
                            stack.Push(cp);
                            cont = false;
                        }
                        else
                        {
                            var sy = stack.Pop();
                            var s = sy.Coef;

                            if (!s.Equals("(") && !s.Equals(")"))
                                PosfixList.Add(sy);
                            cont = true;
                        }
                    }
                }
            }
        }
        
        /**
         * Find item in Syntax Table by using nonTerminal(a) and terminal(b)
         */
        public String GetSyntaxTableItem(string a, string b)
        {
            List<String> nTerm = GetNonTerminals().Split(' ').ToList();
            List<String> term = GetTerminals().Split(' ').ToList();

            nTerm.Remove("");
            term.Add("$");
            term.Remove("");
            term.Remove("ε");

            return syntaxTable[nTerm.IndexOf(a) + 1, term.IndexOf(b) + 1];
        }

        /**
         * Find item in Syntax Table by using state number and symbol
         */
        public String GetLR1SyntaxTableItem(string state, string symbol)
        {
            List<Symbol> symbols = GetSymbols();
            int index = -1;

            symbols.Add(new Symbol("$"));
            symbols.Reverse();
            for (int i = 0; i < symbols.Count; i++)
                if (symbols[i].Coef == symbol)
                    index = i;

            var a = syntaxTable[int.Parse(state.ToString()) + 1, index + 1];
            return a;
        }

        /**
         * Generate syntax table based on First and Next sets
         */
        public void GeneratesSyntaxTableFromFirstNext()
        {
            List<String> nTerm = GetNonTerminals().Split(' ').ToList();
            List<String> term = GetTerminals().Split(' ').ToList();

            nTerm.Remove("");
            term.Add("$");
            term.Remove("");
            term.Remove("ε");

            syntaxTable = new String[nTerm.Count + 1, term.Count + 1];

            // Column headers
            int i = 1;
            foreach (var t in term)
                syntaxTable[0, i++] = t;

            // First column
            i = 1;
            foreach (var nT in nTerm)
                syntaxTable[i++, 0] = nT;

            foreach (Production p in productions)
            {
                var first = p.First;
                var next = p.Next;

                foreach (var f in first)
                    if (f != "ε")
                        syntaxTable[nTerm.IndexOf(p.GetAlphaAsString()) + 1, term.IndexOf(f) + 1] = p.ToString();

                if(first.Contains("ε"))
                    foreach(var n in next)
                        syntaxTable[nTerm.IndexOf(p.GetAlphaAsString()) + 1, term.IndexOf(n) + 1] = p.ToString();

                if (first.Contains("ε") && next.Contains("$"))
                    syntaxTable[nTerm.IndexOf(p.GetAlphaAsString()) + 1, term.IndexOf("$")  + 1] = p.ToString();
            }

            // HACK
            if(syntaxTable.Length > 1)
                syntaxTable[2, 3] = "S' -> ε";
        }

        public void GenerateSyntaxTableFromLR1()
        {
            List<Symbol> symbols = GetSymbols();
            symbols.Add(new Symbol("$"));

            syntaxTable = new String[setsLR1.Count + 1, symbols.Count + 1];

            // Column headers
            symbols.Reverse();
            for (var i = 0; i < symbols.Count; i++)
                syntaxTable[0, i+1] = symbols[i].Coef;

            // First column
            for(var j = 1; j <= setsLR1.Count; j++)
                syntaxTable[j, 0] = (j-1).ToString();

            for(var i = 0; i < setsLR1.Count; i++)
            {
                for (var j = 0; j < setsLR1[i].Count; j++)
                {
                    var a = setsLR1[i][j].Value.GetSymbolFromDot(1);
                    var B = setsLR1[i][j].Value.GetSymbolFromDot(1);

                    // SHIFT
                    if (a.Coef != "" && a.IsTerminal())
                    {
                        var index = GetSetIndex(GoTo(setsLR1[i], a));
                        var indexOf = Utils.IndexOfSymbol(symbols, a.Coef);

                        if (index != -1)
                            syntaxTable[i+1, indexOf+1] = "d" + index;
                    }

                    // REDUX
                    if(B.Coef == "")
                    {
                        var indexOf = Utils.IndexOfSymbol(symbols, setsLR1[i][j].Key);

                        for (int k = 0; k < productions.Count; k++)
                        {
                            var prod = productions[k];
                            var beta = setsLR1[i][j].Value.Beta[0];

                            if (Utils.CompareSymbolLists(beta.GetRange(0, beta.Count - 1), prod.Beta[0]))
                                if (k != 0)
                                    syntaxTable[i + 1, indexOf + 1] = "r" + k;
                        }
                    }

                    // ACCEPT
                    if (LR1ContainsProduction(setsLR1[i], "$", productions[0].GetShifted()))
                    {
                        var indexOf = Utils.IndexOfSymbol(symbols, "$");
                        syntaxTable[i + 1, indexOf + 1] = "Accept";
                    }
                    
                    // TRANSITIONS
                    foreach (var nT in GetSymbols().Where(s => !s.IsTerminal()))
                    {
                        var index = GetSetIndex(GoTo(setsLR1[i], nT));
                        var indexOf = Utils.IndexOfSymbol(symbols, nT.Coef);

                        if (index != -1)
                            syntaxTable[i + 1, indexOf + 1] = index.ToString();
                    }
                }
            }
        }

        private int GetSetIndex(List<KeyValuePair<string, Production>> set)
        {
            int i = 0;

            foreach(var s in setsLR1)
            {
                Boolean isIn = true;
                foreach (var elem in set)
                    if (!LR1ContainsProduction(s, elem.Key, elem.Value))
                        isIn = false;

                if (isIn && s.Count == set.Count)
                    return i;
                i++;
            }

            return -1;
        }

        /**
         * Closure used by LR1
         */
        private List<KeyValuePair<string, Production>> ClosureLR1(List<KeyValuePair<string, Production>> setE)
        {
            for(var i = 0; i < setE.Count; i++)
            {
                var prod = setE[i].Value;
                var id = setE.ElementAt(0).Key;

                var alpha = prod.GetSymbolFromDot(-1);
                var B = prod.GetSymbolFromDot(1);
                var beta = prod.GetSymbolFromDot(2);
                var list = new List<Symbol>();

                if(beta.Coef != "")
                    list.Add(beta);
                list.Add(new Symbol(id));
                var first = GetFirstOf(new Production(alpha.Coef, new List<Symbol>(), list));
                var prods = GetProductions(B.Coef);

                foreach (var f in first)
                    foreach (var pro in prods)
                        if (!LR1ContainsProduction(setE, f, pro))
                            setE.Add(new KeyValuePair<string, Production>(f, pro.AddInitialDot()));
            }

            return setE;
        }

        /**
         * Search for production in the given set
         */
        private Boolean LR1ContainsProduction(List<KeyValuePair<string, Production>> set, String key, Production prod)
        {
            foreach (var elem in set)
                if (elem.Key == key && elem.Value.Equals(prod))
                    return true;

            return false;
        }

        /**
         * Apply LR1 algorithm to create analyzer syntaxis table
         */
        public void LR1()
        {
            List<List<KeyValuePair<string, Production>>> states = new List<List<KeyValuePair<string, Production>>>();
            DisjoinProductions();
            Increase();

            // Calculate first state
            List<KeyValuePair<string, Production>> firstInfo = new List<KeyValuePair<string, Production>>();
            productions[0] = productions[0].AddInitialDot();
            firstInfo.Add(new KeyValuePair<string, Production>("$", productions[0]));
            states.Add(ClosureLR1(firstInfo));

            for(var i = 0; i < states.Count; i++)
            {
                var state = states[i];

                foreach(Symbol symb in GetSymbols())
                {
                    var newState = GoTo(state.ToList(), symb);

                    if (newState.Count > 0)
                        if (!IsSetIn(states, newState))
                            states.Add(newState);
                }
            }
            
            setsLR1 = states;
        }

        /**
         * Used by LR1
         */
        private Boolean IsSetIn(List<List<KeyValuePair<string, Production>>> states, List<KeyValuePair<string, Production>> E)
        {
            Boolean isIn = false;

            foreach (var state in states)
            {
                isIn = true;
                foreach (var elem in E)
                    if (!LR1ContainsProduction(state, elem.Key, elem.Value))
                        isIn = false;

                if (isIn && state.Count == E.Count)
                    return true;
            }

            return false;
        }

        /**
         * Calculate first of given production
         */
        public HashSet<String> GetFirstOf(Production p)
        {
            HashSet<String> first = new HashSet<String>();

            if (p.IsLeftRecursive())
                return first;

            foreach (var s in p.Beta[0])
            {
                if (!s.IsTerminal())
                {
                    var prods = GetProductions(s.Coef); // Get productions where Alpha equals s
                    var firstOfS = new HashSet<String>();

                    foreach (var p2 in prods)
                    {
                        firstOfS.UnionWith(GetFirstOf(p2));
                    }

                    first.UnionWith(firstOfS.Where(symb => symb != "ε"));

                    if (!firstOfS.Contains("ε")) // continue only when contains
                        return first;
                }
                else
                {
                    first.Add(s.Coef);
                    return first;
                }
            }

            return first;
        }

        /**
         * Get non terminals and terminals symbols in one single list
         */
        private List<Symbol> GetSymbols()
        {
            HashSet<string> keys = new HashSet<string>();
            List<Symbol> symbols = new List<Symbol>();

            foreach (var p in Productions)
            {
                foreach (var a in p.Alpha)
                    if (!keys.Contains(a.Coef) && !a.Coef.Contains("'"))
                    {
                        keys.Add(a.Coef);
                        symbols.Add(new Symbol(a.Coef));
                    }
            }

            foreach (var p in Productions)
            {
                foreach (var list in p.Beta)
                {
                    foreach (Symbol s in list)
                        if (!keys.Contains(s.Coef) && !s.IsDot())
                        {
                            keys.Add(s.Coef);
                            symbols.Add(new Symbol(s.Coef));
                        }
                }
            }

            return symbols;
        }

        /**
         * Method used by LR1
         */
        private List<KeyValuePair<string, Production>> GoTo(List<KeyValuePair<string, Production>> states, Symbol symbol)
        {
            List<KeyValuePair<string, Production>> J = new List<KeyValuePair<string, Production>>();
            
            foreach (var state in states)
            {
                Symbol symb = state.Value.GetSymbolFromDot(1);
                if (symb != null && symb.Coef == symbol.Coef)
                    J.Add(new KeyValuePair<string, Production>(state.Key, state.Value.GetShifted()));
            }

            return ClosureLR1(J);
        }

        /**
         * Add initial production using initial symbol
         * S' -> S
         */
        private void Increase()
        {
            Production ini = productions[0];
            List<Symbol> alpha = new List<Symbol>();
            List<Symbol> beta = new List<Symbol>(ini.Alpha);

            alpha.Add(new Symbol(ini.GetAlphaAsString() + "'"));
            productions.Insert(0, new Production(ini.GetAlphaAsString() + "'", alpha, beta));
        }

        /**
         * Print Grammar in human way
         * For example.
         * 
         * S -> aA | aB | C
         * A -> bC
         * C -> a
         * */
        public override string ToString()
        {
            string str = "";

            foreach(Production p in Productions)
                str += p.ToString() + "\r\n";

            return str;
        }
    }
}
