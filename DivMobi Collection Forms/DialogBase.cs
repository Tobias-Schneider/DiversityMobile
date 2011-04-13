using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UBT.AI4.Toolbox.Controls;
using UBT.AI4.Bio.DivMobi.DataManagement;

namespace UBT.AI4.Bio.DivMobi.Forms
{
    public partial class DialogBase : Form
    {
        public DialogBase()
        {
            InitializeComponent();
            
           
        }

        protected void adjustControlSizes()
        {
            Rectangle r = Screen.PrimaryScreen.Bounds;
            Size s = new Size();
            
            try
            {
                if (UserProfiles.Instance.Current.ToolbarIcons.ToLower().Equals("large"))
                {
                    foreach (Control c in this.Controls)
                    {
                        if (c.GetType().Equals(typeof(ClickComboBox)) || c.GetType().Equals(typeof(ListView)))
                        {
                            c.Font = new Font(c.Font.Name, 14, c.Font.Style);
                        }
                        
                    }
                }
                else if (UserProfiles.Instance.Current.ToolbarIcons.ToLower().Equals("medium"))
                {
                    foreach (Control c in this.Controls)
                    {
                        if (c.GetType().Equals(typeof(ClickComboBox)) || c.GetType().Equals(typeof(ListView)))
                        {
                            c.Font = new Font(c.Font.Name, 12, c.Font.Style);
                            
                        }
                       
                    }

                }
                else
                {
                    foreach (Control c in this.Controls)
                    {
                        if (c.GetType().Equals(typeof(ClickComboBox)) || c.GetType().Equals(typeof(ListView)))
                        {
                            c.Font = new Font(c.Font.Name, 10, c.Font.Style);
                        }
                        
                    }
                    
                }
                //r.Width = Math.Max(r.Width, this.Size.Width);
                //r.Height = Math.Max(r.Height, this.Size.Height);
                //s = new Size(r.Width, this.ClientSize.Height);
                //this.ClientSize = s;
            }
               
            catch (ConnectionCorruptedException)
            {
                foreach (Control c in this.Controls)
                {
                    if (c.GetType().Equals(typeof(ClickComboBox)))
                    {
                        ClickComboBox combo = (ClickComboBox)c;
                        combo.Font = new Font(c.Font.Name, 12, c.Font.Style);
                    }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.DrawRectangle(
                new Pen(Color.Black), 0, 0, this.Size.Width - 1, this.Size.Height - 1);
                //0, 0, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);
                //0, 0, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
        }
    }
}