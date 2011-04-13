using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using UBT.AI4.Toolbox.Controls;

//UMF Imports
using UBT.AI4.Bio.DivMobi.UMF.Layout;
using UBT.AI4.Bio.DivMobi.UMF.Layout.Attributes;
using UBT.AI4.Bio.DivMobi.UMF.Layout.Layouts;
using UBT.AI4.Bio.DivMobi.UMF.Context;
using UBT.AI4.Bio.DivMobi.DataManagement;
using System.Drawing;

namespace UBT.AI4.Bio.DivMobi.Forms.ContextForms
{
    [Layout(typeof(BoxLayoutFactory))]
    public partial class ContextForm : Form, ILayouted
    {
        private ILayout layout;

        private String formName;
        private String formDescription;
        private bool hasContext = false;
        private bool isInCustomization = false;

        public ContextForm()
        {
            InitializeComponent();
        }

        public ContextForm(String name, String description)
            :this()
        {
            this.formName = name;
            this.formDescription = description;
        }
        
        #region Implementierung von ILayouted

        public ILayout Layout { get { return this.layout; } }

        #endregion

        public String FormName { get { return this.formName != null ? this.formName : ""; } }

        public String FormDescription { get { return this.formDescription != null ? this.formDescription : ""; } }

        protected bool HasContext
        {
            get { return this.hasContext; }
        }

        protected bool IsInCustomization
        {
            get { return this.isInCustomization; }
        }

        protected bool writeCustomizedContext()
        {
            try
            {
                String contextPath = String.Concat(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), @"\CustomizedContext\");
                String contextFile = String.Concat(contextPath, this.Name, ".xml");

                StreamWriter write = new StreamWriter(contextFile, false);
                write.WriteLine("<!-- Start Configure "+this.Name+" -->");
                write.WriteLine("<ClassConfiguration ClassName=\"UBT.AI4.Bio.DivMobi.Forms.ContextForms." + this.Name + ", DivMobi Collection Forms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\">");

                foreach (Control ctrl in this.Controls)
                {
                    // Write FieldDescription for every ContextPanel
                    if (ctrl.GetType() == typeof(ContextPanel))
                    {
                        StringBuilder text = new StringBuilder();

                        // Visibility
                        text.Append("<FieldDescriptor FieldName=\"");
                        text.Append(((ContextPanel)ctrl).Name);
                        text.Append("\">");
                        text.AppendLine();

                        if (((ContextPanel)ctrl).IsVisible)
                        {
                            text.AppendLine("<Action ActionId=\"Visible\" Parameter=\"true\" />");
                            text.AppendLine(" </FieldDescriptor>");
                        }
                        else
                        {
                            text.AppendLine("<Action ActionId=\"Visible\" Parameter=\"false\" />");
                            text.AppendLine(" </FieldDescriptor>");
                        }

                        // Label Text
                        if (((ContextPanel)ctrl).Text != null && !((ContextPanel)ctrl).Text.Equals(String.Empty))
                        {
                            text.Append("<FieldDescriptor FieldName=\"");
                            text.Append(((ContextPanel)ctrl).Name);
                            text.Append("\">");
                            text.AppendLine();


                            text.Append("<Action ActionId=\"SetTextAction\" Parameter=\"");
                            text.Append(((ContextPanel)ctrl).Text);
                            text.Append("\" />");
                            text.AppendLine();

                            text.AppendLine(" </FieldDescriptor>");
                        }

                        // DefaultValue
                        if (((ContextPanel)ctrl).DefaultValue != null && !((ContextPanel)ctrl).DefaultValue.Equals(String.Empty))
                        {
                            text.Append("<FieldDescriptor FieldName=\"");
                            text.Append(((ContextPanel)ctrl).Name);
                            text.Append("\">");
                            text.AppendLine();


                            text.Append("<Action ActionId=\"SetDefaultValueAction\" Parameter=\"");
                            text.Append(((ContextPanel)ctrl).DefaultValue);
                            text.Append("\" />");
                            text.AppendLine();

                            text.AppendLine(" </FieldDescriptor>");
                        }

                        write.Write(text.ToString());
                    }
                }

                write.WriteLine("</ClassConfiguration>");
                write.WriteLine("<!-- End Configure " + this.Name + " -->");

                write.Flush();
                write.Close();

                MessageBox.Show("Changes of customized Form will be shown after next start of application.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected void loadInContext()
        {
            this.hasContext = true;
            // Further initialization
            try
            {
                //Layout erzeugen (Keine Überschneidungen)
                layout = LayoutFactory.Instance.CreateLayout(this, true);

                //Beim Kontextmanager registrieren
                ContextManager.Instance.Register(this);
            }
            catch (ContextCorruptedException ex)
            {
                throw new ContextCorruptedException("Form can't be shown and will be closed. (" + ex.Message + ")");
            }

            //Kontext ausführen
            //Sprachkontext
            String language = null;
            try
            {
                language = UserProfiles.Instance.Current.LanguageContext;
            }
            catch (ConnectionCorruptedException) { }

            try
            {
                if (language != null && !language.Equals(String.Empty))
                {
                    if (ContextManager.Instance.GetContext(language) != null)
                        ContextManager.Instance.GetContext(language).Configure(this);
                    else
                    {
                        throw new ContextCorruptedException("Form can't be shown and will be closed. (" + language + " context doesn't exist)");
                    }
                }
            }
            catch (Exception)
            {
                throw new ContextCorruptedException("Form can't be shown and will be closed. (Error while configuring language context)");
            }

            //Weiterer Kontext
            String context = null;
            try
            {
                context = UserProfiles.Instance.Current.Context;
            }
            catch (ConnectionCorruptedException) { }

            try
            {
                if (context != null && !context.Equals(String.Empty))
                {

                    if (ContextManager.Instance.GetContext(context) != null)
                        ContextManager.Instance.GetContext(context).Configure(this);
                    else
                    {
                        throw new ContextCorruptedException("Form can't be shown and will be closed. (" + context + " context doesn't exist)");
                    }
                }
            }
            catch (Exception)
            {
                throw new ContextCorruptedException("Form can't be shown and will be closed. (Error while configuring " + context + " context)");
            }
        }

        protected void loadInCustomization()
        {
            this.isInCustomization = true;
            // Form will be customized 
            // Therefor contextMenuCustomizeContext has to be associated
            // and Controls will be disabled
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl.GetType() == typeof(ContextPanel))
                {
                    if (!hasContext)
                    {
                        ((ContextPanel)ctrl).ContextMenu = ((ContextPanel)ctrl).contextMenuCustomizeContext;
                        ((ContextPanel)ctrl).Click += new System.EventHandler(((ContextPanel)ctrl).ContextPanel_Click);
                    }
                    foreach (Control child in ctrl.Controls)
                    {
                        child.Enabled = false;
                    }

                    ctrl.Enabled = true;
                }
                else
                {
                    // OK and Cancel Button should be enabled
                    if (ctrl.GetType() == typeof(Button))
                    {
                        if (((Button)ctrl).DialogResult != DialogResult.None)
                            continue;
                    }

                    ctrl.Enabled = false;
                }
            }
        }

        private void ContextForm_Closed(object sender, EventArgs e)
        {
            ContextManager.Instance.UnRegisterAll(this);
        }
    }
}
