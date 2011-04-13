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

//UMF Imports
using UBT.AI4.Bio.DivMobi.UMF.Layout;
using UBT.AI4.Bio.DivMobi.UMF.Layout.Attributes;
using UBT.AI4.Bio.DivMobi.UMF.Layout.Layouts;
using UBT.AI4.Bio.DivMobi.UMF.Context;

namespace UBT.AI4.Bio.DivMobi.Forms.Dialogs
{
    [Layout(typeof(BoxLayoutFactory))]
    public partial class NewAgentDialog : DialogBase, ILayouted
    {
        private ILayout layout;
        private String formName;
        private String formDescription;

        // Default Constructor for creating instance without initialization of components
        public NewAgentDialog():base() 
        {
            this.formName = "New Agent Dialog";
            this.formDescription = "Create new Collection Agent";
        }

        public NewAgentDialog(bool loadContext)
            : this()
        {
            InitializeComponent();
            base.adjustControlSizes();
            this.buttonOk.Enabled = false;


            if (loadContext)
            {
                this.fillComboBox();

                if (this.comboBoxUsers.Items.Count > 0)
                {
                    this.comboBoxUsers.SelectedIndex = 0;
                    this.buttonOk.Enabled = true;
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
        }

        //Implementierung von ILayouted
        public ILayout Layout { get { return this.layout; } }

        public String FormName { get { return this.formName != null ? this.formName : ""; } }

        public String FormDescription { get { return this.formDescription != null ? this.formDescription : ""; } }


        public int Collector
        {
            get { return this.comboBoxUsers.SelectedIndex; }
        }

        private void fillComboBox()
        {
            Cursor.Current = Cursors.WaitCursor;
            this.comboBoxUsers.Items.Clear();

            foreach (UserProfile item in UserProfiles.Instance.List)
            {
                if (item != null)
                    this.comboBoxUsers.Items.Add(item.CombinedNameCache);
            }
            Cursor.Current = Cursors.Default;
        }

        public void Dispose()
        {
            ContextManager.Instance.UnRegisterAll(this);
        }
    }
}