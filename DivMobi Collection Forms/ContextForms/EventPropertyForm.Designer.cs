namespace UBT.AI4.Bio.DivMobi.Forms.ContextForms
{
    partial class EventPropertyForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EventPropertyForm));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.labelCaption = new System.Windows.Forms.Label();
            this.labelAverageValueCaption = new System.Windows.Forms.Label();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.labelValueCaption = new System.Windows.Forms.Label();
            this.textBoxDisplayText = new System.Windows.Forms.TextBox();
            this.labelDisplayTextCaption = new System.Windows.Forms.Label();
            this.labelAverageValue = new System.Windows.Forms.Label();
            this.labelResponsibleNameCaption = new System.Windows.Forms.Label();
            this.labelResponsible = new System.Windows.Forms.Label();
            this.panelTop = new System.Windows.Forms.Panel();
            this.pictureBoxFormImage = new System.Windows.Forms.PictureBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelCaption
            // 
            this.labelCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelCaption.Location = new System.Drawing.Point(41, 6);
            this.labelCaption.Name = "labelCaption";
            this.labelCaption.Size = new System.Drawing.Size(182, 20);
            this.labelCaption.Text = "Event Property";
            this.labelCaption.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelAverageValueCaption
            // 
            this.labelAverageValueCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelAverageValueCaption.Location = new System.Drawing.Point(9, 126);
            this.labelAverageValueCaption.Name = "labelAverageValueCaption";
            this.labelAverageValueCaption.Size = new System.Drawing.Size(82, 34);
            this.labelAverageValueCaption.Text = "Average Value:";
            // 
            // textBoxValue
            // 
            this.textBoxValue.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxValue.Location = new System.Drawing.Point(83, 77);
            this.textBoxValue.MaxLength = 255;
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.Size = new System.Drawing.Size(143, 19);
            this.textBoxValue.TabIndex = 4;
            // 
            // labelValueCaption
            // 
            this.labelValueCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelValueCaption.Location = new System.Drawing.Point(9, 66);
            this.labelValueCaption.Name = "labelValueCaption";
            this.labelValueCaption.Size = new System.Drawing.Size(68, 30);
            this.labelValueCaption.Text = "Property Value:";
            // 
            // textBoxDisplayText
            // 
            this.textBoxDisplayText.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxDisplayText.Location = new System.Drawing.Point(83, 38);
            this.textBoxDisplayText.MaxLength = 255;
            this.textBoxDisplayText.Name = "textBoxDisplayText";
            this.textBoxDisplayText.Size = new System.Drawing.Size(143, 19);
            this.textBoxDisplayText.TabIndex = 1;
            // 
            // labelDisplayTextCaption
            // 
            this.labelDisplayTextCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelDisplayTextCaption.Location = new System.Drawing.Point(9, 38);
            this.labelDisplayTextCaption.Name = "labelDisplayTextCaption";
            this.labelDisplayTextCaption.Size = new System.Drawing.Size(68, 20);
            this.labelDisplayTextCaption.Text = "Display Text:";
            // 
            // labelAverageValue
            // 
            this.labelAverageValue.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelAverageValue.Location = new System.Drawing.Point(83, 141);
            this.labelAverageValue.Name = "labelAverageValue";
            this.labelAverageValue.Size = new System.Drawing.Size(143, 19);
            // 
            // labelResponsibleNameCaption
            // 
            this.labelResponsibleNameCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelResponsibleNameCaption.Location = new System.Drawing.Point(9, 106);
            this.labelResponsibleNameCaption.Name = "labelResponsibleNameCaption";
            this.labelResponsibleNameCaption.Size = new System.Drawing.Size(68, 20);
            this.labelResponsibleNameCaption.Text = "Responsible:";
            // 
            // labelResponsible
            // 
            this.labelResponsible.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelResponsible.Location = new System.Drawing.Point(83, 106);
            this.labelResponsible.Name = "labelResponsible";
            this.labelResponsible.Size = new System.Drawing.Size(143, 20);
            this.labelResponsible.Text = "Resp.";
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.pictureBoxFormImage);
            this.panelTop.Controls.Add(this.labelCaption);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(240, 32);
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
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonCancel.Location = new System.Drawing.Point(134, 163);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(92, 20);
            this.buttonCancel.TabIndex = 15;
            this.buttonCancel.Text = "Cancel";
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonOk.Location = new System.Drawing.Point(9, 163);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(92, 20);
            this.buttonOk.TabIndex = 14;
            this.buttonOk.Text = "OK";
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // EventPropertyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScrollMargin = new System.Drawing.Size(0, 10);
            this.ClientSize = new System.Drawing.Size(240, 217);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.labelResponsible);
            this.Controls.Add(this.labelResponsibleNameCaption);
            this.Controls.Add(this.labelAverageValue);
            this.Controls.Add(this.labelAverageValueCaption);
            this.Controls.Add(this.textBoxValue);
            this.Controls.Add(this.labelValueCaption);
            this.Controls.Add(this.textBoxDisplayText);
            this.Controls.Add(this.labelDisplayTextCaption);
            this.Menu = this.mainMenu1;
            this.Name = "EventPropertyForm";
            this.Text = "Diversity Mobile";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.EventPropertyForm_Closing);
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelCaption;
        private System.Windows.Forms.Label labelAverageValueCaption;
        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.Label labelValueCaption;
        private System.Windows.Forms.TextBox textBoxDisplayText;
        private System.Windows.Forms.Label labelDisplayTextCaption;
        private System.Windows.Forms.Label labelAverageValue;
        private System.Windows.Forms.Label labelResponsibleNameCaption;
        private System.Windows.Forms.Label labelResponsible;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.PictureBox pictureBoxFormImage;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
    }
}