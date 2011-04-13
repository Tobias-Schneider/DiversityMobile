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
