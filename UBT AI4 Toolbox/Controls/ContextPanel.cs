using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

using System.Drawing;

namespace UBT.AI4.Toolbox.Controls
{
    public class ContextPanel : Panel
    {
        private MenuItem menuItemSetText;
        private MenuItem menuItemVisible;
        private MenuItem menuItemSetDefaultValue;
        public ContextMenu contextMenuCustomizeContext;
        private Label labelPanelCaption;
        private bool _isVisible;
        private String _defaultValue;
    
        public ContextPanel():base()
        {
            InitializeComponent();
            this._isVisible = true;
        }

        #region Member

        public override String Text
        {
            set {

                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl.GetType() == typeof(Label))
                    {
                        ctrl.Text = value;
                    }
                }
            }

            get
            {
                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl.GetType() == typeof(Label))
                    {
                        return ctrl.Text;
                    }
                }
                return null;
            }
        }

        public String DefaultValue
        {
            set {
                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl.GetType() == typeof(TextBox))
                    {
                        ctrl.Text = this._defaultValue = value;
                    }
                    else if (ctrl.GetType() == typeof(ComboBox))
                    {
                        if (((ComboBox)ctrl).Items.Contains(value))
                        {
                            ((ComboBox)ctrl).SelectedItem = value;
                        }
                        else
                        {
                            if (((ComboBox)ctrl).DropDownStyle == ComboBoxStyle.DropDown)
                            {
                                ((ComboBox)ctrl).Items.Add(value);
                                ((ComboBox)ctrl).SelectedItem = this._defaultValue = value;
                            }
                        }
                    }
                    else if (ctrl.GetType() == typeof(DateTimePicker))
                    {
                        try
                        {
                            ((DateTimePicker)ctrl).Value = DateTime.Parse(value.ToString());
                            this._defaultValue = value.ToString();
                        }
                        catch (Exception)
                        {
                            ((DateTimePicker)ctrl).Value = DateTime.Now;
                            this._defaultValue = DateTime.Now.ToString();
                        }
                    }
                }
            }

            get
            {
                return this._defaultValue;
            }
        }

        public bool IsVisible
        {
            get { return this._isVisible; }
        }

        #endregion

        private void InitializeComponent()
        {
            this.menuItemSetText = new System.Windows.Forms.MenuItem();
            this.menuItemVisible = new System.Windows.Forms.MenuItem();
            this.menuItemSetDefaultValue = new System.Windows.Forms.MenuItem();
            this.contextMenuCustomizeContext = new System.Windows.Forms.ContextMenu();
            this.labelPanelCaption = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // menuItemSetText
            // 
            this.menuItemSetText.Text = "Set Text";
            this.menuItemSetText.Click += new System.EventHandler(this.menuItemSetText_Click);
            // 
            // menuItemVisible
            // 
            this.menuItemVisible.Checked = true;
            this.menuItemVisible.Text = "Show components";
            this.menuItemVisible.Click += new System.EventHandler(this.menuItemVisible_Click);
            // 
            // menuItemSetDefaultValue
            // 
            this.menuItemSetDefaultValue.Text = "Set DefaultValue";
            this.menuItemSetDefaultValue.Click += new System.EventHandler(this.menuItemSetDefaultValue_Click);
            // 
            // contextMenuCustomizeContext
            // 
            this.contextMenuCustomizeContext.MenuItems.Add(this.menuItemVisible);
            this.contextMenuCustomizeContext.MenuItems.Add(this.menuItemSetText);
            this.contextMenuCustomizeContext.MenuItems.Add(this.menuItemSetDefaultValue);
            this.contextMenuCustomizeContext.Popup += new System.EventHandler(this.contextMenuCustomizeContext_Popup);
            // 
            // labelPanelCaption
            // 
            this.labelPanelCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelPanelCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.labelPanelCaption.Location = new System.Drawing.Point(1, 1);
            this.labelPanelCaption.Name = "labelPanelCaption";
            this.labelPanelCaption.Size = new System.Drawing.Size(100, 22);
            // 
            // ContextPanel
            // 
            this.ResumeLayout(false);
        }

        private void menuItemSetText_Click(object sender, EventArgs e)
        {
            TextInputDialog tid = new TextInputDialog("Set Label Text");
            if (tid.ShowDialog() == DialogResult.OK)
            {
                this.Text = tid.Value;
            }
        }

        private void menuItemVisible_Click(object sender, EventArgs e)
        {
            if (this.menuItemVisible.Checked)
            {
                this.menuItemVisible.Checked = false;
                
                // Panel shall not be shown
                foreach (Control ctrl in this.Controls)
                {
                    ctrl.Visible = false;
                }

                labelPanelCaption.Text = ">>" + this.Name;
                this.Controls.Add(labelPanelCaption);
                
                this._isVisible = false;
            }
            else
            {
                this.menuItemVisible.Checked = true;
                // Panel is visible
                foreach (Control ctrl in this.Controls)
                {
                    ctrl.Visible = true;
                }

                labelPanelCaption.Text = "";
                this.Controls.Remove(labelPanelCaption);
                this._isVisible = true;
            } 
        }

        private void menuItemSetDefaultValue_Click(object sender, EventArgs e)
        {
            TextInputDialog tid = new TextInputDialog("Set Default Value");
            if (tid.ShowDialog() == DialogResult.OK)
            {
                this.DefaultValue = tid.Value;
            }
        }

        private void contextMenuCustomizeContext_Popup(object sender, EventArgs e)
        {
            this.OnClick(e);
            if (this._isVisible)
            {
                menuItemSetDefaultValue.Enabled = true;
                menuItemSetText.Enabled = true;
            }
            else
            {
                menuItemSetDefaultValue.Enabled = false;
                menuItemSetText.Enabled = false;
            }
        }

        public void ContextPanel_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in this.Parent.Controls)
            {
                if (ctrl.GetType() == typeof(ContextPanel))
                {
                    ctrl.BackColor = System.Drawing.SystemColors.Window;
                }
            }
            this.BackColor = System.Drawing.SystemColors.Highlight;
        }
    }
}
