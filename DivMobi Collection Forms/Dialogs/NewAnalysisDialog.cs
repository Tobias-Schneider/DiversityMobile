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
    public partial class NewAnalysisDialog : DialogBase, ILayouted
    {
        private ILayout layout;
        private String formName;
        private String formDescription;

        private analysisData _selectedAnalysis;
        private int _lastAnalysis;

        // Default Constructor for creating instance without initialization of components
        public NewAnalysisDialog() :base()
        {
            this.formName = "New Analysis Dialog";
            this.formDescription = "Create new Analysis for IU";
        }

        public NewAnalysisDialog(bool loadContext)
            : this()
        {
            InitializeComponent();
            base.adjustControlSizes();
            this.dateTimePickerAnalysis.Value = DateTime.Today;
            this.buttonOk.Enabled = false;

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

        public NewAnalysisDialog(int lastAnalysis)
            :this(true)
        {
            this._lastAnalysis = lastAnalysis;
        }

        //Implementierung von ILayouted
        public ILayout Layout { get { return this.layout; } }

        public String FormName { get { return this.formName != null ? this.formName : ""; } }

        public String FormDescription { get { return this.formDescription != null ? this.formDescription : ""; } }


        public struct analysisData
        {
            public int? AnalysisID;
            public String DisplayText;
            public String MeasurementUnit;
            public String Description;

            public override String ToString()
            {
                StringBuilder stbu = new StringBuilder();
                if(DisplayText != null)
                    stbu.Append(DisplayText + " ");
                if (MeasurementUnit != null)
                    stbu.Append("in " + MeasurementUnit);
                return stbu.ToString();
            }
        }

        public IList<Analysis> Analysis
        {
            set
            {
                foreach (Analysis a in value) 
                {
                    if (a != null)
                    {
                        analysisData data = new analysisData();
                        data.AnalysisID = a.AnalysisID;
                        data.Description = a.Description;
                        data.MeasurementUnit = a.MeasurementUnit;
                        data.DisplayText = a.DisplayText;
                        this.comboBoxAnalysis.Items.Add(data);
                    }
                }

                for (int index = 0; index < this.comboBoxAnalysis.Items.Count; index++)
                {
                    if (((analysisData)this.comboBoxAnalysis.Items[index]).AnalysisID == this._lastAnalysis)
                    {
                        this.comboBoxAnalysis.SelectedIndex = index;
                        break;
                    }
                }
            }
        }

        public int PerformedAnalysis
        {
            get
            {
                if (_selectedAnalysis.AnalysisID != null)
                    return (int)_selectedAnalysis.AnalysisID;
               
                return -1;
            }
        }

        private bool isInList(int analysisID ,IList<Analysis> list)
        {
            foreach (Analysis item in list)
            {
                if (item != null)
                {
                    if (item.AnalysisID == analysisID)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public String Value
        {
            get { return this.comboBoxAnalysisValue.Text; }
        }

        public DateTime AnalysisDate
        {
            get { return this.dateTimePickerAnalysis.Value; }
        }
        
        #region Events

        private void comboBoxAnalysis_SelectedIndexChanged(object sender, EventArgs e)
        {
            analysisData data = (analysisData)comboBoxAnalysis.SelectedItem;
            
            labelAnalysisDescription.Text = data.Description;
            _selectedAnalysis = data;

            IList<AnalysisResult> list = new List<AnalysisResult>();
            try
            {
                list = DataFunctions.Instance.RetrieveAnalysisResults((int)data.AnalysisID);
                this.comboBoxAnalysisValue.Items.Clear();
            }
            catch (ConnectionCorruptedException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                this.comboBoxAnalysisValue.Items.Clear();
            }

            
            if (list.Count > 0)
            {
                foreach (AnalysisResult item in list)
                {
                    if (item != null)
                        this.comboBoxAnalysisValue.Items.Add(item.DisplayText);
                }
                this.comboBoxAnalysisValue.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            else
            {
                this.comboBoxAnalysisValue.DropDownStyle = ComboBoxStyle.DropDown;
            }
        }
        
        private void comboBoxAnalysisValue_TextChanged(object sender, EventArgs e)
        {
            if (this.comboBoxAnalysisValue.Text.Equals(String.Empty) || this.comboBoxAnalysisValue.Text.Equals("<TBD>"))
            {
                this.buttonOk.Enabled = false;
            }
            else
            {
                this.buttonOk.Enabled = true;
            }
        }

        private void comboBoxAnalysisValue_GotFocus(object sender, EventArgs e)
        {
            if(comboBoxAnalysisValue.Items.Count == 1)
            {
                if (comboBoxAnalysisValue.Items[0].Equals("<TBD>"))
                {
                    comboBoxAnalysisValue.Items.Clear();
                }
            }
            this.inputPanel1.Enabled = true;
        }
        
        #endregion

        public void Dispose()
        {
            ContextManager.Instance.UnRegisterAll(this);
        }

  
    }
}