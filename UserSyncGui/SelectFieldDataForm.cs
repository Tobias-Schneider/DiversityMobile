using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer.Collections;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Restrictions;
using UBT.AI4.Bio.DivMobi.DataItemFormTools;

namespace UserSyncGui
{
    public partial class SelectFieldDataForm : Form
    {
        private int projectID;
        private Serializer serializer;
        private IRestriction completeRestriction;
        private IRestriction projectRestriction;
        private List<Guid> selectedGuids;
        private List<ISerializableObject> selectedObjects;
        private TreeViewOperations _tvOperations;

        // Progress Information
        ProgressThread progressThread = null;

        private Dictionary<Guid,ISerializableObject> selectedItems;
        
        #region Internal types

         //<summary>
         //Specifies the index of icons in the icon image list.
         //</summary>
        private enum IconIndex : int
        {
            /// <summary>
            /// Unknown symbol (usually this refers to a purely white rectangle).
            /// </summary>
            Unknown = 0,

            Event = 1,
            EventGrey = 2,

            SiteProperty = 3,
            SitePropertyGrey = 4,

            EventSeries = 5,
            EventSeriesGrey = 6,

            Location = 7,
            LocationGrey = 8,

            Agent = 9,
            AgentGrey = 10,

            Specimen = 11,
            SpecimenGrey = 12,
            SpecimenRed = 42,

            Tree = 13,
            TreeGrey = 14,

            Branch = 15,
            BranchGrey = 16,

            Plant = 17,
            PlantGrey = 18,

            Other = 19,
            OtherGrey = 20,

            Identification = 21,
            IdentificationGrey = 22,

            Analysis = 23,
            AnalysisGrey = 24,

            Foto = 25,
            FotoGrey = 26,

            Image = 27,
            ImageGrey = 28,

            Alga = 29,
            Assel = 30,
            Bacterium = 31,
            Bird = 32,
            Bryophyte = 33,
            Fish = 34,
            Fungus = 35,
            Insect = 36,
            Lichen = 37,
            Mammal = 38,
            Mollusc = 39,
            Myxomycete = 40,
            Virus = 41
        }

         //<summary>
         //Describes the types of nodes of the tree view control.
         //</summary>
        private enum TreeViewNodeTypes
        {
            Unknown,
            EventNode,
            LocalisationNode,
            SitePropertyNode,
            SpecimenNode,
            IdentificationUnitNode,
            AgentNode,
            AnalysisNode,
            EventSeriesNode
        }
        private class TreeViewNodeData
        {
            public int ID;
            public string CollectorsName;
            public int IdentificationUnitID;
            public int CollectionSpecimenID;
            public int AnalysisID;
            public string AnalysisNumber;
            public TreeViewNodeTypes NodeType;

            /// <summary>
            /// Initializes a new instance of the <see cref="TreeViewNodeData"/> class.
            /// </summary>
            /// <param name="ID">The ID.</param>
            /// <param name="NodeType">Type of the node.</param>
            public TreeViewNodeData(int ID, TreeViewNodeTypes NodeType)
            {
                this.ID = ID;
                this.NodeType = NodeType;
            }

            public TreeViewNodeData(string collectorsName, int CollectionSpecimenID)
            {
                this.CollectorsName = collectorsName;
                this.CollectionSpecimenID = CollectionSpecimenID;
                this.NodeType = TreeViewNodeTypes.AgentNode;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="TreeViewNodeData"/> class.
            /// </summary>
            /// <param name="IdentificationUnitID">The identification unit ID.</param>
            /// <param name="CollectionSpecimenID">The collection specimen ID.</param>
            /// <param name="AnalysisID">The analysis ID.</param>
            /// <param name="AnalysisNumber">The analysis number.</param>
            public TreeViewNodeData(int IdentificationUnitID, int CollectionSpecimenID, int AnalysisID, string AnalysisNumber)
            {
                this.NodeType = TreeViewNodeTypes.AnalysisNode;
                this.IdentificationUnitID = IdentificationUnitID;
                this.CollectionSpecimenID = CollectionSpecimenID;
                this.AnalysisID = AnalysisID;
                this.AnalysisNumber = AnalysisNumber;
            }

        }

        private class SelectionContainer
        {
            private ISerializableObject owner;
            private List<ISerializableObject> relatedObjects;
            private bool truncate;
            private ISerializableObject root;


            public SelectionContainer(ISerializableObject iso, bool truncate)
            {
                owner = iso;
                relatedObjects = new List<ISerializableObject>();               
                this.truncate = truncate;
                if (truncate == false)
                    topDown(owner, relatedObjects);
                relatedObjects.Add(owner);
                bottomUp(owner, relatedObjects);
            }

            private void bottomUp(ISerializableObject iso,List<ISerializableObject> parents)
            {
                if(iso.GetType().Equals(typeof(IdentificationUnitAnalysis)))
                {
                    IdentificationUnitAnalysis iua=(IdentificationUnitAnalysis) iso;
                    IdentificationUnit iu = iua.IdentificationUnit;
                    if (iu != null)
                    {
                        parents.Add(iu);
                        bottomUp(iu, parents);
                    }
                    else throw new Exception();                 
                }
                if (iso.GetType().Equals(typeof(IdentificationUnit)))
                {
                    IdentificationUnit iu = (IdentificationUnit)iso;
                    IDirectAccessIterator<Identification> identifications = iu.Identifications;
                    short i=0;
                    Identification ident = null;
                    foreach (Identification id in identifications)
                    {
                        if (id.IdentificationSequence != null && id.IdentificationSequence > i)
                        {
                            i = (short)id.IdentificationSequence;
                            ident = id;
                        }
                    }
                    if (ident != null)
                        parents.Add(ident);
                    IdentificationUnit relatedUnit = iu.RelatedUnit;
                    if (relatedUnit != null)
                    {
                        parents.Add(relatedUnit);
                        bottomUp(relatedUnit, parents);
                    }
                    else
                    {
                        CollectionSpecimen spec = iu.CollectionSpecimen;
                        if (spec != null)
                        {
                            parents.Add(spec);
                            bottomUp(spec, parents);
                        }
                        else
                            throw new Exception();
                    }
                }
                if (iso.GetType().Equals(typeof(CollectionSpecimen)))
                {
                    CollectionSpecimen spec = (CollectionSpecimen)iso;
                    CollectionAgent ca = spec.CollectionAgent.First();
                    if (ca != null)
                        parents.Add(ca);
                    CollectionEvent ce = spec.CollectionEvent;
                    if (ce != null)
                    {
                        parents.Add(ce);
                        bottomUp(ce, parents);
                    }
                    else this.root = spec; ;//Warnung dass das Specimen nicht angezeigt werden kann
                }
                if (iso.GetType().Equals(typeof(CollectionEvent)))
                {
                    CollectionEvent ce=(CollectionEvent) iso;
                    IDirectAccessIterator<CollectionEventLocalisation> locations = ce.CollectionEventLocalisation;
                    foreach (CollectionEventLocalisation loc in locations)
                    {
                        parents.Add(loc);
                    }
                    IDirectAccessIterator<CollectionEventProperty> properties = ce.CollectionEventProperties;
                    foreach (CollectionEventProperty prop in properties)
                    {
                        parents.Add(prop);
                    }
                    CollectionEventSeries cs = ce.CollectionEventSeries;
                    if (cs != null)
                    {
                        parents.Add(cs);
                        this.root = cs;
                    }
                    else this.root = ce;
                }
            }
            private void topDown(ISerializableObject iso, List<ISerializableObject> children)
            {
                if (iso.GetType().Equals(typeof(CollectionEventSeries)))
                {
                    CollectionEventSeries cs = (CollectionEventSeries)iso;
                    IDirectAccessIterator<CollectionEvent> events = cs.CollectionEvents;
                    foreach (CollectionEvent ce in events)
                    {
                        children.Add(ce);
                        topDown(ce, children);
                    }
                }
                if (iso.GetType().Equals(typeof(CollectionEvent)))
                {
                    CollectionEvent ce = (CollectionEvent)iso;
                    IDirectAccessIterator<CollectionEventLocalisation> locations = ce.CollectionEventLocalisation;
                    foreach (CollectionEventLocalisation loc in locations)
                    {
                        children.Add(loc);
                    }
                    IDirectAccessIterator<CollectionEventProperty> properties = ce.CollectionEventProperties;
                    foreach (CollectionEventProperty prop in properties)
                    {
                        children.Add(prop);
                    }
                    IDirectAccessIterator<CollectionSpecimen> specimen = ce.CollectionSpecimen;
                    foreach (CollectionSpecimen spec in specimen)
                    {
                        children.Add(spec);
                        topDown(spec, children);
                    }
                }
                if (iso.GetType().Equals(typeof(CollectionSpecimen)))
                {
                    CollectionSpecimen spec = (CollectionSpecimen)iso;
                    CollectionAgent ca = spec.CollectionAgent.First();
                    if (ca != null)
                        children.Add(ca);
                    IDirectAccessIterator<IdentificationUnit> units = spec.IdentificationUnits;
                    foreach (IdentificationUnit iu in units)
                    {
                        if (iu.RelatedUnit == null)//Hier kann der Aufwand optimiert werden indem gleich alle IdentificationUnits angehängt werden, alerdings muss dann der Fall von einer IU als Startpunkt gesondert behandelt werden
                        {
                            children.Add(iu);
                            topDown(iu, children);
                        }
                    }
                }
                if (iso.GetType().Equals(typeof(IdentificationUnit)))
                {
                    IdentificationUnit iu = (IdentificationUnit)iso;
                    IDirectAccessIterator<IdentificationUnitAnalysis> analyses = iu.IdentificationUnitAnalysis;
                    foreach (IdentificationUnitAnalysis iua in analyses)
                    {
                        children.Add(iua);
                    }
                    IDirectAccessIterator<IdentificationUnit> units = iu.ChildUnits;
                    foreach (IdentificationUnit childUnit in units)
                    {
                        children.Add(childUnit);
                        topDown(childUnit, children);
                    }
                }
            }

            public List<ISerializableObject> RelatedObjects{get {return this.relatedObjects;}}
            public ISerializableObject Owner { get { return this.owner; } }
            public ISerializableObject Root { get { return this.root; } }
            public override string ToString()
            {
                return this.owner.ToString();
            }

        }

        #endregion

        #region Constuctor
        public SelectFieldDataForm(int projectID,Serializer repositorySerializer)
        {
            InitializeComponent();
            selectedGuids = new List<Guid>();
            selectedObjects = new List<ISerializableObject>();
            //selectedItems = new Dictionary<Guid, ISerializableObject>();
            this.projectID = projectID;
            this.serializer = repositorySerializer;
            this._tvOperations = new TreeViewOperations(ExpandLevel.EventSeries, this.serializer.Connector);
            projectRestriction = RestrictionFactory.Eq(typeof(CollectionProject), "_ProjectID", projectID);

            // Adjust SearchType Panels
            this.panelEventSeries.Visible = false;
            this.panelEventSeries.Location = new Point(12, 176);

            this.panelIdentificationUnits.Visible = false;
            this.panelIdentificationUnits.Location = new Point(12, 211);

            this.panelSamplingPlot.Visible = false;
            this.panelSamplingPlot.Location = new Point(12, 257);

            // Start ProgressThread
        }
        #endregion
        #region Actions
        private void buttonSelect_Click(object sender, EventArgs e)
        {
            // Start Thread for ProgressInformation 
            this.startProgressThread("Query Database regarding specified restrictions");
            this.setProgressValue(0);

            listBoxResult.Items.Clear();
            Type t = null;
            List<IRestriction> restrictions = null;

            if (this.comboBoxSelectType.SelectedItem.ToString().ToLower().Equals("identification unit"))
            {
                restrictions = this.getIURestrictions();
                t = typeof(IdentificationUnit);
            }
            else if (this.comboBoxSelectType.SelectedItem.ToString().ToLower().Equals("sampling plot"))
            {
                restrictions = this.getSamplingRestrictions();
                t = typeof(CollectionEventLocalisation);
            }
            else if (this.comboBoxSelectType.SelectedItem.ToString().ToLower().Equals("collection event series"))
            {
                restrictions = this.getSeriesRestrictions();
                t =typeof(CollectionEventSeries);
            }
            
            IRestriction userRestrictions=null;
            bool firstrun = true;
            foreach (IRestriction res in restrictions)
            {     
                if (firstrun)
                {
                    userRestrictions = res;
                    firstrun = false;
                }
                else
                    userRestrictions = RestrictionFactory.And().Add(userRestrictions).Add(res);
            }
            if (userRestrictions != null)
                completeRestriction = RestrictionFactory.And().Add(userRestrictions).Add(projectRestriction);
            else
                completeRestriction = projectRestriction;

            this.setProgressValue(20);
            this.setProgressInformation("Load List from database");
            //Parametrisieren:Fälle 3-5
            IList<ISerializableObject> iso = serializer.Connector.LoadList(t, completeRestriction);
            
            float rate = 0;
            if(iso.Count > 0)
                rate = 75/iso.Count;
            int index = 0;
            foreach (ISerializableObject item in iso)
            {
                index++;
                this.setProgressValue(20 + Int32.Parse((index * rate).ToString()));
                
                listBoxResult.Items.Add(item);
            }
            this.setProgressValue(100);
            this.setProgressInformation("Finished");

            this.endProgressThread(false); 
        }

        private List<IRestriction> getIURestrictions()
        {
            List<IRestriction> restrictions = new List<IRestriction>();
            if (!textBoxLastIdentification.Text.Equals(String.Empty))
            {
                IRestriction res = RestrictionFactory.Eq(typeof(IdentificationUnit), "_LastIdentificationCache", textBoxLastIdentification.Text);
                restrictions.Add(res);
            }
            if (!textBoxTaxonomicGroup.Text.Equals(String.Empty))
            {
                IRestriction res = RestrictionFactory.Eq(typeof(IdentificationUnit), "_TaxonomicGroup", textBoxTaxonomicGroup.Text);
                restrictions.Add(res);

            }
            if (!textBoxUnitdescription.Text.Equals(String.Empty))
            {
                IRestriction res = RestrictionFactory.Eq(typeof(IdentificationUnit), "_UnitDescription", textBoxUnitdescription.Text);
                restrictions.Add(res);

            }
            if (this.checkBoxLog.Checked == true)
            {
                IRestriction res = RestrictionFactory.Btw(typeof(IdentificationUnit), "_LogUpdatedWhen", this.dateTimePickerLogStart.Value, this.dateTimePickerLogEnd.Value);
                restrictions.Add(res);
            }
            return restrictions;
        }

        private List<IRestriction> getSamplingRestrictions()
        {
            List<IRestriction> restrictions = new List<IRestriction>();
            if (!textBoxLocation.Text.Equals(String.Empty))
            {
                IRestriction res = RestrictionFactory.Like(typeof(CollectionEventSeries), "_Location1", textBoxLocation.Text);
                restrictions.Add(res);
            }
            if (this.checkBoxDDate.Checked)
            {

                IRestriction res = RestrictionFactory.Btw(typeof(CollectionEventSeries), "_DeterminationDate", dateTimePickerDeterminationDateStart.Value, dateTimePickerDeterminationDateEnd.Value);
                restrictions.Add(res);

            }
            if (this.checkBoxLog.Checked == true)
            {
                IRestriction res = RestrictionFactory.Btw(typeof(CollectionEventSeries), "_LogUpdatedWhen", this.dateTimePickerLogStart.Value, this.dateTimePickerLogEnd.Value);
                restrictions.Add(res);
            }
            return restrictions;
        }

        private List<IRestriction> getSeriesRestrictions()
        {
            List<IRestriction> restrictions = new List<IRestriction>();
            if (!textBoxSeriesCode.Text.Equals(String.Empty))
            {
                IRestriction res = RestrictionFactory.Eq(typeof(CollectionEventSeries), "_SeriesCode", textBoxSeriesCode.Text);
                restrictions.Add(res);
            }
            if (!textBoxSeriesDescription.Text.Equals(String.Empty))
            {
                IRestriction res = RestrictionFactory.Like(typeof(CollectionEventSeries), "_Description", textBoxSeriesDescription.Text);
                restrictions.Add(res);

            }
            if (this.checkBoxESStart.Checked)
            {
                
                IRestriction res = RestrictionFactory.Btw(typeof(CollectionEventSeries), "_DateStart", dateTimePickerDateStartStart.Value, dateTimePickerDateStartEnd.Value);
                restrictions.Add(res);
                
            }
            if (this.checkBoxESEnd.Checked)
            {
                
                IRestriction res = RestrictionFactory.Btw(typeof(CollectionEventSeries), "_DateEnd", dateTimePickerEndStart.Value, dateTimePickerDateEndEnd.Value);
                restrictions.Add(res);
  
            }
            if (this.checkBoxLog.Checked == true)
            {
                IRestriction res = RestrictionFactory.Btw(typeof(CollectionEventSeries), "_LogUpdatedWhen", this.dateTimePickerLogStart.Value, this.dateTimePickerLogEnd.Value);
                restrictions.Add(res);
            }
            return restrictions;
        }

        //private void listBoxResult_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    TreeNode root = null;
        //    treeViewResult.Nodes.Clear();
        //    ISerializableObject lastSelection = null;
        //    if (listBoxResult.SelectedItems.Count==0)
        //    {
        //        treeViewResult.Nodes.Clear();
        //        return;
        //    }
        //    lastSelection = (ISerializableObject) listBoxResult.SelectedItems[0];//Die Collection ist ein Stack. Das letzte Item liegt oben
        //    //switch
        //    if (lastSelection.GetType().Equals(typeof(IdentificationUnit)))
        //    {
        //        IdentificationUnit iu = (IdentificationUnit)lastSelection;
        //        root = this._tvOperations.displayIdentificationUnit(iu);
        //    }
        //    if (listBoxResult.SelectedItem.GetType().Equals(typeof(CollectionEvent)))
        //    {
        //        root=this._tvOperations.displayEvent((CollectionEvent) lastSelection);
        //    }
        //    if (listBoxResult.SelectedItem.GetType().Equals(typeof(CollectionEventSeries)))
        //    {
        //        root = this._tvOperations.displayEventSeries((CollectionEventSeries)lastSelection);
        //    }
        //    treeViewResult.SuspendLayout();
        //    treeViewResult.Nodes.Add(root);
        //    treeViewResult.ExpandAll();
        //    treeViewResult.ResumeLayout();
        //}

        //private void listBoxSelection_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (listBoxActualSelection.SelectedItems.Count == 0)
        //    {
        //        treeActualViewSelection.Nodes.Clear();
        //        return;
        //    }
        //    SelectionContainer sec = (SelectionContainer)listBoxActualSelection.SelectedItems[0];
        //    List<ISerializableObject> list = sec.RelatedObjects;
        //    treeActualViewSelection.Nodes.Clear();
        //    treeActualViewSelection.Nodes.Add(displayListTopDown(list, sec.Root));
        //    treeActualViewSelection.ExpandAll();
        //}

        private void buttonSelection_Click(object sender, EventArgs e)
        {
            // Start Thread for ProgressInformation 
            this.startProgressThread("Select Items of Result for Synchronization");
            this.setProgressValue(0);

            float rate = 0;
            if (listBoxResult.SelectedItems.Count > 0)
                rate = 80 / listBoxResult.SelectedItems.Count;
            int index = 0;

            this.setProgressInformation("Transfer selected items in actual selection");

            List<ISerializableObject> lastSelection = new List<ISerializableObject>();
            foreach (Object o in listBoxResult.SelectedItems)
            {
                index++;
                this.setProgressValue(Int32.Parse((index * rate).ToString()));

                ISerializableObject iso = (ISerializableObject)o;
                lastSelection.Add(iso);
                SelectionContainer sec = new SelectionContainer(iso, checkBoxTruncate.Checked);
                listBoxActualSelection.Items.Add(sec);
            }
            this.setProgressInformation("Remove selected items from Result List");
            foreach (ISerializableObject iso in lastSelection)
            {
                listBoxResult.Items.Remove(iso);
            }

            this.setProgressValue(100);
            this.setProgressInformation("Finished");

            this.endProgressThread(false);
        }

        private void listBoxResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            TreeNode root = null;
            treeViewResult.Nodes.Clear();
            ISerializableObject lastSelection = null;
            if (listBoxResult.SelectedItems.Count == 0)
            {
                treeViewResult.Nodes.Clear();
                return;
            }
            lastSelection = (ISerializableObject)listBoxResult.SelectedItems[0];//Die Collection ist ein Stack. Das letzte Item liegt oben
            if (lastSelection.GetType().Equals(typeof(IdentificationUnit)))
            {
                IdentificationUnit iu = (IdentificationUnit)lastSelection;
                TreeNode iuNode = displayTopDownIdentificationUnit(iu);

                if (iu.RelatedUnit != null)
                    root = displayBottomUpIdentificationUnit(iu.RelatedUnit, iuNode);
                else
                {
                    if (iu.CollectionSpecimen != null)
                        root = displayBottomUpSpecimen(iu.CollectionSpecimen, iuNode);
                    else root = iuNode;
                }
            }
            if (listBoxResult.SelectedItem.GetType().Equals(typeof(CollectionEvent)))
            {
                root = displayTopDownEvent((CollectionEvent)lastSelection);
            }
            treeViewResult.Nodes.Add(root);
            treeViewResult.ExpandAll();
        }

        private void listBoxSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxActualSelection.SelectedItems.Count == 0)
            {
                treeActualViewSelection.Nodes.Clear();
                return;
            }
            SelectionContainer sec = (SelectionContainer)listBoxActualSelection.SelectedItems[0];
            List<ISerializableObject> list = sec.RelatedObjects;
            treeActualViewSelection.Nodes.Clear();
            treeActualViewSelection.Nodes.Add(displayListTopDown(list, sec.Root));
            treeActualViewSelection.ExpandAll();
        }


        private void buttonDeselection_Click(object sender, EventArgs e)
        {
        
            // Start Thread for ProgressInformation 
            this.startProgressThread("Remove selected items from actual selection list");
            this.setProgressValue(0);

            float rate = 0;
            if (listBoxActualSelection.SelectedItems.Count > 0)
                rate = 80 / listBoxActualSelection.SelectedItems.Count;
            int index = 0;

            this.setProgressInformation("Transfer selected items back in result items");

            List<SelectionContainer> lastDeselection = new List<SelectionContainer>();
            foreach (Object o in listBoxActualSelection.SelectedItems)
            {
                index++;
                this.setProgressValue(Int32.Parse((index * rate).ToString()));

                SelectionContainer sec = (SelectionContainer)o;
                lastDeselection.Add(sec);
                ISerializableObject iso = sec.Owner;
                listBoxResult.Items.Add(iso);
            }
            this.setProgressInformation("Remove selected items from actual selection list");
            foreach (SelectionContainer sec in lastDeselection)
            {
                listBoxActualSelection.Items.Remove(sec);
            }
            this.setProgressValue(100);
            this.setProgressInformation("Finished");

            this.endProgressThread(false);
        }
       
        #endregion
        #region Treeview

        //#region BottomUp
        private TreeNode displayBottomUpIdentificationUnitAnalysis(IdentificationUnitAnalysis iua)
        {
            TreeNode iuaNode = new TreeNode();
            TreeNode subRootNode = null;
            parametrizeIUANode(iua, iuaNode);
            IdentificationUnit iu = iua.IdentificationUnit;
            if (iu != null)
            {
                subRootNode = displayBottomUpIdentificationUnit(iu, iuaNode);
            }
            if (subRootNode != null)
            {
                return subRootNode;
            }
            return iuaNode;
        }

        private TreeNode displayBottomUpIdentificationUnit(IdentificationUnit iu)
        {
            TreeNode iuNode = new TreeNode();
            TreeNode subRootNode = null;
            parametrizeIUNode(iu, iuNode);
            IdentificationUnit parent = iu.RelatedUnit;
            if (parent != null)
            {
                subRootNode = displayBottomUpIdentificationUnit(parent, iuNode);
            }
            else
            {
                CollectionSpecimen spec = iu.CollectionSpecimen;
                subRootNode = displayBottomUpSpecimen(spec, iuNode);
            }
            if (subRootNode != null)
            {
                return subRootNode;
            }
            return iuNode;
        }

        private TreeNode displayBottomUpIdentificationUnit(IdentificationUnit iu, TreeNode childNode)
        {
            TreeNode iuNode = new TreeNode();
            TreeNode subRootNode = null;
            parametrizeIUNode(iu, iuNode);
            IdentificationUnit parent = iu.RelatedUnit;
            iuNode.Nodes.Add(childNode);
            if (parent != null)
            {
                subRootNode = displayBottomUpIdentificationUnit(parent, iuNode);
            }
            else
            {
                CollectionSpecimen spec = iu.CollectionSpecimen;
                subRootNode = displayBottomUpSpecimen(spec, iuNode);
            }
            if (subRootNode != null)
            {
                return subRootNode;
            }
            return iuNode;
        }

        private TreeNode displayBottomUpSpecimen(CollectionSpecimen specimen)
        {
            TreeNode specNode = new TreeNode();
            TreeNode subRootNode = null;
            parametrizeSpecimenNode(specimen, specNode);
            CollectionEvent ev = specimen.CollectionEvent;
            if (ev != null)
            {
                subRootNode = displayBottomUpEvent(ev, specNode);
                return subRootNode;
            }
            return specNode;
        }

        private TreeNode displayBottomUpSpecimen(CollectionSpecimen specimen, TreeNode childNode)
        {
            TreeNode specNode = new TreeNode();
            TreeNode subRootNode = null;
            parametrizeSpecimenNode(specimen, specNode);
            specNode.Nodes.Add(childNode);
            CollectionEvent ev = specimen.CollectionEvent;
            if (ev != null)
            {
                subRootNode = displayBottomUpEvent(ev, specNode);
                return subRootNode;
            }
            return specNode;
        }

        private TreeNode displayBottomUpEvent(CollectionEvent ev)
        {
            TreeNode evNode = new TreeNode();
            TreeNode subRootNode = null;
            parametrizeEventNode(ev, evNode);
            //Property und LocalisationSystem einbinden
            CollectionEventSeries es = ev.CollectionEventSeries;
            if (es != null)
            {
                subRootNode = displayBottomUpEventSeries(es, evNode);
                return subRootNode;
            }
            return evNode;
        }

        private TreeNode displayBottomUpEvent(CollectionEvent ev, TreeNode childNode)
        {
            TreeNode eventNode = new TreeNode();
            TreeNode subRootNode = null;
            parametrizeEventNode(ev, eventNode);
            eventNode.Nodes.Add(childNode);
            //Property und LocalisationSystem einbinden
            CollectionEventSeries es = ev.CollectionEventSeries;
            if (es != null)
            {
                subRootNode = displayBottomUpEventSeries(es, eventNode);
                return subRootNode;
            }
            return eventNode;
        }

        private TreeNode displayBottomUpEventSeries(CollectionEventSeries es, TreeNode childNode)
        {
            TreeNode esNode = new TreeNode();
            parametrizeEventSeriesNode(es, esNode);
            esNode.Nodes.Add(childNode);
            return esNode;
        }

        //#endregion

        #region Top-Down

        private TreeNode displayTopDownEventSeries(CollectionEventSeries es)
        {
            TreeNode esNode = new TreeNode();
            parametrizeEventSeriesNode(es, esNode);
            if (es.CollectionEvents.First()!=null)
            {
                CollectionEvent first = es.CollectionEvents.First();
                esNode.Nodes.Add(displayTopDownEvent(first));
                while (es.CollectionEvents.HasNext())
                {
                    CollectionEvent next = es.CollectionEvents.Next();
                    esNode.Nodes.Add(displayTopDownEvent(next));
                }
            }
            return esNode;
        }

        private TreeNode displayTopDownEvent(CollectionEvent ev)
        {
            TreeNode eventNode = new TreeNode();
            parametrizeEventNode(ev, eventNode);
            if (ev.CollectionSpecimen.First() != null)
            {
                eventNode.Nodes.Add(displayTopDownSpecimen(ev.CollectionSpecimen.First()));
                while(ev.CollectionSpecimen.HasNext())
                {
                    CollectionSpecimen next=ev.CollectionSpecimen.Next();
                    eventNode.Nodes.Add(displayTopDownSpecimen(next));
                }
            }
            return eventNode;
        }

        private TreeNode displayTopDownSpecimen(CollectionSpecimen spec)
        {
            TreeNode specimenNode = new TreeNode();
            parametrizeSpecimenNode(spec, specimenNode);
            IRestriction r1 = RestrictionFactory.Eq(typeof(IdentificationUnit), "_CollectionSpecimenID", spec.CollectionSpecimenID);
            IRestriction r2 = RestrictionFactory.Eq(typeof(IdentificationUnit), "_RelatedUnitID", null);
            IRestriction iuRestriction = RestrictionFactory.And().Add(r1).Add(r2);
            IList<ISerializableObject> ius = serializer.Connector.LoadList(typeof(IdentificationUnit), iuRestriction);
            foreach (IdentificationUnit iu in ius)
            {
                specimenNode.Nodes.Add(displayTopDownIdentificationUnit(iu));
            }
            return specimenNode;
        }
        private TreeNode displayTopDownIdentificationUnit(IdentificationUnit iUnit)
        {
            TreeNode iuNode = new TreeNode();
            parametrizeIUNode(iUnit, iuNode);
            if (iUnit.IdentificationUnitAnalysis.First() != null)
            {
                iuNode.Nodes.Add(displayTopDownIUA(iUnit.IdentificationUnitAnalysis.First()));
                while (iUnit.IdentificationUnitAnalysis.HasNext())
                {
                    IdentificationUnitAnalysis next = iUnit.IdentificationUnitAnalysis.Next();
                    iuNode.Nodes.Add(displayTopDownIUA(next));
                }
            }
            if (iUnit.ChildUnits.First() != null)
            {
                iuNode.Nodes.Add(displayTopDownIdentificationUnit(iUnit.ChildUnits.First()));
                while (iUnit.ChildUnits.HasNext())
                {
                    IdentificationUnit next = iUnit.ChildUnits.Next();
                    iuNode.Nodes.Add(displayTopDownIdentificationUnit(next));
                }
            }
            return iuNode;
        }
        private TreeNode displayTopDownIUA(IdentificationUnitAnalysis iua)
        {
            TreeNode iuaNode = new TreeNode();
            parametrizeIUANode(iua, iuaNode);
            return iuaNode;
        }

        //Noch zusätzlich mit Berücksichtigung von Restriktionen

        //private TreeNode displayTopDownEventSeries(CollectionEventSeries es)
        //{
        //    TreeNode esNode = new TreeNode();
        //    parametrizeEventSeriesNode(es, esNode);
        //    if (es.CollectionEvents.CountItems() > 0)
        //    {
        //        CollectionEvent first = es.CollectionEvents.First();
        //        esNode.Nodes.Add(displayTopDownEvent(first));
        //        while (es.CollectionEvents.HasNext())
        //        {
        //            CollectionEvent next = es.CollectionEvents.Next();
        //            esNode.Nodes.Add(displayTopDownEvent(next));
        //        }
        //    }
        //    return esNode;
        //}

        //private TreeNode displayTopDownEvent(CollectionEvent ev)
        //{
        //    TreeNode eventNode = new TreeNode();
        //    parametrizeEventNode(ev, eventNode);
        //    IRestriction r = RestrictionFactory.Eq(typeof(CollectionSpecimen), "_CollectionEventID", ev.CollectionEventID);
        //    IRestriction specimenRestriction = RestrictionFactory.And().Add(r).Add(completeRestriction);
        //    IList<ISerializableObject> specimen = serializer.Connector.LoadList(typeof(CollectionSpecimen), specimenRestriction);
        //    foreach (CollectionSpecimen spec in specimen)
        //    {
        //        eventNode.Nodes.Add(displaySpecimen(spec));
        //    }
        //    return eventNode;
        //}

        //private TreeNode displaySpecimen(CollectionSpecimen spec)
        //{
        //    TreeNode specimenNode = new TreeNode();
        //    parametrizeSpecimenNode(spec, specimenNode);
        //    IRestriction r1 = RestrictionFactory.Eq(typeof(IdentificationUnit), "_CollectionSpecimenID", spec.CollectionSpecimenID);
        //    IRestriction r2 = RestrictionFactory.Eq(typeof(IdentificationUnit), "_RelatedUnitID", null);
        //    IRestriction iuRestriction = RestrictionFactory.And().Add(r1).Add(r2).Add(completeRestriction);
        //    IList<ISerializableObject> ius = serializer.Connector.LoadList(typeof(IdentificationUnit), iuRestriction);
        //    foreach (IdentificationUnit iu in ius)
        //    {
        //        specimenNode.Nodes.Add(displayTopDownIdentificationUnit(iu));
        //    }
        //    return specimenNode;
        //}
        //private TreeNode displayTopDownIdentificationUnit(IdentificationUnit iUnit)
        //{
        //    TreeNode iuNode = new TreeNode();
        //    parametrizeIUNode(iUnit, iuNode);
        //    IRestriction analysisRestriction = RestrictionFactory.Eq(typeof(IdentificationUnitAnalysis), "_IdentificationUnitID", iUnit.IdentificationUnitID);
        //    IRestriction iuaRestriction = RestrictionFactory.And().Add(analysisRestriction).Add(completeRestriction);
        //    IList<ISerializableObject> iuas = serializer.Connector.LoadList(typeof(IdentificationUnitAnalysis), iuaRestriction);
        //    foreach (IdentificationUnitAnalysis iua in iuas)
        //    {
        //        TreeNode iuaNode = new TreeNode();
        //        parametrizeIUANode(iua, iuaNode);
        //        iuaNode.Nodes.Add(iuaNode);
        //    }
        //    IRestriction r = RestrictionFactory.Eq(typeof(IdentificationUnit), "_RelatedUnitID", iUnit.RelatedUnitID);
        //    IRestriction iuRestriction = RestrictionFactory.And().Add(r).Add(completeRestriction);
        //    IList<ISerializableObject> ius = serializer.Connector.LoadList(typeof(IdentificationUnit), iuRestriction);
        //    foreach (IdentificationUnit iu in ius)
        //    {
        //        iuNode.Nodes.Add(displayTopDownIdentificationUnit(iu));
        //    }
        //    return iuNode;
        //}

        private TreeNode displayListTopDown(List<ISerializableObject> objects, ISerializableObject root)
        {
            TreeNode tn = new TreeNode();
            if (root.GetType().Equals(typeof(CollectionEventSeries)))
            {
                CollectionEventSeries cs = (CollectionEventSeries)root;
                parametrizeEventSeriesNode(cs, tn);
                foreach (ISerializableObject iso in objects)
                {
                    if (iso.GetType().Equals(typeof(CollectionEvent)))
                    {
                        tn.Nodes.Add(displayListTopDown(objects, iso));
                    }
                }
            }
            if (root.GetType().Equals(typeof(CollectionEvent)))
            {
                CollectionEvent ce = (CollectionEvent)root;
                parametrizeEventNode(ce, tn, objects);
                foreach (ISerializableObject iso in objects)
                {
                    if (iso.GetType().Equals(typeof(CollectionSpecimen)))
                        tn.Nodes.Add(displayListTopDown(objects, iso));
                }
            }
            if (root.GetType().Equals(typeof(CollectionSpecimen)))
            {
                CollectionSpecimen spec = (CollectionSpecimen)root;
                parametrizeSpecimenNode(spec, tn, objects);
                foreach (ISerializableObject iso in objects)
                {
                    if (iso.GetType().Equals(typeof(IdentificationUnit)))
                    {
                        IdentificationUnit iu = (IdentificationUnit)iso;
                        if (iu.CollectionSpecimenID == spec.CollectionSpecimenID && iu.RelatedUnitID == null)
                            tn.Nodes.Add(displayListTopDown(objects, iu));
                    }
                }
            }
            if (root.GetType().Equals(typeof(IdentificationUnit)))
            {
                IdentificationUnit iu = (IdentificationUnit)root;
                parametrizeIUNode(iu, tn);//Muss nicht auf die Liste angewendet werden, da weder ein Iterator verwendet wird noch auf die DB zugegriffen wird)
                foreach (ISerializableObject iso in objects)
                {
                    if (iso.GetType().Equals(typeof(IdentificationUnitAnalysis)))
                    {
                        IdentificationUnitAnalysis iua = (IdentificationUnitAnalysis)iso;
                        if (iua.IdentificationUnitID == iu.IdentificationUnitID)
                        {
                            TreeNode analysisNode = new TreeNode();
                            parametrizeIUANode(iua, analysisNode);
                            tn.Nodes.Add(analysisNode);
                        }
                    }
                    if (iso.GetType().Equals(typeof(IdentificationUnit)))
                    {
                        IdentificationUnit childUnit = (IdentificationUnit)iso;
                        if (childUnit.RelatedUnitID == iu.IdentificationUnitID)
                        {
                            tn.Nodes.Add(displayListTopDown(objects, childUnit));
                        }
                    }
                }
            }
            return tn;
        }

        //private TreeNode displayListTopDown(List<ISerializableObject> objects, ISerializableObject root)
        //{
        //    TreeNode tn = new TreeNode();
        //    if (root.GetType().Equals(typeof(CollectionEventSeries)))
        //    {
        //        CollectionEventSeries cs = (CollectionEventSeries)root;
        //        this._tvOperations.parametrizeEventSeriesNode(cs, tn);
        //        foreach (ISerializableObject iso in objects)
        //        {
        //            if (iso.GetType().Equals(typeof(CollectionEvent)))
        //            {
        //                tn.Nodes.Add(displayListTopDown(objects, iso));
        //            }
        //        }
        //    }
        //    if (root.GetType().Equals(typeof(CollectionEvent)))
        //    {
        //        CollectionEvent ce = (CollectionEvent)root;
        //        this._tvOperations.parametrizeEventNode(ce, tn, objects);
        //        foreach (ISerializableObject iso in objects)
        //        {
        //            if (iso.GetType().Equals(typeof(CollectionSpecimen)))
        //                tn.Nodes.Add(displayListTopDown(objects, iso));
        //        }
        //    }
        //    if (root.GetType().Equals(typeof(CollectionSpecimen)))
        //    {
        //        CollectionSpecimen spec = (CollectionSpecimen)root;
        //        this._tvOperations.parametrizeSpecimenNode(spec, tn, objects);
        //        foreach (ISerializableObject iso in objects)
        //        {
        //            if (iso.GetType().Equals(typeof(IdentificationUnit)))
        //            {
        //                IdentificationUnit iu = (IdentificationUnit)iso;
        //                if (iu.CollectionSpecimenID == spec.CollectionSpecimenID && iu.RelatedUnitID == null)
        //                    tn.Nodes.Add(displayListTopDown(objects, iu));
        //            }
        //        }
        //    }
        //    if (root.GetType().Equals(typeof(IdentificationUnit)))
        //    {
        //        IdentificationUnit iu = (IdentificationUnit)root;
        //        this._tvOperations.parametrizeIUNode(iu, tn);//Muss nicht auf die Liste angewendet werden, da weder ein Iterator verwendet wird noch auf die DB zugegriffen wird)
        //        foreach (ISerializableObject iso in objects)
        //        {
        //            if (iso.GetType().Equals(typeof(IdentificationUnitAnalysis)))
        //            {
        //                IdentificationUnitAnalysis iua = (IdentificationUnitAnalysis)iso;
        //                if (iua.IdentificationUnitID == iu.IdentificationUnitID)
        //                {
        //                    TreeNode analysisNode = new TreeNode();
        //                    this._tvOperations.parametrizeIUANode(iua, analysisNode);
        //                    tn.Nodes.Add(analysisNode);
        //                }
        //            }
        //            if (iso.GetType().Equals(typeof(IdentificationUnit)))
        //            {
        //                IdentificationUnit childUnit = (IdentificationUnit)iso;
        //                if (childUnit.RelatedUnitID == iu.IdentificationUnitID)
        //                {
        //                    tn.Nodes.Add(displayListTopDown(objects, childUnit));
        //                }
        //            }
        //        }
        //    }
        //    return tn;
        //}
        #endregion
       #endregion
        #region Node Parametrization

        private static void parametrizeIUANode(IdentificationUnitAnalysis iua, TreeNode iuaNode)
        {
            Analysis analysis = iua.Analysis;
            iuaNode.Text = analysis.DisplayText + ": " + iua.AnalysisResult + " " + analysis.MeasurementUnit;
            iuaNode.ImageIndex = iuaNode.SelectedImageIndex = (int)IconIndex.Analysis;
            iuaNode.Tag = TreeViewNodeTypes.AnalysisNode;
        }

        private static void parametrizeIUNode(IdentificationUnit iu, TreeNode iuNode)
        {
            if (iu.UnitIdentifier == null || iu.UnitIdentifier.Equals(String.Empty))
            {
                iuNode.Text = iu.LastIdentificationCache;
            }
            else
            {
                iuNode.Text = String.Concat("(", iu.UnitIdentifier, ") ", iu.LastIdentificationCache);
            }
            iuNode.Tag = TreeViewNodeTypes.IdentificationUnitNode;
            iuNode.ImageIndex = 0;
            iuNode.SelectedImageIndex = 0;

            if (iu.TaxonomicGroup != null)
            {
                switch (iu.TaxonomicGroup.ToLower())
                {
                    case "plant":
                        if (iu.UnitDescription != null)
                        {
                            switch (iu.UnitDescription.ToLower())
                            {
                                case "tree":
                                    iuNode.ImageIndex = (int)IconIndex.Tree;
                                    iuNode.SelectedImageIndex = (int)IconIndex.Tree;
                                    break;
                                case "branch":
                                    iuNode.ImageIndex = (int)IconIndex.Branch;
                                    iuNode.SelectedImageIndex = (int)IconIndex.Branch;
                                    break;
                                case "leaf":
                                    iuNode.ImageIndex = (int)IconIndex.Plant;
                                    iuNode.SelectedImageIndex = (int)IconIndex.Plant;
                                    break;
                                case "gall":
                                    iuNode.ImageIndex = (int)IconIndex.Other;
                                    iuNode.SelectedImageIndex = (int)IconIndex.Other;
                                    break;
                                default:
                                    iuNode.ImageIndex = (int)IconIndex.Tree;
                                    iuNode.SelectedImageIndex = (int)IconIndex.Tree;
                                    break;
                            }
                        }
                        break;
                    default:
                        int index;
                        try
                        {
                            index = (int)Enum.Parse(typeof(IconIndex), iu.TaxonomicGroup.ToLower(), true);
                        }
                        catch (Exception)
                        {
                            index = (int)IconIndex.Unknown;
                        }
                        iuNode.ImageIndex = index;
                        iuNode.SelectedImageIndex = index;
                        break;
                }
            }
        }

        private void parametrizeSpecimenNode(CollectionSpecimen spec, TreeNode specimenNode)
        {
            specimenNode.Text = spec.CollectionSpecimenID.ToString();
            specimenNode.ImageIndex = (int)IconIndex.Specimen;
            specimenNode.SelectedImageIndex = (int)IconIndex.SpecimenRed;
            specimenNode.Tag = TreeViewNodeTypes.SpecimenNode;
            IRestriction r = RestrictionFactory.Eq(typeof(CollectionAgent), "_CollectionSpecimenID", spec.CollectionSpecimenID);
            IList<ISerializableObject> agents = serializer.Connector.LoadList(typeof(CollectionAgent), r);
            foreach (CollectionAgent agent in agents)
            {
                TreeNode agentNode = new TreeNode();
                parameterizeCollectionAgentNode(agent, agentNode);
                specimenNode.Nodes.Add(agentNode);
            }
        }

        private void parametrizeSpecimenNode(CollectionSpecimen spec, TreeNode specimenNode, List<ISerializableObject> list)
        {
            specimenNode.Text = spec.CollectionSpecimenID.ToString();
            specimenNode.ImageIndex = (int)IconIndex.Specimen;
            specimenNode.SelectedImageIndex = (int)IconIndex.SpecimenRed;
            specimenNode.Tag = TreeViewNodeTypes.SpecimenNode;
            foreach (ISerializableObject iso in list)
            {
                if (iso.GetType().Equals(typeof(CollectionAgent)))
                {
                    CollectionAgent agent = (CollectionAgent)iso;
                    if (agent.CollectionSpecimenID == spec.CollectionSpecimenID)
                    {
                        TreeNode agentNode = new TreeNode();
                        parameterizeCollectionAgentNode(agent, agentNode);
                        specimenNode.Nodes.Add(agentNode);
                    }
                }
            }
        }

        private void parameterizeCollectionAgentNode(CollectionAgent agent, TreeNode node)
        {
            node.Text = agent.CollectorsName;
            node.ImageIndex = node.SelectedImageIndex = (int)IconIndex.Agent;
            node.Tag = TreeViewNodeTypes.AgentNode;
        }

        private void parametrizeEventNode(CollectionEvent ev, TreeNode eventNode)
        {
            StringBuilder sbEventTitle = new StringBuilder();
            sbEventTitle.Append(ev.CollectionDate.Year);
            sbEventTitle.Append('/');
            sbEventTitle.Append(ev.CollectionDate.Month);
            sbEventTitle.Append('/');
            sbEventTitle.Append(ev.CollectionDate.Day);

            if (ev.CollectorsEventNumber != null && !ev.CollectorsEventNumber.Equals(string.Empty))
            {
                sbEventTitle.Append(": No.");
                sbEventTitle.Append(ev.CollectorsEventNumber);
            }

            if (ev.LocalityDescription != null && !ev.LocalityDescription.Equals(string.Empty))
            {
                sbEventTitle.Append(" (");
                sbEventTitle.Append(ev.LocalityDescription);
                sbEventTitle.Append(")");
            }
            eventNode.Text = sbEventTitle.ToString();
            eventNode.ImageIndex = eventNode.SelectedImageIndex = (int)IconIndex.Event;
            IRestriction propRestriction = RestrictionFactory.Eq(typeof(CollectionEventProperty), "_CollectionEventID", ev.CollectionEventID);
            IList<ISerializableObject> properties = serializer.Connector.LoadList(typeof(CollectionEventProperty), propRestriction);
            foreach (CollectionEventProperty cp in properties)
            {
                TreeNode propNode = new TreeNode();
                parameterizeCollectionEventPropertyNode(cp, propNode);
                eventNode.Nodes.Add(propNode);
            }
            IRestriction locRestriction = RestrictionFactory.Eq(typeof(CollectionEventLocalisation), "_CollectionEventID", ev.CollectionEventID);
            IList<ISerializableObject> localisations = serializer.Connector.LoadList(typeof(CollectionEventLocalisation), locRestriction);
            foreach (CollectionEventLocalisation loc in localisations)
            {
                TreeNode locNode = new TreeNode();
                parameterizeCollectionEventLocalisationNode(loc, locNode);
                eventNode.Nodes.Add(locNode);
            }
        }

        private void parametrizeEventNode(CollectionEvent ev, TreeNode eventNode, List<ISerializableObject> list)
        {
            StringBuilder sbEventTitle = new StringBuilder();
            sbEventTitle.Append(ev.CollectionDate.Year);
            sbEventTitle.Append('/');
            sbEventTitle.Append(ev.CollectionDate.Month);
            sbEventTitle.Append('/');
            sbEventTitle.Append(ev.CollectionDate.Day);

            if (ev.CollectorsEventNumber != null && !ev.CollectorsEventNumber.Equals(string.Empty))
            {
                sbEventTitle.Append(": No.");
                sbEventTitle.Append(ev.CollectorsEventNumber);
            }

            if (ev.LocalityDescription != null && !ev.LocalityDescription.Equals(string.Empty))
            {
                sbEventTitle.Append(" (");
                sbEventTitle.Append(ev.LocalityDescription);
                sbEventTitle.Append(")");
            }
            eventNode.Text = sbEventTitle.ToString();
            eventNode.ImageIndex = eventNode.SelectedImageIndex = (int)IconIndex.Event;
            foreach (ISerializableObject iso in list)
            {
                if (iso.GetType().Equals(typeof(CollectionEventProperty)))
                {
                    CollectionEventProperty cp = (CollectionEventProperty)iso;
                    if (cp.CollectionEventID == ev.CollectionEventID)
                    {
                        TreeNode propNode = new TreeNode();
                        parameterizeCollectionEventPropertyNode(cp, propNode);
                        eventNode.Nodes.Add(propNode);
                    }
                }
                if (iso.GetType().Equals(typeof(CollectionEventLocalisation)))
                {
                    CollectionEventLocalisation loc = (CollectionEventLocalisation)iso;
                    if (loc.CollectionEventID == ev.CollectionEventID)
                    {
                        TreeNode locNode = new TreeNode();
                        parameterizeCollectionEventLocalisationNode(loc, locNode);
                        eventNode.Nodes.Add(locNode);
                    }
                }
            }
        }

        private void parameterizeCollectionEventPropertyNode(CollectionEventProperty ceProp, TreeNode node)
        {
            //Property prop = ceProp.Property;
            IRestriction res = RestrictionFactory.Eq(typeof(Property), "_PropertyID", ceProp.PropertyID);
            Property prop = serializer.Connector.Load<Property>(res);
            node.Text = prop.DisplayText + ": " + ceProp.DisplayText;
            node.ImageIndex = node.SelectedImageIndex = (int)IconIndex.SiteProperty;
            node.Tag = TreeViewNodeTypes.SitePropertyNode;
        }

        private  void parameterizeCollectionEventLocalisationNode(CollectionEventLocalisation ceLoc, TreeNode node)
        {
            //LocalisationSystem loc = ceLoc.LocalisationSystem;
            IRestriction res = RestrictionFactory.Eq(typeof(LocalisationSystem), "_LocalisationSystemID", ceLoc.LocalisationSystemID);
            LocalisationSystem loc = this.serializer.Connector.Load<LocalisationSystem>(res);
            node.Text = loc.DisplayText + ": " + ceLoc.Location1 + @";" + ceLoc.Location2;
            node.ImageIndex = node.SelectedImageIndex = (int)IconIndex.Location;
            node.Tag = TreeViewNodeTypes.LocalisationNode;

        }

        private static void parametrizeEventSeriesNode(CollectionEventSeries ceSeries, TreeNode node)
        {
            node.Text = ceSeries.SeriesCode;
            node.ImageIndex = node.SelectedImageIndex = (int)IconIndex.EventSeries;
            node.Tag = TreeViewNodeTypes.EventSeriesNode;
        }
        #endregion

        //private void buttonFinish_Click(object sender, EventArgs e)
        //{
        //    treeViewSelection.Nodes.Clear();
        //    List<ISerializableObject> roots = new List<ISerializableObject>();
        //    foreach (Object o in listBoxActualSelection.Items)
        //    {
        //        SelectionContainer sec = (SelectionContainer)o;
        //        foreach (ISerializableObject iso in sec.RelatedObjects)
        //        {
        //            if (!selectedGuids.Contains(iso.Rowguid))
        //            {
        //                selectedGuids.Add(iso.Rowguid);
        //                selectedObjects.Add(iso);
        //                if (isRoot(iso) == true)
        //                    roots.Add(iso);
        //            }
        //            if (!selectedItems.ContainsKey(iso.Rowguid))
        //                selectedItems.Add(iso.Rowguid, iso);
        //            this.treeViewSelection.ExpandAll();
        //        }
        //    }
        //    foreach (ISerializableObject root in roots)
        //    {
        //        treeViewSelection.Nodes.Add(displayListTopDown(selectedObjects, root));
        //    }
        //    treeViewSelection.Visible = true;
        //    buttonReturn.Visible = true;
        //    buttonSynchronize.Visible = true;
        //}

        private void buttonFinish_Click(object sender, EventArgs e)
        {
            treeViewSelection.Nodes.Clear();
            List<ISerializableObject> roots = new List<ISerializableObject>();
            foreach (Object o in listBoxActualSelection.Items)
            {
                SelectionContainer sec = (SelectionContainer)o;
                foreach (ISerializableObject iso in sec.RelatedObjects)
                {
                    if (!selectedGuids.Contains(iso.Rowguid))
                    {
                        selectedGuids.Add(iso.Rowguid);
                        selectedObjects.Add(iso);
                        if (isRoot(iso) == true)
                            roots.Add(iso);
                    }
                    //if (!selectedItems.ContainsKey(iso.Rowguid))
                    //    selectedItems.Add(iso.Rowguid, iso);
                    this.treeViewSelection.ExpandAll();
                }
            }
            foreach (ISerializableObject root in roots)
            {
                treeViewSelection.Nodes.Add(displayListTopDown(selectedObjects, root));
            }
            treeViewSelection.Visible = true;
            buttonReturn.Visible = true;
            buttonSynchronize.Visible = true;
        }
        bool isRoot(ISerializableObject iso)
        {
            if(iso.GetType().Equals(typeof(CollectionEventSeries)))
                return true;
            if(iso.GetType().Equals(typeof(CollectionEvent)))
            {
                CollectionEvent ce=(CollectionEvent) iso;
                if(ce.SeriesID==null)
                    return true;
            }
            if (iso.GetType().Equals(typeof(CollectionSpecimen)))
            {
                CollectionSpecimen spec = (CollectionSpecimen)iso;
                if (spec.CollectionEventID == null)
                    return true;
            }
            return false;
        }
        //public Dictionary<Guid, ISerializableObject> Selection { get { return selectedItems; } }
        public List<ISerializableObject> Selection { get { return selectedObjects; } }

        private void buttonReturn_Click(object sender, EventArgs e)
        {
            treeViewSelection.Nodes.Clear();
            treeViewSelection.Visible = false;
            buttonSynchronize.Visible = false;
            buttonReturn.Visible = false;
            selectedGuids.Clear();
            selectedObjects.Clear();
        }

        private void buttonSynchronize_Click(object sender, EventArgs e)
        {

        }

        private void buttonSelectAll_Click(object sender, EventArgs e)
        {
            // Start Thread for ProgressInformation 
            this.startProgressThread("Select all Items of Result for Synchronization");
            this.setProgressValue(0);

            float rate = 0;
            if (listBoxResult.Items.Count > 0)
                rate = 95 / listBoxResult.Items.Count;
            int index = 0;

            foreach(Object o in listBoxResult.Items)
            {
                index++;
                this.setProgressValue(Int32.Parse((index * rate).ToString()));

                ISerializableObject iso = (ISerializableObject)o;
                SelectionContainer sec = new SelectionContainer(iso, checkBoxTruncate.Checked);
                listBoxActualSelection.Items.Add(sec);
            }
            listBoxResult.Items.Clear();

            this.setProgressValue(100);
            this.setProgressInformation("Finished");

            this.endProgressThread(false);
        }

        private void comboBoxSelectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxSelectType.SelectedItem.ToString().ToLower().Equals("identification unit"))
            {
                this.panelEventSeries.Visible = false;
                this.panelSamplingPlot.Visible = false;
                this.panelIdentificationUnits.Visible = true;
            }
            else if (this.comboBoxSelectType.SelectedItem.ToString().ToLower().Equals("sampling plot"))
            {
                this.panelEventSeries.Visible = false;
                this.panelIdentificationUnits.Visible = false;
                this.panelSamplingPlot.Visible = true;
            }
            else if (this.comboBoxSelectType.SelectedItem.ToString().ToLower().Equals("collection event series"))
            {
                this.panelIdentificationUnits.Visible = false;
                this.panelSamplingPlot.Visible = false;
                this.panelEventSeries.Visible = true;
            }
            else
            {
                this.panelEventSeries.Visible = false;
                this.panelIdentificationUnits.Visible = false;
                this.panelSamplingPlot.Visible = false;
            }
        }

        #region Progress Information

        private void startProgressThread()
        {
            this.progressThread = new ProgressThread();
            this.progressThread.MyThread.Start();
            this.setProgressInformation("");
            this.setActionInformation("");
        }

        private void startProgressThread(string description)
        {
            this.progressThread = new ProgressThread();
            this.progressThread.MyThread.Start();
            this.setActionInformation(description);
        }

        private void setProgressInformation(String additionalInformation)
        {
            if (this.progressThread != null)
            {
                this.progressThread.AdditionalInformation = additionalInformation;
            }
        }

        private void setActionInformation(String actionInformation)
        {
            if (this.progressThread != null)
            {
                this.progressThread.ActionInformation = actionInformation;
                this.setProgressInformation("");
            }
        }

        private void setProgressValue(int value)
        {
            if (this.progressThread != null)
            {
                this.progressThread.Value = value;
            }
        }

        private void endProgressThread(bool cancel)
        {
            if (this.progressThread != null)
            {
                if (cancel)
                {
                    this.setProgressInformation("Excecution was canceled");
                    this.setProgressValue(0);
                }
                else
                {
                    this.setProgressInformation("Complete");
                    this.setProgressValue(100);
                }

                this.progressThread.ShowCloseButton = true;

                this.progressThread.MyThread.Join();
            }
        }

        #endregion
    }
}
