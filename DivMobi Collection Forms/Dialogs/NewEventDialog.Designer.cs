namespace UBT.AI4.Bio.DivMobi.Forms.Dialogs
{
    partial class NewEventDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewEventDialog));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.textBoxCollectorsEventNumber = new System.Windows.Forms.TextBox();
            this.labelCollectorsEventNumber = new System.Windows.Forms.Label();
            this.textBoxNotes = new System.Windows.Forms.TextBox();
            this.labelNotesCaption = new System.Windows.Forms.Label();
            this.textBoxDateSupplement = new System.Windows.Forms.TextBox();
            this.labelDateSupplement = new System.Windows.Forms.Label();
            this.panelTop = new System.Windows.Forms.Panel();
            this.pictureBoxFormImage = new System.Windows.Forms.PictureBox();
            this.labelCaption = new System.Windows.Forms.Label();
            this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonCancel.Location = new System.Drawing.Point(121, 158);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(96, 20);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonOk.Location = new System.Drawing.Point(10, 158);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(96, 20);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "OK";
            // 
            // textBoxCollectorsEventNumber
            // 
            this.textBoxCollectorsEventNumber.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxCollectorsEventNumber.Location = new System.Drawing.Point(92, 38);
            this.textBoxCollectorsEventNumber.MaxLength = 50;
            this.textBoxCollectorsEventNumber.Name = "textBoxCollectorsEventNumber";
            this.textBoxCollectorsEventNumber.Size = new System.Drawing.Size(125, 19);
            this.textBoxCollectorsEventNumber.TabIndex = 1;
            // 
            // labelCollectorsEventNumber
            // 
            this.labelCollectorsEventNumber.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelCollectorsEventNumber.Location = new System.Drawing.Point(10, 40);
            this.labelCollectorsEventNumber.Name = "labelCollectorsEventNumber";
            this.labelCollectorsEventNumber.Size = new System.Drawing.Size(76, 19);
            this.labelCollectorsEventNumber.Text = "Event No.:";
            // 
            // textBoxNotes
            // 
            this.textBoxNotes.AcceptsReturn = true;
            this.textBoxNotes.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxNotes.Location = new System.Drawing.Point(69, 103);
            this.textBoxNotes.MaxLength = 255;
            this.textBoxNotes.Multiline = true;
            this.textBoxNotes.Name = "textBoxNotes";
            this.textBoxNotes.Size = new System.Drawing.Size(148, 40);
            this.textBoxNotes.TabIndex = 3;
            // 
            // labelNotesCaption
            // 
            this.labelNotesCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelNotesCaption.Location = new System.Drawing.Point(10, 103);
            this.labelNotesCaption.Name = "labelNotesCaption";
            this.labelNotesCaption.Size = new System.Drawing.Size(53, 20);
            this.labelNotesCaption.Text = "Notes:";
            // 
            // textBoxDateSupplement
            // 
            this.textBoxDateSupplement.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxDateSupplement.Location = new System.Drawing.Point(92, 65);
            this.textBoxDateSupplement.MaxLength = 100;
            this.textBoxDateSupplement.Multiline = true;
            this.textBoxDateSupplement.Name = "textBoxDateSupplement";
            this.textBoxDateSupplement.Size = new System.Drawing.Size(125, 33);
            this.textBoxDateSupplement.TabIndex = 2;
            // 
            // labelDateSupplement
            // 
            this.labelDateSupplement.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelDateSupplement.Location = new System.Drawing.Point(10, 67);
            this.labelDateSupplement.Name = "labelDateSupplement";
            this.labelDateSupplement.Size = new System.Drawing.Size(76, 31);
            this.labelDateSupplement.Text = "Date Supplement:";
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.SystemColors.Info;
            this.panelTop.Controls.Add(this.pictureBoxFormImage);
            this.panelTop.Controls.Add(this.labelCaption);
            this.panelTop.Location = new System.Drawing.Point(1, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(228, 32);
            // 
            // pictureBoxFormImage
            // 
            this.pictureBoxFormImage.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBoxFormImage.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxFormImage.Image")));
            this.pictureBoxFormImage.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxFormImage.Name = "pictureBoxFormImage";
            this.pictureBoxFormImage.Size = new System.Drawing.Size(40, 32);
            this.pictureBoxFormImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            // 
            // labelCaption
            // 
            this.labelCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelCaption.Location = new System.Drawing.Point(48, 6);
            this.labelCaption.Name = "labelCaption";
            this.labelCaption.Size = new System.Drawing.Size(152, 20);
            this.labelCaption.Text = "Collection Event";
            this.labelCaption.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // NewEventDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(230, 191);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.textBoxDateSupplement);
            this.Controls.Add(this.labelDateSupplement);
            this.Controls.Add(this.textBoxCollectorsEventNumber);
            this.Controls.Add(this.labelCollectorsEventNumber);
            this.Controls.Add(this.textBoxNotes);
            this.Controls.Add(this.labelNotesCaption);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Menu = this.mainMenu1;
            this.Name = "NewEventDialog";
            this.Text = "Diversity Mobile";
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.TextBox textBoxCollectorsEventNumber;
        private System.Windows.Forms.Label labelCollectorsEventNumber;
        private System.Windows.Forms.TextBox textBoxNotes;
        private System.Windows.Forms.Label labelNotesCaption;
        private System.Windows.Forms.TextBox textBoxDateSupplement;
        private System.Windows.Forms.Label labelDateSupplement;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.PictureBox pictureBoxFormImage;
        private System.Windows.Forms.Label labelCaption;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
        private System.Windows.Forms.MainMenu mainMenu1;
    }
}
