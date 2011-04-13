using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace UserSyncGui
{
    public class ProgressThread
    {
        public readonly Thread MyThread;
        private ProgressInformationForm form;

        public ProgressThread()
        {
            this.MyThread = new Thread(new ThreadStart(Execute));
        }

        void Execute()
        {
            form = new ProgressInformationForm();
            this.form.ShowDialog();
        }

        public String AdditionalInformation
        {
            set
            {
                if (form != null)
                {
                    form.setAdditionalInformation(value);
                }
            }
        }

        public String ActionInformation
        {
            set
            {
                while (form == null)
                    System.Threading.Thread.Sleep(1000);

                form.setActionInformation(value);
                //if (form != null)
                //{
                //    form.setActionInformation(value);
                //}
            }
        }

        public int Value
        {
            set
            {
                if (form != null)
                {
                    form.setValue(value);
                }
            }
        }

        public bool ShowCloseButton
        {
            set
            {
                if (form != null)
                {
                    form.setCursorState(false);
                    form.enableCloseButton(value);
                }
            }
        }
    }
}
