namespace UBT.AI4.Bio.DivMobi.Forms.Forms
{
    partial class EditContextForm
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
            this.labelCaption = new System.Windows.Forms.Label();
            this.listViewContextForms = new System.Windows.Forms.ListView();
            this.columnFormTitle = new System.Windows.Forms.ColumnHeader();
            this.columnFormCustomized = new System.Windows.Forms.ColumnHeader();
            this.columnDescription = new System.Windows.Forms.ColumnHeader();
            this.mainMenuEditContextForm = new System.Windows.Forms.MainMenu();
            this.menuItemShowMenu = new System.Windows.Forms.MenuItem();
            this.menuItemCustomizeContext = new System.Windows.Forms.MenuItem();
            this.menuItemDeleteContext = new System.Windows.Forms.MenuItem();
            this.menuItemShowCustomizedForm = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // labelCaption
            // 
            this.labelCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelCaption.Location = new System.Drawing.Point(4, 9);
            this.labelCaption.Name = "labelCaption";
            this.labelCaption.Size = new System.Drawing.Size(154, 20);
            this.labelCaption.Text = "Editable Context Forms:";
            // 
            // listViewContextForms
            // 
            this.listViewContextForms.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewContextForms.Columns.Add(this.columnFormTitle);
            this.listViewContextForms.Columns.Add(this.columnFormCustomized);
            this.listViewContextForms.Columns.Add(this.columnDescription);
            this.listViewContextForms.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listViewContextForms.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.listViewContextForms.FullRowSelect = true;
            this.listViewContextForms.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewContextForms.Location = new System.Drawing.Point(0, 32);
            this.listViewContextForms.Name = "listViewContextForms";
            this.listViewContextForms.Size = new System.Drawing.Size(257, 156);
            this.listViewContextForms.TabIndex = 1;
            this.listViewContextForms.View = System.Windows.Forms.View.Details;
            this.listViewContextForms.ItemActivate += new System.EventHandler(this.listViewContextForms_ItemActivate);
            // 
            // columnFormTitle
            // 
            this.columnFormTitle.Text = "Form Title";
            this.columnFormTitle.Width = 150;
            // 
            // columnFormCustomized
            // 
            this.columnFormCustomized.Text = "";
            this.columnFormCustomized.Width = 60;
            // 
            // columnDescription
            // 
            this.columnDescription.Text = "Description";
            this.columnDescription.Width = 250;
            // 
            // mainMenuEditContextForm
            // 
            this.mainMenuEditContextForm.MenuItems.Add(this.menuItemShowMenu);
            // 
            // menuItemShowMenu
            // 
            this.menuItemShowMenu.MenuItems.Add(this.menuItemCustomizeContext);
            this.menuItemShowMenu.MenuItems.Add(this.menuItemDeleteContext);
            this.menuItemShowMenu.MenuItems.Add(this.menuItemShowCustomizedForm);
            this.menuItemShowMenu.Text = "Menu";
            // 
            // menuItemCustomizeContext
            // 
            this.menuItemCustomizeContext.Text = "Customize Context";
            this.menuItemCustomizeContext.Click += new System.EventHandler(this.menuItemCustomizeContext_Click);
            // 
            // menuItemDeleteContext
            // 
            this.menuItemDeleteContext.Text = "Delete Context";
            this.menuItemDeleteContext.Click += new System.EventHandler(this.menuItemDeleteContext_Click);
            // 
            // menuItemShowCustomizedForm
            // 
            this.menuItemShowCustomizedForm.Text = "Show customized Form";
            this.menuItemShowCustomizedForm.Click += new System.EventHandler(this.menuItemShowCustomizedForm_Click);
            // 
            // EditContextForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(257, 188);
            this.Controls.Add(this.listViewContextForms);
            this.Controls.Add(this.labelCaption);
            this.Menu = this.mainMenuEditContextForm;
            this.Name = "EditContextForm";
            this.Text = "Diversity Mobile";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelCaption;
        private System.Windows.Forms.ListView listViewContextForms;
        private System.Windows.Forms.ColumnHeader columnFormTitle;
        private System.Windows.Forms.ColumnHeader columnDescription;
        private System.Windows.Forms.MainMenu mainMenuEditContextForm;
        private System.Windows.Forms.MenuItem menuItemShowMenu;
        private System.Windows.Forms.MenuItem menuItemCustomizeContext;
        private System.Windows.Forms.MenuItem menuItemDeleteContext;
        private System.Windows.Forms.MenuItem menuItemShowCustomizedForm;
        private System.Windows.Forms.ColumnHeader columnFormCustomized;
    }
}