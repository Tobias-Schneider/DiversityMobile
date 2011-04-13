namespace UserSyncGui
{
    partial class ProgressInformationForm
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
            this.progressBarProgressInformation = new System.Windows.Forms.ProgressBar();
            this.labelProgressInformation = new System.Windows.Forms.Label();
            this.labelProgressInformationCaption = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.labelActionInformation = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBarProgressInformation
            // 
            this.progressBarProgressInformation.Location = new System.Drawing.Point(12, 78);
            this.progressBarProgressInformation.Name = "progressBarProgressInformation";
            this.progressBarProgressInformation.Size = new System.Drawing.Size(307, 23);
            this.progressBarProgressInformation.TabIndex = 15;
            this.progressBarProgressInformation.UseWaitCursor = true;
            // 
            // labelProgressInformation
            // 
            this.labelProgressInformation.AutoSize = true;
            this.labelProgressInformation.Location = new System.Drawing.Point(12, 54);
            this.labelProgressInformation.Name = "labelProgressInformation";
            this.labelProgressInformation.Size = new System.Drawing.Size(108, 13);
            this.labelProgressInformation.TabIndex = 14;
            this.labelProgressInformation.Text = "Additional Information";
            this.labelProgressInformation.UseWaitCursor = true;
            // 
            // labelProgressInformationCaption
            // 
            this.labelProgressInformationCaption.AutoSize = true;
            this.labelProgressInformationCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelProgressInformationCaption.Location = new System.Drawing.Point(12, 7);
            this.labelProgressInformationCaption.Name = "labelProgressInformationCaption";
            this.labelProgressInformationCaption.Size = new System.Drawing.Size(159, 16);
            this.labelProgressInformationCaption.TabIndex = 13;
            this.labelProgressInformationCaption.Text = "Progress Information: ";
            this.labelProgressInformationCaption.UseWaitCursor = true;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(244, 107);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 16;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.UseWaitCursor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // labelActionInformation
            // 
            this.labelActionInformation.AutoSize = true;
            this.labelActionInformation.Location = new System.Drawing.Point(12, 31);
            this.labelActionInformation.Name = "labelActionInformation";
            this.labelActionInformation.Size = new System.Drawing.Size(37, 13);
            this.labelActionInformation.TabIndex = 17;
            this.labelActionInformation.Text = "Action";
            this.labelActionInformation.UseWaitCursor = true;
            // 
            // ProgressInformationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 133);
            this.ControlBox = false;
            this.Controls.Add(this.labelActionInformation);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.progressBarProgressInformation);
            this.Controls.Add(this.labelProgressInformation);
            this.Controls.Add(this.labelProgressInformationCaption);
            this.MaximizeBox = false;
            this.Name = "ProgressInformationForm";
            this.TopMost = true;
            this.UseWaitCursor = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBarProgressInformation;
        private System.Windows.Forms.Label labelProgressInformation;
        private System.Windows.Forms.Label labelProgressInformationCaption;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label labelActionInformation;
    }
}