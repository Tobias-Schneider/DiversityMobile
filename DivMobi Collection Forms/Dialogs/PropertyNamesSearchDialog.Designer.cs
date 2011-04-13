namespace UBT.AI4.Bio.DivMobi.Forms.Dialogs
{
    partial class PropertyNamesSearchDialog
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
            this.labelPropertyGroup = new System.Windows.Forms.Label();
            this.treeViewPropertyNames = new System.Windows.Forms.TreeView();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.SuspendLayout();
            // 
            // labelPropertyGroup
            // 
            this.labelPropertyGroup.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelPropertyGroup.Location = new System.Drawing.Point(5, 3);
            this.labelPropertyGroup.Name = "labelPropertyGroup";
            this.labelPropertyGroup.Size = new System.Drawing.Size(206, 20);
            this.labelPropertyGroup.Text = "PropertyGroup";
            // 
            // treeViewPropertyNames
            // 
            this.treeViewPropertyNames.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.treeViewPropertyNames.Location = new System.Drawing.Point(9, 26);
            this.treeViewPropertyNames.Name = "treeViewPropertyNames";
            this.treeViewPropertyNames.ShowPlusMinus = false;
            this.treeViewPropertyNames.ShowRootLines = false;
            this.treeViewPropertyNames.Size = new System.Drawing.Size(206, 111);
            this.treeViewPropertyNames.TabIndex = 1;
            this.treeViewPropertyNames.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewPropertyNames_AfterSelect);
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonOK.Location = new System.Drawing.Point(9, 143);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(83, 20);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonCancel.Location = new System.Drawing.Point(132, 143);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(83, 20);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            // 
            // PropertyNamesSearchDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoScrollMargin = new System.Drawing.Size(5, 5);
            this.ClientSize = new System.Drawing.Size(230, 170);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.treeViewPropertyNames);
            this.Controls.Add(this.labelPropertyGroup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Menu = this.mainMenu1;
            this.Name = "PropertyNamesSearchDialog";
            this.Text = "Diversity Mobile";
            this.Load += new System.EventHandler(this.PropertyNamesSearchDialog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelPropertyGroup;
        private System.Windows.Forms.TreeView treeViewPropertyNames;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
        private System.Windows.Forms.MainMenu mainMenu1;
    }
}
