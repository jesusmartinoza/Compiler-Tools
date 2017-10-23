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
using Shields.GraphViz.Models;
using System.IO;
using System.Threading;
using Shields.GraphViz.Services;
using Shields.GraphViz.Components;
using System.Collections.Immutable;

namespace Compilers
{
    public partial class MainForm : MaterialForm
    {
        private Grammar grammar;
        private GraphCreator graphCreator;
        Dictionary<String, String> dicFirst = new Dictionary<String, String>();
        Dictionary<String, String> dicNext = new Dictionary<String, String>();

        public MainForm()
        {
            InitializeComponent();
            CenterToScreen();

            grammar = new Grammar();
            graphCreator = new GraphCreator();

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
                        MessageBox.Show("An epsilon has been detected on the left side of the production... "
                            + str
                            + ". \nIt has been removed from grammar to avoid strange behavior." );
                    } else {
                        string symbolsStr = str.Substring(str.LastIndexOf("-") + 2).Trim(); // find first '-' and get string after 2 char
                        string[] tokens = symbolsStr.Split('|');
                        List<Symbol> alpha = ObtainSymbols(name);

                        foreach (var t in tokens)
                        {
                            List<Symbol> symbols = ObtainSymbols(t);
                            grammar.AddProduction(new Production(name, alpha, symbols));
                        }
                    }
                }
            }

            grammar.CalculateFormalType();
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
            {
                if(c == '\'')
                {
                    var symb = symbols.Last();
                    symb.Coef += "'";
                } else if(c != ' ')
                {
                    symbols.Add(new Symbol(c.ToString()));
                }

            }
            
            return symbols;
        }

        /**
         * Insert epsilon symbol on textBox grammar
         * */
        private void InsertarEpsilon_click(object sender, EventArgs e)
        {
            tbGrammar.AppendText("ε");
        }

        /**
         * Create Regex from regular grammar.
         * First simplify grammar.
         * Then iterate looking for productions in way Xi = αiXi | Ψ
         * 
         * ..... to be continued :P
         * 
         * */
        private void btnRegex_Click(object sender, EventArgs e)
        {
            OnBtnProductions_Click(sender, e);
            if (grammar.Type != Grammar.GrammarType.REGULAR)
                MessageBox.Show("Grammar must be REGULAR type");
            else
            {
                textBoxRegexLog.Text = "ORIGINAL GRAMMAR\r\n";
                textBoxRegexLog.Text += grammar.ToString() + "\r\n";

                textBoxRegexLog.Text += "STEP 1.\r\n";
                grammar.Simplify();
                textBoxRegexLog.Text += grammar.ToString();

                grammar.GenerateRegex(textBoxRegexLog);

                textBoxRegexLog.Text += "\r\n\r\n*************************\r\n";
                textBoxRegexLog.Text += "EXAM START HERE\r\n";
                textBoxRegexLog.Text += "*************************\r\n";
                textBoxRegexLog.Text += "Expresion aumentada\r\n";
                grammar.Expand();
                textBoxRegexLog.Text += grammar.Regex;

                textBoxRegexLog.Text += "\r\n\r\nConvert to posfix\r\n";
                textBoxRegexLog.Text += grammar.ConvertToPosfix();
            }
        }

        private void CalculateNext()
        {
            foreach (var p in grammar.Productions)
            {
                foreach(var list in p.Beta)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        Symbol s = list[i];
                        if (!s.IsTerminal())
                        {
                            if(i + 1 < list.Count)
                            {
                                var first = dicFirst[list[i + 1].Coef];
                                dicNext.Add(p.GetAlphaAsString(), first.Replace("ε", ""));

                                if (first.Contains("ε"))
                                    break;
                            }
                        }
                    }
                }
            }

        }

        private void CalculateFirst()
        {
            listViewFirst.Items.Clear();
            dicFirst.Clear();

            foreach(var p in grammar.Productions)
            {
                if(!dicFirst.ContainsKey(p.GetAlphaAsString()))
                    dicFirst.Add(p.GetAlphaAsString(), GetFirstOf(p));
                else
                    dicFirst[p.GetAlphaAsString()] += ", " + GetFirstOf(p);
            }

            foreach(string k in dicFirst.Keys)
            {
                var listViewItem = new ListViewItem(k + "    { " + dicFirst[k] + " }");
                listViewFirst.Items.Add(listViewItem);
            }
        }

        private String GetFirstOf(Production p)
        {
            var first = "";

            if (p == null)
                return first;

            foreach (var list in p.Beta)
            {
                var firstSymb = list[0];
                if (firstSymb.IsTerminal())
                {
                    first += firstSymb.Coef;
                    break;
                }
                else
                {
                    foreach (var p2 in grammar.GetProductions(firstSymb.Coef))
                        if (!p.IsLeftRecursive())
                        {
                            var res = GetFirstOf(p2);
                            first += res == "," ? "" : res + ", ";
                        }
;                }
            }

            return first;
        }

        private async void btnAFN_Click(object sender, EventArgs e)
        {
            btnRegex_Click(sender, e);
            Object[] row = new Object[2];

            gridAFN.Rows.Clear();
            graphCreator.Symbols = grammar.PosfixList;
            graphCreator.CreateImages();

            // Draw 2 graphs per row
            int i = 0;
            foreach(var im in graphCreator.Images.Values)
            {
                row[i % 2] = im;

                if (i % 2 == 1)
                    gridAFN.Rows.Add(row);

                i++;
            }
        }

        private void btnAFD_Click(object sender, EventArgs e)
        {
            btnRegex_Click(sender, e);
            Object[] row = new Object[2];

            gridAFN.Rows.Clear();
            graphCreator.Symbols = grammar.PosfixList;
            graphCreator.CreateAFDfromAFN();

            pictureAFD.Image = graphCreator.Images.Values.Last();
        }

        private void materialTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = ((MaterialTabControl)sender).SelectedIndex;
            
            switch(index)
            {
                case 4: CalculateFirst(); /*CalculateNext(); */break;
            }
        }
    }
}
