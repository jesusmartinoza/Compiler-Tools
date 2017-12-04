using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers
{
    static class Utils
    {
        /**
         * Get a substring before certain string.
         * Code by Fredou. 
         * https://stackoverflow.com/questions/1857513/get-substring-everything-before-certain-char
         * 
         * @return Sustring
         * */
        public static string GetUntilOrEmpty(this string text, string stopAt = "=")
        {
            if (!String.IsNullOrWhiteSpace(text))
            {
                int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    return text.Substring(0, charLocation);
                }
            }

            return String.Empty;
        }

        /**
         * Compare lists item by item
         */
        public static Boolean CompareSymbolLists(List<Symbol> A, List<Symbol>B)
        {
            Boolean equals = A.Count == B.Count;

            if(equals)
            {
                for (int i = 0; i < A.Count; i++)
                    if (A[i].Coef != B[i].Coef)
                        return false;
            }

            return equals;
        }

        /**
         * Find index of item in given list
         */
        public static int IndexOfSymbol(List<Symbol> symbols, String symbol)
        {
            for (int i = 0; i < symbols.Count; i++)
                if (symbols[i].Coef == symbol)
                    return i;

            return -1;
        }
    }
}
