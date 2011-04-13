namespace UBT.AI4.Bio.DivMobi.Forms.Forms
{
    partial class QuickForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.labelIdentification = new System.Windows.Forms.Label();
            this.labelSynonym = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonNotes = new System.Windows.Forms.Button();
            this.buttonIU = new System.Windows.Forms.Button();
            this.buttonAnalysis = new System.Windows.Forms.Button();
            this.buttonGPS = new System.Windows.Forms.Button();
            this.buttonDetails = new System.Windows.Forms.Button();
            this.comboBoxTaxonomicGroup = new UBT.AI4.Toolbox.Controls.ClickComboBox();
            this.comboBox1 = new UBT.AI4.Toolbox.Controls.ClickComboBox();
            this.SuspendLayout();
            // 
            // labelIdentification
            // 
            this.labelIdentification.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelIdentification.Location = new System.Drawing.Point(13, 52);
            this.labelIdentification.Name = "labelIdentification";
            this.labelIdentification.Size = new System.Drawing.Size(84, 20);
            this.labelIdentification.Text = "Identification";
            // 
            // labelSynonym
            // 
            this.labelSynonym.BackColor = System.Drawing.Color.White;
            this.labelSynonym.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelSynonym.Location = new System.Drawing.Point(104, 52);
            this.labelSynonym.Name = "labelSynonym";
            this.labelSynonym.Size = new System.Drawing.Size(118, 16);
            this.labelSynonym.Text = "Working Name";
            this.labelSynonym.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonSave
            // 
            this.buttonSave.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonSave.Location = new System.Drawing.Point(13, 215);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(209, 25);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.Text = "Save and Next";
            // 
            // buttonNotes
            // 
            this.buttonNotes.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonNotes.Location = new System.Drawing.Point(13, 114);
            this.buttonNotes.Name = "buttonNotes";
            this.buttonNotes.Size = new System.Drawing.Size(72, 20);
            this.buttonNotes.TabIndex = 5;
            this.buttonNotes.Text = "Notes";
            // 
            // buttonIU
            // 
            this.buttonIU.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonIU.Location = new System.Drawing.Point(13, 153);
            this.buttonIU.Name = "buttonIU";
            this.buttonIU.Size = new System.Drawing.Size(127, 25);
            this.buttonIU.TabIndex = 6;
            this.buttonIU.Text = "IdentificationUnit";
            // 
            // buttonAnalysis
            // 
            this.buttonAnalysis.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonAnalysis.Location = new System.Drawing.Point(13, 184);
            this.buttonAnalysis.Name = "buttonAnalysis";
            this.buttonAnalysis.Size = new System.Drawing.Size(127, 25);
            this.buttonAnalysis.TabIndex = 7;
            this.buttonAnalysis.Text = "Analysis";
            // 
            // buttonGPS
            // 
            this.buttonGPS.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonGPS.Location = new System.Drawing.Point(91, 114);
            this.buttonGPS.Name = "buttonGPS";
            this.buttonGPS.Size = new System.Drawing.Size(63, 20);
            this.buttonGPS.TabIndex = 8;
            this.buttonGPS.Text = "GPS";
            // 
            // buttonDetails
            // 
            this.buttonDetails.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonDetails.Location = new System.Drawing.Point(161, 113);
            this.buttonDetails.Name = "buttonDetails";
            this.buttonDetails.Size = new System.Drawing.Size(61, 20);
            this.buttonDetails.TabIndex = 9;
            this.buttonDetails.Text = "Details";
            // 
            // comboBoxTaxonomicGroup
            // 
            this.comboBoxTaxonomicGroup.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.comboBoxTaxonomicGroup.Location = new System.Drawing.Point(13, 18);
            this.comboBoxTaxonomicGroup.Name = "comboBoxTaxonomicGroup";
            this.comboBoxTaxonomicGroup.Size = new System.Drawing.Size(209, 20);
            this.comboBoxTaxonomicGroup.TabIndex = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.comboBox1.Location = new System.Drawing.Point(13, 75);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(209, 20);
            this.comboBox1.TabIndex = 2;
            // 
            // QuickForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoScrollMargin = new System.Drawing.Size(0, 5);
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(262, 270);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.comboBoxTaxonomicGroup);
            this.Controls.Add(this.buttonDetails);
            this.Controls.Add(this.buttonGPS);
            this.Controls.Add(this.buttonAnalysis);
            this.Controls.Add(this.buttonIU);
            this.Controls.Add(this.buttonNotes);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.labelSynonym);
            this.Controls.Add(this.labelIdentification);
            this.Menu = this.mainMenu1;
            this.Name = "QuickForm";
            this.Text = "QuickForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelIdentification;
        private System.Windows.Forms.Label labelSynonym;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonNotes;
        private System.Windows.Forms.Button buttonIU;
        private System.Windows.Forms.Button buttonAnalysis;
        private System.Windows.Forms.Button buttonGPS;
        private System.Windows.Forms.Button buttonDetails;
        private UBT.AI4.Toolbox.Controls.ClickComboBox comboBoxTaxonomicGroup;
        private UBT.AI4.Toolbox.Controls.ClickComboBox comboBox1;
    }
}