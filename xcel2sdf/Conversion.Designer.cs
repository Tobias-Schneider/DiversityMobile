namespace xcel2sdf
{
    partial class Conversion
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
            this.buttonExecute = new System.Windows.Forms.Button();
            this.labelExcelHeading = new System.Windows.Forms.Label();
            this.labelSdfHeading = new System.Windows.Forms.Label();
            this.buttonSelectExcel = new System.Windows.Forms.Button();
            this.buttonSelectSdfPath = new System.Windows.Forms.Button();
            this.labelExcelPath = new System.Windows.Forms.Label();
            this.labelSdfPath = new System.Windows.Forms.Label();
            this.comboBoxTaxonomicGroup = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // buttonExecute
            // 
            this.buttonExecute.Enabled = false;
            this.buttonExecute.Location = new System.Drawing.Point(479, 127);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(75, 23);
            this.buttonExecute.TabIndex = 0;
            this.buttonExecute.Text = "Execute";
            this.buttonExecute.UseVisualStyleBackColor = true;
            this.buttonExecute.Click += new System.EventHandler(this.buttonExecute_Click);
            // 
            // labelExcelHeading
            // 
            this.labelExcelHeading.AutoSize = true;
            this.labelExcelHeading.Location = new System.Drawing.Point(21, 21);
            this.labelExcelHeading.Name = "labelExcelHeading";
            this.labelExcelHeading.Size = new System.Drawing.Size(111, 13);
            this.labelExcelHeading.TabIndex = 1;
            this.labelExcelHeading.Text = "Location of ExcelFile: ";
            // 
            // labelSdfHeading
            // 
            this.labelSdfHeading.AutoSize = true;
            this.labelSdfHeading.Location = new System.Drawing.Point(21, 75);
            this.labelSdfHeading.Name = "labelSdfHeading";
            this.labelSdfHeading.Size = new System.Drawing.Size(101, 13);
            this.labelSdfHeading.TabIndex = 2;
            this.labelSdfHeading.Text = "Location of SdfFile: ";
            // 
            // buttonSelectExcel
            // 
            this.buttonSelectExcel.Location = new System.Drawing.Point(24, 37);
            this.buttonSelectExcel.Name = "buttonSelectExcel";
            this.buttonSelectExcel.Size = new System.Drawing.Size(75, 23);
            this.buttonSelectExcel.TabIndex = 3;
            this.buttonSelectExcel.Text = "Select";
            this.buttonSelectExcel.UseVisualStyleBackColor = true;
            this.buttonSelectExcel.Click += new System.EventHandler(this.buttonSelectExcel_Click);
            // 
            // buttonSelectSdfPath
            // 
            this.buttonSelectSdfPath.Location = new System.Drawing.Point(24, 101);
            this.buttonSelectSdfPath.Name = "buttonSelectSdfPath";
            this.buttonSelectSdfPath.Size = new System.Drawing.Size(75, 23);
            this.buttonSelectSdfPath.TabIndex = 4;
            this.buttonSelectSdfPath.Text = "Select";
            this.buttonSelectSdfPath.UseVisualStyleBackColor = true;
            this.buttonSelectSdfPath.Click += new System.EventHandler(this.buttonSelectSdfPath_Click);
            // 
            // labelExcelPath
            // 
            this.labelExcelPath.AutoSize = true;
            this.labelExcelPath.Location = new System.Drawing.Point(139, 21);
            this.labelExcelPath.Name = "labelExcelPath";
            this.labelExcelPath.Size = new System.Drawing.Size(49, 13);
            this.labelExcelPath.TabIndex = 5;
            this.labelExcelPath.Text = "<Select>";
            // 
            // labelSdfPath
            // 
            this.labelSdfPath.AutoSize = true;
            this.labelSdfPath.Location = new System.Drawing.Point(139, 75);
            this.labelSdfPath.Name = "labelSdfPath";
            this.labelSdfPath.Size = new System.Drawing.Size(49, 13);
            this.labelSdfPath.TabIndex = 6;
            this.labelSdfPath.Text = "<Select>";
            // 
            // comboBoxTaxonomicGroup
            // 
            this.comboBoxTaxonomicGroup.FormattingEnabled = true;
            this.comboBoxTaxonomicGroup.Items.AddRange(new object[] {
            "Plants",
            "Fungi"});
            this.comboBoxTaxonomicGroup.Location = new System.Drawing.Point(238, 37);
            this.comboBoxTaxonomicGroup.Name = "comboBoxTaxonomicGroup";
            this.comboBoxTaxonomicGroup.Size = new System.Drawing.Size(121, 21);
            this.comboBoxTaxonomicGroup.TabIndex = 7;
            this.comboBoxTaxonomicGroup.Text = "<Taxonomic Group>";
            // 
            // Conversion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 162);
            this.Controls.Add(this.comboBoxTaxonomicGroup);
            this.Controls.Add(this.labelSdfPath);
            this.Controls.Add(this.labelExcelPath);
            this.Controls.Add(this.buttonSelectSdfPath);
            this.Controls.Add(this.buttonSelectExcel);
            this.Controls.Add(this.labelSdfHeading);
            this.Controls.Add(this.labelExcelHeading);
            this.Controls.Add(this.buttonExecute);
            this.Name = "Conversion";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonExecute;
        private System.Windows.Forms.Label labelExcelHeading;
        private System.Windows.Forms.Label labelSdfHeading;
        private System.Windows.Forms.Button buttonSelectExcel;
        private System.Windows.Forms.Button buttonSelectSdfPath;
        private System.Windows.Forms.Label labelExcelPath;
        private System.Windows.Forms.Label labelSdfPath;
        private System.Windows.Forms.ComboBox comboBoxTaxonomicGroup;
    }
}

