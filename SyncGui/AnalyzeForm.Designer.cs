namespace UBT.AI4.Bio.DivMobi.SyncGui
{
    partial class AnalyzeForm
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
            this.buttonResolve = new System.Windows.Forms.Button();
            this.propertyGridRepository = new System.Windows.Forms.PropertyGrid();
            this.propertyGridEdit = new System.Windows.Forms.PropertyGrid();
            this.propertyGridMobileDB = new System.Windows.Forms.PropertyGrid();
            this.buttonTakeRepository = new System.Windows.Forms.Button();
            this.buttonTakeMobileDB = new System.Windows.Forms.Button();
            this.labelRepository = new System.Windows.Forms.Label();
            this.labelMerge = new System.Windows.Forms.Label();
            this.labelMobile = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonResolve
            // 
            this.buttonResolve.Location = new System.Drawing.Point(752, 451);
            this.buttonResolve.Name = "buttonResolve";
            this.buttonResolve.Size = new System.Drawing.Size(90, 27);
            this.buttonResolve.TabIndex = 0;
            this.buttonResolve.Text = "Resolve";
            this.buttonResolve.UseVisualStyleBackColor = true;
            this.buttonResolve.Click += new System.EventHandler(this.buttonResolve_Click);
            // 
            // propertyGridRepository
            // 
            this.propertyGridRepository.Location = new System.Drawing.Point(12, 35);
            this.propertyGridRepository.Name = "propertyGridRepository";
            this.propertyGridRepository.Size = new System.Drawing.Size(223, 397);
            this.propertyGridRepository.TabIndex = 1;
            // 
            // propertyGridEdit
            // 
            this.propertyGridEdit.Location = new System.Drawing.Point(302, 35);
            this.propertyGridEdit.Name = "propertyGridEdit";
            this.propertyGridEdit.Size = new System.Drawing.Size(229, 397);
            this.propertyGridEdit.TabIndex = 2;
            // 
            // propertyGridMobileDB
            // 
            this.propertyGridMobileDB.Location = new System.Drawing.Point(578, 35);
            this.propertyGridMobileDB.Name = "propertyGridMobileDB";
            this.propertyGridMobileDB.Size = new System.Drawing.Size(231, 397);
            this.propertyGridMobileDB.TabIndex = 3;
            // 
            // buttonTakeRepository
            // 
            this.buttonTakeRepository.Location = new System.Drawing.Point(251, 168);
            this.buttonTakeRepository.Name = "buttonTakeRepository";
            this.buttonTakeRepository.Size = new System.Drawing.Size(37, 23);
            this.buttonTakeRepository.TabIndex = 4;
            this.buttonTakeRepository.Text = "->";
            this.buttonTakeRepository.UseVisualStyleBackColor = true;
            // 
            // buttonTakeMobileDB
            // 
            this.buttonTakeMobileDB.Location = new System.Drawing.Point(537, 167);
            this.buttonTakeMobileDB.Name = "buttonTakeMobileDB";
            this.buttonTakeMobileDB.Size = new System.Drawing.Size(35, 23);
            this.buttonTakeMobileDB.TabIndex = 5;
            this.buttonTakeMobileDB.Text = "<-";
            this.buttonTakeMobileDB.UseVisualStyleBackColor = true;
            // 
            // labelRepository
            // 
            this.labelRepository.AutoSize = true;
            this.labelRepository.Location = new System.Drawing.Point(79, 17);
            this.labelRepository.Name = "labelRepository";
            this.labelRepository.Size = new System.Drawing.Size(57, 13);
            this.labelRepository.TabIndex = 6;
            this.labelRepository.Text = "Repository";
            // 
            // labelMerge
            // 
            this.labelMerge.AutoSize = true;
            this.labelMerge.Location = new System.Drawing.Point(363, 17);
            this.labelMerge.Name = "labelMerge";
            this.labelMerge.Size = new System.Drawing.Size(37, 13);
            this.labelMerge.TabIndex = 7;
            this.labelMerge.Text = "Merge";
            // 
            // labelMobile
            // 
            this.labelMobile.AutoSize = true;
            this.labelMobile.Location = new System.Drawing.Point(646, 17);
            this.labelMobile.Name = "labelMobile";
            this.labelMobile.Size = new System.Drawing.Size(38, 13);
            this.labelMobile.TabIndex = 8;
            this.labelMobile.Text = "Mobile";
            // 
            // AnalyzeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 490);
            this.Controls.Add(this.labelMobile);
            this.Controls.Add(this.labelMerge);
            this.Controls.Add(this.labelRepository);
            this.Controls.Add(this.buttonTakeMobileDB);
            this.Controls.Add(this.buttonTakeRepository);
            this.Controls.Add(this.propertyGridMobileDB);
            this.Controls.Add(this.propertyGridEdit);
            this.Controls.Add(this.propertyGridRepository);
            this.Controls.Add(this.buttonResolve);
            this.Name = "AnalyzeForm";
            this.Text = "AnalyzeForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonResolve;
        private System.Windows.Forms.PropertyGrid propertyGridRepository;
        private System.Windows.Forms.PropertyGrid propertyGridEdit;
        private System.Windows.Forms.PropertyGrid propertyGridMobileDB;
        private System.Windows.Forms.Button buttonTakeRepository;
        private System.Windows.Forms.Button buttonTakeMobileDB;
        private System.Windows.Forms.Label labelRepository;
        private System.Windows.Forms.Label labelMerge;
        private System.Windows.Forms.Label labelMobile;
    }
}