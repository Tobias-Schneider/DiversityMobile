namespace UBT.AI4.Bio.DivMobi.Forms.Dialogs
{
    partial class TaxonInputDialog
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
            this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.labelTaxonName = new System.Windows.Forms.Label();
            this.labelGenus = new System.Windows.Forms.Label();
            this.labelEpitheton = new System.Windows.Forms.Label();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.listBoxResult = new System.Windows.Forms.ListBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxTaxonName = new System.Windows.Forms.TextBox();
            this.textBoxGenus = new System.Windows.Forms.TextBox();
            this.textBoxEpitheton = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelTaxonName
            // 
            this.labelTaxonName.Location = new System.Drawing.Point(19, 16);
            this.labelTaxonName.Name = "labelTaxonName";
            this.labelTaxonName.Size = new System.Drawing.Size(74, 20);
            this.labelTaxonName.Text = "TaxonName";
            // 
            // labelGenus
            // 
            this.labelGenus.Location = new System.Drawing.Point(19, 44);
            this.labelGenus.Name = "labelGenus";
            this.labelGenus.Size = new System.Drawing.Size(74, 20);
            this.labelGenus.Text = "Genus";
            // 
            // labelEpitheton
            // 
            this.labelEpitheton.Location = new System.Drawing.Point(19, 71);
            this.labelEpitheton.Name = "labelEpitheton";
            this.labelEpitheton.Size = new System.Drawing.Size(74, 20);
            this.labelEpitheton.Text = "Epitheton";
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(19, 97);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(72, 20);
            this.buttonSearch.TabIndex = 6;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(146, 97);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(72, 20);
            this.buttonClear.TabIndex = 7;
            this.buttonClear.Text = "Clear";
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // listBoxResult
            // 
            this.listBoxResult.Location = new System.Drawing.Point(19, 135);
            this.listBoxResult.Name = "listBoxResult";
            this.listBoxResult.Size = new System.Drawing.Size(199, 100);
            this.listBoxResult.TabIndex = 8;
            this.listBoxResult.SelectedIndexChanged += new System.EventHandler(this.listBoxResult_SelectedIndexChanged);
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Enabled = false;
            this.buttonOK.Location = new System.Drawing.Point(19, 242);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(72, 20);
            this.buttonOK.TabIndex = 9;
            this.buttonOK.Text = "OK";
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(146, 241);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(72, 20);
            this.buttonCancel.TabIndex = 10;
            this.buttonCancel.Text = "Cancel";
            // 
            // textBoxTaxonName
            // 
            this.textBoxTaxonName.Location = new System.Drawing.Point(118, 15);
            this.textBoxTaxonName.Name = "textBoxTaxonName";
            this.textBoxTaxonName.Size = new System.Drawing.Size(100, 21);
            this.textBoxTaxonName.TabIndex = 14;
            this.textBoxTaxonName.TextChanged += new System.EventHandler(this.textBoxTaxonName_TextChanged);
            // 
            // textBoxGenus
            // 
            this.textBoxGenus.Location = new System.Drawing.Point(118, 44);
            this.textBoxGenus.Name = "textBoxGenus";
            this.textBoxGenus.Size = new System.Drawing.Size(100, 21);
            this.textBoxGenus.TabIndex = 15;
            // 
            // textBoxEpitheton
            // 
            this.textBoxEpitheton.Location = new System.Drawing.Point(118, 71);
            this.textBoxEpitheton.Name = "textBoxEpitheton";
            this.textBoxEpitheton.Size = new System.Drawing.Size(100, 21);
            this.textBoxEpitheton.TabIndex = 16;
            // 
            // TaxonInputDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(240, 283);
            this.Controls.Add(this.textBoxEpitheton);
            this.Controls.Add(this.textBoxGenus);
            this.Controls.Add(this.textBoxTaxonName);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.listBoxResult);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.labelEpitheton);
            this.Controls.Add(this.labelGenus);
            this.Controls.Add(this.labelTaxonName);
            this.Name = "TaxonInputDialog";
            this.Text = "Find Taxon";
            this.Load += new System.EventHandler(this.TaxonInputDialog_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.TaxonInputDialog_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
        private System.Windows.Forms.Label labelTaxonName;
        private System.Windows.Forms.Label labelGenus;
        private System.Windows.Forms.Label labelEpitheton;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.ListBox listBoxResult;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxTaxonName;
        private System.Windows.Forms.TextBox textBoxGenus;
        private System.Windows.Forms.TextBox textBoxEpitheton;
    }
}
