namespace UBT.AI4.Bio.DivMobi.Forms.Forms
{
    partial class DirectPlayForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DirectPlayForm));
            this.toolBarImages = new System.Windows.Forms.ToolBar();
            this.toolBarButtonFirst = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonPrevious = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonNext = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonLast = new System.Windows.Forms.ToolBarButton();
            this.imageListToolbarButtons = new System.Windows.Forms.ImageList();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.labelPosition = new System.Windows.Forms.Label();
            this.panelTop = new System.Windows.Forms.Panel();
            this.pictureBoxFormImage = new System.Windows.Forms.PictureBox();
            this.labelCaption = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
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
            // buttonPlay
            // 
            this.buttonPlay.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonPlay.Location = new System.Drawing.Point(61, 98);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(105, 36);
            this.buttonPlay.TabIndex = 2;
            this.buttonPlay.Text = "Play";
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // labelPosition
            // 
            this.labelPosition.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelPosition.Location = new System.Drawing.Point(61, 66);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Size = new System.Drawing.Size(105, 20);
            this.labelPosition.Text = "Position:";
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
            // labelCaption
            // 
            this.labelCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelCaption.Location = new System.Drawing.Point(48, 6);
            this.labelCaption.Name = "labelCaption";
            this.labelCaption.Size = new System.Drawing.Size(152, 20);
            this.labelCaption.Text = "Direct Play";
            this.labelCaption.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // DirectPlayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(240, 188);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.labelPosition);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.toolBarImages);
            this.Name = "DirectPlayForm";
            this.Text = "Diversity Mobile";
            this.Closed += new System.EventHandler(this.DirectPlayForm_Closed);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.DirectPlayForm_Closing);
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolBar toolBarImages;
        private System.Windows.Forms.ToolBarButton toolBarButtonFirst;
        private System.Windows.Forms.ToolBarButton toolBarButtonPrevious;
        private System.Windows.Forms.ToolBarButton toolBarButtonNext;
        private System.Windows.Forms.ToolBarButton toolBarButtonLast;
        private System.Windows.Forms.ImageList imageListToolbarButtons;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Label labelPosition;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.PictureBox pictureBoxFormImage;
        private System.Windows.Forms.Label labelCaption;
    }
}