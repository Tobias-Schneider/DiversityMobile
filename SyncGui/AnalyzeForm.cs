using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.SyncTool.SyncTool;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;

namespace UBT.AI4.Bio.DivMobi.SyncGui
{
    public partial class AnalyzeForm : Form
    {
        ISerializableObject sourceObject;
        ISerializableObject sinkObject;
        ISerializableObject editObject;
        Type objectType;
        List<MemberTypes> properties;

        public ISerializableObject solution;


        public AnalyzeForm(SyncContainer cont)
        {
            InitializeComponent();
            sourceObject = cont.SourceObject;
            sinkObject = cont.SinkObject;

            if(sourceObject.GetType() != sinkObject.GetType())
            {
                MessageBox.Show("Incompatible Types!");
                this.Close();
            }
          
            propertyGridRepository.SelectedObject = sourceObject;
            propertyGridMobileDB.SelectedObject = sinkObject;
            objectType =sourceObject.GetType();
            readType(objectType);
            editObject= (ISerializableObject) objectType.GetConstructor(new Type[] {}).Invoke(new object[] {});//Konstruktor für unbekanntes Objektt
            propertyGridEdit.SelectedObject = editObject;

        }

        internal void readType(Type typ)
        {
            MessageBox.Show(typ.Name);
            MemberInfo[] members = typ.GetMembers(BindingFlags.Instance | BindingFlags.Public);
            StringBuilder sb = new StringBuilder();
            sb.Append("Start: ");
            foreach (MemberInfo mi in members)
            {
                MemberTypes mt = mi.MemberType;
                sb.Append(mi.Name + " " + mt);
                sb.Append("\n");
            }
            MessageBox.Show(sb.ToString());

        }

        private void buttonResolve_Click(object sender, EventArgs e)
        {
            solution = editObject;
            this.Close();
        }

    }
}
