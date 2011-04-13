namespace UBT.AI4.Bio.DivMobi.Forms.Dialogs
{
    partial class EventSeriesDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EventSeriesDialog));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.labelDescriptionCaption = new System.Windows.Forms.Label();
            this.labelDateCaption = new System.Windows.Forms.Label();
            this.labelDate = new System.Windows.Forms.Label();
            this.labelDateEndValue = new System.Windows.Forms.Label();
            this.labelDateEnd = new System.Windows.Forms.Label();
            this.labelSeriesCode = new System.Windows.Forms.Label();
            this.textBoxSeriesCode = new System.Windows.Forms.TextBox();
            this.buttonFinish = new System.Windows.Forms.Button();
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
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonCancel.Location = new System.Drawing.Point(122, 193);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(85, 20);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonOk.Location = new System.Drawing.Point(12, 193);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(85, 20);
            this.buttonOk.TabIndex = 5;
            this.buttonOk.Text = "OK";
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxDescription.Location = new System.Drawing.Point(99, 72);
            this.textBoxDescription.MaxLength = 255;
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(108, 65);
            this.textBoxDescription.TabIndex = 2;
            this.textBoxDescription.TextChanged += new System.EventHandler(this.textBoxDescription_TextChanged);
            this.textBoxDescription.GotFocus += new System.EventHandler(this.textBoxDescription_GotFocus);
            // 
            // labelDescriptionCaption
            // 
            this.labelDescriptionCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelDescriptionCaption.Location = new System.Drawing.Point(12, 93);
            this.labelDescriptionCaption.Name = "labelDescriptionCaption";
            this.labelDescriptionCaption.Size = new System.Drawing.Size(62, 20);
            this.labelDescriptionCaption.Text = "Description:";
            // 
            // labelDateCaption
            // 
            this.labelDateCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelDateCaption.Location = new System.Drawing.Point(12, 140);
            this.labelDateCaption.Name = "labelDateCaption";
            this.labelDateCaption.Size = new System.Drawing.Size(62, 20);
            this.labelDateCaption.Text = "Start Date:";
            // 
            // labelDate
            // 
            this.labelDate.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelDate.Location = new System.Drawing.Point(80, 140);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(127, 20);
            // 
            // labelDateEndValue
            // 
            this.labelDateEndValue.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelDateEndValue.Location = new System.Drawing.Point(80, 164);
            this.labelDateEndValue.Name = "labelDateEndValue";
            this.labelDateEndValue.Size = new System.Drawing.Size(127, 20);
            // 
            // labelDateEnd
            // 
            this.labelDateEnd.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelDateEnd.Location = new System.Drawing.Point(12, 166);
            this.labelDateEnd.Name = "labelDateEnd";
            this.labelDateEnd.Size = new System.Drawing.Size(62, 20);
            this.labelDateEnd.Text = "End Date:";
            // 
            // labelSeriesCode
            // 
            this.labelSeriesCode.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelSeriesCode.Location = new System.Drawing.Point(12, 43);
            this.labelSeriesCode.Name = "labelSeriesCode";
            this.labelSeriesCode.Size = new System.Drawing.Size(62, 19);
            this.labelSeriesCode.Text = "Code:";
            // 
            // textBoxSeriesCode
            // 
            this.textBoxSeriesCode.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxSeriesCode.Location = new System.Drawing.Point(99, 43);
            this.textBoxSeriesCode.Name = "textBoxSeriesCode";
            this.textBoxSeriesCode.Size = new System.Drawing.Size(108, 19);
            this.textBoxSeriesCode.TabIndex = 1;
            // 
            // buttonFinish
            // 
            this.buttonFinish.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonFinish.Enabled = false;
            this.buttonFinish.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonFinish.Location = new System.Drawing.Point(12, 220);
            this.buttonFinish.Name = "buttonFinish";
            this.buttonFinish.Size = new System.Drawing.Size(85, 20);
            this.buttonFinish.TabIndex = 20;
            this.buttonFinish.Text = "Finish";
            this.buttonFinish.Click += new System.EventHandler(this.buttonFinish_Click);
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.SystemColors.Info;
            this.panelTop.Controls.Add(this.pictureBoxFormImage);
            this.panelTop.Controls.Add(this.labelCaption);
            this.panelTop.Location = new System.Drawing.Point(1, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(215, 32);
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
            this.labelCaption.Text = "Collection Event Series";
            this.labelCaption.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // EventSeriesDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoScrollMargin = new System.Drawing.Size(0, 5);
            this.ClientSize = new System.Drawing.Size(231, 253);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.buttonFinish);
            this.Controls.Add(this.textBoxSeriesCode);
            this.Controls.Add(this.labelSeriesCode);
            this.Controls.Add(this.labelDateEndValue);
            this.Controls.Add(this.labelDateEnd);
            this.Controls.Add(this.labelDate);
            this.Controls.Add(this.labelDateCaption);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.labelDescriptionCaption);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "EventSeriesDialog";
            this.Text = "Diversity Mobile";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.EventSeriesDialog_Closing);
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label labelDescriptionCaption;
        private System.Windows.Forms.Label labelDateCaption;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.Label labelDateEndValue;
        private System.Windows.Forms.Label labelDateEnd;
        private System.Windows.Forms.Label labelSeriesCode;
        private System.Windows.Forms.TextBox textBoxSeriesCode;
        private System.Windows.Forms.Button buttonFinish;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.PictureBox pictureBoxFormImage;
        private System.Windows.Forms.Label labelCaption;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
        private System.Windows.Forms.MainMenu mainMenu1;
    }
}
