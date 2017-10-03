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
                symbols.Add(new Symbol(c.ToString()));
            
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

        private async void btnAFN_Click(object sender, EventArgs e)
        {
            IRenderer renderer = new Renderer(@"C:\Program Files (x86)\Graphviz2.38\bin");

            Object[] row = new Object[2];

            // Draw 3 graphs per row
            for (int i = 0; i < grammar.PosfixList.Count; i++)
            {
                var s = grammar.PosfixList[i];

                if(row.Count() == 2)
                    gridPictureBox.Rows.Add(row);

                if (!s.IsOperator())
                {
                    var ep = ImmutableDictionary.CreateBuilder<Id, Id>();
                    ep.Add("label", "ε");

                    var label = ImmutableDictionary.CreateBuilder<Id, Id>();
                    ep.Add("label", s.Coef);

                    Graph graph = Graph.Directed
                        .Add(AttributeStatement.Graph.Set("rankdir", "LR"))
                        .Add(AttributeStatement.Graph.Set("labelloc", "t"))
                        .Add(AttributeStatement.Graph.Set("label", "Graph " + i + " for " + s.Coef))
                        .Add(new EdgeStatement("1", "2", ep.ToImmutable()))
                        .Add(new EdgeStatement("2", "3", label.ToImmutable()))
                        .Add(new EdgeStatement("3", "4", ep.ToImmutable()));

                    using (Stream file = new MemoryStream())
                    {
                        await renderer.RunAsync(
                            graph, file,
                            RendererLayouts.Dot,
                            RendererFormats.Png,
                            CancellationToken.None);

                        Image image = Image.FromStream(file);
                        row[i % 2] = image;
                    }
                } else
                {

                    var ep = ImmutableDictionary.CreateBuilder<Id, Id>();
                    ep.Add("label", "ε");

                    var label = ImmutableDictionary.CreateBuilder<Id, Id>();
                    ep.Add("label", s.Coef);
                    Graph graph = Graph.Directed
                        .Add(AttributeStatement.Graph.Set("rankdir", "LR"))
                        .Add(AttributeStatement.Graph.Set("labelloc", "t"))
                        .Add(AttributeStatement.Graph.Set("label", "Graph " + i + " for " + s.Coef))
                        .Add(new EdgeStatement("1", "2", ep.ToImmutable()))
                        .Add(new EdgeStatement("2", "3", label.ToImmutable()))
                        .Add(new EdgeStatement("3", "2", label.ToImmutable()))
                        .Add(new EdgeStatement("3", "4", ep.ToImmutable()));

                    using (Stream file = new MemoryStream())
                    {
                        await renderer.RunAsync(
                            graph, file,
                            RendererLayouts.Dot,
                            RendererFormats.Png,
                            CancellationToken.None);

                        Image image = Image.FromStream(file);
                        row[i % 2] = image;
                    }
                }
            }
        }
    }
}
