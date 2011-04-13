//#######################################################################
//Diversity Mobile Synchronization
//Project Homepage:  http://www.diversitymobile.net
//Copyright (C) 2011  Tobias Schneider, Lehrstuhl Angewndte Informatik IV, Universität Bayreuth
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//#######################################################################
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;
using UBT.AI4.Bio.DivMobi.DataManagement;

using UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes;

//UMF Imports
using UBT.AI4.Bio.DivMobi.UMF.Layout;
using UBT.AI4.Bio.DivMobi.UMF.Layout.Attributes;
using UBT.AI4.Bio.DivMobi.UMF.Layout.Layouts;
using UBT.AI4.Bio.DivMobi.UMF.Context;


namespace UBT.AI4.Bio.DivMobi.Forms.Dialogs
{
    [Layout(typeof(BoxLayoutFactory))]
    public partial class NewSitePropertyDialog : DialogBase, ILayouted
    {
        #region Member

        private ILayout layout;
        private String formName;
        private String formDescription;

        private PropertyNames pn;
        #endregion

        #region Constructor

        // Default Constructor for creating instance without initialization of components
        public NewSitePropertyDialog() :base()
        {
            this.formName = "New SiteProperty Dialog";
            this.formDescription = "Create new SiteProperty for Collection Event";
        }

        public NewSitePropertyDialog(bool loadContext)
            : this()
        {
            InitializeComponent();
            base.adjustControlSizes();
            if (loadContext)
            {
                // Further initialization
                try
                {
                    //Layout erzeugen (Keine Überschneidungen)
                    layout = LayoutFactory.Instance.CreateLayout(this, false);

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
        }
        #endregion

        //Implementierung von ILayouted
        public ILayout Layout { get { return this.layout; } }

        public String FormName { get { return this.formName != null ? this.formName : ""; } }

        public String FormDescription { get { return this.formDescription != null ? this.formDescription : ""; } }


        public String DisplayText
        {
            get {
                if (pn != null)
                    return pn.DisplayText;
                else
                    return null;
            }
        }

        public IList<Property> PropertyList
        {
            set
            {
                foreach (Property propGroup in value)
                {
                    if(propGroup != null)
                        this.comboBoxPropertyGroup.Items.Add(propGroup.PropertyName);
                }
                if (this.comboBoxPropertyGroup.Items.Count > 0)//Hier letztes merken
                    this.comboBoxPropertyGroup.SelectedItem = this.comboBoxPropertyGroup.Items[0];
            }
        }

        public int PropertyID
        {
            get {
                if (pn != null)
                    return pn.PropertyID;
                else
                    return -1;
            }
        }

        public String PropertyURI
        {
            get {
                if (pn != null)
                    return pn.PropertyURI;
                else
                    return null;
            }
        }

        private TreeNode buildBottomUp(PropertyNames pn)
        {
            if (pn == null)
                return new TreeNode();
            TreeNode tn = new TreeNode(pn.DisplayText);
            if (pn.ParentPropertyName != null)
            {
                TreeNode parentNode = buildBottomUp(pn.ParentPropertyName, tn);
                return parentNode;
            }
            return tn;
        }

        private TreeNode buildBottomUp(PropertyNames pn, TreeNode childnode)
        {
            if (pn == null || childnode == null)
                return new TreeNode();
            TreeNode tn = new TreeNode(pn.DisplayText);
            tn.Nodes.Add(childnode);
            if (pn.ParentPropertyName != null)
            {
                TreeNode parentNode = buildBottomUp(pn.ParentPropertyName, tn);
                return parentNode;
            }
            return tn;
        }

        private void SetOKButton()
        {
            if (this.pn != null)
                this.buttonOk.Enabled = true;
            else
                this.buttonOk.Enabled = false;
        }

        private void findProperties()
        {
            if (this.textBoxSearch.Text.Trim().Length >= 1)
            {
                try
                {
                    // Ausgabe des Zeichens verhindern
                    PropertyNames actual = DataFunctions.Instance.CreatePropertyNames();
                    String input = this.textBoxSearch.Text.Trim();
                    input += "%";
                    input = input.Replace(" ", "% ");
                    input = input.Replace("'", "''");
                    this.comboBoxDisplayText.Items.Clear();
                    String table = this.comboBoxPropertyGroup.Text.Replace(" ", "");
                    table = table.Replace("(", "");
                    table = table.Replace(")", "");
                    IList<String> Ilist = DataFunctions.Instance.RetrievePropertyNames(table, input);
                    foreach (String propn in Ilist)
                    {
                        if (propn != null)
                        {
                            comboBoxDisplayText.Items.Add(propn);
                            if (propn.Equals(comboBoxDisplayText.Text))//prüfen,ob der  Arbeitsname bereits eine korrekte Bezeichnung ist. Falls ja diesen durch das vollständige Objekt ersetzen
                            {
                                actual = DataFunctions.Instance.RetrievePropertyName(table, actual.DisplayText);
                                pn = actual;
                                if (actual != null)
                                    comboBoxDisplayText.SelectedItem = propn;
                            }
                            else
                                pn = null;
                        }
                    }
                }
                catch (ConnectionCorruptedException ex)
                {
                    throw ex;
                }
            }
            SetOKButton();
        }

        private void comboBoxPropertyGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            treeViewPropertyHierarchy.Nodes.Clear();
            this.comboBoxDisplayText.Items.Clear();
            this.comboBoxDisplayText.Text = String.Empty;
            this.textBoxSearch.Text = String.Empty;
            this.buttonOk.Enabled = false;
            this.treeViewPropertyHierarchy.Nodes.Clear();
            this.pn = null;
            String table;
            table = this.comboBoxPropertyGroup.Text;
            table = table.Replace("(", "");
            table = table.Replace(")", "");
            if(MappingDictionary.Mapping.ContainsKey(typeof(PropertyNames)))
                MappingDictionary.Mapping[typeof(PropertyNames)]=table;
            else
                MappingDictionary.Mapping.Add(typeof(PropertyNames),table);
        }

        private void textBoxSearch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                this.findProperties();
            }
            catch (ConnectionCorruptedException) { }
        }

        private void comboBoxDisplayText_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PropertyNames actual = DataFunctions.Instance.CreatePropertyNames();
                String table = this.comboBoxPropertyGroup.Text.Replace(" ", "");
                table = table.Replace("(", "");
                table = table.Replace(")", "");
                actual = DataFunctions.Instance.RetrievePropertyName(table, this.comboBoxDisplayText.Text);
                pn = actual;
                this.treeViewPropertyHierarchy.Nodes.Clear();
                this.treeViewPropertyHierarchy.Nodes.Add(buildBottomUp(pn));
                this.treeViewPropertyHierarchy.ExpandAll();
                SetOKButton();
            }
            catch (ConnectionCorruptedException)
            {
                this.treeViewPropertyHierarchy.Nodes.Clear();
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            String table = this.comboBoxPropertyGroup.Text.Replace(" ", "");
            table = table.Replace("(", "");
            table = table.Replace(")", "");
            PropertyNamesSearchDialog dlg = new PropertyNamesSearchDialog(table);
            dlg.Location = new Point((this.Size.Width) / 2 - (dlg.Size.Width) / 2, this.Location.Y);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                PropertyNames tmp = dlg.PropertyName;
                this.comboBoxDisplayText.Items.Clear();
                if(tmp != null)
                    this.comboBoxDisplayText.Text = tmp.DisplayText;
                pn = tmp;
                this.treeViewPropertyHierarchy.Nodes.Clear();
                this.treeViewPropertyHierarchy.Nodes.Add(buildBottomUp(pn));
                this.treeViewPropertyHierarchy.ExpandAll();
            }
            Cursor.Current = Cursors.Default;
            SetOKButton();
        }

        public void Dispose()
        {
            ContextManager.Instance.UnRegisterAll(this);
        }

        private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            // MaxLength for Identification = 255
            if (this.comboBoxDisplayText.Text.Length > 254)
            {
                // no more insert possible
                e.Handled = true;
                return;
            }
        }

        private void comboBoxDisplayText_GotFocus(object sender, EventArgs e)
        {
            try
            {
                this.findProperties();
            }
            catch (ConnectionCorruptedException) { }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.findProperties();
            }
            catch (ConnectionCorruptedException) { }
        }
    }
}