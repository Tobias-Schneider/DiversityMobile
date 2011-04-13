namespace UBT.AI4.Bio.DivMobi.Forms.Dialogs
{
    partial class NewAnalysisDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewAnalysisDialog));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.labelValueCaption = new System.Windows.Forms.Label();
            this.labelAnalysisDescription = new System.Windows.Forms.Label();
            this.dateTimePickerAnalysis = new System.Windows.Forms.DateTimePicker();
            this.comboBoxAnalysisValue = new UBT.AI4.Toolbox.Controls.ClickComboBox();
            this.comboBoxAnalysis = new UBT.AI4.Toolbox.Controls.ClickComboBox();
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
            this.buttonCancel.Location = new System.Drawing.Point(128, 206);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(86, 20);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonOk.Location = new System.Drawing.Point(9, 206);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(88, 20);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "OK";
            // 
            // labelValueCaption
            // 
            this.labelValueCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelValueCaption.Location = new System.Drawing.Point(10, 92);
            this.labelValueCaption.Name = "labelValueCaption";
            this.labelValueCaption.Size = new System.Drawing.Size(43, 20);
            this.labelValueCaption.Text = "Value:";
            // 
            // labelAnalysisDescription
            // 
            this.labelAnalysisDescription.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelAnalysisDescription.Location = new System.Drawing.Point(9, 129);
            this.labelAnalysisDescription.Name = "labelAnalysisDescription";
            this.labelAnalysisDescription.Size = new System.Drawing.Size(204, 22);
            this.labelAnalysisDescription.Text = "<Description>";
            // 
            // dateTimePickerAnalysis
            // 
            this.dateTimePickerAnalysis.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.dateTimePickerAnalysis.Location = new System.Drawing.Point(9, 165);
            this.dateTimePickerAnalysis.Name = "dateTimePickerAnalysis";
            this.dateTimePickerAnalysis.Size = new System.Drawing.Size(205, 25);
            this.dateTimePickerAnalysis.TabIndex = 3;
            // 
            // comboBoxAnalysisValue
            // 
            this.comboBoxAnalysisValue.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.comboBoxAnalysisValue.Location = new System.Drawing.Point(58, 85);
            this.comboBoxAnalysisValue.Name = "comboBoxAnalysisValue";
            this.comboBoxAnalysisValue.Size = new System.Drawing.Size(155, 27);
            this.comboBoxAnalysisValue.TabIndex = 2;
            this.comboBoxAnalysisValue.GotFocus += new System.EventHandler(this.comboBoxAnalysisValue_GotFocus);
            this.comboBoxAnalysisValue.TextChanged += new System.EventHandler(this.comboBoxAnalysisValue_TextChanged);
            // 
            // comboBoxAnalysis
            // 
            this.comboBoxAnalysis.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.comboBoxAnalysis.Location = new System.Drawing.Point(10, 40);
            this.comboBoxAnalysis.Name = "comboBoxAnalysis";
            this.comboBoxAnalysis.Size = new System.Drawing.Size(204, 29);
            this.comboBoxAnalysis.TabIndex = 1;
            this.comboBoxAnalysis.SelectedIndexChanged += new System.EventHandler(this.comboBoxAnalysis_SelectedIndexChanged);
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
            this.labelCaption.Text = "New Analysis";
            this.labelCaption.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // NewAnalysisDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoScrollMargin = new System.Drawing.Size(0, 5);
            this.ClientSize = new System.Drawing.Size(237, 230);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.comboBoxAnalysis);
            this.Controls.Add(this.comboBoxAnalysisValue);
            this.Controls.Add(this.dateTimePickerAnalysis);
            this.Controls.Add(this.labelAnalysisDescription);
            this.Controls.Add(this.labelValueCaption);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Menu = this.mainMenu1;
            this.Name = "NewAnalysisDialog";
            this.Text = "Diversity Mobile";
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Label labelValueCaption;
        private System.Windows.Forms.Label labelAnalysisDescription;
        private System.Windows.Forms.DateTimePicker dateTimePickerAnalysis;
        private UBT.AI4.Toolbox.Controls.ClickComboBox comboBoxAnalysisValue;
        private UBT.AI4.Toolbox.Controls.ClickComboBox comboBoxAnalysis;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.PictureBox pictureBoxFormImage;
        private System.Windows.Forms.Label labelCaption;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
        private System.Windows.Forms.MainMenu mainMenu1;
    }
}