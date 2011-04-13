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
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;
using UBT.AI4.Bio.DivMobi.UMF.Layout;
using UBT.AI4.Bio.DivMobi.UMF.Layout.Attributes;


namespace UBT.AI4.Bio.DivMobi.UMF.Layout.Layouts
{
    public class BoxLayoutFactory : ILayoutFactory
    {

        public BoxLayoutFactory()
        {

        }

        private class LayoutableGroup : IComparable<LayoutableGroup>
        {
            int _start = int.MaxValue;
            int _size = int.MinValue;
            private List<AbstractLayoutable> _controls = new List<AbstractLayoutable>();
            private ICustomLayoutControl _customLayoutControl = null;

            public LayoutableGroup(BoxLayoutType type, AbstractLayoutable c)
            {
                Add(type, c);
            }

            public void Add(BoxLayoutType type, AbstractLayoutable c)
            {
                int uTmp = 0, uSizeTmp = 0, tmp = 0;
                type.UV(c.VirtualX, c.VirtualY, ref uTmp, ref tmp);
                type.UV(c.VirtualWidth, c.VirtualHeight, ref uSizeTmp, ref tmp);
                _start = Math.Min(_start, uTmp);
                _size = Math.Max(_size, (uTmp + uSizeTmp) - _start);
                _controls.Add(c);
            }

            public IEnumerable<AbstractLayoutable> Controls
            {
                get { return _controls; }
            }

            public int Start
            {
                get { return _start; }
            }

            public int Size
            {
                get { return _size; }
            }

            public bool Intersects(BoxLayoutType type, AbstractLayoutable c)
            {
                int uTmp = 0, uSizeTmp = 0, tmp = 0;
                type.UV(c.VirtualX, c.VirtualY, ref uTmp, ref tmp);
                type.UV(c.VirtualWidth, c.VirtualHeight, ref uSizeTmp, ref tmp);

                Rectangle r1 = new Rectangle(uTmp, 0, uSizeTmp, 1);
                Rectangle r2 = new Rectangle(_start, 0, _size, 1);

                return r1.IntersectsWith(r2);
            }

            public bool Intersects(LayoutableGroup c)
            {
                Rectangle r1 = new Rectangle(c._start, 0, c._size, 1);
                Rectangle r2 = new Rectangle(this._start, 0, this._size, 1);

                return r1.IntersectsWith(r2);
            }

            public void Merge(BoxLayoutType type, LayoutableGroup m2)
            {
                foreach (AbstractLayoutable c in m2._controls)
                {
                    if (!this._controls.Contains(c))
                    {
                        this.Add(type, c);
                    }
                }
            }

            public int CompareTo(LayoutableGroup other)
            {
                if (this._start < other._start) return -1;
                if (this._start > other._start) return 1;
                else return 0;
            }

            private bool _fixed = false;
            public bool Fixed
            {
                get { return _fixed; }
                set { _fixed = value; }
            }

            public ICustomLayoutControl CustomLayoutControl
            {
                get { return _customLayoutControl; }
                set { _customLayoutControl = value; }
            }
        }

        public ILayout CreateLayout(Control control)
        {
            TopLevelVirtualContainer tvc = new TopLevelVirtualContainer(null);

            if (control is Form)
            {
                ((Form)control).Activated += new EventHandler(tvc.Activated);
            }

            Control declaringControl = DeclaringControl(control, control);

            tvc.VirtualWidth = control.Width;
            tvc.VirtualHeight = control.Height;
            tvc.Layoutables = CreateControlAdapters(declaringControl, control).Cast<AbstractLayoutable>().ToList();

            List<LayoutableGroup> tmp = FindUserGroups(declaringControl);

            foreach (LayoutableGroup lg in tmp)
            {
                if (lg.CustomLayoutControl != null)
                {
                    tvc.CustomLayoutControls.Add(lg.CustomLayoutControl);
                }
            }

            Create(VBoxLayoutType.Instance, tvc, 1, tmp);
            tvc.ComputeBottomSpace();
            tvc.ComputeRightSpace();
            return tvc;
        }

        private void Create(BoxLayoutType boxType, VirtualContainer parent, int nestingDepth, List<LayoutableGroup> userConfiguredLayoutableGroups)
        {
            parent.LayoutManager = new BoxLayout(boxType, parent);

            if (parent.Layoutables.Count == 1) return;

            List<AbstractLayoutable> layoutables = parent.Layoutables.Cast<AbstractLayoutable>().ToList();
            List<LayoutableGroup> layoutableGroups = new List<LayoutableGroup>();

            BuildInterimLayoutablesGroups(layoutables, boxType, layoutableGroups);
            MergeIntersectingLayoutableGroups(boxType, layoutableGroups);
            layoutableGroups.Sort();
            ConfigureLayoutableGroups(userConfiguredLayoutableGroups, layoutableGroups);

            List<VirtualContainer> vc = new List<VirtualContainer>(layoutableGroups.Count);
            int uSizeTmp = 0, vSizeTmp = 0;
            boxType.UV(parent.VirtualWidth, parent.VirtualHeight, ref uSizeTmp, ref vSizeTmp);
            for (int i = 0; i < layoutableGroups.Count; i++)
            {
                if (i == 0)
                {
                    vc.Add(new VirtualContainer(parent));
                    vc[i].Grouped = layoutableGroups[i].Fixed;
                    vc[i].CustomLayoutControl = layoutableGroups[i].CustomLayoutControl;
                    vc[i].LayoutManager = new BoxLayout(boxType, vc[i]);

                    //U und V müssen nicht gesetzt werden, da sie beim
                    //ersten Element immer 0 sind;

                    boxType.SetVirtualUSize(vc[i], layoutableGroups[i].Start + layoutableGroups[i].Size);
                    boxType.SetVirtualVSize(vc[i], vSizeTmp);

                    foreach (AbstractLayoutable asl in layoutableGroups[i].Controls)
                    {
                        asl.VirtualX = asl.AbsoluteX - vc[i].AbsoluteX;
                        asl.VirtualY = asl.AbsoluteY - vc[i].AbsoluteY;
                        //code für die aktuelle Breite und Höhe
                        asl.Parent = vc[i];
                        vc[i].Add(asl);
                    }
                }
                if (i < layoutableGroups.Count - 1)
                {
                    int tmp = layoutableGroups[i + 1].Start - (layoutableGroups[i].Start + layoutableGroups[i].Size);
                    int endExtend;
                    int start;

                    endExtend = tmp / 2;
                    start = tmp / 2;
                    if (tmp % 2 != 0)
                    {
                        start++;
                    }

                    tmp = boxType.GetVirtualUSize(vc[i]) + endExtend;
                    boxType.SetVirtualUSize(vc[i], tmp);

                    vc.Add(new VirtualContainer(parent));
                    //"V" muss nicht gesetzt werden, da es immer 0 ist...
                    vc[i + 1].Grouped = layoutableGroups[i + 1].Fixed;
                    vc[i + 1].CustomLayoutControl = layoutableGroups[i + 1].CustomLayoutControl;
                    vc[i + 1].LayoutManager = new BoxLayout(boxType, vc[i + 1]);
                    boxType.SetVirtualU(vc[i + 1], layoutableGroups[i + 1].Start - start);
                    boxType.SetVirtualUSize(vc[i + 1], layoutableGroups[i + 1].Size + start);
                    boxType.SetVirtualVSize(vc[i + 1], vSizeTmp);

                    foreach (AbstractLayoutable asl in layoutableGroups[i + 1].Controls)
                    {
                        asl.VirtualX = asl.AbsoluteX - vc[i + 1].AbsoluteX;
                        asl.VirtualY = asl.AbsoluteY - vc[i + 1].AbsoluteY;
                        //code für die aktuelle Breite und Höhe
                        asl.Parent = vc[i + 1];
                        vc[i + 1].Add(asl);
                    }
                }
                if (i == layoutableGroups.Count - 1)
                {
                    int tmp = uSizeTmp - (layoutableGroups[i].Start + layoutableGroups[i].Size);
                    tmp = tmp + boxType.GetVirtualUSize(vc[i]);
                    boxType.SetVirtualUSize(vc[i], tmp);
                }
            }

            parent.Layoutables = vc.Cast<AbstractLayoutable>().ToList();

            foreach (VirtualContainer v in vc)
            {
                Create(boxType.SubBoxLayoutType(), v, nestingDepth + 1, userConfiguredLayoutableGroups);
            }

            return;
        }

        private static void ConfigureLayoutableGroups(List<LayoutableGroup> userDefindedGroups, List<LayoutableGroup> layoutableGroups)
        {
            List<LayoutableGroup> removes = new List<LayoutableGroup>();
            foreach (LayoutableGroup lg1 in userDefindedGroups)
            {
                foreach (LayoutableGroup lg2 in layoutableGroups)
                {
                    bool found = false;
                    if (lg1.Controls.Count() == lg2.Controls.Count())
                    {
                        foreach (AbstractLayoutable asl in lg2.Controls)
                        {
                            if (lg1.Controls.Contains(asl))
                            {
                                found = true;
                            }
                            else
                            {
                                found = false;
                                break;
                            }
                        }
                    }

                    if (found)
                    {
                        lg2.CustomLayoutControl = lg1.CustomLayoutControl;
                        lg2.Fixed = true;
                        removes.Add(lg1);
                    }
                }
            }

            foreach (LayoutableGroup lg in removes)
            {
                userDefindedGroups.Remove(lg);
            }
        }



        private static List<ControlAdapter> CreateControlAdapters(Control declaring, Control control)
        {
            List<ControlAdapter> res = new List<ControlAdapter>();
            foreach (Control c in control.Controls)
            {
                ControlAdapter ca = null;
                FieldInfo fi = ReflectiveControlSearch(declaring, c);

                if (c is Panel)
                {
                    Object[] o = fi.GetCustomAttributes(typeof(LayoutAttribute), false);

                    if (o.Count() > 0)
                    {
                        LayoutAttribute la = (LayoutAttribute)o[0];
                        ca = new PanelAdapter(null, la.LayoutManager, (Panel)c);
                    }
                    else
                    {
                        ca = new ControlAdapter(null, c);
                    }
                }
                else
                {
                    ca = new ControlAdapter(null, c);
                }

                Object[] hfa = fi.GetCustomAttributes(typeof(HorizontalFixedAttribute), false);
                if (hfa.Count() > 0)
                {
                    ca.HorizontalFix = true;
                }

                hfa = fi.GetCustomAttributes(typeof(VerticalFixedAttribute), false);
                if (hfa.Count() > 0)
                {
                    ca.VerticalFix = true;
                }

                
                ca.VirtualX = c.Location.X;
                ca.VirtualY = c.Location.Y;
                ca.VirtualWidth = c.Width;
                ca.VirtualHeight = c.Height;
                res.Add(ca);
            }

            return res;
        }

        private static void BuildInterimLayoutablesGroups(List<AbstractLayoutable> layoutables, BoxLayoutType boxType, List<LayoutableGroup> layoutableGroups)
        {
            foreach (AbstractLayoutable c in layoutables)
            {
                bool inserted = false;
                foreach (LayoutableGroup m in layoutableGroups)
                {
                    if (m.Intersects(boxType, c))
                    {
                        inserted = true;
                        m.Add(boxType, c);
                    }
                }

                if (!inserted)
                {
                    LayoutableGroup m = new LayoutableGroup(boxType, c);
                    layoutableGroups.Add(m);
                }
            }
        }

        private static void MergeIntersectingLayoutableGroups(BoxLayoutType boxType, List<LayoutableGroup> layoutableGroups)
        {
            List<LayoutableGroup> tmp1 = new List<LayoutableGroup>(layoutableGroups);
            List<LayoutableGroup> tmp2 = new List<LayoutableGroup>(layoutableGroups);
            foreach (LayoutableGroup l1 in tmp1)
            {
                foreach (LayoutableGroup l2 in tmp2)
                {
                    if (l1 != l2 && l1.Intersects(l2))
                    {
                        layoutableGroups.Remove(l1);
                        layoutableGroups.Remove(l2);
                        l1.Merge(boxType, l2);
                        layoutableGroups.Add(l1);
                    }
                }
            }
        }



        private static List<LayoutableGroup> FindUserGroups(Control declaringControl)
        {
            Dictionary<String, LayoutableGroup> dictionary = new Dictionary<String, LayoutableGroup>();

            FieldInfo[] fis = declaringControl.GetType().GetFields(
                BindingFlags.NonPublic |
                BindingFlags.Public |
                BindingFlags.Instance);

            foreach (FieldInfo fi in fis)
            {
                Object[] attr = fi.GetCustomAttributes(typeof(IFixedLayoutGroup), true);


                foreach (Object o in attr)
                {
                    Control c = (Control)fi.GetValue(declaringControl);
                    IFixedLayoutGroup lg = (IFixedLayoutGroup)o;

                    if (!dictionary.ContainsKey(lg.GroupId))
                    {
                        dictionary[lg.GroupId] = new LayoutableGroup(VBoxLayoutType.Instance, new ControlAdapter(null, c));
                    }
                    else
                    {
                        LayoutableGroup l = dictionary[lg.GroupId];

                        AbstractLayoutable asl = new ControlAdapter(null, c);
                        if (l.Controls.Contains(asl))
                        {
                            throw new Exception("MultipleDefinition");
                        }

                        dictionary[lg.GroupId].Add(VBoxLayoutType.Instance, asl);
                    }

                    if (lg.CustomLayoutControl != null)
                    {
                        ICustomLayoutControl clc = (ICustomLayoutControl)Activator.CreateInstance(lg.CustomLayoutControl);
                        clc.DeclaringControl = declaringControl;
                        dictionary[lg.GroupId].CustomLayoutControl = clc;
                    }
                }
            }

            List<LayoutableGroup> tmp = new List<LayoutableGroup>();

            foreach (LayoutableGroup lg in dictionary.Values)
            {
                tmp.Add(lg);
            }

            return tmp;
        }

        private static Control DeclaringControl(Control parent, Control control)
        {
            if (parent == null) return control;

            FieldInfo[] fis = parent.GetType().GetFields(
                BindingFlags.NonPublic |
                BindingFlags.Public |
                BindingFlags.Instance);



            foreach (FieldInfo f in fis)
            {
                if (f.GetValue(parent) == control)
                {
                    return parent;
                }
            }

            return DeclaringControl(parent.Parent, control);
        }

        private static FieldInfo ReflectiveControlSearch(Control declaringControl, Control c)
        {



            FieldInfo[] fis = declaringControl.GetType().GetFields(
                BindingFlags.NonPublic |
                BindingFlags.Public |
                BindingFlags.Instance);

            foreach (FieldInfo f in fis)
            {
                if (f.GetValue(declaringControl) == c)
                {
                    return f;
                }
            }

            return null;
        }
    }
}
