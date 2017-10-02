using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers
{
    class Symbol
    {
        private string coef;
        private int priority;

        internal string Coef { get => coef; set => coef = value; }
        internal int Priority { get => priority; set => priority = value; }

        public Symbol(char c)
        {
            this.coef = c.ToString();
            switch (coef)
            {
                case "(":
                    Priority = 0;
                    break;
                case ")":
                    Priority = 1;
                    break;
                case "|":
                    Priority = 2;
                    break;
                case ".":
                    Priority = 3;
                    break;
                case "+":
                case "*":
                    Priority = 4;
                    break;
            }
        }

        public Symbol(string coef)
        {
            this.coef = coef;
        }

        /**
         * Return true if it's lower case or number
         * */
        public bool IsTerminal()
        {
            int n;
            
            return coef.All(char.IsLower) || Int32.TryParse(coef, out n);
        }

        /**
         * Change brackets to operator
         * For example:
         *      { a } will be a*
         * and
         *      { ab } will be (ab)*
         * */
        public void ConvertToOperator()
        {
            if(coef.Contains("{") && coef.Contains("}"))
            {
                if (coef.Length == 3)
                {
                    // Remove brackets and add *
                    coef = coef.Remove(0, 1);
                    coef = coef.Remove(1, 1);
                    coef += "*";
                }
                else
                {
                    coef = coef.Remove(0, 1);
                    coef = coef.Insert(0, "(");
                    coef = coef.Remove(coef.Length - 1, 1);
                    coef += ")*";
                }
            }
        } 

        public string GetCleanCoef()
        {
            // TMP
            string c = coef.Replace("{", "");
            c = coef.Replace("}", "");
            c = coef.Replace("(", "");
            c = coef.Replace(")", "");

            if (c.Contains("*"))
                c = c.Remove(c.Length - 1);

            return c;
        }


        public Boolean IsOperator()
        {
            return coef == "(" || coef == ")" || coef == "|" || coef == "." || coef == "+" || coef == "*";
        }

        public Boolean IsBinaryOperator()
        {
            return coef == "|" || coef == ".";
        }

        public Boolean IsUnaryOperator()
        {
            return coef == "+" || coef == "*";
        }
    }
}
