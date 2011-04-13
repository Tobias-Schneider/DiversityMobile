using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UBT.AI4.Bio.DivMobi.UMF.Layout.Layouts
{
    public abstract class BoxLayoutType
    {
        public abstract void UV(int x, int y, ref int u, ref int v);

        public abstract int GetVirtualU(AbstractLayoutable vc);
        public abstract void SetVirtualU(AbstractLayoutable vc, int u);

        public abstract int GetVirtualUSize(AbstractLayoutable vc);
        public abstract void SetVirtualUSize(AbstractLayoutable vc, int u);

        public abstract void SetVirtualVSize(AbstractLayoutable vc, int v);
        public abstract BoxLayoutType SubBoxLayoutType();
    }

    public class VBoxLayoutType : BoxLayoutType
    {
        private static VBoxLayoutType _instance = new VBoxLayoutType();

        private VBoxLayoutType()
        {

        }

        public static VBoxLayoutType Instance
        {
            get { return _instance; }
        }


        public override void UV(int x, int y, ref int u, ref int v)
        {
            u = y;
            v = x;
        }

        public override int GetVirtualU(AbstractLayoutable vc)
        {
            return vc.VirtualY;
        }

        public override void SetVirtualU(AbstractLayoutable vc, int u)
        {
            vc.VirtualY = u;
        }

        public override void SetVirtualUSize(AbstractLayoutable vc, int u)
        {
            vc.VirtualHeight = u;
        }

        public override int GetVirtualUSize(AbstractLayoutable vc)
        {
            return vc.VirtualHeight;
        }

        public override void SetVirtualVSize(AbstractLayoutable vc, int v)
        {
            vc.VirtualWidth = v;
        }

        public override BoxLayoutType SubBoxLayoutType()
        {
            return HBoxLayoutType.Instance;
        }

    }

    public class HBoxLayoutType : BoxLayoutType
    {
        private static HBoxLayoutType _instance = new HBoxLayoutType();

        private HBoxLayoutType()
        {

        }

        public static HBoxLayoutType Instance
        {
            get { return _instance; }
        }

        public override void UV(int x, int y, ref int u, ref int v)
        {
            u = x;
            v = y;
        }

        public override int GetVirtualU(AbstractLayoutable vc)
        {
            return vc.VirtualX;
        }

        public override void SetVirtualU(AbstractLayoutable vc, int u)
        {
            vc.VirtualX = u;
        }

        public override void SetVirtualUSize(AbstractLayoutable vc, int u)
        {
            vc.VirtualWidth = u;
        }

        public override int GetVirtualUSize(AbstractLayoutable vc)
        {
            return vc.VirtualWidth;
        }

        public override void SetVirtualVSize(AbstractLayoutable vc, int v)
        {
            vc.VirtualHeight = v;
        }

        public override BoxLayoutType SubBoxLayoutType()
        {
            return VBoxLayoutType.Instance;
        }

    }
}
