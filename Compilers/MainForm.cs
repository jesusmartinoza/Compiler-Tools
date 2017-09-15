using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;


namespace Compilers
{
    public partial class MainForm : MaterialForm
    {
        private Grammar grammar;

        public MainForm()
        {
            InitializeComponent();

            grammar = new Grammar();
            
            // Config material skin
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Red800, Primary.Red900, Primary.Red500, Accent.Teal400, TextShade.WHITE);
        }

        private void OnBtnProductions_Click(object sender, EventArgs e)
        {
            grammar = new Grammar();
            string plainGrammar = tbGrammar.Text;

            // Read line by line and obtain name and symbols
            foreach (var str in plainGrammar.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (str != null)
                {
                    string name = Utils.GetUntilOrEmpty(str, "->").Trim();

                    if(name.Contains("ε"))
                    {
                        MessageBox.Show("Se ha detectado un epsilon del lado izquierdo de la producción... "
                            + str
                            + ". \nSe ha eliminado de la gramatica para evitar comportamiento extraño." );
                    } else {
                        string symbolsStr = str.Substring(str.LastIndexOf("-") + 2).Trim(); // find first '-' and get string after 2 char
                        string[] tokens = symbolsStr.Split('|');
                        List<Symbol> alpha = ObtainSymbols(name);

                        foreach (var t in tokens)
                        {
                            List<Symbol> symbols = ObtainSymbols(t);
                            grammar.AddProduction(new Production(alpha, symbols));
                        }
                    }
                }
            }

            // Update GroupBox Result
            labelTerminal.Text = grammar.GetTerminals();
            labelNonTerminal.Text = grammar.GetNonTerminals();
            lblGrammarType.Text = grammar.GetFormalType();
        }

        /**
         * Obtains List of symbols from plain string
         * @param symbolsStr
         * @return List filled
         **/
        private List<Symbol> ObtainSymbols(String symbolsStr)
        {
            List<Symbol> symbols = new List<Symbol>();

            symbolsStr.Trim();
            foreach (var c in symbolsStr)
                symbols.Add(new Symbol(c.ToString()));

            //Regex r = new Regex(@"'\w*'");
            //MatchCollection mc = r.Matches(symbolsStr);
            //var otherSymbols = Regex.Split(symbolsStr, @"<(.+?)>");

            // First add all symbols between < >
            //foreach (Match m in mc)
            //{
            //    symbols.Add(new Symbol(m.Value));
            //}
            
            return symbols;
        }

        private void InsertarEpsilon_click(object sender, EventArgs e)
        {
            tbGrammar.AppendText("ε");
        }

        private void btnRegex_Click(object sender, EventArgs e)
        {

        }
    }
}
