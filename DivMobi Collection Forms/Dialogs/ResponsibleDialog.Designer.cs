namespace UBT.AI4.Bio.DivMobi.Forms.Dialogs
{
    partial class ResponsibleDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResponsibleDialog));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.labelRespNameCaption = new System.Windows.Forms.Label();
            this.labelRespURLCaption = new System.Windows.Forms.Label();
            this.textBoxRespName = new System.Windows.Forms.TextBox();
            this.textBoxRespURL = new System.Windows.Forms.TextBox();
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
            this.buttonCancel.Location = new System.Drawing.Point(125, 121);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(96, 20);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonOk.Location = new System.Drawing.Point(7, 121);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(96, 20);
            this.buttonOk.TabIndex = 3;
            this.buttonOk.Text = "OK";
            // 
            // labelRespNameCaption
            // 
            this.labelRespNameCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelRespNameCaption.Location = new System.Drawing.Point(7, 39);
            this.labelRespNameCaption.Name = "labelRespNameCaption";
            this.labelRespNameCaption.Size = new System.Drawing.Size(81, 20);
            this.labelRespNameCaption.Text = "Responsible:";
            // 
            // labelRespURLCaption
            // 
            this.labelRespURLCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelRespURLCaption.Location = new System.Drawing.Point(7, 70);
            this.labelRespURLCaption.Name = "labelRespURLCaption";
            this.labelRespURLCaption.Size = new System.Drawing.Size(83, 20);
            this.labelRespURLCaption.Text = "URL:";
            // 
            // textBoxRespName
            // 
            this.textBoxRespName.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxRespName.Location = new System.Drawing.Point(94, 38);
            this.textBoxRespName.MaxLength = 255;
            this.textBoxRespName.Name = "textBoxRespName";
            this.textBoxRespName.Size = new System.Drawing.Size(124, 19);
            this.textBoxRespName.TabIndex = 1;
            // 
            // textBoxRespURL
            // 
            this.textBoxRespURL.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxRespURL.Location = new System.Drawing.Point(7, 93);
            this.textBoxRespURL.MaxLength = 255;
            this.textBoxRespURL.Name = "textBoxRespURL";
            this.textBoxRespURL.Size = new System.Drawing.Size(214, 19);
            this.textBoxRespURL.TabIndex = 2;
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
            this.labelCaption.Text = "Responsible";
            this.labelCaption.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ResponsibleDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(230, 150);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.textBoxRespURL);
            this.Controls.Add(this.textBoxRespName);
            this.Controls.Add(this.labelRespURLCaption);
            this.Controls.Add(this.labelRespNameCaption);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(5, 26);
            this.Menu = this.mainMenu1;
            this.Name = "ResponsibleDialog";
            this.Text = "Diversity Mobile";
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Label labelRespNameCaption;
        private System.Windows.Forms.Label labelRespURLCaption;
        private System.Windows.Forms.TextBox textBoxRespName;
        private System.Windows.Forms.TextBox textBoxRespURL;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.PictureBox pictureBoxFormImage;
        private System.Windows.Forms.Label labelCaption;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
        private System.Windows.Forms.MainMenu mainMenu1;
    }
}