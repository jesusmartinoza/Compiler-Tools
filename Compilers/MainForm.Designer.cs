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
            this.gpGrammar.SuspendLayout();
            this.gpResult.SuspendLayout();
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
            this.gpGrammar.Location = new System.Drawing.Point(12, 76);
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
            this.gpResult.Location = new System.Drawing.Point(341, 76);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(670, 364);
            this.Controls.Add(this.gpResult);
            this.Controls.Add(this.gpGrammar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Formal Grammar";
            this.gpGrammar.ResumeLayout(false);
            this.gpGrammar.PerformLayout();
            this.gpResult.ResumeLayout(false);
            this.gpResult.PerformLayout();
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
    }
}

