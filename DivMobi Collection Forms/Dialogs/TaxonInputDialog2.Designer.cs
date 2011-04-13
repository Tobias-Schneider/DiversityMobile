namespace UBT.AI4.Bio.DivMobi.Forms.Dialogs
{
    partial class TaxonInputDialog2
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.labelTaxonName = new System.Windows.Forms.Label();
            this.labelGenus = new System.Windows.Forms.Label();
            this.labelEpitheton = new System.Windows.Forms.Label();
            this.textBoxTaxonName = new System.Windows.Forms.TextBox();
            this.textBoxGenus = new System.Windows.Forms.TextBox();
            this.textBoxEpitheton = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.comboBoxResult = new UBT.AI4.Toolbox.Controls.ClickComboBox();
            this.SuspendLayout();
            // 
            // labelTaxonName
            // 
            this.labelTaxonName.Location = new System.Drawing.Point(12, 18);
            this.labelTaxonName.Name = "labelTaxonName";
            this.labelTaxonName.Size = new System.Drawing.Size(70, 20);
            this.labelTaxonName.Text = "TaxonName";
            // 
            // labelGenus
            // 
            this.labelGenus.Location = new System.Drawing.Point(12, 42);
            this.labelGenus.Name = "labelGenus";
            this.labelGenus.Size = new System.Drawing.Size(100, 20);
            this.labelGenus.Text = "Genus";
            // 
            // labelEpitheton
            // 
            this.labelEpitheton.Location = new System.Drawing.Point(12, 66);
            this.labelEpitheton.Name = "labelEpitheton";
            this.labelEpitheton.Size = new System.Drawing.Size(75, 20);
            this.labelEpitheton.Text = "Epitheton";
            // 
            // textBoxTaxonName
            // 
            this.textBoxTaxonName.HideSelection = false;
            this.textBoxTaxonName.Location = new System.Drawing.Point(88, 17);
            this.textBoxTaxonName.Name = "textBoxTaxonName";
            this.textBoxTaxonName.Size = new System.Drawing.Size(133, 21);
            this.textBoxTaxonName.TabIndex = 3;
            this.textBoxTaxonName.TextChanged += new System.EventHandler(this.textBoxTaxonName_TextChanged);
            // 
            // textBoxGenus
            // 
            this.textBoxGenus.HideSelection = false;
            this.textBoxGenus.Location = new System.Drawing.Point(88, 41);
            this.textBoxGenus.Name = "textBoxGenus";
            this.textBoxGenus.Size = new System.Drawing.Size(133, 21);
            this.textBoxGenus.TabIndex = 4;
            this.textBoxGenus.TextChanged += new System.EventHandler(this.textBoxGenus_TextChanged);
            // 
            // textBoxEpitheton
            // 
            this.textBoxEpitheton.HideSelection = false;
            this.textBoxEpitheton.Location = new System.Drawing.Point(88, 68);
            this.textBoxEpitheton.Name = "textBoxEpitheton";
            this.textBoxEpitheton.Size = new System.Drawing.Size(133, 21);
            this.textBoxEpitheton.TabIndex = 5;
            this.textBoxEpitheton.TextChanged += new System.EventHandler(this.textBoxEpitheton_TextChanged);
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(12, 95);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(72, 20);
            this.buttonSearch.TabIndex = 6;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(149, 95);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(72, 20);
            this.buttonClear.TabIndex = 7;
            this.buttonClear.Text = "Clear";
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Enabled = false;
            this.buttonOK.Location = new System.Drawing.Point(12, 159);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(72, 20);
            this.buttonOK.TabIndex = 9;
            this.buttonOK.Text = "OK";
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(149, 159);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(72, 20);
            this.buttonCancel.TabIndex = 10;
            this.buttonCancel.Text = "Cancel";
            // 
            // comboBoxResult
            // 
            this.comboBoxResult.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.comboBoxResult.Location = new System.Drawing.Point(12, 121);
            this.comboBoxResult.Name = "comboBoxResult";
            this.comboBoxResult.Size = new System.Drawing.Size(209, 32);
            this.comboBoxResult.TabIndex = 14;
            this.comboBoxResult.SelectedIndexChanged += new System.EventHandler(this.comboBoxResult_SelectedIndexChanged_1);
            // 
            // TaxonInputDialog2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(240, 191);
            this.Controls.Add(this.comboBoxResult);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.textBoxEpitheton);
            this.Controls.Add(this.textBoxGenus);
            this.Controls.Add(this.textBoxTaxonName);
            this.Controls.Add(this.labelEpitheton);
            this.Controls.Add(this.labelGenus);
            this.Controls.Add(this.labelTaxonName);
            this.Name = "TaxonInputDialog2";
            this.Text = "Find Taxon";
            this.Load += new System.EventHandler(this.TaxonInputDialog2_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.TaxonInputDialog2_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelTaxonName;
        private System.Windows.Forms.Label labelGenus;
        private System.Windows.Forms.Label labelEpitheton;
        private System.Windows.Forms.TextBox textBoxTaxonName;
        private System.Windows.Forms.TextBox textBoxGenus;
        private System.Windows.Forms.TextBox textBoxEpitheton;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
        private UBT.AI4.Toolbox.Controls.ClickComboBox comboBoxResult;
    }
}
