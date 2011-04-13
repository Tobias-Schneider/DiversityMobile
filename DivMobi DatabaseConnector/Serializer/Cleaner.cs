using System;
using System.Runtime.InteropServices;

namespace UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer
{
    public abstract class Cleaner : IDisposable
    {
        public const String DEFAULT = "DEFAULT"; 

        private ICleanable _cleanable;

        public ICleanable Cleanable
        {
            get { return _cleanable; }
            set { _cleanable = value; }
        }

        abstract internal void Cleanup();
        

        protected void StartClean()
        {
            if (_cleanable == null)
            {
                throw new NullReferenceException("cleanable not set");
            }
            _cleanable.Cleanup();
        }

        public virtual void Dispose()
        {
            _cleanable = null;
        }

        internal static Cleaner CreateCleaner(String id)
        {
            return new DefaultCleaner();
        }
    }

    internal class DefaultCleaner : Cleaner
    {
        internal override void Cleanup()
        {
            base.StartClean();
        }
    }

    //internal class ThreadedCleaner : Cleaner
    //{
    //    private System.Threading.Thread _thread;


    //    internal sealed override void Cleanup()
    //    {
    //        if (_thread == null)
    //        {
    //            _thread = new System.Threading.Thread(new System.Threading.ThreadStart(CleanupThread));
    //            _thread.Start();
    //        }
    //    }

    //    private void CleanupThread()
    //    {
    //        while (true)
    //        {
    //            base.StartClean();
    //            System.Threading.Thread.Sleep(10000);
    //        }
    //    }

    //    public override void Dispose()
    //    {
    //        base.Dispose();
    //        _thread.Abort();
    //    }
    //}

    //internal class DOT_NET_CF_DiscSpaceThreadCleaner : Cleaner
    //{
    //    private System.Threading.Thread _thread;


    //    internal sealed override void Cleanup() 
    //    {
    //        if (_thread == null)
    //        {
    //            _thread = new System.Threading.Thread(new System.Threading.ThreadStart(CleanupThread));
    //            _thread.Start();
    //        }
    //    }

    //    [DllImport("coredll", SetLastError = true)]
    //    private static extern bool GetDiskFreeSpaceEx(string directoryName, ref long longfreeBytesAvailable, ref long totalBytes, ref long totalFreeBytes);

    //    private void CleanupThread()
    //    {
    //        while (true)
    //        {
    //            long availFree = 0, total = 0, totalFree = 0;
    //            if (!GetDiskFreeSpaceEx(@"\Windows", ref availFree, ref total, ref totalFree))
    //            {
    //                base.StartClean();
    //            }

    //            if (availFree < 1024 * 1024 * 8)
    //            {
    //                base.StartClean();
    //            }
    //            System.Threading.Thread.Sleep(10000);
    //        }
    //    }

    //    public override void Dispose()
    //    {
    //        base.Dispose();
    //        _thread.Abort();
    //    }
    //}
}
