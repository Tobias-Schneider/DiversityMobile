﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes;

namespace UBT.AI4.Bio.DivMobi.ListSynchronization
{
    public class ConflictHandling
    {
        private Type ownerType;
        private ISerializableObject object1;
        private ISerializableObject object2;
        private IList<Conflict> conflicts;

        public ConflictHandling(ISerializableObject o1, ISerializableObject o2)
        {
            if (o1 == null || o2 == null)
                throw new Exception("Objects may not be null");

            if(o1.GetType()!=o2.GetType())
                throw new Exception("You can only compare objects of the same type.");
            this.ownerType = o1.GetType();
            this.object1 = o1;
            this.object2 = o2;
            this.conflicts = new List<Conflict>();
        }

        public bool compareObjects()
        {
            bool inConflict = false;
            FieldInfo[] fields = AttributeWorker.RetrieveAllFields(ownerType);
            foreach (FieldInfo fi in fields)
            {
                Attribute at=Attribute.GetCustomAttribute(fi, typeof(ColumnAttribute));
                if (at != null)
                {

                    Conflict cf = compareFields(fi, object1, object2);
                    if (cf != null)
                    {
                        inConflict = true;
                        this.conflicts.Add(cf);
                    }
                }
            }
            return inConflict;
        }

        public bool analyzeObjects()
        {
            if (object1.Rowguid != object2.Rowguid)
            {
                throw new Exception("Objects don´t correspond.");
                
            }
            bool inConflict = false;
            FieldInfo[] fields = AttributeWorker.RetrieveAllFields(ownerType);
            foreach (FieldInfo fi in fields)
            {
                Attribute at = Attribute.GetCustomAttribute(fi, typeof(ColumnAttribute));
                IDAttribute id =(IDAttribute) Attribute.GetCustomAttribute(fi, typeof(IDAttribute));
                if (at != null)
                {
                    if (id != null && id.Autoinc == true)
                    {
                    }
                    else
                    {
                        Conflict cf = compareFields(fi, object1, object2);
                        if (cf != null)
                        {
                            inConflict = true;
                            this.conflicts.Add(cf);
                        }
                    }
                }
            }
            return inConflict;
        }

        public Conflict compareFields(FieldInfo fi, ISerializableObject o1, ISerializableObject o2)
        {
            Conflict conflict=null;
            Object v1 = fi.GetValue(object1);
            Object v2 = fi.GetValue(object2);
            if (v1 == null)
            {
                if (v2 != null)
                {
                    conflict=new Conflict(ownerType, fi, v1, v2);
                }

            }
            else if (v2 == null)
            {
                conflict=new Conflict(ownerType, fi, v1, v2);
            }
            else if (!v1.Equals(v2))
            {
                conflict=new Conflict(ownerType, fi, v1, v2);
            }
            return conflict;
        }

        public void printConflicts()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Conflict conf in conflicts)
            {
                sb.Append(conf.ToString());
                sb.Append("\n");
            }
           MessageBox.Show(sb.ToString());
        }
    }
}
