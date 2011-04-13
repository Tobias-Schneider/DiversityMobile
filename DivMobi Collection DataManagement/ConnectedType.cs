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

using UBT.AI4.Bio.DivMobi.DataLayer;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using System.Windows.Forms;
using System.IO;

namespace UBT.AI4.Bio.DivMobi.DataManagement
{
    public abstract class ConnectedType
    {
        protected static string _progPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
        private static string _dbPATH = String.Concat(_progPath, @"\MobileDB.sdf");
        protected static Serializer SERIALIZER;

        // Read DataItems from TaxonName Database
        protected static string taxonomydbPATH = String.Concat(_progPath, @"\TaxonNames.sdf");
        protected static Serializer TaxonomySERIALIZER;

        static ConnectedType()
        {
            try
            {
                SERIALIZER = new MS_SqlCeSerializer(_dbPATH);
                TaxonomySERIALIZER = new MS_SqlCeSerializer(taxonomydbPATH);
                registerTypes();
            }
            catch (Exception ex)
            {
                throw new ConnectionCorruptedException(ex.Message);
            }
        }

        //static ConnectedType(Serializer ser, Serializer taxSer)
        //{
        //    SERIALIZER = ser;
        //    TaxonomySERIALIZER = taxSer;
        //    registerTypes();
        //}

        private static void registerTypes()
        {
            try
            {
                SERIALIZER.RegisterType(typeof(Analysis));
                SERIALIZER.RegisterType(typeof(AnalysisTaxonomicGroup));
                SERIALIZER.RegisterType(typeof(CollectionAgent));
                SERIALIZER.RegisterType(typeof(CollectionEvent));
                SERIALIZER.RegisterType(typeof(CollectionEventImage));
                SERIALIZER.RegisterType(typeof(CollEventImageType_Enum));
                SERIALIZER.RegisterType(typeof(CollectionEventLocalisation));
                SERIALIZER.RegisterType(typeof(CollectionSpecimen));
                SERIALIZER.RegisterType(typeof(CollectionSpecimenImage));
                SERIALIZER.RegisterType(typeof(CollSpecimenImageType_Enum));
                SERIALIZER.RegisterType(typeof(CollTaxonomicGroup_Enum));
                SERIALIZER.RegisterType(typeof(Identification));
                SERIALIZER.RegisterType(typeof(IdentificationUnit));
                SERIALIZER.RegisterType(typeof(IdentificationUnitAnalysis));
                SERIALIZER.RegisterType(typeof(LocalisationSystem));
                SERIALIZER.RegisterType(typeof(CollectionEventProperty));
                SERIALIZER.RegisterType(typeof(Property));
                SERIALIZER.RegisterType(typeof(CollCircumstances_Enum));
                SERIALIZER.RegisterType(typeof(CollUnitRelationType_Enum));
                SERIALIZER.RegisterType(typeof(CollectionEventSeries));
                SERIALIZER.RegisterType(typeof(UserProfile));

                //Bis hier: Korrepondiert zu DBVersion 20
                //SERIALIZER.RegisterType(typeof(CollectionEventSeriesImage));
                //SERIALIZER.RegisterType(typeof(CollEventSeriesImageType_Enum));
                //Bis hier: Korrepondiert zu DBVersion 22
                SERIALIZER.RegisterType(typeof(CollIdentificationCategory_Enum));
                //SERIALIZER.RegisterType(typeof(CollTypeStatus_Enum));
                SERIALIZER.RegisterType(typeof(CollIdentificationQualifier_Enum));
                //Bis hier: Korrepondiert zu DBVersion 25
                //SERIALIZER.RegisterType(typeof(CollLabelTranscriptionState_Enum));
                //SERIALIZER.RegisterType(typeof(CollLabelType_Enum));
                //Bis hier: Korrepondiert zu DBVersion 27
                //SERIALIZER.RegisterType(typeof(Collection));
                SERIALIZER.RegisterType(typeof(CollectionProject));
                //SERIALIZER.RegisterType(typeof(CollectionSpecimenPart));
                //SERIALIZER.RegisterType(typeof(CollMaterialCategory_Enum));
                //Bis hier: Korrepondiert zu DBVersion 31

                //SERIALIZER.RegisterType(typeof(AnalysisDisplayOrder));
                SERIALIZER.RegisterType(typeof(IdentificationUnitGeoAnalysis));
                SERIALIZER.RegisterType(typeof(AnalysisResult));
                SERIALIZER.RegisterType(typeof(UserTaxonomicGroupTable));
                //Bis hier: Korrepondiert zu DBVersion 34

                SERIALIZER.Activate();


                TaxonomySERIALIZER.RegisterType(typeof(TaxonNames));
                TaxonomySERIALIZER.RegisterType(typeof(PropertyNames));
                TaxonomySERIALIZER.Activate();
            }
            catch (Exception)
            {
                throw new ConnectionCorruptedException("Error while registering Types.");
            }
        }
    }
}
