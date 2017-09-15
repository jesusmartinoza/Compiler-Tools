﻿namespace Compilers
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
            this.textBoxRegexLog = new System.Windows.Forms.TextBox();
            this.btnRegex = new MaterialSkin.Controls.MaterialRaisedButton();
            this.materialTabSelector = new MaterialSkin.Controls.MaterialTabSelector();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gpGrammar.SuspendLayout();
            this.gpResult.SuspendLayout();
            this.materialTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbGrammar
            // 
            this.tbGrammar.Location = new System.Drawing.Point(9, 64);
            this.tbGrammar.Multiline = true;
            this.tbGrammar.Name = "tbGrammar";
            this.tbGrammar.Size = new System.Drawing.Size(295, 163);
            this.tbGrammar.TabIndex = 0;
            // 
            // gpGrammar
            // 
            this.gpGrammar.Controls.Add(this.btnInsterEpsilon);
            this.gpGrammar.Controls.Add(this.btnProductions);
            this.gpGrammar.Controls.Add(this.lblGrammarInfo);
            this.gpGrammar.Controls.Add(this.tbGrammar);
            this.gpGrammar.Location = new System.Drawing.Point(18, 6);
            this.gpGrammar.Name = "gpGrammar";
            this.gpGrammar.Size = new System.Drawing.Size(313, 276);
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
            this.btnInsterEpsilon.Location = new System.Drawing.Point(51, 234);
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
            this.btnProductions.Location = new System.Drawing.Point(137, 234);
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
            this.gpResult.Location = new System.Drawing.Point(347, 6);
            this.gpResult.Name = "gpResult";
            this.gpResult.Size = new System.Drawing.Size(313, 276);
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
            this.materialTabControl1.Depth = 0;
            this.materialTabControl1.Location = new System.Drawing.Point(-1, 101);
            this.materialTabControl1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.SelectedIndex = 0;
            this.materialTabControl1.Size = new System.Drawing.Size(674, 321);
            this.materialTabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gpResult);
            this.tabPage1.Controls.Add(this.gpGrammar);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(666, 295);
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
            this.tabPage2.Size = new System.Drawing.Size(666, 295);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Regex";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBoxRegexLog
            // 
            this.textBoxRegexLog.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxRegexLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxRegexLog.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBoxRegexLog.Location = new System.Drawing.Point(6, 19);
            this.textBoxRegexLog.Multiline = true;
            this.textBoxRegexLog.Name = "textBoxRegexLog";
            this.textBoxRegexLog.Size = new System.Drawing.Size(634, 208);
            this.textBoxRegexLog.TabIndex = 1;
            // 
            // btnRegex
            // 
            this.btnRegex.AutoSize = true;
            this.btnRegex.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRegex.Depth = 0;
            this.btnRegex.Icon = null;
            this.btnRegex.Location = new System.Drawing.Point(544, 253);
            this.btnRegex.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnRegex.Name = "btnRegex";
            this.btnRegex.Primary = true;
            this.btnRegex.Size = new System.Drawing.Size(116, 36);
            this.btnRegex.TabIndex = 0;
            this.btnRegex.Text = "Obtain Regex";
            this.btnRegex.UseVisualStyleBackColor = true;
            this.btnRegex.Click += new System.EventHandler(this.btnRegex_Click);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(670, 397);
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
    }
}
