namespace UBT.AI4.Bio.DivMobi.Forms.Forms
{
    partial class ImageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageForm));
            this.toolBarImages = new System.Windows.Forms.ToolBar();
            this.toolBarButtonFirst = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonPrevious = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonNext = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonLast = new System.Windows.Forms.ToolBarButton();
            this.imageListToolbarButtons = new System.Windows.Forms.ImageList();
            this.imageListMediumToolbarButtons = new System.Windows.Forms.ImageList();
            this.imageListLargeToolbarButtons = new System.Windows.Forms.ImageList();
            this.pictureBoxImages = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // toolBarImages
            // 
            this.toolBarImages.Buttons.Add(this.toolBarButtonFirst);
            this.toolBarImages.Buttons.Add(this.toolBarButtonPrevious);
            this.toolBarImages.Buttons.Add(this.toolBarButtonNext);
            this.toolBarImages.Buttons.Add(this.toolBarButtonLast);
            this.toolBarImages.ImageList = this.imageListToolbarButtons;
            this.toolBarImages.Name = "toolBarImages";
            this.toolBarImages.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBarImages_ButtonClick);
            // 
            // toolBarButtonFirst
            // 
            this.toolBarButtonFirst.ImageIndex = 0;
            // 
            // toolBarButtonPrevious
            // 
            this.toolBarButtonPrevious.ImageIndex = 1;
            // 
            // toolBarButtonNext
            // 
            this.toolBarButtonNext.ImageIndex = 2;
            // 
            // toolBarButtonLast
            // 
            this.toolBarButtonLast.ImageIndex = 3;
            this.imageListToolbarButtons.Images.Clear();
            this.imageListToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
            this.imageListToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
            this.imageListToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource2"))));
            this.imageListToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource3"))));
            // 
            // imageListMediumToolbarButtons
            // 
            this.imageListMediumToolbarButtons.ImageSize = new System.Drawing.Size(32, 32);
            this.imageListMediumToolbarButtons.Images.Clear();
            this.imageListMediumToolbarButtons.Images.Add(((System.Drawing.Icon)(resources.GetObject("resource4"))));
            this.imageListMediumToolbarButtons.Images.Add(((System.Drawing.Icon)(resources.GetObject("resource5"))));
            this.imageListMediumToolbarButtons.Images.Add(((System.Drawing.Icon)(resources.GetObject("resource6"))));
            this.imageListMediumToolbarButtons.Images.Add(((System.Drawing.Icon)(resources.GetObject("resource7"))));
            // 
            // imageListLargeToolbarButtons
            // 
            this.imageListLargeToolbarButtons.ImageSize = new System.Drawing.Size(48, 48);
            this.imageListLargeToolbarButtons.Images.Clear();
            this.imageListLargeToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource8"))));
            this.imageListLargeToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource9"))));
            this.imageListLargeToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource10"))));
            this.imageListLargeToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource11"))));
            // 
            // pictureBoxImages
            // 
            this.pictureBoxImages.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxImages.Name = "pictureBoxImages";
            this.pictureBoxImages.Size = new System.Drawing.Size(100, 50);
            // 
            // ImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(240, 188);
            this.Controls.Add(this.pictureBoxImages);
            this.Controls.Add(this.toolBarImages);
            this.Name = "ImageForm";
            this.Text = "Diversity Mobile";
            this.Load += new System.EventHandler(this.ImageForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolBar toolBarImages;
        private System.Windows.Forms.ToolBarButton toolBarButtonFirst;
        private System.Windows.Forms.ToolBarButton toolBarButtonPrevious;
        private System.Windows.Forms.ToolBarButton toolBarButtonNext;
        private System.Windows.Forms.ToolBarButton toolBarButtonLast;
        private System.Windows.Forms.ImageList imageListToolbarButtons;
        private System.Windows.Forms.ImageList imageListMediumToolbarButtons;
        private System.Windows.Forms.ImageList imageListLargeToolbarButtons;
        private System.Windows.Forms.PictureBox pictureBoxImages;
    }
}