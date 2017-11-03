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

        private Boolean CalculateNextOf(Production p)
        {
            HashSet<String> next = new HashSet<String>();
            Boolean changes = false;

            var list = p.Beta[0]; // Just first beta

            for (int i = 0; i < list.Count; i++)
            {
                Symbol s = list[i];

                next.Clear();
                if (!s.IsTerminal())
                {
                    var selectedProd = grammar.Productions.Where(pr => pr.GetAlphaAsString() == s.Coef).First();

                    if (i == list.Count - 1)
                    {
                        var siblingProds = grammar.Productions.Where(pr => pr.GetAlphaAsString() == p.GetAlphaAsString());

                        foreach (var p2 in siblingProds)
                            next.UnionWith(p2.Next);
                    } else
                    {
                        HashSet<String> first = new HashSet<String>();

                        for (int j = list.Count - 1; j > i; j--)
                        {
                            var symb = list[j];

                            if (symb.IsTerminal())
                                first.Add(symb.Coef);
                            else
                            {
                                var siblingProds = grammar.Productions.Where(pr => pr.GetAlphaAsString() == symb.Coef);

                                foreach (var p2 in siblingProds)
                                    first.UnionWith(GetFirstOf(p2));
                            }
                        }

                        if(first.Contains("ε"))
                        {

                        }
                        next.UnionWith(first.Where(symb => symb != "ε"));
                    }

                    changes = !selectedProd.Next.IsSupersetOf(next);

                    selectedProd.Next.UnionWith(next);
                }
            }

            return changes;
        }

        private HashSet<String> GetFirstOf(Production p)
        {
            HashSet<String> first = new HashSet<String>();
     
            if (p.IsLeftRecursive())
                return first;
            
            foreach (var s in p.Beta[0]) 
            {
                if (!s.IsTerminal())
                {
                    var productions = grammar.GetProductions(s.Coef); // Get productions where Alpha equals s
                    var firstOfS = new HashSet<String>();

                    foreach (var p2 in productions)
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
                case 4: // First/Next tab
                    listViewFirst.Items.Clear();
                    listViewNext.Items.Clear();

                    // Add initial symbol to first production
                    if(grammar.Productions.Count > 0)
                        grammar.Productions.First().Next.Add("$");

                    // First calculate set of first 
                    foreach (var p in grammar.Productions)
                        p.First = GetFirstOf(p);

                    Boolean recalculate = false;

                    // Calculate again if is there a change in one next set.
                    do
                    {
                        recalculate = false;
                        foreach (var p in grammar.Productions)
                        {
                            Boolean innerRec = CalculateNextOf(p);

                            if (!recalculate) recalculate = innerRec;
                        }
                    } while (recalculate);

                    grammar.Simplify();

                    foreach (var p in grammar.Productions)
                    {
                        var listViewItem = new ListViewItem(p.GetAlphaAsString() + "    {  " + p.GetFirstAsString() + "  }");
                        listViewFirst.Items.Add(listViewItem);

                        listViewItem = new ListViewItem(p.GetAlphaAsString() + "    {  " + p.GetNextAsString() + "  }");
                        listViewNext.Items.Add(listViewItem);
                    }

                    break;
            }
        }
    }
}
