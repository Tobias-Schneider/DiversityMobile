using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Microsoft.Office.Interop.Excel;
using Microsoft.Office;

using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Restrictions;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;

namespace xcel2sdf

    //Erzeugt aus einer ExcelTabelle iene TaxonDatenbank im sdf-Format. Leerzeilen brechen ab.

{
    public partial class Conversion : Form
    {
        String excelPath;
        String sdfPath;

        public Conversion()
        {
            InitializeComponent();
        }

        public String getPath(String filter) //Gibt den Pfad zur Datenbank über einen Dialog zurück
        {
            OpenFileDialog dbConnectionDialog = new OpenFileDialog();
            String path;
            dbConnectionDialog.Filter = filter;
            if (dbConnectionDialog.ShowDialog() == DialogResult.OK)
            {
                path = dbConnectionDialog.FileName;

            }
            else path = "Error";
            return path;
        }

        private void buttonSelectExcel_Click(object sender, EventArgs e)
        {
            String filter = "Excel files (*.xlsx)|*.xlsx|Excel files 2003 (*.xls)|*.xls";
            excelPath=getPath(filter);
            labelExcelPath.Text = excelPath;
            if((excelPath!=null) && (sdfPath!=null))
                buttonExecute.Enabled=true;
        }

        private void buttonSelectSdfPath_Click(object sender, EventArgs e)
        {
            String filter = "sdf files (*.sdf)|*.sdf";
            sdfPath=getPath(filter);
            labelSdfPath.Text = sdfPath;
            if ((excelPath != null) && (sdfPath != null))
                buttonExecute.Enabled = true;
        }

        private void buttonExecute_Click(object sender, EventArgs e)
        {
            if (comboBoxTaxonomicGroup.SelectedItem.Equals("Plants") || comboBoxTaxonomicGroup.SelectedItem.Equals("Fungi"))
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.ApplicationClass();
                Workbook wb = excel.Workbooks.Open(excelPath, ExcelKonstanten.UpdateLinks.DontUpdate, ExcelKonstanten.ReadOnly, ExcelKonstanten.Format.Nothing,
                "", // Passwort
                "", // WriteResPasswort
                ExcelKonstanten.IgnoreReadOnlyRecommended,
                XlPlatform.xlWindows,
                "", // Trennzeichen
                ExcelKonstanten.Editable,
                ExcelKonstanten.DontNotifiy,
                ExcelKonstanten.Converter.Default,
                ExcelKonstanten.DontAddToMru,
                ExcelKonstanten.Local,
                ExcelKonstanten.CorruptLoad.NormalLoad);
                Sheets sheets = wb.Worksheets;
                Worksheet ws = (Worksheet)sheets.get_Item(1);
                int rowStart = 2; // der ersten Zeile stehen die Überschriften
                int rowMax = ws.UsedRange.Rows.Count;
                MessageBox.Show(rowMax.ToString());

                Serializer ser = new MS_SqlCeSerializer(sdfPath);
                if (comboBoxTaxonomicGroup.SelectedItem.Equals("Plants"))
                    ser.RegisterType(typeof(TaxonNames));
                if (comboBoxTaxonomicGroup.SelectedItem.Equals("Fungi"))
                    ser.RegisterType(typeof(TaxonNames));
                ser.Activate();
                buttonExecute.Text = "Executing";
                for (int i = rowStart; i <= rowMax; i++)
                {
                    Microsoft.Office.Interop.Excel.Range uriCell = (Microsoft.Office.Interop.Excel.Range)ws.Cells.get_Item(i, 1);
                    String uri = uriCell.Value2.ToString();
                    Microsoft.Office.Interop.Excel.Range nameCell = (Microsoft.Office.Interop.Excel.Range)ws.Cells.get_Item(i, 2);
                    String name = nameCell.Value2.ToString();
                    if (comboBoxTaxonomicGroup.SelectedItem.Equals("Plants"))
                    {
                        TaxonNames taxon = ser.CreateISerializableObject<TaxonNames>();
                        taxon.NameURI = uri;
                        taxon.TaxonNameCache = name;
                        ser.Connector.BeginTransaction();
                        ser.Connector.InsertPlain(taxon);
                    }
                    if (comboBoxTaxonomicGroup.SelectedItem.Equals("Fungi"))
                    {
                        TaxonNames taxon = ser.CreateISerializableObject<TaxonNames>();
                        taxon.NameURI = uri;
                        taxon.TaxonNameCache = name;
                        ser.Connector.BeginTransaction();
                        ser.Connector.InsertPlain(taxon);
                    }
                    ser.Connector.Commit();

                }
                buttonExecute.Text = "Finished";
            }
            else
            {
                MessageBox.Show("Bitte unterstützte taxonomische Gruppe einstellen!");
                return;
            }
        }

    }
}
