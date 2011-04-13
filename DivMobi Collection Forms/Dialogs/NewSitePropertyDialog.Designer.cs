namespace UBT.AI4.Bio.DivMobi.Forms.Dialogs
{
    partial class NewSitePropertyDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewSitePropertyDialog));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.treeViewPropertyHierarchy = new System.Windows.Forms.TreeView();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.comboBoxDisplayText = new UBT.AI4.Toolbox.Controls.ClickComboBox();
            this.comboBoxPropertyGroup = new UBT.AI4.Toolbox.Controls.ClickComboBox();
            this.panelTop = new System.Windows.Forms.Panel();
            this.pictureBoxFormImage = new System.Windows.Forms.PictureBox();
            this.labelCaption = new System.Windows.Forms.Label();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonCancel.Location = new System.Drawing.Point(127, 230);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(74, 20);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Enabled = false;
            this.buttonOk.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonOk.Location = new System.Drawing.Point(7, 230);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 20);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "OK";
            // 
            // treeViewPropertyHierarchy
            // 
            this.treeViewPropertyHierarchy.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.treeViewPropertyHierarchy.Location = new System.Drawing.Point(7, 170);
            this.treeViewPropertyHierarchy.Name = "treeViewPropertyHierarchy";
            this.treeViewPropertyHierarchy.Size = new System.Drawing.Size(206, 54);
            this.treeViewPropertyHierarchy.TabIndex = 8;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonSearch.Location = new System.Drawing.Point(138, 128);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(75, 20);
            this.buttonSearch.TabIndex = 3;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // comboBoxDisplayText
            // 
            this.comboBoxDisplayText.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.comboBoxDisplayText.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.comboBoxDisplayText.Location = new System.Drawing.Point(7, 79);
            this.comboBoxDisplayText.Name = "comboBoxDisplayText";
            this.comboBoxDisplayText.Size = new System.Drawing.Size(206, 32);
            this.comboBoxDisplayText.TabIndex = 2;
            this.comboBoxDisplayText.SelectedIndexChanged += new System.EventHandler(this.comboBoxDisplayText_SelectedIndexChanged);
            // 
            // comboBoxPropertyGroup
            // 
            this.comboBoxPropertyGroup.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.comboBoxPropertyGroup.Location = new System.Drawing.Point(7, 38);
            this.comboBoxPropertyGroup.Name = "comboBoxPropertyGroup";
            this.comboBoxPropertyGroup.Size = new System.Drawing.Size(206, 35);
            this.comboBoxPropertyGroup.TabIndex = 1;
            this.comboBoxPropertyGroup.SelectedIndexChanged += new System.EventHandler(this.comboBoxPropertyGroup_SelectedIndexChanged);
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
            this.labelCaption.Text = "New Site Property";
            this.labelCaption.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(7, 128);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(126, 21);
            this.textBoxSearch.TabIndex = 9;
            this.textBoxSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxSearch_KeyUp);
            // 
            // NewSitePropertyDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoScrollMargin = new System.Drawing.Size(0, 5);
            this.ClientSize = new System.Drawing.Size(229, 269);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.comboBoxPropertyGroup);
            this.Controls.Add(this.comboBoxDisplayText);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.treeViewPropertyHierarchy);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Menu = this.mainMenu1;
            this.Name = "NewSitePropertyDialog";
            this.Text = "Diversity Mobile";
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.TreeView treeViewPropertyHierarchy;
        private System.Windows.Forms.Button buttonSearch;
        private UBT.AI4.Toolbox.Controls.ClickComboBox comboBoxDisplayText;
        private UBT.AI4.Toolbox.Controls.ClickComboBox comboBoxPropertyGroup;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.PictureBox pictureBoxFormImage;
        private System.Windows.Forms.Label labelCaption;
        private System.Windows.Forms.TextBox textBoxSearch;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
        private System.Windows.Forms.MainMenu mainMenu1;
    }
}