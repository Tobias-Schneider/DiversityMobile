using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer.Util;

namespace Diversity_Synchronization
{
    public partial class ProgressReporter : IReportDetailedProgress , INotifyPropertyChanged
    {
        private double progressPercentage;
        private int progressDescriptionID;
        private string progressOutput;
        private bool isProgressIndeterminate;
        private bool isProgressFinished;
        private bool isCancelRequested;

        private ProgressScaler internalProgress;
            

        public ProgressReporter()
        {
            internalProgress = new ProgressReporter.ProgressScaler(this);            

            reset();
        }

        public void reset()
        {
            internalProgress = new ProgressScaler(this) ;
            progressPercentage = 0d;
            ProgressOutput = "";
            ProgressDescriptionID = 1824; //Please Wait...
            IsCancelRequested = false;
            IsProgressFinished = false;
            IsProgressIndeterminate = false;
        }


        #region Progress Notification
        public int ProgressPercentage
        {
            get
            {
                return (IsProgressFinished) ? 0 : (int)(progressPercentage + internalProgress.ExternalCurrent);
            }            
        }
        public int ProgressDescriptionID
        {
            get
            {
                return progressDescriptionID;
            }
            set
            {
                progressDescriptionID = value;
                this.RaisePropertyChanged(() => ProgressDescriptionID, PropertyChanged);
            }
        }
        public string ProgressOutput
        {
            get
            {
                return progressOutput;
            }
            set
            {
                progressOutput = value;
                this.RaisePropertyChanged(() => ProgressOutput, PropertyChanged);
            }
        }
        public bool IsProgressIndeterminate
        {
            get
            {
                return isProgressIndeterminate;
            }
            set
            {
                isProgressIndeterminate = value;
                if(isProgressIndeterminate)
                    internalProgress.sealProgress();
                this.RaisePropertyChanged(() => IsProgressIndeterminate, PropertyChanged);
            }
        }
        public bool IsProgressFinished
        {
            get
            {
                return isProgressFinished;
            }
            set
            {
                isProgressFinished = value;
                this.RaisePropertyChanged(() => IsProgressFinished, PropertyChanged);
            }
        }
        public bool IsCancelRequested
        {
            get
            {
                return isCancelRequested;
            }
            set
            {
                isCancelRequested = value;
                this.RaisePropertyChanged(() => IsCancelRequested, PropertyChanged);
            }
        }
        #endregion

        #region INotifyPropertyChanged Member

        public event PropertyChangedEventHandler PropertyChanged;        

        #endregion

        #region IReportDetailedProgress Member

        private bool hasInternalProgress = false;
        public bool HasInternalProgress
        {
            get
            {
                return hasInternalProgress;
            }
            private set
            {
                hasInternalProgress = value;
                this.RaisePropertyChanged(() => HasInternalProgress, PropertyChanged);
            }
        }

        private void internalProgressChanged()
        {
            this.RaisePropertyChanged(() => InternalProgressPercentage, PropertyChanged);
            this.RaisePropertyChanged(() => ProgressPercentage, PropertyChanged);
            this.RaisePropertyChanged(() => HasInternalProgress, PropertyChanged);
        }     

        public int InternalProgressPercentage
        {
            get { return (int)internalProgress.InternalCurrentPercentage; }
        }

        #endregion

        #region IReportDetailedProgress Member


        

        #endregion        

        
        #region IReportDetailedProgress Member


        public void advanceProgress(double percent)
        {
            internalProgress.sealProgress();
            advanceOverallProgress(percent);
            IsProgressIndeterminate = false;
        }
        private void advanceOverallProgress(double percent)
        {
            progressPercentage += percent;
            if (progressPercentage > 100)
                progressPercentage = 100d;

            this.RaisePropertyChanged(() => ProgressPercentage, PropertyChanged);
        }

        #endregion

        #region IReportDetailedProgress Member


        public IAdvanceProgress startExternal(double external, int counterTotal)
        {
            HasInternalProgress = false;
            internalProgress.startProgress(external, counterTotal);
            return internalProgress;
        }
        public IAdvanceProgress startExternal(double external)
        {
            return startExternal(external, 1);
        }

        public IAdvanceProgress startInternal(double external, int internalTotal)
        {
            HasInternalProgress = true;
            internalProgress.startProgress(external, internalTotal);
            return internalProgress;
        }
        public IAdvanceProgress startInternal(double external)
        {
            return startInternal(external,1);            
        }

       

        #endregion
       
    }
}
