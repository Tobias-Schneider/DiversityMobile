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

namespace UBT.AI4.Bio.DivMobi.Forms.ContextForms
{
    [Layout(typeof(BoxLayoutFactory))]
    public partial class IdentificationUnitAnalysisForm : DialogBase, ILayouted
    {
        private ILayout layout;
        private String formName;
        private String formDescription;

        private IdentificationUnitAnalysis _iua = null;

        // Default Constructor for creating instance without initialization of components
        public IdentificationUnitAnalysisForm():base() 
        {
            this.formName = "Identification Unit Analysis Form";
            this.formDescription = "Edit existing Identification Unit Analysis";
        }

        public IdentificationUnitAnalysisForm(bool loadContext)
            : this()
        {
            this.InitializeComponent();
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

        public IdentificationUnitAnalysisForm(IdentificationUnitAnalysis currentIUA)
            : this(true)
        {
            if (currentIUA != null)
            {
                this._iua = currentIUA;
                
                Cursor.Current = Cursors.WaitCursor;
                this.fillIUAData();
                Cursor.Current = Cursors.Default;
            }
            else
                this.Close();
        }

        //Implementierung von ILayouted
        public ILayout Layout { get { return this.layout; } }

        public String FormName { get { return this.formName != null ? this.formName : ""; } }

        public String FormDescription { get { return this.formDescription != null ? this.formDescription : ""; } }


        private void fillIUAData()
        {
            if (this._iua != null)
            {
                // Eigenschaftswerte setzen
                this.labelCS.Text = _iua.CollectionSpecimenID.ToString();
                this.labelAnalysis.Text = _iua.AnalysisID.ToString();

                if (_iua.IdentificationUnit.UnitIdentifier == null || _iua.IdentificationUnit.UnitIdentifier.Equals(String.Empty))
                {
                    this.labelIU.Text = _iua.IdentificationUnit.LastIdentificationCache;
                }
                else
                {
                    this.labelIU.Text = String.Concat("(", _iua.IdentificationUnit.UnitIdentifier, ") ", _iua.IdentificationUnit.LastIdentificationCache);
                }

                if (this._iua.AnalysisResult != null)
                {
                    this.textBoxAnalysisResult.Text = this._iua.AnalysisResult;
                }

                if (this._iua.AnalysisDate != null)
                {
                    this.textBoxAnalysisDate.Text = this._iua.AnalysisDate;
                }
            }
        }

        private bool saveIUAData()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (this._iua != null)
            {
                if (!this.textBoxAnalysisResult.Text.Equals(String.Empty))
                    this._iua.AnalysisResult = this.textBoxAnalysisResult.Text;
                else
                    this._iua.AnalysisResult = null;

                if (!this.textBoxAnalysisDate.Text.Equals(String.Empty))
                    this._iua.AnalysisDate = this.textBoxAnalysisDate.Text;
                else
                    this._iua.AnalysisDate = null;
                try
                {
                    DataFunctions.Instance.Update(this._iua);
                }
                catch (Exception ex)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("IU Analysis Data couldn't be saved. (" + ex.Message + ")", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return false;
                }
            }
            Cursor.Current = Cursors.Default;
            return true;
        }

        private void IdentificationUnitAnalysisForm_Closing(object sender, CancelEventArgs e)
        {
            if (this.DialogResult == DialogResult.Cancel)
            {
                if (MessageBox.Show("Data won't be saved. Do You really want to leave the Dialog?", "Data not saved", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }
            else if(this.DialogResult == DialogResult.Abort)
            {
                e.Cancel = true;
                return;
            }
        }

        public void Dispose()
        {
            ContextManager.Instance.UnRegisterAll(this);
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (!this.saveIUAData())
                this.DialogResult = DialogResult.Abort;

            ContextManager.Instance.UnRegisterAll(this);
        }
    }
}