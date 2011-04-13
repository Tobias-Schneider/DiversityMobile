namespace UBT.AI4.Bio.DivMobi.Forms.ContextForms
{
    partial class AgentForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgentForm));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.panelTop = new System.Windows.Forms.Panel();
            this.pictureBoxFormImage = new System.Windows.Forms.PictureBox();
            this.labelCaption = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.contextPanelCollectorsName = new UBT.AI4.Toolbox.Controls.ContextPanel();
            this.textBoxCollectorsName = new System.Windows.Forms.TextBox();
            this.labelCollectorsNameCaption = new System.Windows.Forms.Label();
            this.buttonChangeName = new System.Windows.Forms.Button();
            this.contextPanelNumber = new UBT.AI4.Toolbox.Controls.ContextPanel();
            this.textBoxNumber = new System.Windows.Forms.TextBox();
            this.labelNumberCaption = new System.Windows.Forms.Label();
            this.contextPanelNotes = new UBT.AI4.Toolbox.Controls.ContextPanel();
            this.textBoxNotes = new System.Windows.Forms.TextBox();
            this.labelNotes = new System.Windows.Forms.Label();
            this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.panelTop.SuspendLayout();
            this.contextPanelCollectorsName.SuspendLayout();
            this.contextPanelNumber.SuspendLayout();
            this.contextPanelNotes.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.pictureBoxFormImage);
            this.panelTop.Controls.Add(this.labelCaption);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(243, 32);
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
            this.labelCaption.Text = "Collection Agent";
            this.labelCaption.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonCancel.Location = new System.Drawing.Point(128, 242);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(91, 20);
            this.buttonCancel.TabIndex = 15;
            this.buttonCancel.Text = "Cancel";
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonOk.Location = new System.Drawing.Point(8, 242);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(92, 20);
            this.buttonOk.TabIndex = 14;
            this.buttonOk.Text = "OK";
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // contextPanelCollectorsName
            // 
            this.contextPanelCollectorsName.Controls.Add(this.textBoxCollectorsName);
            this.contextPanelCollectorsName.Controls.Add(this.labelCollectorsNameCaption);
            this.contextPanelCollectorsName.Controls.Add(this.buttonChangeName);
            this.contextPanelCollectorsName.Location = new System.Drawing.Point(8, 38);
            this.contextPanelCollectorsName.Name = "contextPanelCollectorsName";
            this.contextPanelCollectorsName.Size = new System.Drawing.Size(211, 51);
            // 
            // textBoxCollectorsName
            // 
            this.textBoxCollectorsName.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxCollectorsName.Location = new System.Drawing.Point(4, 27);
            this.textBoxCollectorsName.MaxLength = 255;
            this.textBoxCollectorsName.Name = "textBoxCollectorsName";
            this.textBoxCollectorsName.ReadOnly = true;
            this.textBoxCollectorsName.Size = new System.Drawing.Size(203, 19);
            this.textBoxCollectorsName.TabIndex = 3;
            // 
            // labelCollectorsNameCaption
            // 
            this.labelCollectorsNameCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelCollectorsNameCaption.Location = new System.Drawing.Point(4, 4);
            this.labelCollectorsNameCaption.Name = "labelCollectorsNameCaption";
            this.labelCollectorsNameCaption.Size = new System.Drawing.Size(91, 20);
            this.labelCollectorsNameCaption.Text = "Collectors Name:";
            // 
            // buttonChangeName
            // 
            this.buttonChangeName.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonChangeName.Location = new System.Drawing.Point(150, 4);
            this.buttonChangeName.Name = "buttonChangeName";
            this.buttonChangeName.Size = new System.Drawing.Size(57, 20);
            this.buttonChangeName.TabIndex = 4;
            this.buttonChangeName.Text = "Change";
            this.buttonChangeName.Click += new System.EventHandler(this.buttonChangeName_Click);
            // 
            // contextPanelNumber
            // 
            this.contextPanelNumber.Controls.Add(this.textBoxNumber);
            this.contextPanelNumber.Controls.Add(this.labelNumberCaption);
            this.contextPanelNumber.Location = new System.Drawing.Point(8, 117);
            this.contextPanelNumber.Name = "contextPanelNumber";
            this.contextPanelNumber.Size = new System.Drawing.Size(211, 32);
            // 
            // textBoxNumber
            // 
            this.textBoxNumber.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxNumber.Location = new System.Drawing.Point(61, 10);
            this.textBoxNumber.MaxLength = 50;
            this.textBoxNumber.Name = "textBoxNumber";
            this.textBoxNumber.Size = new System.Drawing.Size(146, 19);
            this.textBoxNumber.TabIndex = 7;
            // 
            // labelNumberCaption
            // 
            this.labelNumberCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelNumberCaption.Location = new System.Drawing.Point(4, 3);
            this.labelNumberCaption.Name = "labelNumberCaption";
            this.labelNumberCaption.Size = new System.Drawing.Size(51, 26);
            this.labelNumberCaption.Text = "Number:";
            // 
            // contextPanelNotes
            // 
            this.contextPanelNotes.Controls.Add(this.textBoxNotes);
            this.contextPanelNotes.Controls.Add(this.labelNotes);
            this.contextPanelNotes.Location = new System.Drawing.Point(8, 167);
            this.contextPanelNotes.Name = "contextPanelNotes";
            this.contextPanelNotes.Size = new System.Drawing.Size(211, 69);
            // 
            // textBoxNotes
            // 
            this.textBoxNotes.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxNotes.Location = new System.Drawing.Point(61, 3);
            this.textBoxNotes.Multiline = true;
            this.textBoxNotes.Name = "textBoxNotes";
            this.textBoxNotes.Size = new System.Drawing.Size(146, 62);
            this.textBoxNotes.TabIndex = 12;
            // 
            // labelNotes
            // 
            this.labelNotes.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelNotes.Location = new System.Drawing.Point(4, 3);
            this.labelNotes.Name = "labelNotes";
            this.labelNotes.Size = new System.Drawing.Size(51, 20);
            this.labelNotes.Text = "Notes:";
            // 
            // AgentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoScrollMargin = new System.Drawing.Size(0, 10);
            this.ClientSize = new System.Drawing.Size(243, 306);
            this.Controls.Add(this.contextPanelNotes);
            this.Controls.Add(this.contextPanelNumber);
            this.Controls.Add(this.contextPanelCollectorsName);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.panelTop);
            this.Menu = this.mainMenu1;
            this.Name = "AgentForm";
            this.Text = "Diversity Mobile";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.AgentForm_Closing);
            this.panelTop.ResumeLayout(false);
            this.contextPanelCollectorsName.ResumeLayout(false);
            this.contextPanelNumber.ResumeLayout(false);
            this.contextPanelNotes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.PictureBox pictureBoxFormImage;
        private System.Windows.Forms.Label labelCaption;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private UBT.AI4.Toolbox.Controls.ContextPanel contextPanelCollectorsName;
        private UBT.AI4.Toolbox.Controls.ContextPanel contextPanelNumber;
        private UBT.AI4.Toolbox.Controls.ContextPanel contextPanelNotes;
        private System.Windows.Forms.TextBox textBoxCollectorsName;
        private System.Windows.Forms.Label labelCollectorsNameCaption;
        private System.Windows.Forms.Button buttonChangeName;
        private System.Windows.Forms.TextBox textBoxNumber;
        private System.Windows.Forms.Label labelNumberCaption;
        private System.Windows.Forms.TextBox textBoxNotes;
        private System.Windows.Forms.Label labelNotes;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
    }
}