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
using Microsoft.WindowsMobile.Forms;
using Microsoft.WindowsMobile.Status;

using UBT.AI4.Bio.DivMobi.DataManagement;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;
using UBT.AI4.Bio.DivMobi.Forms.Forms;
using UBT.AI4.Bio.DivMobi.Forms.ContextForms;
using UBT.AI4.Bio.DivMobi.Forms.Dialogs;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer.Collections;
using UBT.AI4.Bio.DiversityCollection.Ressource.GPS;
using UBT.AI4.Bio.DivMobi.DataItemFormTools;
using UBT.AI4.Bio.DivMobi.UMF.Context;


namespace UBT.AI4.Bio.DiversityCollection.Mobile.Forms
{
    public partial class MainForm : Form
    {
        #region Properties
        private CollectionEvents _events = null;
        private Specimen         _specimen = null;
        private IdentificationUnits _iu = null;
        private bool _expandTrigger = true;
        private ImageList _currentDiversityImageList = new ImageList();
        private ImageList _currentToolbarImageList = new ImageList();
        private TreeViewOperations _tvOperations;
        private TreeViewNodeTypes _actualLvl;
        private TreeNode _lastSelection;
        List<Point> locationsPictureBox;
        
        #endregion

        #region Constructors

        public MainForm()
        {          
            // initialization related to windows forms
            Cursor.Current = Cursors.WaitCursor;
            try 
            {
		        InitializeComponent();
            }
            catch (NotSupportedException)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Diversity Mobile is not supported by your system!", "Not supported", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                this.Close();
            }
            
#if DEBUG
            this.MinimizeBox = false;
            this.ControlBox = true;
#else
            this.MinimizeBox = true;
            this.ControlBox = true;            
#endif
            
            this._actualLvl = TreeViewNodeTypes.Root;
            this._currentToolbarImageList = imageListToolbarButtons;
            this._currentDiversityImageList = imageListDiversityCollection;     
        }

        public MainForm(UserProfile up)
            : this()
        {
            try
            {
                UserProfiles.Instance.Current = up;
            }
            catch (ConnectionCorruptedException ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Severe Program Exception (" + ex.Message + "). Program couldn't be started.");
                this.Close();
            }
        }

        #endregion

        #region Events

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Visible = true;
            try
            {
                if (UserProfiles.Instance.Current != null)
                {
                    try
                    {
                        _tvOperations = new TreeViewOperations((ExpandLevel)UserProfiles.Instance.Current.Displaylevel.Index, EventSeriess.Instance.Connector);
                    }
                    catch (TreeViewOperationsException ex)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Severe Program Exception (" + ex.Message + "). Program couldn't be started.");
                        this.Close();
                    }
                }

                // Toolbar Icons on the basis of UserProfile
                this.setIconSize();

                //Load AllEventSeries
                this.displayAllEventSeries();
                this.enableDisableButtons(TreeViewNodeTypes.Root);
                this.enableDisableToolbarButtons(_actualLvl);
                EventSeriess.Instance.Current = null;
            }
            catch (ConnectionCorruptedException ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Severe Program Exception (" + ex.Message + "). Program couldn't be started.");
                this.Close();
            }

            // GPS
            try
            {
                this.StartGPS();
                StaticGPS.updatePositionDataHandler = new EventHandler(UpdatePositionData);
                StaticGPS.updateDeviceDataHandler = new EventHandler(UpdateDeviceData);
                if (StaticGPS.device == null || StaticGPS.device.DeviceState == GpsServiceState.Off || StaticGPS.position == null)
                {
                    this.setPictureBoxImage((int)TreeViewIconIndex.GPSGrey, this.pictureBoxGPS);
                    this.setPictureBoxImage((int)TreeViewIconIndex.Location0, this.pictureBoxNewLocalisation);
                }
                else
                {
                    if (StaticGPS.position != null)
                    {
                        if (StaticGPS.position.SatelliteCount <= 2)
                        {
                            this.setPictureBoxImage((int)TreeViewIconIndex.GPSGrey, this.pictureBoxGPS);
                        }
                        else
                        {
                            this.setPictureBoxImage((int)TreeViewIconIndex.GPS, this.pictureBoxGPS);
                        }

                        if (StaticGPS.position.SatelliteCount < 4)
                            this.setPictureBoxImage((int)TreeViewIconIndex.Location0, this.pictureBoxNewLocalisation);
                        else if (StaticGPS.position.SatelliteCount == 4)
                            this.setPictureBoxImage((int)TreeViewIconIndex.Location4, this.pictureBoxNewLocalisation);
                        else if (StaticGPS.position.SatelliteCount == 5)
                            this.setPictureBoxImage((int)TreeViewIconIndex.Location5, this.pictureBoxNewLocalisation);
                        else if (StaticGPS.position.SatelliteCount == 6)
                            this.setPictureBoxImage((int)TreeViewIconIndex.Location6, this.pictureBoxNewLocalisation);
                        else
                            this.setPictureBoxImage((int)TreeViewIconIndex.LocationMore, this.pictureBoxNewLocalisation);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("GPS functions aren't available. ("+ex.Message+")", "GPS Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            
            Cursor.Current = Cursors.Default;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            IList<PictureBox> list = new List<PictureBox>();
            list.Add(this.pictureBoxNewAnalysis);
            list.Add(this.pictureBoxNewIdentificationUnit);
            list.Add(this.pictureBoxNewSpecimen);
            list.Add(this.pictureBoxNewLocalisation);
            list.Add(this.pictureBoxNewIUGeoAnalysis);
            list.Add(this.pictureBoxNewEventProperty);
            list.Add(this.pictureBoxNewEvent);
            list.Add(this.pictureBoxNewEventSeries);
            //list.Add(this.pictureBoxHome);
            //list.Add(this.pictureBoxEdit);
            //list.Add(this.pictureBoxDelete);
            //list.Add(this.pictureBoxGPS);
            calculatePictureBoxPositions(list);
            locationsPictureBox = new List<Point>();
            this.initializeLocations(list, locationsPictureBox);
            this.groupHorizontal(list, locationsPictureBox);
        }

        /// <summary>
        /// Handles the ButtonClick event of the toolBar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.ToolBarButtonClickEventArgs"/> instance containing the event data.</param>
        private void toolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            try
            {
                if (e.Button == this.toolBarButtonMoveFirst && this.treeViewFieldData.SelectedNode != null)
                {
                    TreeNode node = this.treeViewFieldData.SelectedNode;
                    TreeViewNodeData data = node.Tag as TreeViewNodeData;
                    try
                    {
                        switch (data.NodeType)
                        {
                            case TreeViewNodeTypes.EventSeriesNode:
                                if (this._tvOperations.expandLvl < ExpandLevel.EventSeries)
                                    return;
                                EventSeriess.Instance.Current = null;
                                this.displayEventSeries(null);
                                this.labelPosition.Text = EventSeriess.Instance.Position;
                                break;
                            case TreeViewNodeTypes.EventNode:
                                if (this._tvOperations.expandLvl < ExpandLevel.Event)
                                    return;
                                if (this._events != null)
                                {
                                    this.displayEvent(this._events.First);
                                    this.labelPosition.Text = this._events.Position;
                                }
                                break;
                            case TreeViewNodeTypes.SpecimenNode:
                                if (this._tvOperations.expandLvl < ExpandLevel.Specimen)
                                    return;
                                if (this._specimen != null)
                                {
                                    this.displaySpecimen(this._specimen.First);
                                    this.labelPosition.Text = this._specimen.Position;
                                }
                                break;
                            case TreeViewNodeTypes.IdentificationUnitNode:
                                if (this._tvOperations.expandLvl < ExpandLevel.IdentificationUnit)
                                    return;
                                if (this._iu != null)
                                {
                                    this.displayIdentificationUnit(this._iu.First);
                                    this.labelPosition.Text = this._iu.Position;
                                }
                                break;
                            default:
                                return;
                        }
                    }
                    catch (DataFunctionsException ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    this.afterSelect(treeViewFieldData.SelectedNode);
                    return;
                }
                else if (e.Button == this.toolBarButtonMoveLast && this.treeViewFieldData.SelectedNode != null)
                {
                    TreeNode node = this.treeViewFieldData.SelectedNode;
                    TreeViewNodeData data = node.Tag as TreeViewNodeData;
                    try
                    {
                        switch (data.NodeType)
                        {
                            case TreeViewNodeTypes.EventSeriesNode:
                                if (this._tvOperations.expandLvl < ExpandLevel.EventSeries)
                                    return;
                                this.displayEventSeries(EventSeriess.Instance.Last);
                                this.labelPosition.Text = EventSeriess.Instance.Position;
                                break;
                            case TreeViewNodeTypes.EventNode:
                                if (this._tvOperations.expandLvl < ExpandLevel.Event)
                                    return;
                                if (this._events != null)
                                {
                                    this.displayEvent(this._events.Last);
                                    this.labelPosition.Text = this._events.Position;
                                }
                                break;
                            case TreeViewNodeTypes.SpecimenNode:
                                if (this._tvOperations.expandLvl < ExpandLevel.Specimen)
                                    return;
                                if (this._specimen != null)
                                {
                                    this.displaySpecimen(this._specimen.Last);
                                    this.labelPosition.Text = this._specimen.Position;
                                }
                                break;
                            case TreeViewNodeTypes.IdentificationUnitNode:
                                if (this._tvOperations.expandLvl < ExpandLevel.IdentificationUnit)
                                    return;
                                if (this._iu != null)
                                {
                                    this.displayIdentificationUnit(this._iu.Last);
                                    this.labelPosition.Text = this._iu.Position;
                                }
                                break;
                            default:
                                return;
                        }
                    }
                    catch (DataFunctionsException ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    this.afterSelect(treeViewFieldData.SelectedNode);
                    return;
                }
                else if (e.Button == this.toolBarButtonNext && this.treeViewFieldData.SelectedNode != null)
                {
                    TreeNode node = this.treeViewFieldData.SelectedNode;
                    TreeViewNodeData data = node.Tag as TreeViewNodeData;
                    try
                    {
                        switch (data.NodeType)
                        {
                            case TreeViewNodeTypes.EventSeriesNode:
                                if (this._tvOperations.expandLvl < ExpandLevel.EventSeries)
                                    return;
                                if (EventSeriess.Instance.Current == null)
                                {
                                    this.displayEventSeries(EventSeriess.Instance.First);
                                    this.labelPosition.Text = EventSeriess.Instance.Position;
                                    return;
                                }
                                if (EventSeriess.Instance.HasNext)
                                {
                                    this.displayEventSeries(EventSeriess.Instance.Next);
                                    this.labelPosition.Text = EventSeriess.Instance.Position;
                                }
                                else
                                    return;
                                break;
                            case TreeViewNodeTypes.EventNode:
                                if (this._events != null)
                                {
                                    if (this._events.HasNext)
                                    {
                                        if (this._tvOperations.expandLvl < ExpandLevel.Event)
                                            return;
                                        this.displayEvent(this._events.Next);
                                        this.labelPosition.Text = this._events.Position;
                                    }
                                    else
                                        return;
                                }
                                break;
                            case TreeViewNodeTypes.SpecimenNode:
                                if (this._specimen != null)
                                {
                                    if (this._specimen.HasNext)
                                    {
                                        if (this._tvOperations.expandLvl < ExpandLevel.Specimen)
                                            return;

                                        this.displaySpecimen(this._specimen.Next);
                                        this.labelPosition.Text = this._specimen.Position;
                                    }
                                    else
                                        return;
                                }
                                break;
                            case TreeViewNodeTypes.IdentificationUnitNode:
                                if (this._iu != null)
                                {
                                    if (this._iu.HasNext)
                                    {
                                        if (this._tvOperations.expandLvl < ExpandLevel.IdentificationUnit)
                                            return;

                                        this.displayIdentificationUnit(this._iu.Next);
                                        this.labelPosition.Text = this._iu.Position;

                                    }
                                    else
                                        return;
                                }
                                break;
                            default:
                                return;
                        }
                    }
                    catch (DataFunctionsException ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    this.afterSelect(treeViewFieldData.SelectedNode);
                    return;
                }
                else if (e.Button == this.toolBarButtonPrevious && this.treeViewFieldData.SelectedNode != null)
                {
                    TreeNode node = this.treeViewFieldData.SelectedNode;
                    TreeViewNodeData data = node.Tag as TreeViewNodeData;
                    try
                    {
                        switch (data.NodeType)
                        {
                            case TreeViewNodeTypes.EventSeriesNode:
                                if (EventSeriess.Instance.HasPrevious)
                                {
                                    if (this._tvOperations.expandLvl < ExpandLevel.EventSeries)
                                        return;
                                    this.toolBarButtonLevelPicture.ImageIndex = 8;
                                    this.displayEventSeries(EventSeriess.Instance.Previous);
                                    this.labelPosition.Text = EventSeriess.Instance.Position;
                                }
                                else
                                {
                                    if (this._tvOperations.expandLvl < ExpandLevel.EventSeries || EventSeriess.Instance.Current == null)
                                        return;
                                    this.toolBarButtonLevelPicture.ImageIndex = 8;
                                    this.displayEventSeries(null);
                                    this.labelPosition.Text = EventSeriess.Instance.Position;
                                }
                                break;
                            case TreeViewNodeTypes.EventNode:
                                if (this._events != null)
                                {
                                    if (this._events.HasPrevious)
                                    {
                                        if (this._tvOperations.expandLvl < ExpandLevel.Event)
                                            return;
                                        this.toolBarButtonLevelPicture.ImageIndex = 9;
                                        this.displayEvent(this._events.Previous);
                                        this.labelPosition.Text = this._events.Position;
                                    }
                                    else
                                        return;
                                }
                                break;
                            case TreeViewNodeTypes.SpecimenNode:
                                if (this._specimen != null)
                                {
                                    if (this._specimen.HasPrevious)
                                    {
                                        if (this._tvOperations.expandLvl < ExpandLevel.Specimen)
                                            return;
                                        this.toolBarButtonLevelPicture.ImageIndex = 10;
                                        this.displaySpecimen(this._specimen.Previous);
                                        this.labelPosition.Text = this._specimen.Position;
                                    }
                                    else
                                        return;
                                }
                                break;
                            case TreeViewNodeTypes.IdentificationUnitNode:
                                if (this._iu != null)
                                {
                                    if (this._iu.HasPrevious)
                                    {
                                        if (this._tvOperations.expandLvl < ExpandLevel.IdentificationUnit)
                                            return;
                                        this.toolBarButtonLevelPicture.ImageIndex = 12;
                                        this.displayIdentificationUnit(this._iu.Previous);
                                        this.labelPosition.Text = this._iu.Position;
                                    }
                                    else
                                        return;
                                }
                                break;
                            default:
                                return;
                        }
                    }
                    catch (DataFunctionsException ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    this.afterSelect(treeViewFieldData.SelectedNode);
                    return;
                }
                else if (e.Button == this.toolBarButtonSearch)
                {
                    SearchForm dlg = new SearchForm();     //Suche bis Anpassung deaktiviert  
                    if (dlg.ShowDialog() == DialogResult.Yes)
                    {
                        switch (dlg.type)
                        {
                            case "Event":
                                this.displayEvent(SearchResults.Instance.getEvent(dlg.resultID));
                                break;
                            case "Specimen":
                                this.displayEvent(SearchResults.Instance.getEventForSpecimen(dlg.resultID));
                                break;
                            case "IU":
                                this.displayEvent(SearchResults.Instance.getEventForIU(dlg.resultID));
                                break;
                        }
                    }

                    this.afterSelect(treeViewFieldData.SelectedNode);
                }
                else if (e.Button == this.toolBarButtonUserProfile)
                {
                    UserProfileDialog dlg = null;
                    try
                    {
                        dlg   = new UserProfileDialog(true);

                        if (dlg == null)
                            return;
                    }
                    catch (ContextCorruptedException ex)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show(ex.Message, "Context Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }

                    // Dialog zentrieren
                    dlg.Location = new Point((this.Size.Width) / 2 - (dlg.Size.Width) / 2, this.Location.Y);
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        this._actualLvl = TreeViewNodeTypes.Unknown;
                        if (treeViewFieldData.SelectedNode == null)
                        {
                            this.pictureBoxHome_Click(null, null);
                            return;
                        }
                        UserProfile up = UserProfiles.Instance.Current;
                        this.setIconSize();
                        if (up != null)
                            try
                            {
                                this._tvOperations = new TreeViewOperations((ExpandLevel)up.Displaylevel.Index, EventSeriess.Instance.Connector);
                            }
                            catch (TreeViewOperationsException ex)
                            {
                                Cursor.Current = Cursors.Default;
                                MessageBox.Show("Severe Program Exception (" + ex.Message + "). Program couldn't be started.");
                                this.Close();
                            }
                        this.afterSelect(treeViewFieldData.SelectedNode);
                        this.treeViewFieldData.Refresh();
                        Cursor.Current = Cursors.Default;
                    }
                }
                else if (e.Button == this.toolBarButtonEditContext)//Das darf doch nicht wahr sein
                {
                    EditContextForm dlg = new EditContextForm();
                    // Dialog zentrieren
                    dlg.Location = new Point((this.Size.Width) / 2 - (dlg.Size.Width) / 2, this.Location.Y);
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                    }
                }
            }
            catch (ConnectionCorruptedException ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                this.pictureBoxHome_Click(null, null);
                return;
            }
        }

        #region TreeView Events

        /// <summary>
        /// Handles the AfterSelect event of the treeViewFieldData control. 
        /// Mainly buttons are en- or disabled on demand.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.TreeViewEventArgs"/> instance containing the event data.</param>
        private void treeViewFieldData_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByKeyboard || e.Action == TreeViewAction.ByMouse)//Muss rein wegen StackOverflowError
            {
                this.afterSelect(e.Node);
            }
        }
        // Auslagern der After Select Funktionalität, um direkten Zugriff zu gewährleisten
        private void afterSelect(TreeNode node)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (node != null)
            {
                TreeViewNodeData data = node.Tag as TreeViewNodeData;
                if (data == null)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("While selecting a node a Display Error occured!", "Display Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    pictureBoxHome_Click(null, null);
                    return;
                }
                TreeNode represent;

                try
                {
                    switch (data.NodeType)
                    {
                        case TreeViewNodeTypes.EventSeriesNode:
                            CollectionEventSeries es = null;
                            if (data.ID != null)
                            {
                                if (EventSeriess.Instance.Find((int)data.ID))
                                {
                                    es = EventSeriess.Instance.Current;
                                    this._events = EventSeriess.Instance.CollectionEvents;
                                }
                                else
                                    throw new ArgumentOutOfRangeException("EventSeries not found");
                            }
                            else
                            {
                                EventSeriess.Instance.Current = null;
                                this._events = new CollectionEvents(null);
                            }
                            this.labelPosition.Text = EventSeriess.Instance.Position;
                            this._actualLvl = TreeViewNodeTypes.EventSeriesNode;
                            represent = this._tvOperations.findRepresentantOfType(node);
                            if (represent.Text.Equals("-1"))
                            {
                                displayEventSeries(es);
                            }
                            else if (represent != this._tvOperations.findRepresentantOfType(this._lastSelection))
                            {
                                try
                                {
                                    displayEventSeries(es);
                                }
                                catch (Exception e)
                                {
                                    Cursor.Current = Cursors.Default;
                                    MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                    pictureBoxHome_Click(null, null);
                                    return;
                                }
                            }
                            this._lastSelection = this.treeViewFieldData.SelectedNode;
                            break;
                        case TreeViewNodeTypes.EventNode:
                            CollectionEvent ce = DataFunctions.Instance.RetrieveCollectionEvent((int)data.ID);
                            if (ce != null)
                            {
                                int? serID = ce.SeriesID;
                                this._events = new CollectionEvents(serID);
                                this._events.Find(ce.CollectionEventID);
                                ce = this._events.Current;
                            }
                            else
                            {
                                Cursor.Current = Cursors.Default;
                                MessageBox.Show("DisplayError", "DisplayError", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                pictureBoxHome_Click(null, null);
                                return;
                            }
                            //Absichern, dass das Event über "+" gefunden wurde.
                            //Absichern, dass das event nicht gefunden wird
                            //Absichern, dass die EventSeries korrekt ist
                            //Absichern, dass specimen belegt ist
                            
                            this._specimen = this._events.Specimen;
                            this.labelPosition.Text = this._events.Position;
                            this._actualLvl = TreeViewNodeTypes.EventNode;
                            represent = this._tvOperations.findRepresentantOfType(node);
                            if (represent.Text.Equals("-1"))
                            {
                                displayEvent(ce);
                            }
                            else if (represent != this._tvOperations.findRepresentantOfType(this._lastSelection))
                            {
                                try
                                {
                                    displayEvent(ce);
                                }
                                catch (Exception e)
                                {
                                    Cursor.Current = Cursors.Default;
                                    MessageBox.Show(e.Message, "DisplayError", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                    pictureBoxHome_Click(null, null);
                                    return;
                                }
                            }
                            this._lastSelection = this.treeViewFieldData.SelectedNode;
                            break;
                        case TreeViewNodeTypes.SpecimenNode:
                            CollectionSpecimen spec = DataFunctions.Instance.RetrieveCollectionSpecimen((int)data.ID);//Wárum DataFunctions?
                            //Absichern, dass das Event über "+" gefunden wurde.
                            //Absichern, dass das event nicht gefunden wird
                            //Absichern, dass die EventSeries korrekt ist
                            //Absichern, dass specimen belegt ist
                            if (spec != null)
                            {
                                int? serID = spec.CollectionEvent.SeriesID;
                                CollectionEvent ev = spec.CollectionEvent;
                                this._events = new CollectionEvents(serID);
                                this._events.Find(ev.CollectionEventID);
                                try
                                {
                                    this._specimen = this._events.Specimen;
                                }
                                catch (DataFunctionsException ex)
                                {
                                    Cursor.Current = Cursors.Default;
                                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                    pictureBoxHome_Click(null, null);
                                    return;
                                }

                                if (!this._specimen.Find(spec.CollectionSpecimenID))
                                {
                                    Cursor.Current = Cursors.Default;
                                    MessageBox.Show("Data of selected Specimen couldn't be retrieved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                    pictureBoxHome_Click(null, null);
                                    return;
                                }
                                this._iu = this._specimen.IdentificationUnits;
                                spec = this._specimen.Current;//Durch Find erledigt, oder
                                this.labelPosition.Text = this._specimen.Position;
                                this._actualLvl = TreeViewNodeTypes.SpecimenNode;
                                represent = this._tvOperations.findRepresentantOfType(node);
                                if (represent.Text.Equals("-1"))
                                {
                                    displaySpecimen(spec);
                                }
                                else if (represent != this._tvOperations.findRepresentantOfType(this._lastSelection))
                                {
                                    try
                                    {
                                        displaySpecimen(spec);
                                    }
                                    catch (Exception e)
                                    {
                                        Cursor.Current = Cursors.Default;
                                        MessageBox.Show(e.Message, "DisplayError", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                        pictureBoxHome_Click(null, null);
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                Cursor.Current = Cursors.Default;
                                MessageBox.Show("DisplayError", "DisplayError", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                pictureBoxHome_Click(null, null);
                                return;
                            }
                            this._lastSelection = this.treeViewFieldData.SelectedNode;
                            break;
                        case TreeViewNodeTypes.IdentificationUnitNode:
                            IdentificationUnit iu = DataFunctions.Instance.RetrieveIdentificationUnit((int)data.ID);
                            //Absichern, dass das Event über "+" gefunden wurde.
                            //Absichern, dass das event nicht gefunden wird
                            //Absichern, dass die EventSeries korrekt ist
                            //Absichern, dass specimen belegt ist
                            if (iu != null)
                            {
                                int? sID = iu.CollectionSpecimen.CollectionEvent.SeriesID;
                                CollectionEvent evIu = iu.CollectionSpecimen.CollectionEvent;
                                this._events = new CollectionEvents(sID);
                                this._events.Find(evIu.CollectionEventID);
                                try
                                {
                                    this._specimen = this._events.Specimen;
                                }
                                catch (DataFunctionsException ex)
                                {
                                    Cursor.Current = Cursors.Default;
                                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                    pictureBoxHome_Click(null, null);
                                    return;
                                }

                                if (!this._specimen.Find(iu.CollectionSpecimenID))
                                {
                                    Cursor.Current = Cursors.Default;
                                    MessageBox.Show("Data of selected Specimen couldn't be retrieved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                    pictureBoxHome_Click(null, null);
                                    return;
                                }

                                this._iu = this._specimen.IdentificationUnits;

                                if (this._iu == null)
                                {
                                    Cursor.Current = Cursors.Default;
                                    MessageBox.Show("Data of associated Identification Units couldn't be retrieved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                    pictureBoxHome_Click(null, null);
                                    return;
                                }

                                this._iu.FindTopLevelIU(this.findIUTopLevelID(iu));
                                this._actualLvl = TreeViewNodeTypes.IdentificationUnitNode;
                                this.labelPosition.Text = this._iu.Position;
                                represent = this._tvOperations.findRepresentantOfType(node);
                                if (represent.Text.Equals("-1"))
                                {
                                    displayIdentificationUnit(iu);
                                }
                                else if (represent != this._tvOperations.findRepresentantOfType(this._lastSelection))
                                {
                                    try
                                    {
                                        displayIdentificationUnit(iu);
                                    }
                                    catch (Exception e)
                                    {
                                        Cursor.Current = Cursors.Default;
                                        MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                        pictureBoxHome_Click(null, null);
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                Cursor.Current = Cursors.Default;
                                MessageBox.Show("DisplayError", "DisplayError", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                pictureBoxHome_Click(null, null);
                                return;
                            }
                            this._lastSelection = this.treeViewFieldData.SelectedNode;
                            break;
                        case TreeViewNodeTypes.AnalysisNode:
                            this._actualLvl = TreeViewNodeTypes.AnalysisNode;
                            this.labelPosition.Text = String.Empty;
                            break;
                        case TreeViewNodeTypes.GeographyNode:
                            this._actualLvl = TreeViewNodeTypes.GeographyNode;
                            this.labelPosition.Text = String.Empty;
                            break;
                        case TreeViewNodeTypes.LocalisationNode:
                            this._actualLvl = TreeViewNodeTypes.LocalisationNode;
                            this.labelPosition.Text = String.Empty;
                            break;
                        case TreeViewNodeTypes.AgentNode:
                            this._actualLvl = TreeViewNodeTypes.AgentNode;
                            this.labelPosition.Text = String.Empty;
                            break;
                        case TreeViewNodeTypes.SitePropertyNode:
                            this._actualLvl = TreeViewNodeTypes.SitePropertyNode;
                            this.labelPosition.Text = String.Empty;
                            break;
                        default:
                            this._actualLvl = TreeViewNodeTypes.Unknown;
                            break;

                    }
                }
                catch (ConnectionCorruptedException ex)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    pictureBoxHome_Click(null, null);
                    return;
                }
                
                Cursor.Current = Cursors.Default;
                enableDisableToolbarButtons(_actualLvl);
                enableDisableButtons(_actualLvl);
            }
        }

        private void treeViewFieldData_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            this._lastSelection = treeViewFieldData.SelectedNode;
        }

        private void treeViewFieldData_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if(this.treeViewFieldData.SelectedNode != this._lastSelection)
                 afterSelect(this.treeViewFieldData.SelectedNode);
        }

        private void treeViewFieldData_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            //Sollte nicht immer ausgeführt werden.
            if (e.Node.Nodes == null)
                return;
            if (this._expandTrigger == true && e.Node.Nodes[0].Tag == null)
            {
                this.beforeExpand(e.Node);
            }
        }

        private void beforeExpand(TreeNode node) //Hier darf Find nicht verwendet werden, weil nicht selected wird
        {
            if (node == null || node.Tag == null)
            {
                MessageBox.Show("ExpandError");
                return;
            }

            Cursor.Current = Cursors.WaitCursor;
            TreeViewNodeData data = node.Tag as TreeViewNodeData;

            try
            {
                switch (data.NodeType)
                {
                    case TreeViewNodeTypes.EventSeriesNode:
                        CollectionEventSeries es = null;
                        if (data.ID != null)
                        {
                            try
                            {
                                es = DataFunctions.Instance.RetrieveEventSeries((int)data.ID);
                            }
                            catch (DataFunctionsException)
                            {
                                Cursor.Current = Cursors.Default;
                                es = null;
                            }
                        }
                        if (es != null)
                        {
                            if (es.CollectionEvents.First() != null)
                            {
                                node.Nodes.RemoveAt(0);//Leeren Knoten löschen
                                CollectionEvent first = es.CollectionEvents.First();
                                TreeNode eventNode = new TreeNode();
                                this._tvOperations.parametrizeEventNode(first, eventNode);
                                if (first.CollectionSpecimen.First() != null)
                                    eventNode.Nodes.Insert(0, new TreeNode());
                                node.Nodes.Add(eventNode);
                                while (es.CollectionEvents.HasNext())
                                {
                                    CollectionEvent next = es.CollectionEvents.Next();
                                    TreeNode nextNode = new TreeNode();
                                    this._tvOperations.parametrizeEventNode(next, nextNode);
                                    if (next.CollectionSpecimen.First() != null)
                                        nextNode.Nodes.Insert(0, new TreeNode());
                                    node.Nodes.Add(nextNode);
                                }
                            }
                        }
                        else
                        {
                            IList<CollectionEvent> events = DataFunctions.Instance.RetrieveCollectionEventsForSeries(null);
                            node.Nodes.RemoveAt(0);
                            foreach (CollectionEvent ce in events)
                            {
                                if (ce != null)
                                {
                                    TreeNode eventNode = new TreeNode();
                                    this._tvOperations.parametrizeEventNode(ce, eventNode);
                                    if (ce.CollectionSpecimen.First() != null)
                                        eventNode.Nodes.Insert(0, new TreeNode());
                                    node.Nodes.Add(eventNode);
                                }
                            }
                        }
                        break;
                    case TreeViewNodeTypes.EventNode:
                        CollectionEvent cEvent;
                        try
                        {
                            cEvent = DataFunctions.Instance.RetrieveCollectionEvent((int)data.ID);
                        }
                        catch (DataFunctionsException ex)
                        {
                            Cursor.Current = Cursors.Default;
                            cEvent = null;
                        }
                        node.Nodes.RemoveAt(0);
                        if (cEvent != null)
                        {
                            if (cEvent.CollectionSpecimen.First() != null)
                            {
                                CollectionSpecimen spec = cEvent.CollectionSpecimen.First();
                                TreeNode specNode = new TreeNode();
                                this._tvOperations.parametrizeOnlySpecimenNode(spec, specNode);
                                if (spec.IdentificationUnits.First() != null)
                                    specNode.Nodes.Insert(0, new TreeNode());
                                node.Nodes.Add(specNode);
                            }
                            while (cEvent.CollectionSpecimen.HasNext())
                            {
                                CollectionSpecimen spec = cEvent.CollectionSpecimen.Next();
                                TreeNode specNode = new TreeNode();
                                this._tvOperations.parametrizeOnlySpecimenNode(spec, specNode);
                                if (spec.IdentificationUnits.First() != null)
                                    specNode.Nodes.Insert(0, new TreeNode());
                                node.Nodes.Add(specNode);
                            }
                        }
                        break;
                    case TreeViewNodeTypes.SpecimenNode:
                        CollectionSpecimen cs = DataFunctions.Instance.RetrieveCollectionSpecimen((int)data.ID);
                        node.Nodes.RemoveAt(0);
                        if (cs != null)
                        {
                            if (cs.IdentificationUnits.First() != null)
                            {
                                IdentificationUnit iu = cs.IdentificationUnits.First();
                                if (iu.RelatedUnit == null)
                                {
                                    TreeNode iuNode = this._tvOperations.displayIdentificationUnit(iu);
                                    node.Nodes.Add(iuNode);
                                }
                            }
                            while (cs.IdentificationUnits.HasNext())
                            {
                                IdentificationUnit iu = cs.IdentificationUnits.Next();
                                if (iu.RelatedUnit == null)
                                {
                                    TreeNode iuNode = this._tvOperations.displayIdentificationUnit(iu);
                                    node.Nodes.Add(iuNode);
                                }
                            }
                        }
                        break;
                    case TreeViewNodeTypes.IdentificationUnitNode:
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("DisplayError!");
                        this.pictureBoxHome_Click(null, null);
                        return;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                pictureBoxHome_Click(null, null);
                return;
            }
            Cursor.Current = Cursors.Default;
        }

        #endregion

        #region PictureBoxMenu Events

        private void pictureBoxEdit_Click(object sender, EventArgs e)
        {
            if (this.treeViewFieldData.SelectedNode == null)
            {
                return;
            }

            //this.saveEventProperties();            
            TreeNode node = this.treeViewFieldData.SelectedNode;
            TreeViewNodeData data = node.Tag as TreeViewNodeData;

            if (data == null)
            {
                MessageBox.Show("Node information not found!");
                return;
            }

            try
            {
                switch (data.NodeType)
                {
                    case TreeViewNodeTypes.EventSeriesNode: // eventSeries node
                        CollectionEventSeries ceSeries = null;
                        if (data.ID != null)
                        {
                            EventSeriess.Instance.Find((int)data.ID);
                            ceSeries = EventSeriess.Instance.Current;
                        }
                        if (ceSeries == null)
                        {
                            MessageBox.Show("You can't edit the NoEvent-Series!");
                            return;
                        }
                        
                        EventSeriesDialog dlg = new EventSeriesDialog(ceSeries);
                        // Dialog zentrieren
                        dlg.Location = new Point((this.Size.Width) / 2 - (dlg.Size.Width) / 2, this.Location.Y);
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            Cursor.Current = Cursors.WaitCursor;
                            try
                            {
                                DataFunctions.Instance.Update(dlg.EventSeries);
                                this._tvOperations.parametrizeEventSeriesNode(ceSeries, node);//Komplett neu Zeichnen?
                            }
                            catch (DataFunctionsException ex)
                            {
                                Cursor.Current = Cursors.Default;
                                MessageBox.Show("Changed data couldn't be saved. (" + ex.Message + ")", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            }
                            this.treeViewFieldData.SelectedNode = node;
                            this.treeViewFieldData.Refresh();
                            Cursor.Current = Cursors.Default;
                            this.treeViewFieldData.Focus();
                        }
                        return;
                    case TreeViewNodeTypes.EventNode: // event node
                        // Show CollectionEventForm
                        if (this._events.Current != null)
                        {
                            if (data.ID != this._events.Current.CollectionEventID)
                            {
                                MessageBox.Show("Display Error!");
                                pictureBoxHome_Click(null, null);
                                return;
                            }
                        }
                        CollectionEventForm ceDetails = new CollectionEventForm(this._events.Current);
                        if (ceDetails.ShowDialog() == DialogResult.OK)
                        {
                            Cursor.Current = Cursors.WaitCursor;
                            this._tvOperations.parametrizeOnlyEventNode(this._events.Current, node);
                            this.treeViewFieldData.SelectedNode = node;
                            this.treeViewFieldData.Refresh();
                            Cursor.Current = Cursors.Default;
                            this.treeViewFieldData.Focus();
                        }
                        return;
                    case TreeViewNodeTypes.SpecimenNode: // specimen node
                        // Show CollectionSpecimenForm
                        if (this._specimen.Current != null)
                        {
                            if (data.ID != this._specimen.Current.CollectionSpecimenID)
                            {
                                MessageBox.Show("DisplayError!");
                                pictureBoxHome_Click(null, null);
                                return;
                            }
                        }
                        SpecimenEditForm csDetails = new SpecimenEditForm(this._specimen.Current);
                        if (csDetails.ShowDialog() == DialogResult.OK)
                        {
                            Cursor.Current = Cursors.WaitCursor;
                            this._tvOperations.parametrizeOnlySpecimenNode(this._specimen.Current, node);
                            this.treeViewFieldData.SelectedNode = node;
                            this.treeViewFieldData.Refresh();
                            Cursor.Current = Cursors.Default;
                            this.treeViewFieldData.Focus();
                        }
                        return;
                    case TreeViewNodeTypes.IdentificationUnitNode: // IdentificationUnit node
                        IdentificationUnit iu = DataFunctions.Instance.RetrieveIdentificationUnit((int)data.ID);

                        if (iu == null)
                        {
                            MessageBox.Show("Selected Identification Unit couldn't be retrieved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            return;
                        }

                        if (this._iu.Current != null)
                        {
                            if (this.findIUTopLevelID(iu) != this._iu.Current.IdentificationUnitID)
                            {
                                MessageBox.Show("DisplayError!");
                                pictureBoxHome_Click(null, null);
                                return;
                            }
                        }
                        // Show IdentificationUnitForm
                        IdentificationUnitForm iuDetails = new IdentificationUnitForm(iu);

                        if (iuDetails.ShowDialog() == DialogResult.OK)
                        {
                            Cursor.Current = Cursors.WaitCursor;
                            this._tvOperations.parametrizeOnlyIUNode(iu, node);
                            this.treeViewFieldData.SelectedNode = node;
                            this.treeViewFieldData.Refresh();
                            Cursor.Current = Cursors.Default;
                            this.treeViewFieldData.Focus();
                        }
                        return;
                    case TreeViewNodeTypes.AnalysisNode:
                        IdentificationUnitAnalysis iua = DataFunctions.Instance.RetrieveIdentificationUnitAnalysis(data.CollectionSpecimenID, data.IdentificationUnitID, data.AnalysisID, data.AnalysisNumber);
                        if (iua == null)
                        {
                            MessageBox.Show("Selected Identification Unit Analysis couldn't be retrieved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            return;
                        }

                        // Show IdentificationUnitAnalysisForm
                        IdentificationUnitAnalysisForm iuaDetails = new IdentificationUnitAnalysisForm(iua);

                        if (iuaDetails.ShowDialog() == DialogResult.OK)
                        {
                            Cursor.Current = Cursors.WaitCursor;
                            this._tvOperations.parametrizeIUANode(iua, node);
                            this.treeViewFieldData.SelectedNode = node;
                            this.treeViewFieldData.Refresh();
                            Cursor.Current = Cursors.Default;
                            this.treeViewFieldData.Focus();
                        }
                        return;
                    case TreeViewNodeTypes.GeographyNode:
                        MessageBox.Show("Details for IdentificationUnitGeoanlyses cannot be displayed. Use \"Show Map\" in the Context Menu to Edit");
                        return;
                    case TreeViewNodeTypes.LocalisationNode: // localisation node
                        // Show EventLocalisationForm with Details
                        CollectionEventLocalisation ceLoc = DataFunctions.Instance.RetrieveCollectionEventLocalisation((int)data.ID, this._events.Current.CollectionEventID);
                        if (ceLoc == null)
                        {
                            MessageBox.Show("Selected Localisation couldn't be retrieved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            return;
                        }
                        LocalisationDetailForm eventLocForm = new LocalisationDetailForm(ceLoc);
                        if (eventLocForm.ShowDialog() == DialogResult.OK)
                        {
                            Cursor.Current = Cursors.WaitCursor;
                            this._tvOperations.parameterizeCollectionEventLocalisationNode(eventLocForm._ceLoc, this.treeViewFieldData.SelectedNode);
                            this.treeViewFieldData.SelectedNode = node;
                            this.treeViewFieldData.Refresh();
                            Cursor.Current = Cursors.Default;
                            this.treeViewFieldData.Focus();
                        }
                        return;
                    case TreeViewNodeTypes.SitePropertyNode: // localisation node
                        // Show EventPropertyForm with Details
                        CollectionEventProperty ceProp = DataFunctions.Instance.RetrieveCollectionEventProperty((int)data.ID, this._events.Current.CollectionEventID);
                        if (ceProp == null)
                        {
                            MessageBox.Show("Selected Event Property couldn't be retrieved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            return;
                        }

                        EventPropertyForm eventPropForm = new EventPropertyForm(ceProp);
                        if (eventPropForm.ShowDialog() == DialogResult.OK)
                        {
                            Cursor.Current = Cursors.WaitCursor;
                            this._tvOperations.parameterizeCollectionEventPropertyNode(eventPropForm._ceProp, node);
                            this.treeViewFieldData.SelectedNode = node;
                            this.treeViewFieldData.Refresh();
                            Cursor.Current = Cursors.Default;
                            this.treeViewFieldData.Focus();
                        }
                        return;
                    case TreeViewNodeTypes.AgentNode: // localisation node
                        // Show AgentNode Details
                        CollectionAgent agent = DataFunctions.Instance.RetrieveCollectionAgent(data.CollectorsName, data.CollectionSpecimenID);
                        if (agent == null)
                        {
                            MessageBox.Show("Selected Agent couldn't be retrieved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            return;
                        }

                        AgentForm agentForm = new AgentForm(agent);
                        if (agentForm.ShowDialog() == DialogResult.OK)
                        {
                            Cursor.Current = Cursors.WaitCursor;
                            this._tvOperations.parameterizeCollectionAgentNode(agentForm._agent, this.treeViewFieldData.SelectedNode);
                            this.treeViewFieldData.SelectedNode = node;
                            this.treeViewFieldData.Refresh();
                            Cursor.Current = Cursors.Default;
                            this.treeViewFieldData.Focus();
                        }
                        this.treeViewFieldData.Focus();
                        return;
                    default:
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("There is no DetailForm for " + data.NodeType.ToString() + "!");
                        return;
                }
            }
            catch (ConnectionCorruptedException ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                pictureBoxHome_Click(null, null);
                return;
            }
            catch (ContextCorruptedException ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message, "Context Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                pictureBoxHome_Click(null, null);
                return;
            }
        }

        private void pictureBoxDelete_Click(object sender, EventArgs e)
        {
            if (this.treeViewFieldData.SelectedNode == null)
            {
                return;
            }
            TreeNode node = this.treeViewFieldData.SelectedNode;
            TreeViewNodeData data = node.Tag as TreeViewNodeData;
            
            if (data == null)
                return;
            if ((treeViewFieldData.SelectedNode.Tag as TreeViewNodeData).NodeType == TreeViewNodeTypes.Unknown || (treeViewFieldData.SelectedNode.Tag as TreeViewNodeData).NodeType == TreeViewNodeTypes.Root)
            {
                return;
            }

            if (data.NodeType == TreeViewNodeTypes.EventSeriesNode)
            {
                bool noEventSeries = false;
                bool emptySeries = false;
                CollectionEventSeries cs;

                if (data.ID == null)
                    noEventSeries = true;
                else
                {
                    try
                    {
                        cs = EventSeriess.Instance.Current;
                    }
                    catch (ConnectionCorruptedException ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }

                    if (cs.CollectionEvents.First() == null)
                        emptySeries = true;
                }
                if (noEventSeries == true)
                {
                    MessageBox.Show("There is the NoEventSeries selected. You cannot delete it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }
                if (emptySeries == false)
                {
                    MessageBox.Show("It's not allowed to delete CollectionEventSeries containing Events. Delete Events first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }
            }
            if (MessageBox.Show("Do you really want to remove the selected data?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    switch (data.NodeType)
                    {
                        case TreeViewNodeTypes.EventSeriesNode:
                            if (data.ID != EventSeriess.Instance.Current.SeriesID)
                            {
                                Cursor.Current = Cursors.Default;
                                MessageBox.Show("CurrentEventSeries and Selected EventSeries do not match! Abort! GUI inconsistent. Reload");
                                this.pictureBoxHome_Click(null, null);
                                return;
                            }
                            CollectionEventSeries cs = EventSeriess.Instance.Current;
                            
                            EventSeriess.Instance.Remove(cs);

                            if (EventSeriess.Instance.Count == 0)
                            {
                                this.pictureBoxHome_Click(null, null);
                                Cursor.Current = Cursors.Default;
                                return;
                            }
                            this.displayEventSeries(EventSeriess.Instance.Current);//Abhängig von ExpandLevel machen
                            this.enableDisableButtons(TreeViewNodeTypes.EventSeriesNode);
                            this.treeViewFieldData.Refresh();
                            this.treeViewFieldData.Focus();
                            this._actualLvl = TreeViewNodeTypes.EventSeriesNode;
                            Cursor.Current = Cursors.Default;
                            return;
                        case TreeViewNodeTypes.EventNode: // event node
                            // Remove Current Collection Event
                            if (data.ID != this._events.Current.CollectionEventID)
                            {
                                MessageBox.Show("CurrentEvent and Selected Event do not match! Abort! GUI inconsitent. Reload");
                                this.pictureBoxHome_Click(null, null);
                                Cursor.Current = Cursors.Default;
                                return;
                            }
                            this._events.Remove();

                            this.enableDisableButtons(TreeViewNodeTypes.EventNode);
                            this._actualLvl = TreeViewNodeTypes.EventNode;
                            if (this._events.Current == null)//Letztes Event wurde gelöscht.
                            {
                                this.displayEventSeries(EventSeriess.Instance.Current);//Abhängig von ExpandLevel machen
                                this.enableDisableButtons(TreeViewNodeTypes.EventSeriesNode);
                                this._actualLvl = TreeViewNodeTypes.EventSeriesNode;
                                this.enableDisableToolbarButtons(_actualLvl);
                                return;
                            }
                            if (this._events.Count == 0)
                            {
                                this.treeViewFieldData.SelectedNode = node.Parent;
                                node.Remove();
                                this.treeViewFieldData.Refresh();
                                this.treeViewFieldData.Focus();
                                this.afterSelect(treeViewFieldData.SelectedNode);
                                Cursor.Current = Cursors.Default;
                                return;
                            }
                            this.displayEvent(this._events.Current);//Durch Remove geupdated                          
                            this.afterSelect(treeViewFieldData.SelectedNode);
                            this.treeViewFieldData.Refresh();
                            this.treeViewFieldData.Focus();
                            Cursor.Current = Cursors.Default;
                            return;
                        case TreeViewNodeTypes.SpecimenNode: // specimen node
                            if (this._specimen != null && this._specimen.Current != null)
                            {
                                if (data.ID != this._specimen.Current.CollectionSpecimenID)
                                {
                                    MessageBox.Show("CurrentSpecimen and SelectedSpecimen do not match! Abort!");
                                    Cursor.Current = Cursors.Default;
                                    this.pictureBoxHome_Click(null, null);
                                    return;
                                }
                                // Remove Collection Specimen
                                this._specimen.Remove(this._specimen.Current);
                                
                                if (this._specimen.Count == 0)
                                {
                                    this.treeViewFieldData.SelectedNode = node.Parent;
                                    node.Remove();
                                    this.treeViewFieldData.Refresh();
                                    this.treeViewFieldData.Focus();
                                    this.afterSelect(treeViewFieldData.SelectedNode);
                                    Cursor.Current = Cursors.Default;
                                    return;
                                }
                                this.displaySpecimen(this._specimen.Current);//Abhängig von ExpandLevel machen
                                this.enableDisableButtons(TreeViewNodeTypes.SpecimenNode);
                                this._actualLvl = TreeViewNodeTypes.SpecimenNode;
                                this.enableDisableToolbarButtons(_actualLvl);
                            }
                            Cursor.Current = Cursors.Default;
                            break;
                        case TreeViewNodeTypes.IdentificationUnitNode: // IdentificationUnit node
                            // Remove IdentificationUnit
                            if (this._iu != null && this._iu.Current != null)
                            {
                                IdentificationUnit iu = DataFunctions.Instance.RetrieveIdentificationUnit((int)data.ID);
                                if (this.findIUTopLevelID(iu) != this._iu.Current.IdentificationUnitID)
                                {
                                    MessageBox.Show("CurrentIU and SelectedIU do not match! Abort!");
                                    return;
                                }
                                if (iu != null)
                                {
                                    IdentificationUnit parent = iu.RelatedUnit;
                                    if (parent == null)
                                    {
                                        this._iu.Remove(iu);
                                        if (this._iu.Count == 0)
                                        {
                                            this.treeViewFieldData.SelectedNode = node.Parent;
                                            node.Remove();
                                            this.afterSelect(treeViewFieldData.SelectedNode);
                                            this.treeViewFieldData.Refresh();
                                            this.treeViewFieldData.Focus();
                                            Cursor.Current = Cursors.Default;
                                            return;
                                        }
                                        this.displayIdentificationUnit(this._iu.Current);//Abhängig von ExpandLevel machen
                                        this.enableDisableButtons(TreeViewNodeTypes.IdentificationUnitNode);
                                        this._actualLvl = TreeViewNodeTypes.IdentificationUnitNode;
                                        this.enableDisableToolbarButtons(_actualLvl);
                                        return;
                                    }
                                    else
                                    {
                                        DataFunctions.Instance.Remove(iu);//Läuft weiter bis zum break 
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("IdentificationUnit Not Found. DisplayError");
                                    this.pictureBoxHome_Click(null, null);
                                    Cursor.Current = Cursors.Default;
                                    return;
                                }
                            }
                            break;
                        case TreeViewNodeTypes.AnalysisNode: // event node
                            //Remove IdentificationUnitAnalysis
                            IdentificationUnitAnalysis iua = DataFunctions.Instance.RetrieveIdentificationUnitAnalysis(data.CollectionSpecimenID, data.IdentificationUnitID, data.AnalysisID, data.AnalysisNumber);
                            DataFunctions.Instance.Remove(iua);
                            break;
                        case TreeViewNodeTypes.LocalisationNode: // localisation node
                            // Remove LocalisationData
                            CollectionEventLocalisation ceLoc = DataFunctions.Instance.RetrieveCollectionEventLocalisation((int)data.ID, this._events.Current.CollectionEventID);
                            DataFunctions.Instance.Remove(ceLoc);
                            break;
                        case TreeViewNodeTypes.GeographyNode: // geography node
                            // Remove LocalisationData
                            IdentificationUnit parentIU = DataFunctions.Instance.RetrieveIdentificationUnit(data.IdentificationUnitID);
                            IdentificationUnitGeoAnalysis iuGeoAnalysis = parentIU.IdentificationUnitGeoAnalysis.First();
                            DataFunctions.Instance.Remove(iuGeoAnalysis);
                            
                            break;
                        case TreeViewNodeTypes.SitePropertyNode: // site property node
                            // Remove SiteProperty for Event
                            
                            CollectionEventProperty ceProp = DataFunctions.Instance.RetrieveCollectionEventProperty((int)data.ID, this._events.Current.CollectionEventID);
                            DataFunctions.Instance.Remove(ceProp);
                            
                            break;
                        case TreeViewNodeTypes.AgentNode: // site property node 
                            // Remove CollectionAgent for Specimen
                            CollectionAgent agent = DataFunctions.Instance.RetrieveCollectionAgent(data.CollectorsName, data.CollectionSpecimenID);
                            DataFunctions.Instance.Remove(agent);
                            
                            break;
                        default:
                            Cursor.Current = Cursors.Default;
                            return;
                    }
                }
                catch (DataFunctionsException ex)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    this.pictureBoxHome_Click(null, null);
                    return;
                }
                catch (ConnectionCorruptedException ex)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Collection Event Series couldn't be removed. (" + ex.Message + ")", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    this.pictureBoxHome_Click(null, null);
                    return;
                }

                node.Remove();//Was geschieht, wenn der node selected ist?
                if (treeViewFieldData.SelectedNode != null)
                    this.afterSelect(treeViewFieldData.SelectedNode);
                else
                {
                    this.pictureBoxHome_Click(null, null);
                    Cursor.Current = Cursors.Default;
                }
                Cursor.Current = Cursors.Default;
            }
        }

        private void pictureBoxHome_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                EventSeriess.Instance.Current = null;
                this._events = null;
                this._specimen = null;
                this._iu = null;
                //this.resetTreeView(); Anzeige wird nicht aktualisert. Warum?
                this.displayAllEventSeries();
                this.labelPosition.Text = String.Empty;
                this.enableDisableButtons(TreeViewNodeTypes.Root);           
                this._actualLvl = TreeViewNodeTypes.Root;
                this.enableDisableToolbarButtons(_actualLvl);
            }
            catch (ConnectionCorruptedException ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Severe Program Exception (" + ex.Message + "). Program will be closed.", "Severe Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                this.Close();
            }
            Cursor.Current = Cursors.Default;
        }

        private void pictureBoxNewEventSeries_Click(object sender, EventArgs e)
        {
            //Ruft den Dialog auf und wechselt direkt zur neuen Eventseries
            EventSeriesDialog dlg = null;
            try
            {
                dlg = new EventSeriesDialog(null);

                if (dlg == null)
                    return;
            }
            catch (ContextCorruptedException ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message, "Context Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            // Dialog zentrieren
            dlg.Location = new Point((this.Size.Width) / 2 - (dlg.Size.Width) / 2, this.Location.Y);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    EventSeriess.Instance.Find(dlg.EventSeries.SeriesID);
                    this.displayEventSeries(dlg.EventSeries);
                    this.labelPosition.Text = EventSeriess.Instance.Position;
                    this.enableDisableButtons(TreeViewNodeTypes.EventSeriesNode);
                    this._actualLvl = TreeViewNodeTypes.EventSeriesNode;
                    this.enableDisableToolbarButtons(_actualLvl);
                }
                catch (ConnectionCorruptedException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    this.pictureBoxHome_Click(null, null);
                    return;
                }
                Cursor.Current = Cursors.Default;
            }
            else
            {
                this.treeViewFieldData.SelectedNode = null;
            }
        }

        private void pictureBoxNewEvent_Click(object sender, EventArgs e)
        {
            if (this.treeViewFieldData.SelectedNode == null || this.treeViewFieldData.SelectedNode.Tag == null)
            {
                MessageBox.Show("Error, EventSeriesNode has to be selected");
                return;
            }
            
            TreeNode seriesNode = this.treeViewFieldData.SelectedNode;
            TreeViewNodeData data = this.treeViewFieldData.SelectedNode.Tag as TreeViewNodeData;
            
            if (data.NodeType != TreeViewNodeTypes.EventSeriesNode)
            {
                MessageBox.Show("Error, EventSeriesNode has to be selected!");
                return;
            }
            int? seriesID = data.ID;
            try
            {
                if (data.ID != null)
                {
                    EventSeriess.Instance.Find((int) data.ID);
                    this._events = EventSeriess.Instance.CollectionEvents;
                }
                else
                    this._events = new CollectionEvents(null);
            }
            catch (ConnectionCorruptedException ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            NewEventDialog dlg = null;
            try
            {
                dlg = new NewEventDialog(true);
                if(dlg == null)
                    return;
            }
            catch (ContextCorruptedException ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message, "Context Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            dlg.Location = new Point((this.Size.Width) / 2 - (dlg.Size.Width) / 2, this.Location.Y);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                if (this._events != null)
                {
                    try
                    {
                        if (StaticGPS.isOpened())
                        {
                            if (StaticGPS.position != null)
                            {
                                double altitude = StaticGPS.position.SeaLevelAltitude;
                                double latitude = float.Parse(StaticGPS.position.Latitude.ToString());
                                double longitude = float.Parse(StaticGPS.position.Longitude.ToString());
                                this._events.CreateNewEvent(seriesID, altitude, longitude, latitude, StaticGPS.position.SatelliteCount, StaticGPS.position.PositionDilutionOfPrecision);
                            }
                            else
                            {
                                this._events.CreateNewEvent(seriesID);
                            }
                        }
                        else
                        {
                            this._events.CreateNewEvent(seriesID);
                        }
                    }
                    catch (ConnectionCorruptedException ex)
                    {
                        MessageBox.Show("New CollectionEvent couldn't be created. ("+ex.Message+")", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    catch (DataFunctionsException ex)
                    {
                        MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }

                    if (dlg.EventNumber != null)
                        this._events.Current.CollectorsEventNumber = dlg.EventNumber;

                    if (dlg.Notes != null)
                        this._events.Current.Notes = dlg.Notes;

                    if (dlg.DateSupplement != null)
                        this._events.Current.CollectionDateSupplement = dlg.DateSupplement;

                    try
                    {
                        DataFunctions.Instance.Update(this._events.Current);
                    }
                    catch (ConnectionCorruptedException ex)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Collection Event Data (Event Number, Notes, Date Supplement) couldn't be saved. (" + ex.Message + ")", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    }
                    catch (DataFunctionsException ex)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Collection Event Data (Event Number, Notes, Date Supplement) couldn't be saved. ("+ex.Message+")", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    }

                    TreeNode evNode = new TreeNode();
                    this.treeViewFieldData.BeginUpdate();
                    this._tvOperations.parametrizeEventNode(this._events.Current, evNode);
                    this.treeViewFieldData.SelectedNode.Nodes.Add(evNode);
                    this.treeViewFieldData.SelectedNode = evNode;
                    if (!this.treeViewFieldData.SelectedNode.IsExpanded)
                    {
                        this._expandTrigger = false;
                        this.treeViewFieldData.SelectedNode.ExpandAll();
                        this._expandTrigger = true;
                    }

                    // create automatically CollectionSpecimen
                    CollectionSpecimen spec = null;
                    try
                    {
                        this._specimen = this._events.Specimen;
                        spec = this._specimen.CreateSpecimen();
                        this._iu = this._specimen.IdentificationUnits;
                    }
                    catch (Exception ex)
                    {
                        String errorMsg = "";
                        Cursor.Current = Cursors.Default;
                        if (spec != null)
                        {
                            try
                            {
                                DataFunctions.Instance.Remove(spec);
                            }
                            catch (ConnectionCorruptedException except)
                            {
                                errorMsg = "Specimen couldn't be automatically assigned to new Collection Event. (" + except.Message + ")";
                            }
                        }

                        if (errorMsg.Equals(String.Empty))
                        {
                            errorMsg = "Specimen couldn't be automatically assigned to new Collection Event. (" + ex.Message + ")";
                        }
                        MessageBox.Show(errorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        
                        this.treeViewFieldData.EndUpdate();
                        return;
                    }

                    TreeNode specNode = new TreeNode();
                    this._tvOperations.parametrizeOnlySpecimenNode(spec, specNode);
                    this.labelPosition.Text = this._specimen.Position;
                    this.treeViewFieldData.SelectedNode.Nodes.Add(specNode);
                    this.treeViewFieldData.SelectedNode = specNode;
                    if (!this.treeViewFieldData.SelectedNode.IsExpanded)
                    {
                        this._expandTrigger = false;
                        this.treeViewFieldData.SelectedNode.ExpandAll();
                        this._expandTrigger = true;
                    }
                    this.enableDisableButtons(TreeViewNodeTypes.SpecimenNode);
                    this._actualLvl = TreeViewNodeTypes.SpecimenNode;
                    enableDisableToolbarButtons(_actualLvl);
                    this.treeViewFieldData.EndUpdate();
                }
            }
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Handles the Click event of the pictureBoxNewSpecimen control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void pictureBoxNewSpecimen_Click(object sender, EventArgs e)
        {
            if (this.treeViewFieldData.Nodes.Count == 0 || this.treeViewFieldData.SelectedNode == null)
            {
                return;
            }
            Cursor.Current = Cursors.WaitCursor;
            TreeNode eventNode = this.treeViewFieldData.SelectedNode;
            TreeViewNodeData eventData = eventNode.Tag as TreeViewNodeData;
            if (eventData == null)
                return;
            if (eventData.NodeType != TreeViewNodeTypes.EventNode)
                return;

            TreeNode seriesNode = eventNode.Parent;
            TreeViewNodeData seriesData = seriesNode.Tag as TreeViewNodeData;
            int? seriesID = seriesData.ID;
            try
            {
                if (seriesData.ID != null)
                {
                    EventSeriess.Instance.Find((int)seriesData.ID);
                    this._events = EventSeriess.Instance.CollectionEvents;

                }
                else
                    this._events = new CollectionEvents(null);
            }
            catch (ConnectionCorruptedException ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            this._events.Find((int)eventData.ID);
            this._specimen = this._events.Specimen;
            if (this._specimen != null)
            {
                CollectionSpecimen cs;
                try
                {
                    cs = this._specimen.CreateSpecimen();
                    this._iu = this._specimen.IdentificationUnits;
                }
                catch (DataFunctionsException ex)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Specimen couldn't be created. (" + ex.Message + ")", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    this.treeViewFieldData.EndUpdate();
                    return;
                }
                this.treeViewFieldData.BeginUpdate();
                TreeNode specNode = new TreeNode();
                this._tvOperations.parametrizeOnlySpecimenNode(cs, specNode);
                this.labelPosition.Text = this._specimen.Position;
                this.treeViewFieldData.SelectedNode.Nodes.Add(specNode);
                this.treeViewFieldData.SelectedNode = specNode;
                if (!this.treeViewFieldData.SelectedNode.IsExpanded)
                {
                    this._expandTrigger = false;
                    this.treeViewFieldData.SelectedNode.ExpandAll();
                    this._expandTrigger = true;
                }
                this.enableDisableButtons(TreeViewNodeTypes.SpecimenNode);
                this._actualLvl = TreeViewNodeTypes.SpecimenNode;
                this.enableDisableToolbarButtons(_actualLvl);
                this.treeViewFieldData.EndUpdate();
            }
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Handles the Click event of the pictureBoxNewIdentificationUnit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void pictureBoxNewIdentificationUnit_Click(object sender, EventArgs e)
        {
            if (this.treeViewFieldData.Nodes.Count == 0 || this.treeViewFieldData.SelectedNode == null)
            {
                return;
            }
            Cursor.Current = Cursors.WaitCursor;
            TreeNode parentNode = this.treeViewFieldData.SelectedNode;
            NewIdentificationUnitDialog dlg;
            TreeViewNodeData data = this.treeViewFieldData.SelectedNode.Tag as TreeViewNodeData;

            if (data == null)
                return;
         


            // if parent Node is also IdentificationUnit then set some default data
            int? seriesID = null;
            CollectionEvent ev = null;
            CollectionSpecimen cs = null;
            IdentificationUnit parentIU = null;
            if (data.NodeType == TreeViewNodeTypes.IdentificationUnitNode)
            {
                try
                {
                    parentIU = DataFunctions.Instance.RetrieveIdentificationUnit((int)data.ID);
                }
                catch (ConnectionCorruptedException ex)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Data of parent Identification Unit couldn't be retrieved. ("+ex.Message+")", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }

                if (parentIU != null)
                {
                    cs = parentIU.CollectionSpecimen;
                    ev = cs.CollectionEvent;
                    seriesID = ev.SeriesID;
                    try
                    {
                        if (seriesID != null)
                        {
                            EventSeriess.Instance.Find((int)seriesID);
                            this._events = EventSeriess.Instance.CollectionEvents;

                        }
                        else
                            this._events = new CollectionEvents(null);
                    }
                    catch (ConnectionCorruptedException ex)
                    {
                        MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    this._events.Find((int)ev.CollectionEventID);
                    this._specimen = this._events.Specimen;
                    this._specimen.Find((int)cs.CollectionSpecimenID);
                    this._iu = this._specimen.IdentificationUnits;
                    try
                    {
                        dlg = new NewIdentificationUnitDialog(cs, parentIU, this._iu);
                    }
                    catch (ContextCorruptedException ex)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show(ex.Message, "Context Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Data of parent Identification Unit couldn't be retrieved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }
            }
            else if (data.NodeType == TreeViewNodeTypes.SpecimenNode)
            {
                try
                {
                    TreeNode eventNode = parentNode.Parent;
                    TreeViewNodeData eventData = eventNode.Tag as TreeViewNodeData;
                    TreeNode seriesNode = eventNode.Parent;
                    TreeViewNodeData seriesData = seriesNode.Tag as TreeViewNodeData;
                    seriesID = seriesData.ID;
               
                    if (seriesID != null)
                    {
                        EventSeriess.Instance.Find((int)seriesID);
                        this._events = EventSeriess.Instance.CollectionEvents;

                    }
                    else
                        this._events = new CollectionEvents(null);
                    this._events.Find((int)eventData.ID);
                    ev = this._events.Current;
                    this._specimen = this._events.Specimen;
                }
                catch (ConnectionCorruptedException ex)
                {
                    MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }
          
                if (this._specimen.Find((int)data.ID))
                {
                    cs = this._specimen.Current;
                    this._iu = this._specimen.IdentificationUnits;
                    try
                    {
                        dlg = new NewIdentificationUnitDialog(cs, this._iu);
                    }
                    catch (ConnectionCorruptedException ex)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Associated IdentificationUnits with selected Specimen couldn't be retrieved. (" + ex.Message + ")", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    catch (ContextCorruptedException ex)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Associated IdentificationUnits with selected Specimen couldn't be retrieved. (" + ex.Message + ")", "Context Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Data of selected Specimen couldn't be retrieved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }
            }
            else
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Wrong node selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            // center dialog
            dlg.Location = new Point((this.Size.Width) / 2 - (dlg.Size.Width) / 2, this.Location.Y);
            Cursor.Current = Cursors.Default;
            dlg.ShowDialog();  
            moveTaxonomicGroupToFront(dlg.TaxonomicGroup);            
            if (dlg.Current != null)
            {
                IdentificationUnit iu = dlg.Current;
                TreeViewNodeData parentData = parentNode.Tag as TreeViewNodeData;
                this.treeViewFieldData.BeginUpdate();
                treeViewFieldData.SuspendLayout();
                if (parentData.NodeType == TreeViewNodeTypes.SpecimenNode)
                {
                    displaySpecimen(cs);
                }
                if (parentData.NodeType == TreeViewNodeTypes.IdentificationUnitNode)
                {
                    displayIdentificationUnit(parentIU);
                }
                foreach (TreeNode node in treeViewFieldData.SelectedNode.Nodes)
                {
                    TreeViewNodeData nodeData = node.Tag as TreeViewNodeData;
                    if (nodeData.NodeType == TreeViewNodeTypes.IdentificationUnitNode && nodeData.ID == iu.IdentificationUnitID)
                    {
                        this.treeViewFieldData.SelectedNode = node;
                        break;
                    }
                }

                if (!this.treeViewFieldData.SelectedNode.IsExpanded && this._tvOperations.expandLvl <= ExpandLevel.IdentificationUnit)
                {
                    this._expandTrigger = false;
                    this.treeViewFieldData.SelectedNode.ExpandAll();
                    this._expandTrigger = true;
                }

                this.treeViewFieldData.SelectedNode.EnsureVisible();
                this._actualLvl = TreeViewNodeTypes.IdentificationUnitNode;
                this.enableDisableToolbarButtons(_actualLvl);
                this.enableDisableButtons(_actualLvl);
                treeViewFieldData.ResumeLayout();
                this.treeViewFieldData.EndUpdate();
            }
        }

        private void pictureBoxNewEventProperty_Click(object sender, EventArgs e)
        {
            if (this.treeViewFieldData.Nodes.Count == 0 || this.treeViewFieldData.SelectedNode == null)
            {
                return;
            }
            TreeNode eventNode = this.treeViewFieldData.SelectedNode;
            TreeViewNodeData eventData = eventNode.Tag as TreeViewNodeData;
            if (eventData == null)
                return;
            if (eventData.NodeType != TreeViewNodeTypes.EventNode)
                return;

            TreeNode seriesNode = eventNode.Parent;
            TreeViewNodeData seriesData = seriesNode.Tag as TreeViewNodeData;
            int? seriesID = seriesData.ID;
            try
            {
                if (seriesData.ID != null)
                {
                    EventSeriess.Instance.Find((int)seriesData.ID);
                    this._events = EventSeriess.Instance.CollectionEvents;

                }
                else
                    this._events = new CollectionEvents(null);
            }
            catch (ConnectionCorruptedException ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            this._events.Find((int)eventData.ID);
            try
            {
                NewSitePropertyDialog dlg = new NewSitePropertyDialog(true);
                // Dialog zentrieren
                dlg.Location = new Point((this.Size.Width) / 2 - (dlg.Size.Width) / 2, this.Location.Y);

                dlg.PropertyList = DataFunctions.Instance.RetrievePossibleProperty((int)this._events.Current.CollectionEventID);

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    this.treeViewFieldData.BeginUpdate();
                    // SiteProperty einbinden
                    string propertyText = dlg.DisplayText;
                    int propertyID = dlg.PropertyID;
                    CollectionEventProperty ceProp;
                    try
                    {
                        ceProp = DataFunctions.Instance.CreateCollectionEventProperty(propertyID, propertyText, this._events.Current);
                    }
                    catch (DataFunctionsException ex)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }

                    TreeNode node = new TreeNode();
                    this._tvOperations.parameterizeCollectionEventPropertyNode(ceProp, node);
                    this.treeViewFieldData.SelectedNode.Nodes.Insert(0, node);//Erst Properties, dann LocalisationSystems
                    this.treeViewFieldData.SelectedNode = node;
                    if (!this.treeViewFieldData.SelectedNode.IsExpanded)
                    {
                        this._expandTrigger = false;
                        this.treeViewFieldData.SelectedNode.Expand();
                        this._expandTrigger = true;
                    }
                    this._actualLvl = TreeViewNodeTypes.SitePropertyNode;
                    this.enableDisableToolbarButtons(_actualLvl);
                    this.enableDisableButtons(_actualLvl);
                    this.treeViewFieldData.EndUpdate();
                    Cursor.Current = Cursors.Default;
                }
            }
            catch (ConnectionCorruptedException ex)
            {
                this.treeViewFieldData.EndUpdate();
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                pictureBoxHome_Click(null, null);
                return;
            }
            catch (ContextCorruptedException ex)
            {
                this.treeViewFieldData.EndUpdate();
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message, "Context Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                pictureBoxHome_Click(null, null);
                return;
            }
        }

        //private void pictureBoxNewAgent_Click(object sender, EventArgs e)
        //{
        //    if (this.treeViewFieldData.Nodes.Count == 0)
        //    {
        //        return;
        //    }

        //    NewAgentDialog dlg = new NewAgentDialog();
        //    // Dialog zentrieren
        //    dlg.Location = new Point((this.Size.Width) / 2 - (dlg.Size.Width) / 2, this.Location.Y);

        //    if (dlg.ShowDialog() == DialogResult.OK)
        //    {
        //        Cursor.Current = Cursors.WaitCursor;

        //        TreeViewNodeData data = this.treeViewFieldData.SelectedNode.Tag as TreeViewNodeData;

        //        // Collection Agent einbinden
        //        int collector = dlg.Collector;

        //        foreach (CollectionAgent item in DataFunctions.Instance.RetrieveAgentForCollectionSpecimen((int)data.ID))
        //        { 
        //            // duplicate entries are not allowed
        //            if (item.CollectorsName.Equals(UserProfiles.Instance.List[collector].CombinedNameCache))
        //            {
        //                Cursor.Current = Cursors.Default;
        //                MessageBox.Show("Agent is already assigned");
        //                return;
        //            }
        //        }

        //        CollectionAgent agent = DataFunctions.Instance.CreateCollectionAgent((int)data.ID, collector);

        //        if (!(bool)UserProfiles.Instance.Current.HideAttribute)
        //        {
        //            TreeNode node = new TreeNode();

        //            this._tvOperations.parameterizeCollectionAgentNode(agent, node);

        //            this.treeViewFieldData.SelectedNode.Nodes.Insert(0, node);

        //            if (!this.treeViewFieldData.SelectedNode.IsExpanded)
        //            {
        //                this._expandTrigger = false;
        //                this.treeViewFieldData.SelectedNode.Expand();
        //                this._expandTrigger = true;
        //            }
        //            this.treeViewFieldData.SelectedNode = node;
        //            this.afterSelect(node);
        //        }
        //        Cursor.Current = Cursors.Default;
        //    }
        //}

        private void pictureBoxNewAnalysis_Click(object sender, EventArgs e)
        {
            if (this.treeViewFieldData.Nodes.Count == 0 || this.treeViewFieldData.SelectedNode == null)
            {
                return;
            }         
            TreeViewNodeData data = this.treeViewFieldData.SelectedNode.Tag as TreeViewNodeData;
            if (data == null)
                return;

            if (data.NodeType != TreeViewNodeTypes.IdentificationUnitNode)
                return;

            try
            {
                IdentificationUnit iu = DataFunctions.Instance.RetrieveIdentificationUnit((int) data.ID);
                if (iu == null)
                {
                    MessageBox.Show("Associated Identification Unit couldn't be retrieved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }
                
                // Erlaubte Analysen für taxonomoische Gruppe werden geladen
                IList<Analysis> list = DataFunctions.Instance.RetrievePossibleAnalysis(iu.TaxonomicGroup); 

                if (list.Count < 1)
                {
                    MessageBox.Show("There is no selectable analysis", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }

                NewAnalysisDialog nad = null;
                try
                {
                    if (UserProfiles.Instance.Current.LastAnalysisID != null)
                        nad = new NewAnalysisDialog((int)UserProfiles.Instance.Current.LastAnalysisID);
                    else
                        nad = new NewAnalysisDialog(true);

                    if (nad == null)
                        return;
                }
                catch (ContextCorruptedException ex)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(ex.Message, "Context Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }

                // Dialog zentrieren
                nad.Location = new Point((this.Size.Width) / 2 - (nad.Size.Width) / 2, this.Location.Y);
                nad.Analysis = list;

                Cursor.Current = Cursors.Default;
                if (nad.ShowDialog() == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    this.treeViewFieldData.BeginUpdate();
                    String analysisValue = nad.Value;
                    int analysisPerformed = nad.PerformedAnalysis;
                    DateTime analysisDate = nad.AnalysisDate;
                    IdentificationUnitAnalysis iua;

                    try
                    {
                        iua = DataFunctions.Instance.CreateIdentificationUnitAnalysis(iu, analysisPerformed, analysisValue, analysisDate);
                    }
                    catch (DataFunctionsException ex)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }

                    TreeNode node = new TreeNode();
                    this._tvOperations.parametrizeIUANode(iua, node);
                    this.treeViewFieldData.SelectedNode.Nodes.Insert(0, node);
                    this.treeViewFieldData.SelectedNode = node;

                    // edit last performed analysis in UserProfile
                    if (UserProfiles.Instance.Current != null)
                    {
                        int oldAnalysis = -1;
                        if (UserProfiles.Instance.Current.LastAnalysisID != null)
                            oldAnalysis = (int)UserProfiles.Instance.Current.LastAnalysisID;

                        UserProfiles.Instance.Current.LastAnalysisID = nad.PerformedAnalysis;

                        try
                        {
                            UserProfiles.Instance.Update(UserProfiles.Instance.Current);
                        }
                        catch (UserProfileCorruptedException ex)
                        {
                            Cursor.Current = Cursors.Default;
                            if (oldAnalysis > -1)
                                UserProfiles.Instance.Current.LastAnalysisID = oldAnalysis;
                            else
                                UserProfiles.Instance.Current.LastAnalysisID = null;

                            MessageBox.Show(ex.Message + " Last performed analysis remains: " + oldAnalysis.ToString());
                        }
                    }
                    this._actualLvl = TreeViewNodeTypes.AnalysisNode;
                    this.enableDisableButtons(_actualLvl);
                    this.enableDisableToolbarButtons(_actualLvl);
                    this.treeViewFieldData.EndUpdate();
                    Cursor.Current = Cursors.Default;
                }
            }
            catch (ConnectionCorruptedException ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                this.pictureBoxHome_Click(null, null);
                return;
            }
        }

        private void pictureBoxNewIUGeoAnalysis_Click(object sender, EventArgs e)
        {
            if (this.treeViewFieldData.Nodes.Count == 0 || this.treeViewFieldData.SelectedNode == null)
            {
                return;
            }
            TreeNode node = this.treeViewFieldData.SelectedNode;
            TreeViewNodeData data = node.Tag as TreeViewNodeData;

            try
            {
                if (data != null && data.NodeType == TreeViewNodeTypes.IdentificationUnitNode)
                {
                    // Create IdentificationUnitGeoAnalysis
                    IdentificationUnit iu = DataFunctions.Instance.RetrieveIdentificationUnit((int)data.ID);
                    if (iu == null)
                    {
                        MessageBox.Show("Associated Identification Unit couldn't be retrieved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    if (iu.IdentificationUnitGeoAnalysis.First() != null)
                    {
                        MessageBox.Show("There exists already an IdentificationUnitGeoAnalysis for this Unit. You can Edit it with the GPS-Icon.", "Analysis already exists", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    IdentificationUnitGeoAnalysis iuGeoAnalysis;
                    try
                    {
                        iuGeoAnalysis = DataFunctions.Instance.CreateIdentificationUnitGeoAnalysis(iu, iu.CollectionSpecimen);
                    }
                    catch (DataFunctionsException ex)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }

                    if (iuGeoAnalysis != null)
                    {
                        if (StaticGPS.isOpened())
                        {
                            if (StaticGPS.position != null)
                            {
                                try
                                {
                                    float latitude = float.Parse(StaticGPS.position.Latitude.ToString());
                                    float longitude = float.Parse(StaticGPS.position.Longitude.ToString());
                                    float altitude = float.Parse(StaticGPS.position.SeaLevelAltitude.ToString());
                                    iuGeoAnalysis.setGeography(latitude,longitude, altitude);
                                }
                                catch (Exception)
                                {
                                    Cursor.Current = Cursors.Default;
                                    MessageBox.Show("GPS-Data couldn`t be read. Data will be set to default values.");
                                }
                            }
                        }
                        try
                        {
                            DataFunctions.Instance.Update(iuGeoAnalysis);
                        }
                        catch (DataFunctionsException ex)
                        {
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show("Geography Data couldn't be automatically created. (" + ex.Message + ")", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        }

                        this.treeViewFieldData.BeginUpdate();
                        TreeNode nodeGeoAnalysis = new TreeNode();
                        this._tvOperations.parameterizeIUGeoANode(iuGeoAnalysis, nodeGeoAnalysis);
                        this.treeViewFieldData.SelectedNode.Nodes.Insert(0, nodeGeoAnalysis);
                        this.treeViewFieldData.SelectedNode = nodeGeoAnalysis;

                        this.treeViewFieldData.EndUpdate();
                    }
                }
            }
            catch (ConnectionCorruptedException ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                this.pictureBoxHome_Click(null, null);
                return;
            }
            this._actualLvl = TreeViewNodeTypes.GeographyNode;
            this.enableDisableButtons(_actualLvl);
            this.enableDisableToolbarButtons(_actualLvl);
        }

        private void pictureBoxGPS_Click(object sender, EventArgs e)
        {
            if (StaticGPS.isOpened() == false || StaticGPS.position==null)
                return;
            if (StaticGPS.position.SatelliteCount <= 2)
                return;
            if (MessageBox.Show("Do You want to update the position?", "Update Position", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3) == DialogResult.Yes)
            {
                TreeNode node = this.treeViewFieldData.SelectedNode;
                if (node == null)
                    return;
                TreeViewNodeData data = node.Tag as TreeViewNodeData;
                if (data == null)
                    return;
                TreeNode parentNode = node.Parent;
                if (parentNode == null)
                    return;
                TreeViewNodeData parentData = parentNode.Tag as TreeViewNodeData;
                if (parentData == null)
                    return;

                CollectionEventLocalisation locGPS;
                CollectionEventLocalisation locAlt;

                try
                {
                    switch (data.NodeType)
                    {
                        case TreeViewNodeTypes.LocalisationNode:
                            TreeNode gpsNode = null;
                            TreeNode altNode = null;
                            if (data.ID == 8)
                            {
                                gpsNode = node;
                                if (parentNode != null)
                                {
                                    foreach (TreeNode tn in parentNode.Nodes)
                                    {
                                        if (tn != null)
                                        {
                                            TreeViewNodeData dat = tn.Tag as TreeViewNodeData;
                                            if (dat.NodeType == TreeViewNodeTypes.LocalisationNode && (int)dat.ID == 4)
                                            {
                                                altNode = tn;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (data.ID == 4)
                            {
                                altNode = node;
                                if (parentNode != null)
                                {
                                    foreach (TreeNode tn in parentNode.Nodes)
                                    {
                                        if (tn != null)
                                        {
                                            TreeViewNodeData dat = tn.Tag as TreeViewNodeData;
                                            if (dat.NodeType == TreeViewNodeTypes.LocalisationNode && (int)dat.ID == 8)
                                            {
                                                gpsNode = tn;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }

                            locGPS = DataFunctions.Instance.RetrieveCollectionEventLocalisation(8, (int)parentData.ID);
                            if (locGPS != null)
                            {
                                locGPS.Location2 = StaticGPS.position.Longitude.ToString("00.00000000");
                                locGPS.AverageLongitudeCache = StaticGPS.position.Longitude;
                                locGPS.Location1 = StaticGPS.position.Latitude.ToString("00.00000000");
                                locGPS.AverageLatitudeCache = StaticGPS.position.Latitude;
                                locGPS.LocationNotes = "GPS Coordinates manually changed via request of actual GPS data.";
                                try
                                {
                                    DataFunctions.Instance.Update(locGPS);
                                    this._tvOperations.parameterizeCollectionEventLocalisationNode(locGPS, gpsNode);

                                }
                                catch (DataFunctionsException ex)
                                {
                                    Cursor.Current = Cursors.Default;
                                    MessageBox.Show("GPS-Localisation couldn't be updated. (" + ex.Message + ")", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                }
                                this.treeViewFieldData.SelectedNode = node;
                                this.treeViewFieldData.Focus();
                            }
                            else
                                MessageBox.Show("GPS-Localisation not found.", "Display Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);


                            locAlt = DataFunctions.Instance.RetrieveCollectionEventLocalisation(4, (int)parentData.ID);
                            if (locAlt != null)
                            {
                                locAlt.Location1 = StaticGPS.position.EllipsoidAltitude.ToString("00.00");
                                locAlt.AverageAltitudeCache = StaticGPS.position.EllipsoidAltitude;
                                locAlt.LocationNotes = "Altitude manually changed via request of actual GPS data.";
                                try
                                {
                                    DataFunctions.Instance.Update(locAlt);
                                    this._tvOperations.parameterizeCollectionEventLocalisationNode(locAlt, altNode);
                                }
                                catch (DataFunctionsException ex)
                                {
                                    Cursor.Current = Cursors.Default;
                                    MessageBox.Show("Altitude Localisation couldn't be updated. (" + ex.Message + ")", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                }

                                this.treeViewFieldData.SelectedNode = node;
                                this.treeViewFieldData.Focus();
                            }
                            else
                                MessageBox.Show("Altitude Localisation not found.", "Display Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                            break;

                        case TreeViewNodeTypes.GeographyNode:
                            IdentificationUnit parentIU = DataFunctions.Instance.RetrieveIdentificationUnit(data.IdentificationUnitID);
                            IdentificationUnitGeoAnalysis iuGeoAnalysis = parentIU.IdentificationUnitGeoAnalysis.First();
               
                            if (iuGeoAnalysis != null)
                            {
                                iuGeoAnalysis.setGeography(StaticGPS.position.Latitude, StaticGPS.position.Longitude, StaticGPS.position.EllipsoidAltitude);
                                //iuGeoAnalysis.Notes = "GPS Coordinates manually changed via GPS request";
                                try
                                {
                                    DataFunctions.Instance.Update(iuGeoAnalysis);
                                    this._tvOperations.parameterizeIUGeoANode(iuGeoAnalysis, node);
                                }
                                catch (DataFunctionsException ex)
                                {
                                    Cursor.Current = Cursors.Default;
                                    MessageBox.Show("Geography Localisation couldn't be updated. (" + ex.Message + ")", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                }

                                this.treeViewFieldData.SelectedNode = node;
                                this.treeViewFieldData.Focus();
                            }
                            break;
                    }
                }
                catch (ConnectionCorruptedException ex)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    this.pictureBoxHome_Click(null, null);
                    return;
                }
            }
        }

        private void pictureBoxNewLocalisation_Click(object sender, EventArgs e)
        {
            if (this.treeViewFieldData.Nodes.Count == 0 || this.treeViewFieldData.SelectedNode == null || this.treeViewFieldData.SelectedNode.Tag == null)
            {
                return;
            }
            SelectLocalisationSystemDialog dlg = new SelectLocalisationSystemDialog();
            dlg.Location = new Point((this.Size.Width) / 2 - (dlg.Size.Width) / 2, this.Location.Y);
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                TreeNode eventNode = this.treeViewFieldData.SelectedNode;
                TreeViewNodeData eventData = eventNode.Tag as TreeViewNodeData;
                if (eventData == null)
                    return;
                if (eventData.NodeType != TreeViewNodeTypes.EventNode)
                    return;

                TreeNode seriesNode = eventNode.Parent;
                TreeViewNodeData seriesData = seriesNode.Tag as TreeViewNodeData;
                int? seriesID = seriesData.ID;
                try
                {
                    if (seriesData.ID != null)
                    {
                        EventSeriess.Instance.Find((int)seriesData.ID);
                        this._events = EventSeriess.Instance.CollectionEvents;

                    }
                    else
                        this._events = new CollectionEvents(null);
                }
                catch (ConnectionCorruptedException ex)
                {
                    MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }
                this._events.Find((int)eventData.ID);
                if (dlg.selection.Equals("GPS"))
                {
                    if (eventData.NodeType == TreeViewNodeTypes.EventNode)
                    {
                        bool updateLocAlt = false;
                        bool updateLocGPS = false;
                        CollectionEventLocalisation locAlt = DataFunctions.Instance.RetrieveCollectionEventLocalisation(4, this._events.Current.CollectionEventID);
                        CollectionEventLocalisation locGPS = DataFunctions.Instance.RetrieveCollectionEventLocalisation(8, this._events.Current.CollectionEventID);

                        //Prüft ob dem Event bereits ein Localisatiobnsystem mit WGS84 Koordinaten und einer Höhe
                        //angelegt sind. Wenn mehrere existieren, wird das erste genommen.
                        if (locAlt != null)
                        {
                            updateLocAlt = true;
                        }
                        else
                        {
                            try
                            {
                                locAlt = DataFunctions.Instance.CreateCollectionEventLocalisation(4, this._events.Current);
                            }
                            catch (DataFunctionsException ex)
                            {
                                Cursor.Current = Cursors.Default;
                                MessageBox.Show(ex.Message + " (Type: Altitude)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                return;
                            }
                        }

                        if (locGPS != null)
                        {
                            updateLocGPS = true;
                        }
                        else
                        {
                            try
                            {
                                locGPS = DataFunctions.Instance.CreateCollectionEventLocalisation(8, this._events.Current);
                            }
                            catch (DataFunctionsException ex)
                            {
                                Cursor.Current = Cursors.Default;
                                MessageBox.Show(ex.Message + " (Type: GPS)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                return;
                            }
                        }

                        NewGPSLocalisationForm gpsForm = null;
                        try
                        {
                            gpsForm = new NewGPSLocalisationForm(locAlt, locGPS);
                            if (gpsForm == null)
                                return;
                        }
                        catch (ContextCorruptedException ex)
                        {
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show(ex.Message, "Context Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            return;
                        }

                        

                        // Dialog zentrieren
                        gpsForm.Location = new Point((this.Size.Width) / 2 - (gpsForm.Size.Width) / 2, this.Location.Y);
                        if (gpsForm.ShowDialog() == DialogResult.OK)
                        {
                            Cursor.Current = Cursors.WaitCursor;
                            this.treeViewFieldData.BeginUpdate();
                            // Localisation einbinden
                            try
                            {
                                DataFunctions.Instance.Update(locGPS);
                                DataFunctions.Instance.Update(locAlt);
                            }
                            catch (DataFunctionsException ex)
                            {
                                Cursor.Current = Cursors.Default;
                                MessageBox.Show(ex.Message + " (Type: GPS / Altitude)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                this.afterSelect(this.treeViewFieldData.SelectedNode);

                                this.treeViewFieldData.EndUpdate();
                                return;
                            }

                            if (!updateLocGPS)
                            {
                                TreeNode nodeGPS = new TreeNode();
                                this._tvOperations.parameterizeCollectionEventLocalisationNode(locGPS, nodeGPS);
                                this.treeViewFieldData.SelectedNode.Nodes.Insert(0, nodeGPS);
                                if (!this.treeViewFieldData.SelectedNode.IsExpanded)
                                {
                                    this._expandTrigger = false;
                                    this.treeViewFieldData.SelectedNode.Expand();
                                    this._expandTrigger = true;
                                }
                            }
                            else
                            {
                                foreach (TreeNode item in this.treeViewFieldData.SelectedNode.Nodes)
                                {
                                    if (item != null)
                                    {
                                        if ((item.Tag as TreeViewNodeData).NodeType == TreeViewNodeTypes.LocalisationNode)
                                        {
                                            if ((item.Tag as TreeViewNodeData).ID == locGPS.LocalisationSystemID)
                                            {
                                                this._tvOperations.parameterizeCollectionEventLocalisationNode(locGPS, item);
                                                this.treeViewFieldData.Refresh();
                                            }
                                        }
                                    }
                                }
                            }

                            if (!updateLocAlt)
                            {
                                TreeNode nodeAlt = new TreeNode();

                                this._tvOperations.parameterizeCollectionEventLocalisationNode(locAlt, nodeAlt);

                                this.treeViewFieldData.SelectedNode.Nodes.Insert(0, nodeAlt);

                                if (!this.treeViewFieldData.SelectedNode.IsExpanded)
                                {
                                    this._expandTrigger = false;
                                    this.treeViewFieldData.SelectedNode.Expand();
                                    this._expandTrigger = true;
                                }

                            }
                            else
                            {
                                foreach (TreeNode item in this.treeViewFieldData.SelectedNode.Nodes)
                                {
                                    if (item != null)
                                    {
                                        if ((item.Tag as TreeViewNodeData).NodeType == TreeViewNodeTypes.LocalisationNode)
                                        {
                                            if ((item.Tag as TreeViewNodeData).ID == locAlt.LocalisationSystemID)
                                            {
                                                this._tvOperations.parameterizeCollectionEventLocalisationNode(locAlt, item);
                                                this.treeViewFieldData.Refresh();
                                            }
                                        }
                                    }
                                }
                            }
                            Cursor.Current = Cursors.Default;
                            this.treeViewFieldData.EndUpdate();
                        }
                    }
                }
                if (dlg.selection.Equals("SamplingPlot"))
                {
                    Cursor.Current = Cursors.WaitCursor;
                    bool update = false;
                    CollectionEventLocalisation ceLoc = DataFunctions.Instance.RetrieveCollectionEventLocalisation(13, this._events.Current.CollectionEventID);
                    if (ceLoc != null)
                    {
                        update = true;
                    }
                    IList<SamplingPlots> plots = DataFunctions.Instance.RetrieveAllSamplingPlots();
                    List<String> list = new List<string>();
                    foreach (SamplingPlots plot in plots)
                    {
                        if (plot != null)
                            list.Add(plot.Name);
                    }
                    if (list.Count < 1)
                    {
                        MessageBox.Show("There are no selectable SamplingPlots!");
                        Cursor.Current = Cursors.Default;
                        return;
                    }
                    SelectDialog sdl = new SelectDialog("Select Sampling Plot", list);
                    Cursor.Current = Cursors.Default;
                    if (sdl.ShowDialog() != DialogResult.OK)
                        return;
                    Cursor.Current = Cursors.WaitCursor;
                    if (update == false)
                    {
                        try
                        {
                            ceLoc = DataFunctions.Instance.CreateCollectionEventLocalisation(13, this._events.Current);
                        }
                        catch (DataFunctionsException ex)
                        {
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show(ex.Message + " (Type: Sampling Plot)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            return;
                        }

                    }

                    if (ceLoc != null)
                    {
                        SamplingPlots selected = DataFunctions.Instance.RetrieveSamplingPlot(sdl.Value);
                        if (selected != null)
                        {
                            ceLoc.Location1 = selected.Name;
                            ceLoc.Location2 = selected.URI;
                            try
                            {
                                DataFunctions.Instance.Update(ceLoc);
                            }
                            catch (DataFunctionsException ex)
                            {
                                Cursor.Current = Cursors.Default;
                                MessageBox.Show(ex.Message + " (Type: Sampling Plot)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                this.afterSelect(this.treeViewFieldData.SelectedNode);
                                return;
                            }
                        }

                        this.treeViewFieldData.BeginUpdate();

                        TreeNode nodePlot = new TreeNode();
                        this._tvOperations.parameterizeCollectionEventLocalisationNode(ceLoc, nodePlot);
                        TreeViewNodeData plotData = nodePlot.Tag as TreeViewNodeData;
                        if (update == false)
                            this.treeViewFieldData.SelectedNode.Nodes.Insert(0, nodePlot);
                        else
                        {
                            foreach (TreeNode tn in this.treeViewFieldData.SelectedNode.Nodes)
                            {
                                if (tn != null)
                                {
                                    TreeViewNodeData childData = tn.Tag as TreeViewNodeData;
                                    if (childData.ID == plotData.ID && childData.NodeType == TreeViewNodeTypes.LocalisationNode)
                                    {
                                        int index = tn.Index;
                                        this.treeViewFieldData.SelectedNode.Nodes.Remove(tn);
                                        this.treeViewFieldData.SelectedNode.Nodes.Insert(index, nodePlot);
                                        break;
                                    }
                                }
                            }
                        }

                        if (!this.treeViewFieldData.SelectedNode.IsExpanded)
                        {
                            this._expandTrigger = false;
                            this.treeViewFieldData.SelectedNode.Expand();
                            this._expandTrigger = true;
                        }
                    }
                }
            }
            catch (ConnectionCorruptedException ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                this.pictureBoxHome_Click(null, null);
                return;
            }
            this._actualLvl = TreeViewNodeTypes.LocalisationNode;
            this.enableDisableToolbarButtons(_actualLvl);
            this.treeViewFieldData.EndUpdate();
            Cursor.Current = Cursors.Default;
        }

        private void pictureBoxNewAnalysis_EnabledChanged(object sender, EventArgs e)
        {
            int index = (int)TreeViewIconIndex.Analysis;
            this.pictureBoxNewAnalysis.Image = this._currentDiversityImageList.Images[this.pictureBoxNewAnalysis.Enabled ? index : index + 1];
        }

        private void pictureBoxNewIdentificationUnit_EnabledChanged(object sender, EventArgs e)
        {
            int index = (int)TreeViewIconIndex.Plant;
            this.pictureBoxNewIdentificationUnit.Image = this._currentDiversityImageList.Images[this.pictureBoxNewIdentificationUnit.Enabled ? index : index + 1];
        }

        private void pictureBoxNewSpecimen_EnabledChanged(object sender, EventArgs e)
        {
            int index = (int)TreeViewIconIndex.Specimen;
            this.pictureBoxNewSpecimen.Image = this._currentDiversityImageList.Images[this.pictureBoxNewSpecimen.Enabled ? index : index + 1];
        }

        private void pictureBoxNewLocalisation_EnabledChanged(object sender, EventArgs e)
        {
            int index = (int)TreeViewIconIndex.Location;
            this.pictureBoxNewLocalisation.Image = this._currentDiversityImageList.Images[this.pictureBoxNewLocalisation.Enabled ? index : index + 1];
        }

        //private void pictureBoxNewAgent_EnabledChanged(object sender, EventArgs e)
        //{
        //    int index = (int)TreeViewIconIndex.Agent;
        //    this.pictureBoxNewAgent.Image = this._currentDiversityImageList.Images[this.pictureBoxNewAgent.Enabled ? index : index + 1];
        //}

        private void pictureBoxNewEventProperty_EnabledChanged(object sender, EventArgs e)
        {
            int index = (int)TreeViewIconIndex.SiteProperty;
            this.pictureBoxNewEventProperty.Image = this._currentDiversityImageList.Images[this.pictureBoxNewEventProperty.Enabled ? index : index + 1];
        }

        private void pictureBoxNewIUGeoAnalysis_EnabledChanged(object sender, EventArgs e)
        {
            int index = (int)TreeViewIconIndex.Geography;
            this.pictureBoxNewIUGeoAnalysis.Image = this._currentDiversityImageList.Images[this.pictureBoxNewIUGeoAnalysis.Enabled ? index : index + 1];
        }

        private void pictureBoxNewEvent_EnabledChanged(object sender, EventArgs e)
        {
            int index = (int)TreeViewIconIndex.Event;
            this.pictureBoxNewEvent.Image = this._currentDiversityImageList.Images[this.pictureBoxNewEvent.Enabled ? index : index + 1];
        }

        private void pictureBoxNewEventSeries_EnabledChanged(object sender, EventArgs e)
        {
            int index = (int)TreeViewIconIndex.EventSeries;
            this.pictureBoxNewEventSeries.Image = this._currentDiversityImageList.Images[this.pictureBoxNewEventSeries.Enabled ? index : index + 1];
        }

        #endregion

        #region Menu Item Events

        private void menuItemTakePicture_Click(object sender, EventArgs e)
        {
            if (this.treeViewFieldData.Nodes.Count == 0 || this.treeViewFieldData.SelectedNode == null || this.treeViewFieldData.SelectedNode.Tag == null)
            {
                return;
            }
            TreeNode node = this.treeViewFieldData.SelectedNode;
            TreeViewNodeData data = node.Tag as TreeViewNodeData;
            if (!(data.NodeType == TreeViewNodeTypes.EventNode || data.NodeType==TreeViewNodeTypes.SpecimenNode || data.NodeType==TreeViewNodeTypes.IdentificationUnitNode))
            {
                MessageBox.Show("You can´t associate a picture with the selected node!");
                return;
            }

            String FileName = null;
            string progPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            string picturePath = progPath + @"\pictures";
            if (!System.IO.Directory.Exists(picturePath))
                System.IO.Directory.CreateDirectory(picturePath);  
            // is camera present?
            if (SystemState.CameraPresent /*&& SystemState.CameraEnabled*/)
            {
                try
                {
                    // Note: Camera is not working in case the GPS thread is running; strange but that is what we found out
                    //       Therefore we "stop" the GPS thread, take the picture and the re-start the GPS service ...
                    if (UserProfiles.Instance.Current != null)
                    {
                        if (UserProfiles.Instance.Current.StopGPS == true)
                            this.StopGPS();
                    }
                    else
                        this.StopGPS();
                }
                catch (ConnectionCorruptedException ex)
                {
                    this.StopGPS();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("GPS functions aren't available. (" + ex.Message + ")", "GPS Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                } 

                CameraCaptureDialog camCapDlg = new CameraCaptureDialog();
                camCapDlg.Mode = CameraCaptureMode.Still;
                camCapDlg.Title = "Diversity Mobile";
                camCapDlg.InitialDirectory = picturePath;
                DialogResult dlgRes = camCapDlg.ShowDialog();
                if (dlgRes == DialogResult.OK)
                {
                    FileName = camCapDlg.FileName;
                }

                try
                {
                    if (UserProfiles.Instance.Current != null)
                    {
                        if (UserProfiles.Instance.Current.StopGPS == true)
                            this.StartGPS();
                    }
                    else
                        this.StartGPS();
                }
                catch (ConnectionCorruptedException ex)
                {
                    this.StartGPS();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("GPS functions aren't available. ("+ex.Message+")", "GPS Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                } 
            }
            else
            {
                MessageBox.Show("No camera access possible!");
            }
            if (!String.IsNullOrEmpty(FileName))
            {
                //Save Image for CollectionSpecimen, IU, CollectionEvent
                try
                {
                    switch (data.NodeType)
                    {
                        case TreeViewNodeTypes.EventNode: // event node
                            DataFunctions.Instance.CreateCollectionEventImage(FileName, picturePath, "photograph", (int)data.ID);
                            break;
                        case TreeViewNodeTypes.SpecimenNode:
                            DataFunctions.Instance.CreateCollectionSpecimenImage(FileName, picturePath, (int)data.ID, "photograph");
                            break;
                        case TreeViewNodeTypes.IdentificationUnitNode:
                            DataFunctions.Instance.CreateCollectionSpecimenImage(FileName, picturePath, this.findSpecimenID(), (int)data.ID, "photograph");
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    this.afterSelect(this.treeViewFieldData.SelectedNode);
                    return;
                }
                
                Cursor.Current = Cursors.Default;
            }
        }

        private void menuItemDisplayPicture_Click(object sender, EventArgs e)
        {
            // Show all assigned Images for selected node
            if (this.treeViewFieldData.SelectedNode != null && this.treeViewFieldData.SelectedNode.Tag != null)
            {
                TreeViewNodeData data = this.treeViewFieldData.SelectedNode.Tag as TreeViewNodeData;
                List<String> fileList = new List<string>();
                try
                {
                    switch (data.NodeType)
                    {
                        case TreeViewNodeTypes.EventNode:
                            IList<CollectionEventImage> ceImgList = DataFunctions.Instance.RetrieveImagesForCollectionEvent("photograph", this._events.Current);
                            foreach (CollectionEventImage ceImg in ceImgList)
                            {
                                if (ceImg != null)
                                    fileList.Add(ceImg.URI);
                            }
                            break;
                        case TreeViewNodeTypes.SpecimenNode:
                            IList<CollectionSpecimenImage> csImgList = DataFunctions.Instance.RetrieveImagesForCollectionSpecimen((int)data.ID, "photograph");
                            foreach (CollectionSpecimenImage csImg in csImgList)
                            {
                                if(csImg != null)
                                    fileList.Add(csImg.URI);
                            }
                            break;
                        case TreeViewNodeTypes.IdentificationUnitNode:
                            IList<CollectionSpecimenImage> csImgListIU = DataFunctions.Instance.RetrieveImagesForCollectionSpecimen(this.findSpecimenID(), (int)data.ID, "photograph");
                            foreach (CollectionSpecimenImage csImg in csImgListIU)
                            {
                                if(csImg != null)
                                    fileList.Add(csImg.URI);
                            }
                            break;
                    }
                }
                catch (Exception f)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Error while finding Pictures for ID´s. ("+f.Message+")", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    this.afterSelect(this.treeViewFieldData.SelectedNode);
                    return;
                }
                if (fileList.Count > 0)
                {
                    try
                    {
                        ImageForm imgForm = new ImageForm(fileList);
                        imgForm.ShowDialog();
                    }
                    catch (Exception)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Not enough Memory");
                        this.afterSelect(this.treeViewFieldData.SelectedNode);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("No assigned image found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void menuRecordVideo_Click(object sender, EventArgs e)
        {
            if (this.treeViewFieldData.Nodes.Count == 0)
            {
                return;
            }

            String FileName = null;
            string progPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            string picturePath = progPath + @"\pictures";
            if (!System.IO.Directory.Exists(picturePath))
                System.IO.Directory.CreateDirectory(picturePath);
          
                // is camera present?
                if (SystemState.CameraPresent /*&& SystemState.CameraEnabled*/)
                {
                    try
                    {
                        if (UserProfiles.Instance.Current != null)
                        {
                            if (UserProfiles.Instance.Current.StopGPS == true)
                                this.StopGPS();
                        }
                        else
                            this.StopGPS();
                    }
                    catch (ConnectionCorruptedException ex)
                    {
                        this.StopGPS();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("GPS functions aren't available. (" + ex.Message + ")", "GPS Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    } 

                    CameraCaptureDialog camCapDlg = new CameraCaptureDialog();
                    camCapDlg.Mode = CameraCaptureMode.VideoWithAudio;
                    camCapDlg.Title = "Diversity Mobile";
                    camCapDlg.InitialDirectory = picturePath;
                    
                    if (camCapDlg.ShowDialog() == DialogResult.OK)
                    {
                        FileName = camCapDlg.FileName;
                    }

                    try
                    {
                        if (UserProfiles.Instance.Current != null)
                        {
                            if (UserProfiles.Instance.Current.StopGPS == true)
                                this.StartGPS();
                        }
                        else
                            this.StartGPS();
                    }
                    catch (ConnectionCorruptedException ex)
                    {
                        this.StartGPS();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("GPS functions aren't available. (" + ex.Message + ")", "GPS Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    MessageBox.Show("No camera access possible!");
                }
            if (!String.IsNullOrEmpty(FileName) && this.treeViewFieldData.SelectedNode != null && this.treeViewFieldData.SelectedNode.Tag != null)
            {
                //Save Image for CollectionSpecimen, IU, CollectionEvent
                Cursor.Current = Cursors.WaitCursor;
                TreeNode node = this.treeViewFieldData.SelectedNode;
                TreeViewNodeData data = node.Tag as TreeViewNodeData;
                List<String> typeList = new List<string>();
                try
                {
                    switch (data.NodeType)
                    {
                        case TreeViewNodeTypes.EventNode: // event node
                            DataFunctions.Instance.CreateCollectionEventImage(FileName, picturePath, "video", (int)data.ID);
                            break;
                        case TreeViewNodeTypes.SpecimenNode:
                            DataFunctions.Instance.CreateCollectionSpecimenImage(FileName, picturePath, (int)data.ID, "video");
                            break;
                        case TreeViewNodeTypes.IdentificationUnitNode:
                            DataFunctions.Instance.CreateCollectionSpecimenImage(FileName, picturePath, this.findSpecimenID(), (int)data.ID, "video");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    this.afterSelect(this.treeViewFieldData.SelectedNode);
                    return;
                }
                Cursor.Current = Cursors.Default;
            }
        }

        private void menuItemShowVideo_Click(object sender, EventArgs e)
        {
            if (this.treeViewFieldData.Nodes.Count == 0 || this.treeViewFieldData.SelectedNode == null || this.treeViewFieldData.SelectedNode.Tag == null)
                return;

            // Show all assigned Images for selected node
            TreeViewNodeData data = this.treeViewFieldData.SelectedNode.Tag as TreeViewNodeData;
            List<String> fileList = new List<string>();
            try
            {
                switch (data.NodeType)
                {
                    case TreeViewNodeTypes.EventNode:
                        IList<CollectionEventImage> ceImgList = DataFunctions.Instance.RetrieveImagesForCollectionEvent("video", this._events.Current);
                        foreach (CollectionEventImage ceImg in ceImgList)
                        {
                            if(ceImg != null)
                                fileList.Add(ceImg.URI);
                        }
                        break;
                    case TreeViewNodeTypes.SpecimenNode:
                        IList<CollectionSpecimenImage> csImgList = DataFunctions.Instance.RetrieveImagesForCollectionSpecimen((int)data.ID, "video");
                        foreach (CollectionSpecimenImage csImg in csImgList)
                        {
                            if (csImg != null)
                                fileList.Add(csImg.URI);
                        }
                        break;
                    case TreeViewNodeTypes.IdentificationUnitNode:
                        IList<CollectionSpecimenImage> csImgListIU = DataFunctions.Instance.RetrieveImagesForCollectionSpecimen(this.findSpecimenID(), (int)data.ID, "video");
                        foreach (CollectionSpecimenImage csImg in csImgListIU)
                        {
                            if (csImg != null)
                                fileList.Add(csImg.URI);
                        }
                        break;
                }
            }
            catch (Exception f)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Error while finding Pictures for ID´s. ("+f.Message+")");
                return;
            }
            if (fileList.Count > 0)
            {
                try
                {
                    DirectPlayForm vidForm = new DirectPlayForm(fileList,_currentToolbarImageList);
                    vidForm.ShowDialog();
                }
                catch (Exception)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Not enough Memory");
                    return;
                }
            }
            else
            {
                MessageBox.Show("No assigned image found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            }
        }

        private void menuItemTakeAudio_Click(object sender, EventArgs e)
        {
            if (this.treeViewFieldData.Nodes.Count == 0 || this.treeViewFieldData.SelectedNode == null || this.treeViewFieldData.SelectedNode.Tag == null)
                return;
            
            String FileName = null;
            string progPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            string picturePath = progPath + @"\pictures";
            if (!System.IO.Directory.Exists(picturePath))
                System.IO.Directory.CreateDirectory(picturePath);
            try
            {
                RecordForm rf = new RecordForm(picturePath);
                FileName = rf.SavePath;
                rf.Text = "Diversity Mobile";
                DialogResult dlgRes = rf.ShowDialog();
                if (dlgRes != DialogResult.OK)
                    return;
            }
            catch (Exception g)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Error when recording! ("+g.Message+")", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                this.afterSelect(this.treeViewFieldData.SelectedNode);
                return;
            }
            Cursor.Current = Cursors.WaitCursor;
            TreeNode node = this.treeViewFieldData.SelectedNode;
            TreeViewNodeData data = node.Tag as TreeViewNodeData;
            try
            {
                switch (data.NodeType)
                {
                    case TreeViewNodeTypes.EventNode: // event node
                        DataFunctions.Instance.CreateCollectionEventImage(FileName, picturePath, "audio", (int)data.ID);
                        break;
                    case TreeViewNodeTypes.SpecimenNode:
                        DataFunctions.Instance.CreateCollectionSpecimenImage(FileName, picturePath, (int)data.ID, "audio");
                        break;
                    case TreeViewNodeTypes.IdentificationUnitNode:
                        DataFunctions.Instance.CreateCollectionSpecimenImage(FileName, picturePath, this.findSpecimenID(), (int)data.ID, "audio");
                        break;
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                this.afterSelect(this.treeViewFieldData.SelectedNode);
                return;
            }
            Cursor.Current = Cursors.Default;
        }

        private void menuItemPlayAudio_Click(object sender, EventArgs e)
        {
            if (this.treeViewFieldData.Nodes.Count == 0 || this.treeViewFieldData.SelectedNode == null || this.treeViewFieldData.SelectedNode.Tag == null)
                return;

            TreeViewNodeData data = this.treeViewFieldData.SelectedNode.Tag as TreeViewNodeData;
            List<String> fileList = new List<string>();

            try
            {
                switch (data.NodeType)
                {
                    case TreeViewNodeTypes.EventNode:
                        IList<CollectionEventImage> ceImgList = DataFunctions.Instance.RetrieveImagesForCollectionEvent("audio", this._events.Current);
                        foreach (CollectionEventImage ceImg in ceImgList)
                        {
                            if (ceImg != null)
                                fileList.Add(ceImg.URI);
                        }
                        break;
                    case TreeViewNodeTypes.SpecimenNode:
                        IList<CollectionSpecimenImage> csImgList = DataFunctions.Instance.RetrieveImagesForCollectionSpecimen((int)data.ID, "audio");
                        foreach (CollectionSpecimenImage csImg in csImgList)
                        {
                            if (csImg != null)
                                fileList.Add(csImg.URI);
                        }
                        break;
                    case TreeViewNodeTypes.IdentificationUnitNode:
                        IList<CollectionSpecimenImage> csImgListIU = DataFunctions.Instance.RetrieveImagesForCollectionSpecimen(this.findSpecimenID(), (int)data.ID, "audio");
                        foreach (CollectionSpecimenImage csImg in csImgListIU)
                        {
                            if (csImg != null)
                                fileList.Add(csImg.URI);
                        }
                        break;
                }
            }
            catch (ConnectionCorruptedException ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                this.afterSelect(this.treeViewFieldData.SelectedNode);
                return;
            }
            if (fileList.Count > 0)
            {
                try
                {
                    //AudioForm ad = new AudioForm(fileList[0]);
                    DirectPlayForm ad = new DirectPlayForm(fileList, _currentToolbarImageList);
                    ad.Show();
                }
                catch(Exception)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Not enough Memory");
                }
            }
            else
            {
                MessageBox.Show("No assigned Record found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            }

        }

        private void menuItemShowMap_Click(object sender, EventArgs e)
        {
            if (this.treeViewFieldData.Nodes.Count == 0 || this.treeViewFieldData.SelectedNode == null || this.treeViewFieldData.SelectedNode.Tag == null)
                return;

            // Maps durchsuchen nach passenden Karten
            TreeNode node = this.treeViewFieldData.SelectedNode;
            TreeViewNodeData data = node.Tag as TreeViewNodeData;
            SelectMapForm form;
            float actualLatitude = 0;
            float actualLongitude = 0;
            if (StaticGPS.isOpened())
            {
                if (StaticGPS.position != null)
                {
                    try
                    {
                        actualLatitude = float.Parse(StaticGPS.position.Latitude.ToString());
                        actualLongitude = float.Parse(StaticGPS.position.Longitude.ToString());
                    }
                    catch(Exception)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("GPS-Data couldn`t be read. Data will be set to default values.");
                    }
                }
            }
            if (data.NodeType == TreeViewNodeTypes.LocalisationNode && data.ID==8)
            {
                try
                {
                    TreeNode parentNode = node.Parent;
                    TreeViewNodeData parentData = parentNode.Tag as TreeViewNodeData;
                    CollectionEventLocalisation loc = null;
                    if (parentData.NodeType == TreeViewNodeTypes.EventNode)
                        loc = DataFunctions.Instance.RetrieveCollectionEventLocalisation((int)data.ID, (int)parentData.ID);
                    else
                    {
                        CollectionSpecimen spec = DataFunctions.Instance.RetrieveCollectionSpecimen((int)data.CollectionSpecimenID);
                        if (spec != null)
                            loc = DataFunctions.Instance.RetrieveCollectionEventLocalisation((int)data.ID, (int)spec.CollectionEventID);
                    }

                    if (loc != null)
                    {
                        if (loc.AverageAltitudeCache == null)
                            loc.AverageAltitudeCache = 0;
                        if (loc.AverageLatitudeCache == null)
                            loc.AverageLatitudeCache = 0;
                        if (loc.AverageLongitudeCache == null)
                            loc.AverageLongitudeCache = 0;
                        form = new SelectMapForm((float)loc.AverageLatitudeCache, (float)loc.AverageLongitudeCache, actualLatitude, actualLongitude);
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            loc.AverageLongitudeCache = form.GPSLong;
                            loc.Location1 = form.GPSLong.ToString("00.00000000");
                            loc.AverageLatitudeCache = form.GPSLat;
                            loc.Location2 = form.GPSLat.ToString("00.00000000");
                            loc.LocationNotes = "GPS Coordinates manually changed via Google Map";

                            try
                            {
                                DataFunctions.Instance.Update(loc);
                                this._tvOperations.parameterizeCollectionEventLocalisationNode(loc, node);
                            }
                            catch (DataFunctionsException ex)
                            {
                                Cursor.Current = Cursors.Default;
                                MessageBox.Show("Changed Data couldn't be saved. " + ex.Message + " (Type: GPS)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                this.afterSelect(this.treeViewFieldData.SelectedNode);
                                return;
                            }

                            this.treeViewFieldData.SelectedNode = node;
                            this.treeViewFieldData.Focus();
                        }
                    }
                }
                catch (OutOfMemoryException)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Not Enough Memory");
                    //StaticGPS.updatePositionDataHandler = new EventHandler(UpdatePositionData);
                    //StaticGPS.updateDeviceDataHandler = new EventHandler(UpdateDeviceData);
                    return;
                }
                catch (ConnectionCorruptedException ex)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    this.afterSelect(this.treeViewFieldData.SelectedNode);
                    return;
                }
            }
            else if (data.NodeType == TreeViewNodeTypes.EventNode)
            {
                try
                {
                    TreeNode eventNode = node;
                    TreeViewNodeData eventData = eventNode.Tag as TreeViewNodeData;
                    CollectionEventLocalisation loc = null;
                    if (eventData.NodeType == TreeViewNodeTypes.EventNode)
                        loc = DataFunctions.Instance.RetrieveCollectionEventLocalisation(8, (int)eventData.ID);
                    else
                    {
                        CollectionEvent ev=DataFunctions.Instance.RetrieveCollectionEvent((int) eventData.ID);
                        loc = DataFunctions.Instance.CreateCollectionEventLocalisation(8, ev);
                    }

                    if (loc != null)
                    {
                        if (loc.AverageAltitudeCache == null)
                            loc.AverageAltitudeCache = 0;
                        if (loc.AverageLatitudeCache == null)
                            loc.AverageLatitudeCache = 0;
                        if (loc.AverageLongitudeCache == null)
                            loc.AverageLongitudeCache = 0;
                        form = new SelectMapForm((float)loc.AverageLatitudeCache, (float)loc.AverageLongitudeCache, actualLatitude, actualLongitude);
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            loc.AverageLongitudeCache = form.GPSLong;
                            loc.Location1 = form.GPSLong.ToString("00.00000000");
                            loc.AverageLatitudeCache = form.GPSLat;
                            loc.Location2 = form.GPSLat.ToString("00.00000000");
                            loc.LocationNotes = "GPS Coordinates manually changed via DiversityMobile";

                            try
                            {
                                DataFunctions.Instance.Update(loc);
                                this._tvOperations.parameterizeCollectionEventLocalisationNode(loc, node);
                            }
                            catch (DataFunctionsException ex)
                            {
                                Cursor.Current = Cursors.Default;
                                MessageBox.Show("Changed Data couldn't be saved. " + ex.Message + " (Type: GPS)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                this.afterSelect(this.treeViewFieldData.SelectedNode);
                                return;
                            }

                            this.treeViewFieldData.SelectedNode = node;
                            this.treeViewFieldData.Focus();
                        }
                    }
                }
                catch (OutOfMemoryException)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Not Enough Memory");
                    //StaticGPS.updatePositionDataHandler = new EventHandler(UpdatePositionData);
                    //StaticGPS.updateDeviceDataHandler = new EventHandler(UpdateDeviceData);
                    return;
                }
                catch (ConnectionCorruptedException ex)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    this.afterSelect(this.treeViewFieldData.SelectedNode);
                    return;
                }
            }
            else if (data.NodeType == TreeViewNodeTypes.GeographyNode)
            {
                try
                {
                    TreeNode iuNode = this.treeViewFieldData.SelectedNode.Parent;
                    TreeViewNodeData iuData=iuNode.Tag as TreeViewNodeData;
                    IdentificationUnit iu = DataFunctions.Instance.RetrieveIdentificationUnit((int)iuData.ID);
                    IdentificationUnitGeoAnalysis iuGeoAnalysis = iu.IdentificationUnitGeoAnalysis.First();
                    if (iuGeoAnalysis != null)
                    {
                        form = new SelectMapForm((float)iuGeoAnalysis.GeoLatitude, (float)iuGeoAnalysis.GeoLongitude, actualLatitude, actualLongitude);
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            double? altitude = iuGeoAnalysis.GeoAltitude;
                            iuGeoAnalysis.setGeography(form.GPSLat, form.GPSLong, altitude);

                            //iuGeoAnalysis.Notes = "GPS Coordinates manually changed via Google Map";

                            try
                            {
                                DataFunctions.Instance.Update(iuGeoAnalysis);
                                this._tvOperations.parameterizeIUGeoANode(iuGeoAnalysis, node);
                            }
                            catch (DataFunctionsException ex)
                            {
                                Cursor.Current = Cursors.Default;
                                MessageBox.Show("Changed Data couldn't be saved. " + ex.Message + " (Type: Geography)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                this.afterSelect(this.treeViewFieldData.SelectedNode);
                                return;
                            }

                            this.treeViewFieldData.SelectedNode = node;
                            this.treeViewFieldData.Focus();
                        }
                    }
                }
                catch (OutOfMemoryException)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Not Enough Memory");
                    //StaticGPS.updatePositionDataHandler = new EventHandler(UpdatePositionData);
                    //StaticGPS.updateDeviceDataHandler = new EventHandler(UpdateDeviceData);
                }
                catch (ConnectionCorruptedException ex)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    this.afterSelect(this.treeViewFieldData.SelectedNode);
                    return;
                }
            }
            else
            {
                try
                {
                    //if (actualLatitude == 0)//zum testen im büro
                    //    actualLatitude = 48;
                    //if (actualLongitude == 0)
                    //    actualLongitude = 11;
                    form = new SelectMapForm(actualLatitude, actualLongitude);
                    form.ShowDialog();
                }
                catch (OutOfMemoryException)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Not Enough Memory");
                    //StaticGPS.updatePositionDataHandler = new EventHandler(UpdatePositionData);
                    //StaticGPS.updateDeviceDataHandler = new EventHandler(UpdateDeviceData);
                }
            }
            //StaticGPS.updatePositionDataHandler = new EventHandler(UpdatePositionData);
            //StaticGPS.updateDeviceDataHandler = new EventHandler(UpdateDeviceData);
        }

        #endregion

        #endregion

        #region TreeView methods for displaying and parameterizing entries of the tree

        protected void displayAllEventSeries()
        {
            Cursor.Current = Cursors.WaitCursor;


            this.treeViewFieldData.BeginUpdate();
            this.treeViewFieldData.SuspendLayout();
            this.treeViewFieldData.Nodes.Clear();
            try
            {
                TreeNode tn = _tvOperations.displayAllEventSeries(null);
                this.treeViewFieldData.Nodes.Add(tn);

                IList<CollectionEventSeries> listEs;
                try
                {
                    listEs = DataFunctions.Instance.RetrieveEventSeries();
                }
                catch (ConnectionCorruptedException ex)
                {
                    this.treeViewFieldData.EndUpdate();
                    Cursor.Current = Cursors.Default;
                    throw ex;
                }

                foreach (CollectionEventSeries es in listEs)
                {
                    if (es != null)
                    {
                        tn = _tvOperations.displayAllEventSeries(es);
                        this.treeViewFieldData.Nodes.Add(tn);
                    }
                }
                this.treeViewFieldData.SelectedNode = null;
                this._actualLvl = TreeViewNodeTypes.Root;

            }
            catch (Exception ex)
            {
                this.treeViewFieldData.SelectedNode = null;
                this.treeViewFieldData.ResumeLayout();
                this.treeViewFieldData.EndUpdate();
                this.treeViewFieldData.Refresh();
                Cursor.Current = Cursors.Default;
                MessageBox.Show("DisplayError: " + ex.Message);
                return;
            }
            this.treeViewFieldData.ResumeLayout();
            this.treeViewFieldData.EndUpdate();
            this.treeViewFieldData.Refresh();
            this.enableDisableButtons(TreeViewNodeTypes.Root);
            this.enableDisableToolbarButtons(_actualLvl);
            Cursor.Current = Cursors.Default;
        }

        protected void displayEventSeries(CollectionEventSeries es)
        {
 
            Cursor.Current = Cursors.WaitCursor;
            this.treeViewFieldData.BeginUpdate();
            this.treeViewFieldData.SuspendLayout();
            TreeNode node = treeViewFieldData.SelectedNode;
            try
            {
                this.treeViewFieldData.Nodes.Clear();
                this.treeViewFieldData.Update();
                TreeNode esNode = new TreeNode();
                esNode = _tvOperations.displayEventSeries(es);
                treeViewFieldData.Nodes.Add(esNode);
                this.treeViewFieldData.SelectedNode = esNode;
                this._expandTrigger = false;
                if (this._tvOperations.expandLvl <= ExpandLevel.EventSeries)
                    esNode.ExpandAll();
                else
                    esNode.Expand();
                this._expandTrigger = true;
                this._actualLvl = TreeViewNodeTypes.EventSeriesNode;
                this.enableDisableButtons(TreeViewNodeTypes.EventSeriesNode);

            }
            catch (Exception ex)
            {
                this.treeViewFieldData.EndUpdate();
                this.treeViewFieldData.ResumeLayout();
                this.treeViewFieldData.Refresh();
                Cursor.Current = Cursors.Default;
                MessageBox.Show("DisplayError: " + ex.Message);
                this.pictureBoxHome_Click(null, null);
                return;
            }
            this.treeViewFieldData.ResumeLayout();
            this.treeViewFieldData.EndUpdate();
            this.treeViewFieldData.Refresh();
            Cursor.Current = Cursors.Default;
       
        }

        /// <summary>
        /// Displays an event.
        /// </summary>
        /// <param name="currentEvent">The current event.</param>
        protected void displayEvent(CollectionEvent ev)
        {
            if (ev != null)
            {
                Cursor.Current = Cursors.WaitCursor;
                this.treeViewFieldData.BeginUpdate();
                this.treeViewFieldData.SuspendLayout();
                TreeNode parent = treeViewFieldData.SelectedNode.Parent;
                TreeNode node = treeViewFieldData.SelectedNode;
                try
                {
                    TreeNode evNode = new TreeNode();
                    treeViewFieldData.SelectedNode = null;
                    IList<TreeNode> removeNodes = new List<TreeNode>();
                    int i = 0;
                    if (this._tvOperations.expandLvl <= ExpandLevel.EventSeries)
                    {
                        removeNodes.Add(node);
                        i = node.Index;
                    }
                    else
                    {
                        foreach (TreeNode tn in parent.Nodes)
                        {
                            TreeViewNodeData td = tn.Tag as TreeViewNodeData;
                            if (td.NodeType == TreeViewNodeTypes.EventNode)
                                removeNodes.Add(tn);
                            else
                                i++;
                        }
                    }
                    
                    evNode = _tvOperations.displayEvent(ev);
                    
                    foreach (TreeNode rem in removeNodes)
                    {
                        parent.Nodes.Remove(rem);
                    }
                    parent.Nodes.Insert(i, evNode);
                    this.treeViewFieldData.SelectedNode = evNode;
                    this._expandTrigger = false;
                    if (this._tvOperations.expandLvl <= ExpandLevel.Event)
                        evNode.ExpandAll();
                    else
                        evNode.Expand();
                    this._expandTrigger = true;
                    this._actualLvl = TreeViewNodeTypes.EventNode;
                    this.enableDisableButtons(TreeViewNodeTypes.EventNode);

                }
                catch (Exception ex)
                {
                    this.treeViewFieldData.SelectedNode = parent;
                    afterSelect(parent);
                    this.treeViewFieldData.EndUpdate();
                    this.treeViewFieldData.ResumeLayout();
                    this.treeViewFieldData.Refresh();
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("DisplayError: " + ex.Message);
                    return;
                }
                this.treeViewFieldData.ResumeLayout();
                this.treeViewFieldData.EndUpdate();
                this.treeViewFieldData.Refresh();
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// Displays a specimen (does not check whether the passed on parameter is valid).
        /// </summary>
        /// <param name="specimen">The current specimen.</param>
        /// <returns></returns>
        /// 
        private void displaySpecimen(CollectionSpecimen spec)
        {
            if (spec != null)
            {
                Cursor.Current = Cursors.WaitCursor;
                this.treeViewFieldData.BeginUpdate();
                this.treeViewFieldData.SuspendLayout();
                TreeNode parent = treeViewFieldData.SelectedNode.Parent;
                TreeNode node = treeViewFieldData.SelectedNode;
                try
                {
                    treeViewFieldData.SelectedNode = null;
                    TreeNode specNode = new TreeNode();
                    IList<TreeNode> removeNodes = new List<TreeNode>();
                    int i = 0;
                    if (this._tvOperations.expandLvl <= ExpandLevel.Event)
                    {
                        removeNodes.Add(node);
                        i = node.Index;
                    }
                    else
                    {
                        foreach (TreeNode tn in parent.Nodes)
                        {
                            TreeViewNodeData td = tn.Tag as TreeViewNodeData;
                            if (td.NodeType == TreeViewNodeTypes.SpecimenNode)
                                removeNodes.Add(tn);
                            else
                                i++;
                        }
                    }

                    specNode = _tvOperations.displaySpecimen(spec);

                    foreach (TreeNode rem in removeNodes)
                    {
                        parent.Nodes.Remove(rem);
                    }
                    parent.Nodes.Insert(i, specNode);
                    this.treeViewFieldData.SelectedNode = specNode;
                    this._expandTrigger = false;
                    if (this._tvOperations.expandLvl <= ExpandLevel.Specimen)
                        specNode.ExpandAll();
                    else
                        specNode.Expand();
                    this._expandTrigger = true;
                    this._actualLvl = TreeViewNodeTypes.SpecimenNode;
                    this.enableDisableButtons(TreeViewNodeTypes.SpecimenNode);

                }
                catch (Exception ex)
                {
                    this.treeViewFieldData.SelectedNode = parent;
                    afterSelect(parent);
                    this.treeViewFieldData.EndUpdate();
                    this.treeViewFieldData.ResumeLayout();
                    this.treeViewFieldData.Refresh();
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("DisplayError: " + ex.Message);
                    return;
                }
                this.treeViewFieldData.ResumeLayout();
                this.treeViewFieldData.EndUpdate();
                this.treeViewFieldData.Refresh();
                Cursor.Current = Cursors.Default;
            }
        }

        private void displayIdentificationUnit(IdentificationUnit iu)
        {
            if (iu != null)
            {
                TreeNode parent = treeViewFieldData.SelectedNode.Parent;
                TreeNode node = treeViewFieldData.SelectedNode;
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    this.treeViewFieldData.BeginUpdate();
                    this.treeViewFieldData.SuspendLayout();
                    treeViewFieldData.SelectedNode = null;
                    TreeNode iuNode = new TreeNode();
                    treeViewFieldData.SelectedNode = null;
                    IList<TreeNode> removeNodes = new List<TreeNode>();
                    int i = 0;
                    if (this._tvOperations.expandLvl <= ExpandLevel.Specimen)
                    {
                        removeNodes.Add(node);
                        i = node.Index;
                    }
                    else
                    {
                        foreach (TreeNode tn in parent.Nodes)
                        {
                            TreeViewNodeData td = tn.Tag as TreeViewNodeData;
                            if (td.NodeType == TreeViewNodeTypes.IdentificationUnitNode)
                                removeNodes.Add(tn);
                            else
                                i++;
                        }
                    }


                    iuNode = _tvOperations.displayIdentificationUnit(iu);
                    foreach (TreeNode rem in removeNodes)
                    {
                        parent.Nodes.Remove(rem);
                    }
                    parent.Nodes.Insert(i, iuNode);
                    this.treeViewFieldData.SelectedNode = iuNode;
                    this._expandTrigger = false;
                    iuNode.ExpandAll();
                    this._expandTrigger = true;
                    this._actualLvl = TreeViewNodeTypes.IdentificationUnitNode;
                    this.enableDisableButtons(TreeViewNodeTypes.IdentificationUnitNode);
                    this.treeViewFieldData.Refresh();
                }
                catch (Exception ex)
                {
                    this.treeViewFieldData.SelectedNode = null;
                    this.treeViewFieldData.ResumeLayout();
                    this.treeViewFieldData.EndUpdate();
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("DisplayError: " + ex.Message);
                    return;
                }

                this.treeViewFieldData.ResumeLayout();
                this.treeViewFieldData.EndUpdate();
                this.treeViewFieldData.Refresh();
                Cursor.Current = Cursors.Default;
            }
        }
        
        private void displayRepresentingNode(TreeNode node)
        {
            TreeViewNodeData data = node.Tag as TreeViewNodeData;
            if (data == null)
                throw new Exception("Irregular Display Command");

            try
            {
                switch (data.NodeType)
                {
                    case TreeViewNodeTypes.EventSeriesNode:
                        if (data.ID != null)
                        {
                            EventSeriess.Instance.Find((int)data.ID);
                            this.displayEventSeries(EventSeriess.Instance.Current);
                        }
                        else
                        {
                            this.displayEventSeries(null);
                        }
                        break;
                    case TreeViewNodeTypes.EventNode:
                        if (this._events != null)
                        {
                            this._events.Find((int)data.ID);
                            this.displayEvent(this._events.Current);
                        }
                        break;
                    case TreeViewNodeTypes.SpecimenNode:
                        if (this._specimen != null)
                        {
                            if (this._specimen.Find((int)data.ID))
                                this.displaySpecimen(this._specimen.Current);
                            else
                            {
                                throw new Exception("Data of selected Specimen couldn't be retrieved.");
                            }
                        }
                        break;
                    case TreeViewNodeTypes.IdentificationUnitNode:
                        if (this._iu != null)
                        {
                            this._iu.FindTopLevelIU((int)data.ID);
                            this.displayIdentificationUnit(this._iu.Current);
                        }
                        break;
                    default:
                        throw new Exception("Irregular Display Command");
                }
            }
            catch (ConnectionCorruptedException ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Helper Methods

        //private void saveEventProperties()
        //{
        //    Cursor.Current = Cursors.WaitCursor;
        //    if (EventSeriess.Instance.Current != null)
        //    {
        //        DataFunctions.Instance.Update(EventSeriess.Instance.Current);

        //    }
        //    if (this._events!=null && this._events.Current != null)
        //        DataFunctions.Instance.Update(this._events.Current);
        //    if(this._specimen!=null&&this._specimen.Current!=null)
        //        DataFunctions.Instance.Update(this._specimen.Current);
        //    if(this._iu!=null&&this._iu.Current!=null)
        //        DataFunctions.Instance.Update(this._iu.Current);
        //    Cursor.Current = Cursors.Default;
        //}

        private void resetTreeView()
        {
            this.treeViewFieldData = new TreeView();
            this.treeViewFieldData.ContextMenu = this.contextMenuTreeView;
            this.treeViewFieldData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewFieldData.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.treeViewFieldData.ImageIndex = 0;
            this.treeViewFieldData.ImageList = this.imageListDiversityCollection;
            this.treeViewFieldData.Indent = 5;
            this.treeViewFieldData.Location = new System.Drawing.Point(0, 22);
            this.treeViewFieldData.Name = "treeViewFieldData";
            this.treeViewFieldData.SelectedImageIndex = 0;
            this.treeViewFieldData.Size = new System.Drawing.Size(240, 166);
            this.treeViewFieldData.TabIndex = 9;
            this.treeViewFieldData.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeViewFieldData_AfterCollapse);
            this.treeViewFieldData.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewFieldData_BeforeExpand);
            this.treeViewFieldData.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewFieldData_BeforeCollapse);
            this.treeViewFieldData.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewFieldData_AfterSelect);
            this.Controls.Add(this.treeViewFieldData);
            this.setIconSize();
            this.treeViewFieldData.Refresh();
            this.treeViewFieldData.Show();
        }

        private void expandAll()
        {
            this._expandTrigger = false;
            this.treeViewFieldData.ExpandAll();
            this._expandTrigger = true;
        }

        /// <summary>
        /// Finds the Event ID of which the actual selected node is related.
        /// </summary>
        /// <returns>The ID of the Event or -1 if no Event node has been found</returns>
        /// 
        private int findSeriesID()
        {
            TreeNode node = this.treeViewFieldData.SelectedNode;
            if (node != null)
            {
                TreeViewNodeData data = node.Tag as TreeViewNodeData;

                if (data == null)
                    return -1;

                while (data.NodeType != TreeViewNodeTypes.EventSeriesNode)
                {
                    node = node.Parent;

                    if (node == null)
                        data = null;
                    else
                        data = node.Tag as TreeViewNodeData;

                    if (data == null)
                        return -1;
                }
                return (int)data.ID;
            }
            return -1;
        }

        /// <summary>
        /// Finds the Event ID of which the actual selected node is related.
        /// </summary>
        /// <returns>The ID of the Event or -1 if no Event node has been found</returns>
        private int findEventID()
        {
            TreeNode node = this.treeViewFieldData.SelectedNode;
            if (node != null)
            {
                TreeViewNodeData data = node.Tag as TreeViewNodeData;

                if (data == null)
                    return -1;

                while (data.NodeType != TreeViewNodeTypes.EventNode)
                {
                    node = node.Parent;

                    if (node == null)
                        data = null;
                    else
                        data = node.Tag as TreeViewNodeData;

                    if (data == null)
                        return -1;
                }
                return (int)data.ID;
            }

            return -1;
        }

        /// <summary>
        /// Finds the specimen ID of which the actual selected node is related.
        /// </summary>
        /// <returns>The ID of the specimen or -1 if no specimen node has been found</returns>
        private int findSpecimenID()
        {
            TreeNode node = this.treeViewFieldData.SelectedNode;
            if (node != null)
            {
                TreeViewNodeData data = node.Tag as TreeViewNodeData;

                if (data == null)
                    return -1;

                while (data.NodeType != TreeViewNodeTypes.SpecimenNode)
                {
                    node = node.Parent;

                    if (node == null)
                        data = null;
                    else
                        data = node.Tag as TreeViewNodeData;

                    if (data == null)
                        return -1;
                }
                return (int)data.ID;
            }
            return -1;
        }

        /// <summary>
        /// Finds the IdentificationUnit ID of which the actual selected node is related.
        /// </summary>
        /// <returns>The ID of the IdentificationUnit or -1 if no IdentificationUnit node has been found</returns>
        private int findIdentificationUnitID()
        {
            TreeNode node = this.treeViewFieldData.SelectedNode;
            if (node != null)
            {
                TreeViewNodeData data;

                do
                {
                    node = node.Parent;

                    if (node == null)
                    {
                        data = null;
                        break;
                    }

                    data = node.Tag as TreeViewNodeData;
                } while (data.NodeType != TreeViewNodeTypes.IdentificationUnitNode);

                if (data == null)
                    return -1;

                return (int)data.ID;
            }
            return -1;
        }

        private int findIUTopLevelID(IdentificationUnit iu)
        {
            if (iu == null)
                return -1;

            if (iu.RelatedUnit == null)
                return iu.IdentificationUnitID;

            while (iu.RelatedUnit != null)
            {
                iu = iu.RelatedUnit;
            }
            return iu.IdentificationUnitID;
        }

        private void moveTaxonomicGroupToFront(String code)
        {
            try
            {
                //Ordnung der Taxonomischen Gruppen aktualisieren
                CollTaxonomicGroup_Enum tax = null;

                //Höchste DisplayOrder identifizieren
                short max = 0;
                IList<CollTaxonomicGroup_Enum> groups = DataFunctions.Instance.RetrieveTaxonomicGroups();
                foreach (CollTaxonomicGroup_Enum tg in groups)
                {
                    if (tg != null)
                    {
                        if (tg.DisplayOrder > max)
                            if (tg.DisplayOrder != 0)
                                max = (short)tg.DisplayOrder;
                        if (code != null && tg.Code == code)
                            tax = tg;
                    }
                }
                if (tax == null)//Fehlerhafte Eingabe
                    return;

                //Aktuelle taxonmische Gruppe nach oben schieben
                if ((max >= tax.DisplayOrder) || (max == 0))
                {
                    try
                    {
                        //Overflow abfangen
                        if (max == Int16.MaxValue)
                        //Overflow Liste neu initialisieren
                        {
                            foreach (CollTaxonomicGroup_Enum tg in groups)
                            {
                                if (tg != null)
                                {
                                    tg.DisplayOrder = 0;
                                    DataFunctions.Instance.Update(tg);
                                }
                            }
                            tax.DisplayOrder = 1;
                            DataFunctions.Instance.Update(tax);
                        }
                        else
                        {
                            tax.DisplayOrder = ((short)(max + 1));
                            DataFunctions.Instance.Update(tax);
                        }
                    }
                    catch (DataFunctionsException ex)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Taxonomic Group order couldn't be changed. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }
                }
            }
            catch (ConnectionCorruptedException ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Taxonomic Group order couldn't be changed. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
        }

        //private void displayChangedNodeData()
        //{
        //    if (this.treeViewFieldData.Nodes.Count == 0)
        //    {
        //        return;
        //    }

        //    TreeViewNodeData data = this.treeViewFieldData.SelectedNode.Tag as TreeViewNodeData;
        //    TreeNode selNode = this.treeViewFieldData.SelectedNode;

        //    while (data != null)
        //    {
        //        // Display Changed TreeNode Data in Overview TabPage
        //        switch (data.NodeType)
        //        {
        //            case TreeViewNodeTypes.IdentificationUnitNode:
        //                this._tvOperations.parametrizeIUNode(this._events.RetrieveIdentificationUnit((int) data.ID), this.treeViewFieldData.SelectedNode);

        //                //AccesionNumber im TreeView des ParentSpecimen aktualisieren
        //                while (this.treeViewFieldData.SelectedNode.Parent != null)
        //                {
        //                    if ((this.treeViewFieldData.SelectedNode.Parent.Tag as TreeViewNodeData).NodeType == TreeViewNodeTypes.SpecimenNode)
        //                    {
        //                        this.treeViewFieldData.SelectedNode.Parent.Text = this._specimen.Current.CollectionSpecimenID.ToString();
        //                        break;
        //                    }
        //                    this.treeViewFieldData.SelectedNode = this.treeViewFieldData.SelectedNode.Parent;
        //                }
        //                this.treeViewFieldData.SelectedNode = this.treeViewFieldData.SelectedNode.Parent;
        //                data = this.treeViewFieldData.SelectedNode.Tag as TreeViewNodeData;
        //                break;

        //            case TreeViewNodeTypes.AnalysisNode:
        //                this._tvOperations.parametrizeIUANode(this._events.RetrieveIdentificationUnitAnalysis(data.CollectionSpecimenID, data.IdentificationUnitID, data.AnalysisID, data.AnalysisNumber), this.treeViewFieldData.SelectedNode);
        //                this.treeViewFieldData.SelectedNode = this.treeViewFieldData.SelectedNode.Parent;
        //                data = this.treeViewFieldData.SelectedNode.Tag as TreeViewNodeData;
        //                break;
        //            case TreeViewNodeTypes.SpecimenNode:
        //                // AccesionNumber im TreeView aktualisieren
        //                this.treeViewFieldData.SelectedNode.Text = this._specimen.Current.CollectionSpecimenID.ToString();
        //                this.treeViewFieldData.SelectedNode = this.treeViewFieldData.SelectedNode.Parent;
        //                data = this.treeViewFieldData.SelectedNode.Tag as TreeViewNodeData;
        //                break;
        //            case TreeViewNodeTypes.EventNode:
        //                if (this._events.Current.SeriesID != null)
        //                {
        //                    if ((int)this._events.Current.SeriesID != GlobalProperties.CurrentSeriesID && GlobalProperties.CurrentSeriesID != -1)
        //                    {
        //                        this._events = null;
        //                        this.displayEvent(this._events.Current);
        //                        return;
        //                    }
        //                }

        //                // Text des Events + Description ändern
        //                StringBuilder sbEventTitle = new StringBuilder();
        //                sbEventTitle.Append(this._events.Current.CollectionDate.Year);
        //                sbEventTitle.Append('/');
        //                sbEventTitle.Append(this._events.Current.CollectionDate.Month);
        //                sbEventTitle.Append('/');
        //                sbEventTitle.Append(this._events.Current.CollectionDate.Day);

        //                if (this._events.Current.CollectorsEventNumber != null && !this._events.Current.CollectorsEventNumber.Equals(string.Empty))
        //                {
        //                    sbEventTitle.Append(": No.");
        //                    sbEventTitle.Append(this._events.Current.CollectorsEventNumber);
        //                }

        //                if (this._events.Current.LocalityDescription != null && !this._events.Current.LocalityDescription.Equals(string.Empty))
        //                {
        //                    sbEventTitle.Append(" (");
        //                    sbEventTitle.Append(this._events.Current.LocalityDescription);
        //                    sbEventTitle.Append(")");
        //                }
        //                this.treeViewFieldData.SelectedNode.Text = sbEventTitle.ToString();

        //                this.treeViewFieldData.SelectedNode = this.treeViewFieldData.SelectedNode.Parent;
        //                data = this.treeViewFieldData.SelectedNode.Tag as TreeViewNodeData;

        //                if (this._events.Current.SeriesID != null)
        //                {
        //                    if (data == null)
        //                        data = new TreeViewNodeData((int)this._events.Current.SeriesID, TreeViewNodeTypes.EventSeriesNode);
        //                    else
        //                    {
        //                        data.ID = (int)this._events.Current.SeriesID;
        //                        data.NodeType = TreeViewNodeTypes.EventSeriesNode;
        //                    }
        //                }
                        
        //                break;
        //            case TreeViewNodeTypes.EventSeriesNode:
        //                CollectionEventSeries ce = null;
        //                if (data.ID != null)
        //                    ce = this._events.RetrieveEventSeries((int)data.ID);
        //                this._tvOperations.parametrizeEventSeriesNode(ce, this.treeViewFieldData.SelectedNode);
        //                data = null;
        //                break;
        //            default:
        //                data = null;
        //                break;
        //        }
        //    }
        //    this.treeViewFieldData.SelectedNode = selNode;
        //    this.treeViewFieldData.Refresh();
        //    this.treeViewFieldData.Focus(); 
        //}

        private void enableDisableToolbarButtons(bool value)
        {
            // Toolbar Buttons
            this.pictureBoxDelete.Enabled = value;
            this.pictureBoxEdit.Enabled = value;
            this.toolBarButtonMoveFirst.Enabled = value;
            this.toolBarButtonMoveLast.Enabled = value;
            this.toolBarButtonNext.Enabled = value;
            this.toolBarButtonPrevious.Enabled = value;
        }

        private void enableDisableToolbarButtons(TreeViewNodeTypes type)
        {
            if (type != null)
            {
                switch (type)
                {
                    case TreeViewNodeTypes.Root:
                        this.enableDisableToolbarButtons(false);
                        break;
                    case TreeViewNodeTypes.EventSeriesNode:
                        this.pictureBoxDelete.Enabled = true;
                        this.pictureBoxEdit.Enabled = true;
                        if (this._tvOperations.expandLvl >= ExpandLevel.EventSeries)
                        {
                            this.toolBarButtonLevelPicture.ImageIndex = 8;
                            this.toolBarButtonLevelPicture.Enabled = true;
                            this.toolBarButtonMoveFirst.Enabled = true;
                            this.toolBarButtonMoveLast.Enabled = true;
                            this.toolBarButtonNext.Enabled = true;
                            this.toolBarButtonPrevious.Enabled = true;
                        }
                        else
                        {
                            this.toolBarButtonLevelPicture.ImageIndex = 8;
                            this.toolBarButtonLevelPicture.Enabled = false;
                            this.toolBarButtonMoveFirst.Enabled = false;
                            this.toolBarButtonMoveLast.Enabled = false;
                            this.toolBarButtonNext.Enabled = false;
                            this.toolBarButtonPrevious.Enabled = false;
                        }
                        break;
                    case TreeViewNodeTypes.EventNode:
                        this.pictureBoxDelete.Enabled = true;
                        this.pictureBoxEdit.Enabled = true;
                        if (this._tvOperations.expandLvl >= ExpandLevel.Event)
                        {
                            this.toolBarButtonLevelPicture.ImageIndex = 9;
                            this.toolBarButtonLevelPicture.Enabled = true;
                            this.toolBarButtonMoveFirst.Enabled = true;
                            this.toolBarButtonMoveLast.Enabled = true;
                            this.toolBarButtonNext.Enabled = true;
                            this.toolBarButtonPrevious.Enabled = true;
                        }
                        else
                        {
                            this.toolBarButtonLevelPicture.ImageIndex = 9;
                            this.toolBarButtonLevelPicture.Enabled = false;
                            this.toolBarButtonMoveFirst.Enabled = false;
                            this.toolBarButtonMoveLast.Enabled = false;
                            this.toolBarButtonNext.Enabled = false;
                            this.toolBarButtonPrevious.Enabled = false;
                        }
                        break;
                    case TreeViewNodeTypes.SpecimenNode:

                        this.pictureBoxDelete.Enabled = true;
                        this.pictureBoxEdit.Enabled = true;
                        if (this._tvOperations.expandLvl >= ExpandLevel.Specimen)
                        {
                            this.toolBarButtonLevelPicture.ImageIndex = 10;
                            this.toolBarButtonLevelPicture.Enabled = true;
                            this.toolBarButtonMoveFirst.Enabled = true;
                            this.toolBarButtonMoveLast.Enabled = true;
                            this.toolBarButtonNext.Enabled = true;
                            this.toolBarButtonPrevious.Enabled = true;
                        }
                        else
                        {
                            this.toolBarButtonLevelPicture.ImageIndex = 10;
                            this.toolBarButtonLevelPicture.Enabled = false;
                            this.toolBarButtonMoveFirst.Enabled = false;
                            this.toolBarButtonMoveLast.Enabled = false;
                            this.toolBarButtonNext.Enabled = false;
                            this.toolBarButtonPrevious.Enabled = false;
                        }
                        break;
                    case TreeViewNodeTypes.IdentificationUnitNode:
                        this.pictureBoxDelete.Enabled = true;
                        this.pictureBoxEdit.Enabled = true;
                        if (this._tvOperations.expandLvl >= ExpandLevel.IdentificationUnit)
                        {
                            this.toolBarButtonLevelPicture.ImageIndex = 12;
                            this.toolBarButtonLevelPicture.Enabled = true;
                            this.toolBarButtonMoveFirst.Enabled = true;
                            this.toolBarButtonMoveLast.Enabled = true;
                            this.toolBarButtonNext.Enabled = true;
                            this.toolBarButtonPrevious.Enabled = true;
                        }
                        else
                        {
                            this.toolBarButtonLevelPicture.ImageIndex = 12;
                            this.toolBarButtonLevelPicture.Enabled = false;
                            this.toolBarButtonMoveFirst.Enabled = false;
                            this.toolBarButtonMoveLast.Enabled = false;
                            this.toolBarButtonNext.Enabled = false;
                            this.toolBarButtonPrevious.Enabled = false;
                        }
                        break;
                    default:
                        this.pictureBoxDelete.Enabled = true;
                        this.pictureBoxEdit.Enabled = true;
                        this.toolBarButtonMoveFirst.Enabled = false;
                        this.toolBarButtonMoveLast.Enabled = false;
                        this.toolBarButtonNext.Enabled = false;
                        this.toolBarButtonPrevious.Enabled = false;
                        break;
                }
            }
        }

        private void enableDisableButtons(TreeViewNodeTypes type)
        {
            if (type != null)
            {
                // enable functions in the tree according to the current selection
                switch (type)
                {
                    case TreeViewNodeTypes.Root:
                        this.pictureBoxGPS.Visible = true;
                        this.pictureBoxNewEventSeries.Visible = true;
                        this.pictureBoxHome.Visible = false;
                        this.pictureBoxNewEvent.Visible = false;
                        this.pictureBoxNewEventProperty.Visible = false;
                        //this.pictureBoxNewAgent.Visible = false;
                        this.pictureBoxNewLocalisation.Visible = false;
                        this.pictureBoxNewIUGeoAnalysis.Visible = false;
                        this.pictureBoxNewSpecimen.Visible = false;
                        this.pictureBoxNewIdentificationUnit.Visible = false;
                        this.pictureBoxNewAnalysis.Visible = false;
                        this.menuItemTakePicture.Enabled = false;
                        this.menuItemDisplayPicture.Enabled = false;
                        this.menuItemTakeVideo.Enabled = false;
                        this.menuItemShowVideo.Enabled = false;
                        this.menuItemTakeAudio.Enabled = false;
                        this.menuItemPlayAudio.Enabled = false;
                        break;
                    case TreeViewNodeTypes.EventSeriesNode:
                        this.pictureBoxGPS.Visible = true;
                        this.pictureBoxNewEventSeries.Visible = false;
                        this.pictureBoxHome.Visible = true;
                        this.pictureBoxNewEvent.Visible = true;
                        this.pictureBoxNewEventProperty.Visible = false;
                        //this.pictureBoxNewAgent.Visible = false;
                        this.pictureBoxNewLocalisation.Visible = false;
                        this.pictureBoxNewIUGeoAnalysis.Visible = false;
                        this.pictureBoxNewSpecimen.Visible = false;
                        this.pictureBoxNewIdentificationUnit.Visible = false;
                        this.pictureBoxNewAnalysis.Visible = false;
                        this.menuItemTakePicture.Enabled = false;
                        this.menuItemDisplayPicture.Enabled = false;
                        this.menuItemTakeVideo.Enabled = false;
                        this.menuItemShowVideo.Enabled = false;
                        this.menuItemTakeAudio.Enabled = false;
                        this.menuItemPlayAudio.Enabled = false;
                        break;
                    case TreeViewNodeTypes.EventNode: // event node
                        this.pictureBoxGPS.Visible = true;
                        this.pictureBoxNewEventSeries.Visible = false;
                        this.pictureBoxHome.Visible = true;
                        this.pictureBoxNewEvent.Visible = false;
                        this.pictureBoxNewEventProperty.Visible = true;
                        //this.pictureBoxNewAgent.Visible = false;
                        this.pictureBoxNewLocalisation.Visible = true;
                        this.pictureBoxNewIUGeoAnalysis.Visible = false;
                        this.pictureBoxNewSpecimen.Visible = true;
                        this.pictureBoxNewIdentificationUnit.Visible = false;
                        this.pictureBoxNewAnalysis.Visible = false;
                        this.menuItemTakePicture.Enabled = true;
                        this.menuItemDisplayPicture.Enabled = true;
                        this.menuItemTakeVideo.Enabled = true;
                        this.menuItemShowVideo.Enabled = true;
                        this.menuItemTakeAudio.Enabled = true;
                        this.menuItemPlayAudio.Enabled = true;
                        break;
                    case TreeViewNodeTypes.LocalisationNode: // property, localisation, agent, analysis
                    case TreeViewNodeTypes.SitePropertyNode:
                    case TreeViewNodeTypes.AgentNode:
                    case TreeViewNodeTypes.AnalysisNode:
                        this.pictureBoxGPS.Visible = true;
                        this.pictureBoxNewEventSeries.Visible = false;
                        this.pictureBoxHome.Visible = true;
                        this.pictureBoxNewEvent.Visible = false;
                        this.pictureBoxNewEventProperty.Visible = false;
                        //this.pictureBoxNewAgent.Visible = false;
                        this.pictureBoxNewLocalisation.Visible = false;
                        this.pictureBoxNewIUGeoAnalysis.Visible = false;
                        this.pictureBoxNewSpecimen.Visible = false;
                        this.pictureBoxNewIdentificationUnit.Visible = false;
                        this.pictureBoxNewAnalysis.Visible = false;
                        this.menuItemTakePicture.Enabled = false;
                        this.menuItemDisplayPicture.Enabled = false;
                        this.menuItemTakeVideo.Enabled = false;
                        this.menuItemShowVideo.Enabled = false;
                        this.menuItemTakeAudio.Enabled = false;
                        this.menuItemPlayAudio.Enabled = false;
                        break;
                    case TreeViewNodeTypes.GeographyNode:
                        this.pictureBoxGPS.Visible = true;
                        this.pictureBoxNewEventSeries.Visible = false;
                        this.pictureBoxHome.Visible = true;
                        this.pictureBoxNewEvent.Visible = false;
                        this.pictureBoxNewEventProperty.Visible = false;
                        //this.pictureBoxNewAgent.Visible = false;
                        this.pictureBoxNewLocalisation.Visible = false;
                        this.pictureBoxNewIUGeoAnalysis.Visible = false;
                        this.pictureBoxNewSpecimen.Visible = false;
                        this.pictureBoxNewIdentificationUnit.Visible = false;
                        this.pictureBoxNewAnalysis.Visible = false;
                        this.menuItemTakePicture.Enabled = false;
                        this.menuItemDisplayPicture.Enabled = false;
                        this.menuItemTakeVideo.Enabled = false;
                        this.menuItemShowVideo.Enabled = false;
                        this.menuItemTakeAudio.Enabled = false;
                        this.menuItemPlayAudio.Enabled = false;
                        break;
                    case TreeViewNodeTypes.SpecimenNode: // specimen
                        this.pictureBoxGPS.Visible = true;
                        this.pictureBoxNewEventSeries.Visible = false;
                        this.pictureBoxHome.Visible = true;
                        this.pictureBoxNewEvent.Visible = false;
                        this.pictureBoxNewEventProperty.Visible = false;
                        //this.pictureBoxNewAgent.Visible = false;
                        this.pictureBoxNewLocalisation.Visible = false;
                        this.pictureBoxNewIUGeoAnalysis.Visible = false;
                        this.pictureBoxNewSpecimen.Visible = false;
                        this.pictureBoxNewIdentificationUnit.Visible = true;
                        this.pictureBoxNewAnalysis.Visible = false;
                        this.menuItemTakePicture.Enabled = true;
                        this.menuItemDisplayPicture.Enabled = true;
                        this.menuItemTakeVideo.Enabled = true;
                        this.menuItemShowVideo.Enabled = true;
                        this.menuItemTakeAudio.Enabled = true;
                        this.menuItemPlayAudio.Enabled = true;
                        break;
                    case TreeViewNodeTypes.IdentificationUnitNode: // identification unit
                        this.pictureBoxGPS.Visible = true;
                        this.pictureBoxNewEventSeries.Visible = false;
                        this.pictureBoxHome.Visible = true;
                        this.pictureBoxNewEvent.Visible = false;
                        this.pictureBoxNewEventProperty.Visible = false;
                        //this.pictureBoxNewAgent.Visible = false;
                        this.pictureBoxNewLocalisation.Visible = false;
                        this.pictureBoxNewIUGeoAnalysis.Visible = true;
                        this.pictureBoxNewSpecimen.Visible = false;
                        this.pictureBoxNewIdentificationUnit.Visible = true;
                        this.pictureBoxNewAnalysis.Visible = true;
                        this.menuItemTakePicture.Enabled = true;
                        this.menuItemDisplayPicture.Enabled = true;
                        this.menuItemTakeVideo.Enabled = true;
                        this.menuItemShowVideo.Enabled = true;
                        this.menuItemTakeAudio.Enabled = true;
                        this.menuItemPlayAudio.Enabled = true;
                        break;
                    case TreeViewNodeTypes.Unknown:
                        this.pictureBoxNewEventSeries.Visible = false;
                        this.pictureBoxHome.Visible = true;
                        this.pictureBoxNewEvent.Visible = false;
                        this.pictureBoxNewEventProperty.Visible = false;
                        //this.pictureBoxNewAgent.Visible = false;
                        this.pictureBoxNewLocalisation.Visible = false;
                        this.pictureBoxNewIUGeoAnalysis.Visible = false;
                        this.pictureBoxNewSpecimen.Visible = false;
                        this.pictureBoxNewIdentificationUnit.Visible = false;
                        this.pictureBoxNewAnalysis.Visible = false;
                        this.menuItemTakePicture.Enabled = false;
                        this.menuItemDisplayPicture.Enabled = false;
                        this.menuItemTakeVideo.Enabled = false;
                        this.menuItemShowVideo.Enabled = false;
                        this.menuItemTakeAudio.Enabled = false;
                        this.menuItemPlayAudio.Enabled = false;
                        break;
                }
                IList<PictureBox> list = new List<PictureBox>();
                list.Add(this.pictureBoxNewAnalysis);
                list.Add(this.pictureBoxNewIdentificationUnit);
                list.Add(this.pictureBoxNewSpecimen);
                list.Add(this.pictureBoxNewLocalisation);
                list.Add(this.pictureBoxNewIUGeoAnalysis);
                list.Add(this.pictureBoxNewEventProperty);
                list.Add(this.pictureBoxNewEvent);
                list.Add(this.pictureBoxNewEventSeries);
                //list.Add(this.pictureBoxHome);
                //list.Add(this.pictureBoxEdit);
                //list.Add(this.pictureBoxDelete);
                //list.Add(this.pictureBoxGPS);
                groupHorizontal(list, locationsPictureBox);
            }
        }

        private void initializeLocations(IList<PictureBox> pictures, IList<Point> locations)
        {
            if (pictures != null && locations != null)
            {
                locations.Clear();
                foreach (PictureBox pb in pictures)
                {
                    if(pb != null)
                        locations.Add(pb.Location);
                }
            }
            
        }

        private void groupHorizontal(IList<PictureBox> pictures, IList<Point> locations)
        {
            if (pictures != null && locations != null)
            {
                int i = 0;
                foreach (PictureBox pb in pictures)
                {
                    if (pb != null)
                    {
                        if (pb.Visible == true)
                        {
                            pb.Location = locations[i];
                            i++;
                        }
                    }
                }
                foreach (PictureBox pb in pictures)
                {
                    if (pb != null)
                    {
                        if (pb.Visible == false)
                        {
                            pb.Location = locations[i];
                            i++;
                        }
                    }
                }
            }
        }

        private void setIconSize()
        {
            try
            {
                if (UserProfiles.Instance.Current.ToolbarIcons.ToLower().Equals("large"))
                {
                    if (Screen.PrimaryScreen.WorkingArea.Width > 420)
                    {
                        this._currentDiversityImageList = imageListLargeDiversityCollection;
                        this._currentToolbarImageList = imageListLargeToolbarButtons;

                        this.pictureBoxGPS.Size = new Size(48, 48);
                        //this.pictureBoxNewAgent.Size = new Size(48, 48);
                        this.pictureBoxNewAnalysis.Size = new Size(48, 48);
                        this.pictureBoxNewEvent.Size = new Size(48, 48);
                        this.pictureBoxNewEventProperty.Size = new Size(48, 48);
                        this.pictureBoxNewEventSeries.Size = new Size(48, 48);
                        this.pictureBoxHome.Size = new Size(48, 48);
                        this.pictureBoxNewIdentificationUnit.Size = new Size(48, 48);
                        this.pictureBoxNewIUGeoAnalysis.Size = new Size(48, 48);
                        this.pictureBoxNewLocalisation.Size = new Size(48, 48);
                        this.pictureBoxNewSpecimen.Size = new Size(48, 48);
                        this.pictureBoxDelete.Size = new Size(48, 48);
                        this.pictureBoxEdit.Size = new Size(48, 48);
                        this.panelFieldDataCommands.Size = new Size(this.Width, 57);
                        this.treeViewFieldData.ImageList = imageListMediumDiversityCollection;

                    }
                    else
                    {
                        String oldToolbarSize = UserProfiles.Instance.Current.ToolbarIcons;
                        UserProfiles.Instance.Current.ToolbarIcons = "medium";

                        try
                        {
                            UserProfiles.Instance.Update(UserProfiles.Instance.Current);
                        }
                        catch (UserProfileCorruptedException ex)
                        {
                            Cursor.Current = Cursors.Default;
                            UserProfiles.Instance.Current.ToolbarIcons = oldToolbarSize;
                            MessageBox.Show(ex.Message + " Size of Toolbar-Icons remains: " + oldToolbarSize);
                        }
                    }
                }

                if (UserProfiles.Instance.Current.ToolbarIcons.ToLower().Equals("medium"))
                {
                    if (Screen.PrimaryScreen.WorkingArea.Width > 290)
                    {
                        this._currentDiversityImageList = imageListMediumDiversityCollection;
                        this._currentToolbarImageList = imageListMediumToolbarButtons;

                        this.pictureBoxGPS.Size = new Size(32, 32);
                        //this.pictureBoxNewAgent.Size = new Size(32, 32);
                        this.pictureBoxNewAnalysis.Size = new Size(32, 32);
                        this.pictureBoxNewEvent.Size = new Size(32, 32);
                        this.pictureBoxNewEventProperty.Size = new Size(32, 32);
                        this.pictureBoxNewEventSeries.Size = new Size(32, 32);
                        this.pictureBoxHome.Size = new Size(32, 32);
                        this.pictureBoxNewIdentificationUnit.Size = new Size(32, 32);
                        this.pictureBoxNewIUGeoAnalysis.Size = new Size(32, 32);
                        this.pictureBoxNewLocalisation.Size = new Size(32, 32);
                        this.pictureBoxNewSpecimen.Size = new Size(32, 32);
                        this.pictureBoxDelete.Size = new Size(32, 32);
                        this.pictureBoxEdit.Size = new Size(32, 32);
                        this.panelFieldDataCommands.Size = new Size(this.Width, 41);
                        this.treeViewFieldData.ImageList = this._currentDiversityImageList;
                    }
                    else
                    {
                        String oldToolbarSize = UserProfiles.Instance.Current.ToolbarIcons;
                        UserProfiles.Instance.Current.ToolbarIcons = "small";

                        try
                        {
                            UserProfiles.Instance.Update(UserProfiles.Instance.Current);
                        }
                        catch (UserProfileCorruptedException ex)
                        {
                            Cursor.Current = Cursors.Default;
                            UserProfiles.Instance.Current.ToolbarIcons = oldToolbarSize;
                            MessageBox.Show(ex.Message + " Size of Toolbar-Icons remains: " + oldToolbarSize);
                        }
                    }
                }

                if (UserProfiles.Instance.Current.ToolbarIcons.ToLower().Equals("small"))
                {
                    this._currentDiversityImageList = imageListDiversityCollection;
                    this._currentToolbarImageList = imageListToolbarButtons;

                    this.pictureBoxGPS.Size = new Size(16, 16);
                    //this.pictureBoxNewAgent.Size = new Size(16, 16);
                    this.pictureBoxNewAnalysis.Size = new Size(16, 16);
                    this.pictureBoxNewEvent.Size = new Size(16, 16);
                    this.pictureBoxNewEventProperty.Size = new Size(16, 16);
                    this.pictureBoxNewEventSeries.Size = new Size(16, 16);
                    this.pictureBoxHome.Size = new Size(16, 16);
                    this.pictureBoxNewIdentificationUnit.Size = new Size(16, 16);
                    this.pictureBoxNewIUGeoAnalysis.Size = new Size(16, 16);
                    this.pictureBoxNewLocalisation.Size = new Size(16, 16);
                    this.pictureBoxNewSpecimen.Size = new Size(16, 16);
                    this.pictureBoxDelete.Size = new Size(16, 16);
                    this.pictureBoxEdit.Size = new Size(16, 16);
                    this.panelFieldDataCommands.Size = new Size(this.Width, 25);
                    this.treeViewFieldData.ImageList = this._currentDiversityImageList;
                }
            }
            catch (ConnectionCorruptedException)
            {
                // Falls Exception auftritt, gibt es standardmäßig small icon size
                this._currentDiversityImageList = imageListDiversityCollection;
                this._currentToolbarImageList = imageListToolbarButtons;

                this.pictureBoxGPS.Size = new Size(16, 16);
                //this.pictureBoxNewAgent.Size = new Size(16, 16);
                this.pictureBoxNewAnalysis.Size = new Size(16, 16);
                this.pictureBoxNewEvent.Size = new Size(16, 16);
                this.pictureBoxNewEventProperty.Size = new Size(16, 16);
                this.pictureBoxNewEventSeries.Size = new Size(16, 16);
                this.pictureBoxHome.Size = new Size(16, 16);
                this.pictureBoxNewIdentificationUnit.Size = new Size(16, 16);
                this.pictureBoxNewIUGeoAnalysis.Size = new Size(16, 16);
                this.pictureBoxNewLocalisation.Size = new Size(16, 16);
                this.pictureBoxNewSpecimen.Size = new Size(16, 16);
                this.pictureBoxDelete.Size = new Size(16, 16);
                this.pictureBoxEdit.Size = new Size(16, 16);
                this.panelFieldDataCommands.Size = new Size(this.Width, 25);
                this.treeViewFieldData.ImageList = this._currentDiversityImageList;
            }

            this.toolBar.ImageList = this._currentToolbarImageList;
            
            // Apply Changes
            int index = (int)TreeViewIconIndex.GPS;
            this.pictureBoxGPS.Image = this._currentDiversityImageList.Images[this.pictureBoxNewEvent.Enabled ? index : index + 1];
            this.pictureBoxGPS.Refresh();

            //this.pictureBoxNewAgent.Refresh();
            //int index = (int)TreeViewIconIndex.Analysis;
            //this.pictureBoxNewAnalysis.Image = this._currentDiversityImageList.Images[this.pictureBoxNewAnalysis.Enabled ? index : index + 1];
            this.pictureBoxNewAnalysis_EnabledChanged(null,null);
            this.pictureBoxNewAnalysis.Refresh();
            this.pictureBoxNewEvent_EnabledChanged(null, null);
            this.pictureBoxNewEvent.Refresh();
            this.pictureBoxNewEventProperty_EnabledChanged(null, null);
            this.pictureBoxNewEventProperty.Refresh();
            this.pictureBoxNewEventSeries_EnabledChanged(null, null);
            this.pictureBoxNewEventSeries.Refresh();
            this.pictureBoxHome.Image = this._currentDiversityImageList.Images[(int)TreeViewIconIndex.Home];
            this.pictureBoxNewIdentificationUnit_EnabledChanged(null, null);
            this.pictureBoxNewIdentificationUnit.Refresh();
            this.pictureBoxNewIUGeoAnalysis_EnabledChanged(null, null);
            this.pictureBoxNewIUGeoAnalysis.Refresh();
            this.pictureBoxNewLocalisation_EnabledChanged(null, null);
            this.pictureBoxNewLocalisation.Refresh();
            this.pictureBoxNewSpecimen_EnabledChanged(null, null);
            this.pictureBoxNewSpecimen.Refresh();
            this.pictureBoxDelete.Image = this._currentToolbarImageList.Images[7];
            this.pictureBoxEdit.Image = this._currentToolbarImageList.Images[5];
            this.treeViewFieldData.Refresh();
            IList<PictureBox> list = new List<PictureBox>();
            list.Add(this.pictureBoxNewAnalysis);
            list.Add(this.pictureBoxNewIdentificationUnit);
            list.Add(this.pictureBoxNewSpecimen);
            list.Add(this.pictureBoxNewLocalisation);
            list.Add(this.pictureBoxNewIUGeoAnalysis);
            list.Add(this.pictureBoxNewEventProperty);
            list.Add(this.pictureBoxNewEvent);
            list.Add(this.pictureBoxNewEventSeries);
            calculatePictureBoxPositions(list);

            this.initializeLocations(list, locationsPictureBox);
            this.groupHorizontal(list, locationsPictureBox);
        }

        private void calculatePictureBoxPositions(IList<PictureBox> list)
        {
            if (list != null)
            {
                int width = this.Width;
                int distance = 0;
                foreach (PictureBox pb in list)
                {
                    if (pb != null)
                    {
                        distance = distance + pb.Size.Width + 5;
                        pb.Location = new Point(width - distance, pb.Location.Y);
                    }

                }
            }
        }

        protected void StartGPS()
        {
            try
            {
                bool test = StaticGPS.isOpened();
                if (!StaticGPS.isOpened())
                {
                    StaticGPS.Open();
                }
                else
                {
                    MessageBox.Show("GPS Running");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("GPS couldn't be started. (" + ex.Message + ")");
            }
        }

        protected void StopGPS()
        {
            if (StaticGPS.isOpened())
            {
                try
                {
                    StaticGPS.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("GPS couldn't be stopped. (" + ex.Message + ")");
                }
            }
        }

        #endregion

        #region Editing Satellite Label

        void UpdatePositionData(object sender, System.EventArgs args)
        {
            if (StaticGPS.device == null || StaticGPS.device.DeviceState == GpsServiceState.Off)
            {
                this.setPictureBoxImage((int)TreeViewIconIndex.GPSGrey, this.pictureBoxGPS);
                this.setPictureBoxImage((int)TreeViewIconIndex.Location0, this.pictureBoxNewLocalisation);
                return;
            }
            
            if (StaticGPS.position == null)
            {
                this.setPictureBoxImage((int)TreeViewIconIndex.GPSGrey, this.pictureBoxGPS);
                this.setPictureBoxImage((int)TreeViewIconIndex.Location0, this.pictureBoxNewLocalisation);
                return;
            }
                

            if (StaticGPS.position.SatelliteCount <= 2)
            {
                this.setPictureBoxImage((int)TreeViewIconIndex.GPSGrey, this.pictureBoxGPS);
            }
            else
            {
                this.setPictureBoxImage((int)TreeViewIconIndex.GPS, this.pictureBoxGPS);
            }

            if (StaticGPS.position.SatelliteCount < 4)
                this.setPictureBoxImage((int)TreeViewIconIndex.Location0, this.pictureBoxNewLocalisation);
            else if (StaticGPS.position.SatelliteCount == 4)
                this.setPictureBoxImage((int)TreeViewIconIndex.Location4, this.pictureBoxNewLocalisation);
            else if (StaticGPS.position.SatelliteCount == 5)
                this.setPictureBoxImage((int)TreeViewIconIndex.Location5, this.pictureBoxNewLocalisation);
            else if (StaticGPS.position.SatelliteCount == 6)
                this.setPictureBoxImage((int)TreeViewIconIndex.Location6, this.pictureBoxNewLocalisation);
            else
                this.setPictureBoxImage((int)TreeViewIconIndex.LocationMore, this.pictureBoxNewLocalisation);
     
            Cursor.Current = Cursors.Default;
        }

        void UpdateDeviceData(object sender, System.EventArgs args)
        {

        }

        delegate void ChangeGPSBoxImage(int imageIndex, PictureBox box);

        delegate void RefreshGPSBox(PictureBox box);

        private void setPictureBoxImage(int imageIndex, PictureBox box)
        {
            if (box.InvokeRequired)
            {
                ChangeGPSBoxImage theDelegate = new ChangeGPSBoxImage(setPictureBoxImage);
                this.Invoke(theDelegate, new object[] { imageIndex, box });

                RefreshGPSBox refreshDelegate = new RefreshGPSBox(refreshPictureBox);
                this.Invoke(refreshDelegate, new object[] { box });

            }
            else
            {
                box.Image = this._currentDiversityImageList.Images[imageIndex];
                box.Refresh();
            }
        }

        private void refreshPictureBox(PictureBox box)
        {
            if(box != null)
                box.Refresh();
        }

        protected override void OnClosed(EventArgs e)
        {
            try
            {
                this.StopGPS();

                StaticGPS.updatePositionDataHandler = null;
                StaticGPS.updateDeviceDataHandler = null;
            }
            catch (Exception) { }

            base.OnClosed(e);
        }
        
        #endregion
    }
}