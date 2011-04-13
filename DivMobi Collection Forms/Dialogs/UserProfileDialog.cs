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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UBT.AI4.Bio.DivMobi.DataManagement;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;
using UBT.AI4.Bio.DivMobi.Forms.Forms;


//UMF Imports
using UBT.AI4.Bio.DivMobi.UMF.Layout;
using UBT.AI4.Bio.DivMobi.UMF.Layout.Attributes;
using UBT.AI4.Bio.DivMobi.UMF.Layout.Layouts;
using UBT.AI4.Bio.DivMobi.UMF.Context;

namespace UBT.AI4.Bio.DivMobi.Forms.Dialogs
{
    [Layout(typeof(BoxLayoutFactory))]
    public partial class UserProfileDialog : DialogBase, ILayouted
    {
        private ILayout layout;
        private String formName;
        private String formDescription;

        private UserProfile user = null;
        private bool cancel = false;

        // Default Constructor for creating instance without initialization of components
        public UserProfileDialog() :base()
        {
            this.formName = "User Profile Dialog";
            this.formDescription = "Edit existing User Profile";
        }

        public UserProfileDialog(bool loadContext)
            : this()
        {    
            
            Cursor.Current = Cursors.WaitCursor;
            InitializeComponent();
            base.adjustControlSizes();

            
#if DEBUG
            this.MinimizeBox = false;
            this.ControlBox = true;
#else
            this.MinimizeBox = true;
            this.ControlBox = true;            
#endif 
            if (loadContext)
            {
                this.labelProjectName.Text = "";

                // Fill comboBoxes
                this.fillComboBoxToolbarIcons();
                this.fillComboBoxContext();
                this.fillComboBoxDisplayLevel();
                this.fillComboBoxLanguageContext();
                this.fillComboBoxTaxonomyGroup();

                try
                {
                    if (UserProfiles.Instance.Current != null)
                    {
                        user = UserProfiles.Instance.Current;
                        this.fillControls();
                    }
                }
                catch (ConnectionCorruptedException ex)
                {
                    throw ex;
                }

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

            
            Cursor.Current = Cursors.Default;
        }

        //Implementierung von ILayouted
        public ILayout Layout { get { return this.layout; } }

        public String FormName { get { return this.formName != null ? this.formName : ""; } }

        public String FormDescription { get { return this.formDescription != null ? this.formDescription : ""; } }


        private void fillComboBoxToolbarIcons()
        {
            this.comboBoxToolbarIcons.Items.Clear();

            this.comboBoxToolbarIcons.Items.Add("small");

            if (Screen.PrimaryScreen.Bounds.Width > 280)
            {
                this.comboBoxToolbarIcons.Items.Add("medium");
            }

            if (Screen.PrimaryScreen.Bounds.Width > 475)
            {
                this.comboBoxToolbarIcons.Items.Add("large");
            }

            if(user != null)
            {
                if (!this.comboBoxToolbarIcons.Items.Contains(user.ToolbarIcons))
                {
                    user.ToolbarIcons = "small";
                }
                this.comboBoxToolbarIcons.SelectedText = user.ToolbarIcons;
            }
        }

        private void fillComboBoxLanguageContext()
        {
            this.comboBoxLanguageContext.Items.Clear();

            this.comboBoxLanguageContext.Items.Add("English");
            this.comboBoxLanguageContext.Items.Add("German");
        }

        private void fillComboBoxContext()
        {
            this.comboBoxContext.Items.Clear();

            this.comboBoxContext.Items.Add("ManagementMobile");
            this.comboBoxContext.Items.Add("Monitoring");
        }

        private void fillComboBoxDisplayLevel()
        {
            this.comboBoxDisplayLevel.Items.Clear();
            this.comboBoxDisplayLevel.Items.Add("Event");
            this.comboBoxDisplayLevel.Items.Add("Specimen");
            this.comboBoxDisplayLevel.Items.Add("IdentificationUnit");
        }

        private void fillComboBoxTaxonomyGroup()
        {
            try
            {
                this.comboBoxTaxonomyGroup.Items.Clear();

                IList<CollTaxonomicGroup_Enum> list = DataFunctions.Instance.RetrieveTaxonomicGroups();
                foreach (CollTaxonomicGroup_Enum item in list)
                {
                    if (item != null)
                        this.comboBoxTaxonomyGroup.Items.Add(item.Code);
                }
            }
            catch (ConnectionCorruptedException ex)
            {
                throw ex;
            }
        }

        private void fillControls()
        {
            if (user != null)
            {
                if (user.LoginName != null)
                    this.textBoxUserLoginName.Text = user.LoginName;

                if (user.CombinedNameCache != null)
                    this.textBoxNameCache.Text = user.CombinedNameCache;

                if (user.ToolbarIcons != null)
                    this.comboBoxToolbarIcons.Text = user.ToolbarIcons;

                if (user.LanguageContext != null)
                    this.comboBoxLanguageContext.SelectedText = user.LanguageContext;

                if (user.Context != null)
                    this.comboBoxContext.SelectedText = user.Context;

                if (user.Displaylevel != null && user.Displaylevel.Index > 0)
                    this.comboBoxDisplayLevel.SelectedIndex = (int)user.Displaylevel.Index-1;//Weil EventSeries nicht angeboten wird.
                else
                    this.comboBoxDisplayLevel.SelectedIndex = 0;
                
                this.checkBoxStopGPS.Checked = (bool)user.StopGPS;
                this.checkBoxDefaultIUGeoAnalysis.Checked = (bool)user.DefaultIUGeographyAnalysis;

                if (user.ProjectName != null)
                    this.labelProjectName.Text = user.ProjectName;
            }
        }

        private void UserProfileDialog_Closing(object sender, CancelEventArgs e)
        {
            if(this.textBoxUserLoginName.Text.Equals(String.Empty)&& cancel == false)
            {
                MessageBox.Show("Required LoginName for UserProfile is missing!");
                e.Cancel = true;
            }
            else if (cancel == true)
                this.DialogResult = DialogResult.Cancel;
            else
            {
                Cursor.Current = Cursors.WaitCursor;
                if (user == null)
                {
                    try
                    {
                        user = UserProfiles.Instance.CreateUserProfile(this.textBoxUserLoginName.Text);
                        if (!this.textBoxNameCache.Text.Equals(String.Empty))
                            user.CombinedNameCache = this.textBoxNameCache.Text;
                    }
                    catch (UserProfileCorruptedException ex)
                    {
                        Cursor.Current = Cursors.Default;
                        if (MessageBox.Show(ex.Message + " New User couldn't be created. Do you want to close dialog nonetheless?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            this.DialogResult = DialogResult.Cancel;
                        }
                        else
                        {
                            e.Cancel = true;
                        }
                    }
                }
                else
                {
                    user.LoginName = this.textBoxUserLoginName.Text;
                    user.CombinedNameCache = this.textBoxNameCache.Text;
                }

                user.LanguageContext = this.comboBoxLanguageContext.Text;
                user.Context = this.comboBoxContext.Text;
                user.StopGPS = this.checkBoxStopGPS.Checked;
                user.DefaultIUGeographyAnalysis = this.checkBoxDefaultIUGeoAnalysis.Checked;
                user.Displaylevel = new DisplayLevel(this.comboBoxDisplayLevel.Text);
                user.ToolbarIcons = this.comboBoxToolbarIcons.Text;
                try
                {
                    UserProfiles.Instance.Update(user);
                    UserProfiles.Instance.Current = user;
                    this.DialogResult = DialogResult.OK;
                }
                catch (UserProfileCorruptedException ex)
                {
                    Cursor.Current = Cursors.Default;
                    if (MessageBox.Show(ex.Message + " Changes won't be saved. Do you want to close dialog nonetheless?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        this.DialogResult = DialogResult.Cancel;
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }

            ContextManager.Instance.UnRegisterAll(this);   
            Cursor.Current = Cursors.Default;
        }

        public void Dispose()
        {
            ContextManager.Instance.UnRegisterAll(this);
        }

        private void comboBoxTaxonomyGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.comboBoxTaxonomyGroup.Text.Equals(String.Empty))
            {
                
                try
                {
                    this.listBoxTaxonTables.DisplayMember = "DisplayText";
                    this.listBoxTaxonTables.ValueMember = "DataSource";
                    this.listBoxTaxonTables.Items.Clear();
                    this.listBoxTaxonTables.Enabled = true;
                    
                    IList<TaxonListsForUser> lists = DataFunctions.Instance.RetrieveTaxonListsForGroup(this.comboBoxTaxonomyGroup.Text);
                    if (lists == null)
                    {
                        MessageBox.Show("No list for Taxonomic Group found");
                        return;
                    }
                    foreach (TaxonListsForUser tl in lists)
                    {
                        this.listBoxTaxonTables.Items.Add(tl);
                    }
                    UserTaxonomicGroupTable userTaxTable = DataFunctions.Instance.RetrieveUserTaxonomicGroupTable(this.comboBoxTaxonomyGroup.Text);
                    if (userTaxTable != null)
                    {
                        this.labelSelectedTaxonomy.Text = userTaxTable.TaxonomicTable;
                    }
                    else
                    {
                        this.labelSelectedTaxonomy.Text = "<No Taxonomy selected>";
                    }
                }
                catch (ConnectionCorruptedException)
                {

                }
            }
        }

        private void listBoxTaxonTables_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (!this.comboBoxTaxonomyGroup.Text.Equals(String.Empty))
            {
                String taxonGroup = this.comboBoxTaxonomyGroup.Text;

                if (this.listBoxTaxonTables.SelectedItem != null)
                {
                    TaxonListsForUser tlu = (TaxonListsForUser)this.listBoxTaxonTables.SelectedItem;
                    String taxonTable = tlu.DataSource;
                    try
                    {
                        DataFunctions.Instance.CreateOrUpdateUserTaxonomicGroupTable(taxonGroup, taxonTable);
                    }
                    catch (Exception ex)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        if(this.comboBoxTaxonomyGroup.Items.Count > 0)
                            this.comboBoxTaxonomyGroup.SelectedIndex = 0;
                        return;
                    }

                    MessageBox.Show("TaxonomicGroup '"+taxonGroup+"' was successfully assigned to table '"+taxonTable+"'");
                    this.labelSelectedTaxonomy.Text = taxonTable;
                }
                else
                {
                    MessageBox.Show("There's no item selected to be saved. Please select item first.");
                }
            }
            else
            {
                MessageBox.Show("Taxonomic Group has to be choosen before selecting TaxonomyTable.");
                this.listBoxTaxonTables.Enabled = false;
            }
        }

        private void comboBoxDisplayLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.user != null)
                this.user.Displaylevel = new DisplayLevel(this.comboBoxDisplayLevel.Text);
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {

        }

        private void tabPageTaxonList_GotFocus(object sender, EventArgs e)
        {
            
        }
    }
}

