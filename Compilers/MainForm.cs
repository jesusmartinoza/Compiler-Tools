using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

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
            
            listViewFirst.Columns[0].Width = 65 - SystemInformation.VerticalScrollBarWidth;
            listViewNext.Columns[0].Width = 65 - SystemInformation.VerticalScrollBarWidth;

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

        private void FillFirstNextSyntaxTable()
        {
            String[,] table = grammar.SyntaxTable;

            // Create columns
            listViewSyntaxisTable.Columns.Add("Non Terminal", 120);
            for (int i = 1; i < table.GetLength(1); i++)
                listViewSyntaxisTable.Columns.Add(table[0, i], 100);

            // Populate table
            for (int i = 1; i < table.GetLength(0); i++)
            {
                String[] row = new String[table.GetLength(1)];

                for (int j = 0; j < table.GetLength(1); j++)
                    row[j] = table[i, j];
                
                var listViewItem = new ListViewItem(row);
                listViewSyntaxisTable.Items.Add(listViewItem);
            }
        }

        private void FillLR1Table()
        {
            String[,] table = grammar.SyntaxTable;

            // Create columns
            listViewLR1Table.Columns.Add("States", 80);
            for (int i = 1; i < table.GetLength(1); i++)
                listViewLR1Table.Columns.Add(table[0, i], 630/table.GetLength(1));

            // Populate table
            for (int i = 1; i < table.GetLength(0); i++)
            {
                String[] row = new String[table.GetLength(1)];

                for (int j = 0; j < table.GetLength(1); j++)
                    row[j] = table[i, j];

                var listViewItem = new ListViewItem(row);
                listViewLR1Table.Items.Add(listViewItem);
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
                    HashSet<String> first = new HashSet<String>();

                    // Get first of the next symbols
                    for (int j = list.Count - 1; j > i; j--)
                    {
                        var symb = list[j];

                        if (symb.IsTerminal())
                            first.Add(symb.Coef);
                        else
                        {
                            // Join FirstSet's productions with same Alpha
                            var twinsProd = grammar.Productions.Where(pr => pr.GetAlphaAsString() == symb.Coef);
                            Boolean allEpsilon = true;

                            foreach (var p2 in twinsProd)
                            {
                                var p2First = grammar.GetFirstOf(p2);

                                if (allEpsilon)
                                    allEpsilon = p2First.Contains("ε");

                                first.UnionWith(p2First);
                            }
                            
                            if (!allEpsilon)
                                first.Remove("ε");
                        }
                    }

                    if (i == list.Count - 1)
                        first.Add("ε");

                    if(first.Contains("ε"))
                    {
                        var siblingProds = grammar.Productions.Where(pr => pr.GetAlphaAsString() == p.GetAlphaAsString());

                        foreach (var p2 in siblingProds)
                            next.UnionWith(p2.Next);

                        selectedProd.Next.UnionWith(next);
                    }

                    next.UnionWith(first.Where(symb => symb != "ε"));

                    // Add next to every alpha selectedProduction
                    var similarProd = grammar.Productions.Where(pr => pr.GetAlphaAsString() == selectedProd.GetAlphaAsString());
                    foreach(var sP in similarProd)
                        sP.Next.UnionWith(next);

                    changes = !selectedProd.Next.IsSupersetOf(next);
                }
            }

            return changes;
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

        private void materialTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = ((MaterialTabControl)sender).SelectedIndex;
            
            switch(index)
            {
                case 3: // First/Next tab
                    OnBtnProductions_Click(sender, e);
                    listViewFirst.Items.Clear();
                    listViewNext.Items.Clear();
                    listViewSyntaxisTable.Clear();

                    // Add initial symbol to first production
                    if (grammar.Productions.Count > 0)
                        grammar.Productions.First().Next.Add("$");

                    // First calculate set of first 
                    foreach (var p in grammar.Productions)
                    {
                        p.First = grammar.GetFirstOf(p);
                        // Add first to every alpha selectedProduction
                        var similarProd = grammar.Productions.Where(pr => pr.GetAlphaAsString() == p.GetAlphaAsString());

                        foreach (var sP in similarProd)
                            sP.First.UnionWith(p.First);
                    }

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

                    grammar.GeneratesSyntaxTableFromFirstNext();
                    FillFirstNextSyntaxTable();
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

        private void btnTestFirstNext_Click(object sender, EventArgs e)
        {
            if(grammar.Productions.Count == 0)
            {
                MessageBox.Show("You must need to calculate FIRST/NEXT table");
                return;
            }

            String test = textBoxTestString.Text + "$";
            Stack<Symbol> stack = new Stack<Symbol>();
            int strPtr = 0;
            Boolean errorFound = false;

            stack.Push(new Symbol("$"));
            stack.Push(grammar.Productions[0].Alpha[0]);
            textBox1.Text = "";
            do
            {
                // Print stack
                var str = "";
                foreach (Symbol s in stack.Reverse())
                    str += s.Coef;
                textBox1.Text += str + "    " + test.Substring(strPtr);

                Symbol top = stack.Peek();
                char testChar = test[strPtr];

                if (top.IsTerminal() || top.Coef == "$")
                {
                    if(top.Coef == testChar.ToString())
                    {
                        stack.Pop();
                        strPtr++;
                    }
                    else
                    {
                        errorFound = true;
                        MessageBox.Show("Error en X");
                    }
                } else
                {
                    String prodStr = grammar.GetSyntaxTableItem(top.Coef, testChar.ToString());

                    if (!String.IsNullOrEmpty(prodStr))
                    {
                        String alpha = Utils.GetUntilOrEmpty(prodStr, "->").Trim();
                        String beta = prodStr.Substring(prodStr.LastIndexOf("-") + 2).Trim();
                        var prod = grammar.FindByBeta(beta).First();
                        
                        stack.Pop();

                        for (int i = prod.Beta[0].Count - 1; i >= 0; i--)
                            if(!prod.Beta[0][i].IsEpsilon())
                                stack.Push(prod.Beta[0][i]);

                        textBox1.Text += "    " + alpha + "->" + beta;
                    }
                    else
                    {
                        errorFound = true;
                        MessageBox.Show("Error al emitir produccion");
                    }
                }
                
                textBox1.Text += "\r\n";
            } while (stack.Peek().Coef != "$" && !errorFound);

            if (!errorFound)
                MessageBox.Show("Syntactically valid string!!");
        }

        private void btnCalculateLR1_Click(object sender, EventArgs e)
        {
            OnBtnProductions_Click(sender, e);
            grammar.LR1();
            grammar.GenerateSyntaxTableFromLR1();
            FillLR1Table();
        }

        private void btnTestLR1_Click(object sender, EventArgs e)
        {
            btnCalculateLR1_Click(sender, e);
            if (grammar.SyntaxTable.Length == 0)
            {
                MessageBox.Show("You must need to calculate LR1 syntaxis table");
                return;
            }

            String test = textBoxTestString.Text + "$";
            Stack<string> stack = new Stack<string>();
            int strPtr = 0;
            Boolean errorFound = false;
            Boolean finish = false;

            stack.Push("0");
            textBox1.Text = "";
            do
            {
                // Print stack
                var str = "";
                foreach (var s in stack.Reverse())
                    str += s;
                textBox1.Text += str + "    " + test.Substring(strPtr);

                var top = stack.Peek();
                var ae = test[strPtr];
                var action = grammar.GetLR1SyntaxTableItem(top, ae.ToString());

                if (action == null)
                {
                    MessageBox.Show("Invalid string using LR1");
                    return;
                }

                if (action[0] == 'd')
                {
                    stack.Push(ae.ToString());
                    // Insert number in stack
                    stack.Push(action.Substring(1));

                    strPtr++;
                    textBox1.Text += "\t Shift(" + action.Substring(1) + ")";
                } else
                {
                    if(action[0] == 'r')
                    {
                        // Get production in position action[1] ignoring S' symbol
                        Production prod = grammar.Productions[int.Parse(action[1].ToString())];
                        int n = 2 * prod.Beta[0].Count;

                        for (int i = 0; i < n; i++)
                            stack.Pop();

                        string sPrim = stack.Peek();
                        string alpha = prod.GetAlphaAsString();
                        stack.Push(alpha);
                        stack.Push(grammar.GetLR1SyntaxTableItem(sPrim, alpha.ToString()));
                        textBox1.Text += "\t" + prod.ToString();
                        textBox1.Text += "\t Redux(" + action.Substring(1) + ")";
                    } else
                    {
                        if (action == "Accept")
                            finish = true;
                        else
                        {
                            MessageBox.Show("Unkown symbol in string");
                            return;
                        }
                    }
                }
                textBox1.Text += "\r\n";
            } while (stack.Peek() != "$" && !errorFound && !finish);

            if (!errorFound)
                MessageBox.Show("Syntactically valid string!!");
        }
    }
}
