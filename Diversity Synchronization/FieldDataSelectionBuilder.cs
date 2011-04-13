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
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using System.ComponentModel;
using UBT.AI4.Bio.DivMobi.DataItemFormTools;
using System.Windows.Threading;

namespace Diversity_Synchronization
{
    public partial class FieldDataSelectionBuilder : INotifyPropertyChanged
    {
        private AsynchronousActor selectionActor = new AsynchronousActor();
        private SearchSpecification query;
        private IList<ISerializableObject> asyncQueryResult;

        #region Properties 

        public ProgressReporter Progress
        {
            get
            {
                return selectionActor.Progress;
            }
        }
        public ObservableCollection<SearchSpecification> SearchTypes
        {
            get;
            private set;
        }

        private ObservableCollection<ISerializableObject> queryResult = new ObservableCollection<ISerializableObject>();
        public ObservableCollection<ISerializableObject> QueryResult
        {
            get
            {
                return queryResult;
            }
            private set
            {
                queryResult = value;
                this.RaisePropertyChanged(() => QueryResult, PropertyChanged);
            }
        }

        private ObservableCollection<SelectionContainer> selectedElements = new ObservableCollection<SelectionContainer>();
        public ObservableCollection<SelectionContainer> SelectedElements
        {
            get
            {
                return selectedElements;
            }
            private set
            {
                selectedElements = value;
                this.RaisePropertyChanged(() => SelectedElements, PropertyChanged);
            }
        }

        private List<ISerializableObject> finishedSelection = new List<ISerializableObject>();
        public List<ISerializableObject> FinishedSelection
        {
            get
            {
                return finishedSelection;
            }
            private set
            {
                finishedSelection = value;
                this.RaisePropertyChanged(() => FinishedSelection, PropertyChanged);
            }
        }
        private List<ISerializableObject> selectionRoots = new List<ISerializableObject>();
        public List<ISerializableObject> SelectionRoots
        {
            get
            {
                return selectionRoots;
            }
        }

        public TreeViewOperations TreeViewBuilder
        {
            get
            {
                return new TreeViewOperations(ExpandLevel.EventSeries, ConnectionsAccess.RepositoryDB.Connector);
            }
        }
        #endregion

        public FieldDataSelectionBuilder()
        {
            fillSearchTypes();                        
        }       
        private void fillSearchTypes()
        {
            SearchTypes = new ObservableCollection<SearchSpecification>()
            {
                //Collection Event Series
                new SearchSpecification(1261,typeof(CollectionEventSeries),new List<SearchSpecification.RestrictionSpecification>()
                {
                    //Series Code
                    new SearchSpecification.RestrictionSpecification(1271,"_SeriesCode",SearchSpecification.RestrictionSpecification.RestrictionType.Like),
                    //Description
                    new SearchSpecification.RestrictionSpecification(1272,"_Description",SearchSpecification.RestrictionSpecification.RestrictionType.Like),
                    //Start Date Range
                    new SearchSpecification.RestrictionSpecification(1273,"_DateStart",SearchSpecification.RestrictionSpecification.RestrictionType.BetweenDates),
                    //End Date Range
                    new SearchSpecification.RestrictionSpecification(1274,"_DateEnd",SearchSpecification.RestrictionSpecification.RestrictionType.BetweenDates),
                }),
                //Identification Unit
                new SearchSpecification(1262,typeof(IdentificationUnit),new List<SearchSpecification.RestrictionSpecification>()
                {
                    //Last Identification
                    new SearchSpecification.RestrictionSpecification(1275,"_LastIdentificationCache",SearchSpecification.RestrictionSpecification.RestrictionType.Like),
                    //Taxonomic Group
                    new SearchSpecification.RestrictionSpecification(1276,"_TaxonomicGroup",SearchSpecification.RestrictionSpecification.RestrictionType.Equals),
                    //Unit Description
                    new SearchSpecification.RestrictionSpecification(1277,"_UnitDescription",SearchSpecification.RestrictionSpecification.RestrictionType.Equals),
                    //Log Updated Between
                    new SearchSpecification.RestrictionSpecification(1278,"_LogUpdatedWhen",SearchSpecification.RestrictionSpecification.RestrictionType.BetweenDates),
                }),
            };
            /*lbi1 = new ListBoxItem();
            lbi1.Content = "Collection Agent";
            lbi2 = new ListBoxItem();
            lbi2.Content = "Collection Event";
            lbi3 = new ListBoxItem();
            lbi3.Content = "Collection Event Image";
            lbi4 = new ListBoxItem();
            lbi4.Content = "Collection Event Localisation";
            lbi5 = new ListBoxItem();
            lbi5.Content = "Collection Event Property";
            lbi6 = new ListBoxItem();
            collectionEventSeries.Content = ;
            lbi7 = new ListBoxItem();
            lbi7.Content = "Collection Specimen";
            lbi8 = new ListBoxItem();
            lbi8.Content = "Identification";
            lbi9 = new ListBoxItem();
            lbi9.Content = ;
            lbi10 = new ListBoxItem();
            lbi10.Content = "Identification Unit Analysis";
            lbi11 = new ListBoxItem();
            lbi11.Content = "Identification Unit Geo Analysis";*/

        }

        #region Query DB

        public void QueryDatabase(SearchSpecification search)
        {
            query = search; 
            selectionActor.beginAction(new Action<IReportDetailedProgress>(queryWorker));
        }
        private void queryWorker(IReportDetailedProgress progress)
        {
            progress.ProgressDescriptionID = 1290;
            var internalProgress = progress.startInternal(20d);

            ConnectionsAccess.RepositoryDB.Progress = internalProgress;
            asyncQueryResult = ConnectionsAccess.RepositoryDB.Connector.LoadList(query.ObjectType, query.GetQueryRestriction());


            progress.ProgressDescriptionID = 1291;
            internalProgress = progress.startInternal(70d, asyncQueryResult.Count);
            var newResult = new ObservableCollection<ISerializableObject>();
            foreach (var result in asyncQueryResult)
            {
                newResult.Add(result);
                internalProgress.advance();
                if (progress.IsCancelRequested)
                    break;
            }

                        
            QueryResult = newResult;
        }
        #endregion

        #region Finish Selection 

        public void finishSelection()
        {
            selectionActor.beginAction(new Action<IReportDetailedProgress>(finishSelectionWorker));
        }
        private void finishSelectionWorker(IReportDetailedProgress progress)
        {
            progress.ProgressDescriptionID = 1331;           

            var newFinishedSelection = new List<ISerializableObject>();
            List<Guid> selectedGUIDs = new List<Guid>();
            
            int containerCount = SelectedElements.Count;           
            var internalProgress = progress.startExternal(100d, containerCount);
            foreach (var sec in SelectedElements)
            {
                foreach (ISerializableObject iso in sec.RelatedObjects)
                {
                    if (!selectedGUIDs.Contains(iso.Rowguid))
                    {
                        selectedGUIDs.Add(iso.Rowguid);
                        newFinishedSelection.Add(iso);
                        if (isRoot(iso))
                            SelectionRoots.Add(iso);
                    }
                }
                internalProgress.advance();
            }

            FinishedSelection = newFinishedSelection;

            this.RaisePropertyChanged(() => FinishedSelection, PropertyChanged);
            this.RaisePropertyChanged(() => SelectionRoots, PropertyChanged);

            SelectedElements = new ObservableCollection<SelectionContainer>();
            QueryResult = new ObservableCollection<ISerializableObject>();
        }
        #endregion

        bool isRoot(ISerializableObject iso)
        {
            if (iso is CollectionEventSeries)
                return true;
            if (iso is CollectionEvent)
            {
                CollectionEvent ce = (CollectionEvent)iso;
                if (ce.SeriesID == null)
                    return true;
            }
            if (iso is CollectionSpecimen)
            {
                CollectionSpecimen spec = (CollectionSpecimen)iso;
                if (spec.CollectionEventID == null)
                    return true;
            }
            return false;
        }
        



        

        #region INotifyPropertyChanged Member

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
