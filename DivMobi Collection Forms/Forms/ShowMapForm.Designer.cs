namespace UBT.AI4.Bio.DivMobi.Forms.Forms
{
    partial class ShowMapForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowMapForm));
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
            this.imageListSmall = new System.Windows.Forms.ImageList();
            this.imageListMedium = new System.Windows.Forms.ImageList();
            this.pictureBoxMap = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // toolBar1
            // 
            this.toolBar1.Buttons.Add(this.toolBarButton2);
            this.toolBar1.Buttons.Add(this.toolBarButton1);
            this.toolBar1.Buttons.Add(this.toolBarButton3);
            this.toolBar1.ImageList = this.imageListSmall;
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // toolBarButton2
            // 
            this.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.ImageIndex = 0;
            // 
            // toolBarButton3
            // 
            this.toolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            this.imageListSmall.Images.Clear();
            this.imageListSmall.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
            // 
            // imageListMedium
            // 
            this.imageListMedium.ImageSize = new System.Drawing.Size(48, 48);
            this.imageListMedium.Images.Clear();
            this.imageListMedium.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
            // 
            // pictureBoxMap
            // 
            this.pictureBoxMap.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxMap.Name = "pictureBoxMap";
            this.pictureBoxMap.Size = new System.Drawing.Size(220, 138);
            this.pictureBoxMap.Click += new System.EventHandler(this.pictureBoxMap_Click);
            this.pictureBoxMap.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxMap_Paint);
            // 
            // ShowMapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 188);
            this.Controls.Add(this.pictureBoxMap);
            this.Controls.Add(this.toolBar1);
            this.Name = "ShowMapForm";
            this.Text = "ShowMode";
            this.Load += new System.EventHandler(this.ShowMapForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ShowMapForm_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolBar toolBar1;
        private System.Windows.Forms.ToolBarButton toolBarButton1;
        private System.Windows.Forms.ToolBarButton toolBarButton2;
        private System.Windows.Forms.ToolBarButton toolBarButton3;
        private System.Windows.Forms.ImageList imageListSmall;
        private System.Windows.Forms.ImageList imageListMedium;
        private System.Windows.Forms.PictureBox pictureBoxMap;
    }
}