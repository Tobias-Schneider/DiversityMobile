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
