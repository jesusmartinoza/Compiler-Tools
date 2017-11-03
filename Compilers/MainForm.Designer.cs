namespace Compilers
{
    partial class MainForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tbGrammar = new System.Windows.Forms.TextBox();
            this.gpGrammar = new System.Windows.Forms.GroupBox();
            this.btnInsterEpsilon = new MaterialSkin.Controls.MaterialFlatButton();
            this.btnProductions = new MaterialSkin.Controls.MaterialRaisedButton();
            this.lblGrammarInfo = new System.Windows.Forms.Label();
            this.gpResult = new System.Windows.Forms.GroupBox();
            this.lblGrammarType = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelNonTerminal = new System.Windows.Forms.Label();
            this.labelTerminal = new System.Windows.Forms.Label();
            this.labelTitle2 = new System.Windows.Forms.Label();
            this.labelTitle1 = new System.Windows.Forms.Label();
            this.materialTabControl1 = new MaterialSkin.Controls.MaterialTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxRegexLog = new System.Windows.Forms.TextBox();
            this.btnRegex = new MaterialSkin.Controls.MaterialRaisedButton();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.gridAFN = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnAFN = new MaterialSkin.Controls.MaterialRaisedButton();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.pictureAFD = new System.Windows.Forms.PictureBox();
            this.btnAFD = new MaterialSkin.Controls.MaterialRaisedButton();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.listViewSyntaxisTable = new MaterialSkin.Controls.MaterialListView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listViewNext = new MaterialSkin.Controls.MaterialListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listViewFirst = new MaterialSkin.Controls.MaterialListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.materialTabSelector = new MaterialSkin.Controls.MaterialTabSelector();
            this.gpGrammar.SuspendLayout();
            this.gpResult.SuspendLayout();
            this.materialTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridAFN)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureAFD)).BeginInit();
            this.tabPage5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbGrammar
            // 
            this.tbGrammar.Location = new System.Drawing.Point(9, 64);
            this.tbGrammar.Multiline = true;
            this.tbGrammar.Name = "tbGrammar";
            this.tbGrammar.Size = new System.Drawing.Size(295, 177);
            this.tbGrammar.TabIndex = 0;
            this.tbGrammar.Text = "S -> xS\'\r\nS\' -> RS\' | ε\r\nR -> (S.R\r\nR -> )\r\n";
            // 
            // gpGrammar
            // 
            this.gpGrammar.Controls.Add(this.btnInsterEpsilon);
            this.gpGrammar.Controls.Add(this.btnProductions);
            this.gpGrammar.Controls.Add(this.lblGrammarInfo);
            this.gpGrammar.Controls.Add(this.tbGrammar);
            this.gpGrammar.Location = new System.Drawing.Point(15, 15);
            this.gpGrammar.Name = "gpGrammar";
            this.gpGrammar.Size = new System.Drawing.Size(313, 302);
            this.gpGrammar.TabIndex = 2;
            this.gpGrammar.TabStop = false;
            this.gpGrammar.Text = "Grammar";
            // 
            // btnInsterEpsilon
            // 
            this.btnInsterEpsilon.AutoSize = true;
            this.btnInsterEpsilon.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnInsterEpsilon.Depth = 0;
            this.btnInsterEpsilon.Icon = null;
            this.btnInsterEpsilon.Location = new System.Drawing.Point(51, 250);
            this.btnInsterEpsilon.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnInsterEpsilon.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnInsterEpsilon.Name = "btnInsterEpsilon";
            this.btnInsterEpsilon.Primary = false;
            this.btnInsterEpsilon.Size = new System.Drawing.Size(79, 36);
            this.btnInsterEpsilon.TabIndex = 4;
            this.btnInsterEpsilon.Text = "Insert ε";
            this.btnInsterEpsilon.UseVisualStyleBackColor = true;
            this.btnInsterEpsilon.Click += new System.EventHandler(this.InsertarEpsilon_click);
            // 
            // btnProductions
            // 
            this.btnProductions.AutoSize = true;
            this.btnProductions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnProductions.Depth = 0;
            this.btnProductions.Icon = null;
            this.btnProductions.Location = new System.Drawing.Point(137, 250);
            this.btnProductions.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnProductions.Name = "btnProductions";
            this.btnProductions.Primary = true;
            this.btnProductions.Size = new System.Drawing.Size(167, 36);
            this.btnProductions.TabIndex = 3;
            this.btnProductions.Text = "Obtain Productions";
            this.btnProductions.UseVisualStyleBackColor = true;
            this.btnProductions.Click += new System.EventHandler(this.OnBtnProductions_Click);
            // 
            // lblGrammarInfo
            // 
            this.lblGrammarInfo.Location = new System.Drawing.Point(6, 18);
            this.lblGrammarInfo.Name = "lblGrammarInfo";
            this.lblGrammarInfo.Size = new System.Drawing.Size(298, 45);
            this.lblGrammarInfo.TabIndex = 0;
            this.lblGrammarInfo.Text = "Type formal grammar using the following syntax:\r\nN -> N | D\r\nD -> 0 | 1 | 2 | 3 |" +
    " 4 | 5 | 6 | 7 | 8 | 9";
            // 
            // gpResult
            // 
            this.gpResult.Controls.Add(this.lblGrammarType);
            this.gpResult.Controls.Add(this.label2);
            this.gpResult.Controls.Add(this.labelNonTerminal);
            this.gpResult.Controls.Add(this.labelTerminal);
            this.gpResult.Controls.Add(this.labelTitle2);
            this.gpResult.Controls.Add(this.labelTitle1);
            this.gpResult.Location = new System.Drawing.Point(344, 15);
            this.gpResult.Name = "gpResult";
            this.gpResult.Size = new System.Drawing.Size(313, 302);
            this.gpResult.TabIndex = 3;
            this.gpResult.TabStop = false;
            this.gpResult.Text = "Result";
            // 
            // lblGrammarType
            // 
            this.lblGrammarType.Location = new System.Drawing.Point(10, 172);
            this.lblGrammarType.Name = "lblGrammarType";
            this.lblGrammarType.Size = new System.Drawing.Size(289, 45);
            this.lblGrammarType.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Grammar type";
            // 
            // labelNonTerminal
            // 
            this.labelNonTerminal.Location = new System.Drawing.Point(9, 96);
            this.labelNonTerminal.Name = "labelNonTerminal";
            this.labelNonTerminal.Size = new System.Drawing.Size(289, 54);
            this.labelNonTerminal.TabIndex = 4;
            // 
            // labelTerminal
            // 
            this.labelTerminal.Location = new System.Drawing.Point(9, 37);
            this.labelTerminal.Name = "labelTerminal";
            this.labelTerminal.Size = new System.Drawing.Size(289, 41);
            this.labelTerminal.TabIndex = 3;
            // 
            // labelTitle2
            // 
            this.labelTitle2.AutoSize = true;
            this.labelTitle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle2.Location = new System.Drawing.Point(8, 79);
            this.labelTitle2.Name = "labelTitle2";
            this.labelTitle2.Size = new System.Drawing.Size(88, 13);
            this.labelTitle2.TabIndex = 2;
            this.labelTitle2.Text = "Non Terminals";
            // 
            // labelTitle1
            // 
            this.labelTitle1.AutoSize = true;
            this.labelTitle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle1.Location = new System.Drawing.Point(9, 20);
            this.labelTitle1.Name = "labelTitle1";
            this.labelTitle1.Size = new System.Drawing.Size(61, 13);
            this.labelTitle1.TabIndex = 1;
            this.labelTitle1.Text = "Terminals";
            // 
            // materialTabControl1
            // 
            this.materialTabControl1.Controls.Add(this.tabPage1);
            this.materialTabControl1.Controls.Add(this.tabPage2);
            this.materialTabControl1.Controls.Add(this.tabPage3);
            this.materialTabControl1.Controls.Add(this.tabPage4);
            this.materialTabControl1.Controls.Add(this.tabPage5);
            this.materialTabControl1.Depth = 0;
            this.materialTabControl1.Location = new System.Drawing.Point(-1, 101);
            this.materialTabControl1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.SelectedIndex = 0;
            this.materialTabControl1.Size = new System.Drawing.Size(674, 328);
            this.materialTabControl1.TabIndex = 4;
            this.materialTabControl1.SelectedIndexChanged += new System.EventHandler(this.materialTabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gpResult);
            this.tabPage1.Controls.Add(this.gpGrammar);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(666, 302);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Productions";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.btnRegex);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(666, 287);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Regex";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxRegexLog);
            this.groupBox1.Location = new System.Drawing.Point(19, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(641, 241);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Log";
            // 
            // textBoxRegexLog
            // 
            this.textBoxRegexLog.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxRegexLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxRegexLog.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBoxRegexLog.Location = new System.Drawing.Point(6, 19);
            this.textBoxRegexLog.Multiline = true;
            this.textBoxRegexLog.Name = "textBoxRegexLog";
            this.textBoxRegexLog.ReadOnly = true;
            this.textBoxRegexLog.Size = new System.Drawing.Size(634, 216);
            this.textBoxRegexLog.TabIndex = 1;
            // 
            // btnRegex
            // 
            this.btnRegex.AutoSize = true;
            this.btnRegex.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRegex.Depth = 0;
            this.btnRegex.Icon = null;
            this.btnRegex.Location = new System.Drawing.Point(544, 260);
            this.btnRegex.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnRegex.Name = "btnRegex";
            this.btnRegex.Primary = true;
            this.btnRegex.Size = new System.Drawing.Size(116, 36);
            this.btnRegex.TabIndex = 0;
            this.btnRegex.Text = "Obtain Regex";
            this.btnRegex.UseVisualStyleBackColor = true;
            this.btnRegex.Click += new System.EventHandler(this.btnRegex_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.gridAFN);
            this.tabPage3.Controls.Add(this.btnAFN);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(666, 287);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "AFN";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // gridAFN
            // 
            this.gridAFN.AllowUserToAddRows = false;
            this.gridAFN.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gridAFN.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridAFN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridAFN.ColumnHeadersVisible = false;
            this.gridAFN.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.gridAFN.Location = new System.Drawing.Point(9, 3);
            this.gridAFN.Name = "gridAFN";
            this.gridAFN.ReadOnly = true;
            this.gridAFN.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gridAFN.RowHeadersVisible = false;
            this.gridAFN.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.White;
            this.gridAFN.RowTemplate.Height = 120;
            this.gridAFN.ShowCellErrors = false;
            this.gridAFN.ShowEditingIcon = false;
            this.gridAFN.ShowRowErrors = false;
            this.gridAFN.Size = new System.Drawing.Size(646, 247);
            this.gridAFN.TabIndex = 2;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "";
            this.Column1.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "";
            this.Column2.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // btnAFN
            // 
            this.btnAFN.AutoSize = true;
            this.btnAFN.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAFN.Depth = 0;
            this.btnAFN.Icon = null;
            this.btnAFN.Location = new System.Drawing.Point(565, 256);
            this.btnAFN.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnAFN.Name = "btnAFN";
            this.btnAFN.Primary = true;
            this.btnAFN.Size = new System.Drawing.Size(90, 36);
            this.btnAFN.TabIndex = 1;
            this.btnAFN.Text = "Draw AFN";
            this.btnAFN.UseVisualStyleBackColor = true;
            this.btnAFN.Click += new System.EventHandler(this.btnAFN_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.pictureAFD);
            this.tabPage4.Controls.Add(this.btnAFD);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(666, 287);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "AFD";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // pictureAFD
            // 
            this.pictureAFD.Location = new System.Drawing.Point(9, 0);
            this.pictureAFD.Name = "pictureAFD";
            this.pictureAFD.Size = new System.Drawing.Size(646, 250);
            this.pictureAFD.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureAFD.TabIndex = 5;
            this.pictureAFD.TabStop = false;
            // 
            // btnAFD
            // 
            this.btnAFD.AutoSize = true;
            this.btnAFD.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAFD.Depth = 0;
            this.btnAFD.Icon = null;
            this.btnAFD.Location = new System.Drawing.Point(565, 256);
            this.btnAFD.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnAFD.Name = "btnAFD";
            this.btnAFD.Primary = true;
            this.btnAFD.Size = new System.Drawing.Size(90, 36);
            this.btnAFD.TabIndex = 4;
            this.btnAFD.Text = "Draw AFN";
            this.btnAFD.UseVisualStyleBackColor = true;
            this.btnAFD.Click += new System.EventHandler(this.btnAFD_Click);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.groupBox4);
            this.tabPage5.Controls.Add(this.groupBox3);
            this.tabPage5.Controls.Add(this.groupBox2);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(666, 302);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "FIRST/NEXT";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.listViewSyntaxisTable);
            this.groupBox4.Location = new System.Drawing.Point(185, 9);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(470, 304);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Syntaxis Table";
            // 
            // listViewSyntaxisTable
            // 
            this.listViewSyntaxisTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewSyntaxisTable.Depth = 0;
            this.listViewSyntaxisTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.listViewSyntaxisTable.FullRowSelect = true;
            this.listViewSyntaxisTable.GridLines = true;
            this.listViewSyntaxisTable.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewSyntaxisTable.Location = new System.Drawing.Point(6, 19);
            this.listViewSyntaxisTable.MouseLocation = new System.Drawing.Point(-1, -1);
            this.listViewSyntaxisTable.MouseState = MaterialSkin.MouseState.OUT;
            this.listViewSyntaxisTable.Name = "listViewSyntaxisTable";
            this.listViewSyntaxisTable.OwnerDraw = true;
            this.listViewSyntaxisTable.Size = new System.Drawing.Size(458, 279);
            this.listViewSyntaxisTable.TabIndex = 1;
            this.listViewSyntaxisTable.UseCompatibleStateImageBehavior = false;
            this.listViewSyntaxisTable.View = System.Windows.Forms.View.Details;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.listViewNext);
            this.groupBox3.Location = new System.Drawing.Point(12, 158);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(160, 155);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "NEXT";
            // 
            // listViewNext
            // 
            this.listViewNext.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewNext.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.listViewNext.Depth = 0;
            this.listViewNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.listViewNext.FullRowSelect = true;
            this.listViewNext.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewNext.Location = new System.Drawing.Point(6, 19);
            this.listViewNext.MouseLocation = new System.Drawing.Point(-1, -1);
            this.listViewNext.MouseState = MaterialSkin.MouseState.OUT;
            this.listViewNext.Name = "listViewNext";
            this.listViewNext.OwnerDraw = true;
            this.listViewNext.Size = new System.Drawing.Size(148, 130);
            this.listViewNext.TabIndex = 1;
            this.listViewNext.UseCompatibleStateImageBehavior = false;
            this.listViewNext.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Width = 80;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listViewFirst);
            this.groupBox2.Location = new System.Drawing.Point(12, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(161, 143);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "FIRST";
            // 
            // listViewFirst
            // 
            this.listViewFirst.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewFirst.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewFirst.Depth = 0;
            this.listViewFirst.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.listViewFirst.FullRowSelect = true;
            this.listViewFirst.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewFirst.Location = new System.Drawing.Point(6, 19);
            this.listViewFirst.MouseLocation = new System.Drawing.Point(-1, -1);
            this.listViewFirst.MouseState = MaterialSkin.MouseState.OUT;
            this.listViewFirst.Name = "listViewFirst";
            this.listViewFirst.OwnerDraw = true;
            this.listViewFirst.Size = new System.Drawing.Size(149, 118);
            this.listViewFirst.TabIndex = 1;
            this.listViewFirst.UseCompatibleStateImageBehavior = false;
            this.listViewFirst.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 80;
            // 
            // materialTabSelector
            // 
            this.materialTabSelector.BaseTabControl = this.materialTabControl1;
            this.materialTabSelector.Depth = 0;
            this.materialTabSelector.Location = new System.Drawing.Point(0, 62);
            this.materialTabSelector.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector.Name = "materialTabSelector";
            this.materialTabSelector.Size = new System.Drawing.Size(673, 37);
            this.materialTabSelector.TabIndex = 5;
            this.materialTabSelector.Text = "materialTabSelector1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(670, 428);
            this.Controls.Add(this.materialTabSelector);
            this.Controls.Add(this.materialTabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Formal Grammar";
            this.gpGrammar.ResumeLayout(false);
            this.gpGrammar.PerformLayout();
            this.gpResult.ResumeLayout(false);
            this.gpResult.PerformLayout();
            this.materialTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridAFN)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureAFD)).EndInit();
            this.tabPage5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbGrammar;
        private System.Windows.Forms.GroupBox gpGrammar;
        private System.Windows.Forms.Label lblGrammarInfo;
        private System.Windows.Forms.GroupBox gpResult;
        private System.Windows.Forms.Label labelTitle1;
        private System.Windows.Forms.Label labelTitle2;
        private System.Windows.Forms.Label labelNonTerminal;
        private System.Windows.Forms.Label labelTerminal;
        private System.Windows.Forms.Label lblGrammarType;
        private System.Windows.Forms.Label label2;
        private MaterialSkin.Controls.MaterialRaisedButton btnProductions;
        private MaterialSkin.Controls.MaterialFlatButton btnInsterEpsilon;
        private MaterialSkin.Controls.MaterialTabControl materialTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector;
        private System.Windows.Forms.TextBox textBoxRegexLog;
        private MaterialSkin.Controls.MaterialRaisedButton btnRegex;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabPage tabPage3;
        private MaterialSkin.Controls.MaterialRaisedButton btnAFN;
        private System.Windows.Forms.DataGridView gridAFN;
        private System.Windows.Forms.DataGridViewImageColumn Column1;
        private System.Windows.Forms.DataGridViewImageColumn Column2;
        private System.Windows.Forms.TabPage tabPage4;
        private MaterialSkin.Controls.MaterialRaisedButton btnAFD;
        private System.Windows.Forms.PictureBox pictureAFD;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.GroupBox groupBox2;
        private MaterialSkin.Controls.MaterialListView listViewFirst;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.GroupBox groupBox3;
        private MaterialSkin.Controls.MaterialListView listViewNext;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.GroupBox groupBox4;
        private MaterialSkin.Controls.MaterialListView listViewSyntaxisTable;
    }
}

