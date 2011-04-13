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
using System.Reflection;
using UBT.AI4.Bio.DivMobi.UMF.Initializer.Attributes;

namespace UBT.AI4.Bio.DivMobi.UMF.Initializer
{
    public abstract class InitializationDirection
    {
        private static InitializationDirection _CONNECTING_TO_CONNECTED = new ConnectingToConnected();
        private static InitializationDirection _CONNECTED_TO_CONNECTING = new ConnectedToConnecting();

        public static InitializationDirection CONNECTING_TO_CONNECTED
        {
            get { return _CONNECTING_TO_CONNECTED; }
        }

        public static InitializationDirection CONNECTED_TO_CONNECTING
        {
            get { return _CONNECTED_TO_CONNECTING; }
        }

        internal abstract void Connect(Object a, Object b, FieldInfo fi, PropertyInfo pi);

        private class ConnectingToConnected : InitializationDirection
        {
            internal override void Connect(object a, object b, FieldInfo fi, PropertyInfo pi)
            {
                Object val = fi.GetValue(a);
                pi.SetValue(b, val, null);
            }
        }

        private class ConnectedToConnecting : InitializationDirection
        {
            internal override void Connect(object a, object b, FieldInfo fi, PropertyInfo pi)
            {
                Object val = pi.GetValue(b, null);
                fi.SetValue(a, val);
            }
        }
    }
    public class Initializer
    {

        public void Initialize(Object connectingObj, Object connectedObj, InitializationDirection d)
        {
            Dictionary<FieldInfo, PropertyInfo> mapping = GetConnectedFields(connectingObj, connectingObj);

            foreach (KeyValuePair<FieldInfo, PropertyInfo> p in mapping)
            {
                d.Connect(connectingObj, connectedObj, p.Key, p.Value);
            }
        }

        private Dictionary<FieldInfo, PropertyInfo> GetConnectedFields(Object from, Object to)
        {
            Dictionary<FieldInfo, PropertyInfo> ret = new Dictionary<FieldInfo, PropertyInfo>();

            Type t = from.GetType();

            FieldInfo[] fis = t.GetFields(
                BindingFlags.NonPublic |
                BindingFlags.Public |
                BindingFlags.Instance);

            foreach (FieldInfo fi in fis)
            {
                Object[] attrs = fi.GetCustomAttributes(typeof(FieldToPropertyConnectionAttribute), false);

                foreach (Object attr in attrs)
                {
                    FieldToPropertyConnectionAttribute ca = (FieldToPropertyConnectionAttribute)attr;

                    if (ca.ConnectedType == to.GetType())
                    {
                        PropertyInfo pi = to.GetType().GetProperty(ca.ConnectedProperty);

                        if (pi == null)
                        {
                            throw new Exception();
                        }

                        ret[fi] = pi;
                    }
                }
            }

            return ret;
        }
    }
}
