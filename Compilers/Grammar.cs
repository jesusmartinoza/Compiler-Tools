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

        // TODO: Correct this
        String postfix = "";
        Stack<CharPrior> stack = new Stack<CharPrior>();

        internal List<Production> Productions { get => productions; set => productions = value; }
        internal GrammarType Type { get => type; set => type = value; }
        internal string Regex { get => regex; set => regex = value; }

        public Grammar()
        {
            Productions = new List<Production>();
            Type = GrammarType.NO_TYPE;
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
                    }
                }

                if (!burnedIndex.Contains(i))
                    simplifiedList.Add(prodA);
            }

            productions = simplifiedList;
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

        public string ConvertToPostfix()
        {
            string aux = productions[0].GetBetaAsString();
            postfix = "";

            for (int i = 0; i < aux.Length; i++)
                PostfixOp(new CharPrior(aux[i]));

            while(stack.Count > 0)
            {
                var cp = stack.Pop();
                if (cp.c != '(' && cp.c != ')')
                    postfix += cp.c;
            }

            return postfix;
        }

        private void PostfixOp(CharPrior cp)
        {
            var c = cp.c;

            if (c == ' ' || c == '#')
                return;

            if (c != '(' && c != ')' && c != '|' && c != '.' && c != '+' && c != '*')
                postfix += c;

            if (c == '(')
                stack.Push(cp);

            if (c == ')' && stack.Count > 0)
            {
                char s;
                do
                {
                    s = stack.Pop().c;
                    if (s != '(' && s != ')')
                        postfix += s;
                } while (s != '(');
            }

            if (c == '(' || c == ')' || c == '|' || c == '.' || c == '+' || c == '*')
            {
                if(c == '+' || c == '*')
                    postfix += c;
                else
                {
                    var cont = true;

                    while(cont)
                    {
                        if (stack.Count == 0 || cp.priority > stack.Peek().priority)
                        {
                            stack.Push(cp);
                            cont = false;
                        }
                        else
                        {
                            var s = stack.Pop().c;

                            if (s != '(' && s != ')')
                                postfix += s;
                            cont = true;
                        }
                    }
                }
            }
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

    public class CharPrior
    {
        public char c;
        public int priority;

        public CharPrior(char c)
        {
            this.c = c;
            switch(c)
            {
                case '(':
                    priority = 0;
                    break;
                case ')':
                    priority = 1;
                    break;
                case '|':
                    priority = 2;
                    break;
                case '.':
                    priority = 3;
                    break;
                case '+':
                case '*':
                    priority = 4;
                    break;
            }
        }
    }
}
