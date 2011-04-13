using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Restrictions;
using UBT.AI4.Bio.DivMobi.ListSynchronization;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;
using Microsoft.Win32;
using UBT.AI4.Bio.DivMobi.DataItemFormTools;

namespace TestConflicts
{
    /// <summary>
    /// Interaktionslogik für Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private Serializer mobSer;
        private Serializer repSer;
        ISerializableObject obj1;
        ISerializableObject obj2;
        IList<Type> divMobiTypes = new List<Type>(); //verwaltet die aus DiversityCollection verwendeten Typen

        public Window1()
        {
            InitializeComponent();
            divMobiTypes.Add(typeof(Property));
            divMobiTypes.Add(typeof(Analysis));
            divMobiTypes.Add(typeof(AnalysisTaxonomicGroup));
            divMobiTypes.Add(typeof(CollectionAgent));
            divMobiTypes.Add(typeof(CollectionEvent));
            divMobiTypes.Add(typeof(CollectionEventImage));
            divMobiTypes.Add(typeof(CollectionEventLocalisation));
            divMobiTypes.Add(typeof(CollectionEventProperty));
            divMobiTypes.Add(typeof(CollectionSpecimen));
            divMobiTypes.Add(typeof(CollectionSpecimenImage));
            divMobiTypes.Add(typeof(CollEventImageType_Enum));
            divMobiTypes.Add(typeof(CollSpecimenImageType_Enum));
            divMobiTypes.Add(typeof(CollTaxonomicGroup_Enum));
            divMobiTypes.Add(typeof(Identification));
            divMobiTypes.Add(typeof(IdentificationUnit));
            divMobiTypes.Add(typeof(IdentificationUnitAnalysis));
            divMobiTypes.Add(typeof(LocalisationSystem));
            divMobiTypes.Add(typeof(CollCircumstances_Enum));
            divMobiTypes.Add(typeof(CollUnitRelationType_Enum));
            divMobiTypes.Add(typeof(CollectionEventSeries));
            //Bis hier: Korrepondiert zu DBVersion 20
            //divMobiTypes.Add(typeof(CollectionEventSeriesImage));
            //divMobiTypes.Add(typeof(CollEventSeriesImageType_Enum));
            ////Bis hier: Korrepondiert zu DBVersion 22
            divMobiTypes.Add(typeof(CollIdentificationCategory_Enum));
            //divMobiTypes.Add(typeof(CollTypeStatus_Enum));
            //divMobiTypes.Add(typeof(CollIdentificationQualifier_Enum));
            ////Bis hier: Korrepondiert zu DBVersion 25
            //divMobiTypes.Add(typeof(CollLabelTranscriptionState_Enum));
            //divMobiTypes.Add(typeof(CollLabelType_Enum));
            ////Bis hier: Korrepondiert zu DBVersion 27
            //divMobiTypes.Add(typeof(Collection));
            //divMobiTypes.Add(typeof(CollectionProject));
            //divMobiTypes.Add(typeof(CollectionSpecimenPart));
            //divMobiTypes.Add(typeof(CollMaterialCategory_Enum));
            //Bis hier: Korrepondiert zu DBVersion 31
            divMobiTypes.Add(typeof(IdentificationUnitGeoAnalysis));
            divMobiTypes.Add(typeof(AnalysisResult));

            divMobiTypes.Add(typeof(UserTaxonomicGroupTable));
            //Bis hier: Korrepondiert zu DBVersion 34
        }

        private void buttonConnect_Click(object sender, RoutedEventArgs e)
        {
            /*
            OpenFileDialog ofd=new OpenFileDialog();
            ofd.Filter = "Mobile Datenbanken |*.sdf";
            if (ofd.ShowDialog() == true)
            {
                mobSer = new MS_SqlCeSerializer(ofd.FileName);
            }
            else return;
            */
            repSer = new MS_SqlServerLocalSerializer("127.0.0.1", "5432", "DiversityCollection_Base");
            repSer.RegisterTypes(divMobiTypes);
            repSer.Activate();
            mobSer.RegisterTypes(divMobiTypes);
            mobSer.Activate();
        }

        private void buttonLoad_Click(object sender, RoutedEventArgs e)
        {
            IRestriction r = RestrictionFactory.TypeRestriction(typeof(CollectionEventLocalisation));
            IList<CollectionEventLocalisation>spec=mobSer.Connector.LoadList<CollectionEventLocalisation>(r);
            CollectionEventLocalisation ce = spec.First();
            Guid g = ce.Rowguid;
            r = RestrictionFactory.Eq(typeof(CollectionEventLocalisation), "_guid", g);
            obj2 = repSer.Connector.Load<CollectionEventLocalisation>(r);
            
        }


        private void buttonTest_Click(object sender, RoutedEventArgs e)
        {
            ConflictHandling ch=new ConflictHandling(obj1,obj2);
            ch.analyzeObjects();
            ch.printConflicts();
        }

        private void buttonCoord_Click(object sender, RoutedEventArgs e)
        {
            if (this.textBoxGrad.Text != null)
            {
                try
                {

                    double d = Double.Parse(textBoxGrad.Text);
                    this.labelShowGrad.Content = TreeViewOperations.dec2degree(d);
                }
                catch(Exception ex)
                {
                    this.labelShowGrad.Content = ex.Message;
                }
            }
        }

       
      
    }
}
