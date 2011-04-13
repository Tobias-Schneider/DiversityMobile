using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer.Util;
using System.Threading;
using System.Windows.Threading;

namespace Diversity_Synchronization
{
    partial class AsynchronousActor
    {        
        private IAsyncResult progressAsyncResult;
        private AsyncCallback progressFinishedCallback;

        private ProgressReporter progress = new ProgressReporter();

        public ProgressReporter Progress
        {
            get
            {
                return progress;
            }
        }

        public Dispatcher Dispatcher
        {
            get;
            set;
        }

        public AsynchronousActor(Dispatcher d)
            : this()
        {
            Dispatcher = d;
        }

        public AsynchronousActor()
        {
            progressFinishedCallback = new AsyncCallback(progressFinished);            
        }

    public void beginAction(Action<IReportDetailedProgress> a)
        {
            if (this.progressAsyncResult == null || this.progressAsyncResult.IsCompleted)
            {
                progress.reset();                 
                this.progressAsyncResult = a.BeginInvoke(progress, progressFinishedCallback, null);
            }
        }

        

        private void progressFinished(IAsyncResult res)
        {
            if (ActionFinished != null)
            {
                if (Dispatcher != null)
                    Dispatcher.BeginInvoke(ActionFinished, null);
                else
                    ActionFinished();
            }
            progress.IsProgressFinished = true;
        }

        public event Action ActionFinished;
        
    }
}
