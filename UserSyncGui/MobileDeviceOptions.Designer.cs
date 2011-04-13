namespace UserSyncGui
{
    partial class MobileDeviceOptions
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
            this.panel = new System.Windows.Forms.Panel();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxHeight = new System.Windows.Forms.TextBox();
            this.labelDeviceHeightCaption = new System.Windows.Forms.Label();
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            this.labelDeviceWidthCaption = new System.Windows.Forms.Label();
            this.formToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCloseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelCloseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panel.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.buttonCancel);
            this.panel.Controls.Add(this.buttonSave);
            this.panel.Controls.Add(this.textBoxHeight);
            this.panel.Controls.Add(this.labelDeviceHeightCaption);
            this.panel.Controls.Add(this.textBoxWidth);
            this.panel.Controls.Add(this.labelDeviceWidthCaption);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 24);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(287, 77);
            this.panel.TabIndex = 1;
            // 
            // buttonSave
            // 
            this.buttonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSave.Location = new System.Drawing.Point(206, 43);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 12;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.button_Click);
            // 
            // textBoxHeight
            // 
            this.textBoxHeight.Location = new System.Drawing.Point(111, 45);
            this.textBoxHeight.Name = "textBoxHeight";
            this.textBoxHeight.Size = new System.Drawing.Size(83, 20);
            this.textBoxHeight.TabIndex = 11;
            // 
            // labelDeviceHeightCaption
            // 
            this.labelDeviceHeightCaption.AutoSize = true;
            this.labelDeviceHeightCaption.Location = new System.Drawing.Point(8, 48);
            this.labelDeviceHeightCaption.Name = "labelDeviceHeightCaption";
            this.labelDeviceHeightCaption.Size = new System.Drawing.Size(98, 13);
            this.labelDeviceHeightCaption.TabIndex = 10;
            this.labelDeviceHeightCaption.Text = "Screen Height (px):";
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.Location = new System.Drawing.Point(111, 19);
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.Size = new System.Drawing.Size(83, 20);
            this.textBoxWidth.TabIndex = 9;
            // 
            // labelDeviceWidthCaption
            // 
            this.labelDeviceWidthCaption.AutoSize = true;
            this.labelDeviceWidthCaption.Location = new System.Drawing.Point(8, 22);
            this.labelDeviceWidthCaption.Name = "labelDeviceWidthCaption";
            this.labelDeviceWidthCaption.Size = new System.Drawing.Size(95, 13);
            this.labelDeviceWidthCaption.TabIndex = 8;
            this.labelDeviceWidthCaption.Text = "Screen Width (px):";
            // 
            // formToolStripMenuItem
            // 
            this.formToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveCloseToolStripMenuItem,
            this.cancelCloseToolStripMenuItem});
            this.formToolStripMenuItem.Name = "formToolStripMenuItem";
            this.formToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.formToolStripMenuItem.Text = "&Form";
            // 
            // saveCloseToolStripMenuItem
            // 
            this.saveCloseToolStripMenuItem.Name = "saveCloseToolStripMenuItem";
            this.saveCloseToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.saveCloseToolStripMenuItem.Text = "&Save + Close";
            this.saveCloseToolStripMenuItem.Click += new System.EventHandler(this.saveCloseToolStripMenuItem_Click);
            // 
            // cancelCloseToolStripMenuItem
            // 
            this.cancelCloseToolStripMenuItem.Name = "cancelCloseToolStripMenuItem";
            this.cancelCloseToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.cancelCloseToolStripMenuItem.Text = "&Cancel";
            this.cancelCloseToolStripMenuItem.Click += new System.EventHandler(this.cancelCloseToolStripMenuItem_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(287, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(206, 17);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 13;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.button_Click);
            // 
            // MobileDeviceOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 101);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MobileDeviceOptions";
            this.Text = "MobileDeviceOptions";
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.TextBox textBoxHeight;
        private System.Windows.Forms.Label labelDeviceHeightCaption;
        private System.Windows.Forms.TextBox textBoxWidth;
        private System.Windows.Forms.Label labelDeviceWidthCaption;
        private System.Windows.Forms.ToolStripMenuItem formToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelCloseToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem saveCloseToolStripMenuItem;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;

    }
}