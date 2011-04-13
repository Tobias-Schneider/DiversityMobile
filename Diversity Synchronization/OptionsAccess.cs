using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml;
using Diversity_Synchronization.Options.Serializer;
using Diversity_Synchronization.Options.Language;
using Diversity_Synchronization.Options;
using Diversity_Synchronization.Icons;
using System.IO;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using log4net;
using log4net.Appender;

namespace Diversity_Synchronization
{
    /// <summary>
    /// Ermöglicht den global verwalteten Zugriff auf die Optionen und die Sprache
    /// </summary>
    public static class OptionsAccess 
    {
        private const string APP_FOLDER = "Diversity Mobile";
        private const string TRANSACTION_FOLDER = "Transactions";
        private const string SETTINGS_FOLDER = "Settings";
        private const string LANGUAGE_FOLDER = "Languages";
        private const string DATABASE_FOLDER = "Database";
        private const string PICTURES_FOLDER = "Pictures";
        private const string MAPS_FOLDER = "Maps";
        private const string ICONS_FOLDER = "Icons";

        private const int MAX_TRANSACTIONS_SAVED = 100;

        private static string currentTransaction;

        private static IOptionsSerializer<ProgramOptions> poSerializer = new OptionsXMLSerializer<ProgramOptions>("defaultProgramOptions.xml", "userProgramOptions.xml");
        private static IOptionsSerializer<RepositoryOptions> roSerializer = new OptionsXMLSerializer<RepositoryOptions>("defaultRepositoryOptions.xml", "userRepositoryOptions.xml");
        private static IOptionsSerializer<ActiveSyncOptions> asoSerializer = new OptionsXMLSerializer<ActiveSyncOptions>("defaultActiveSyncOptions.xml", "userActiveSyncOptions.xml");
        private static IOptionsSerializer<SynchronizationOptions> syncSerializer = new OptionsXMLSerializer<SynchronizationOptions>("defaultSynchronizationOptions.xml", "userSynchronizationOptions.xml");
        private static IOptionsSerializer<SearchOptions> searchSerializer = new OptionsXMLSerializer<SearchOptions>("defaultSearchOptions.xml", "userSearchOptions.xml");
        private static IOptionsSerializer<MapSaveOptions> mapSaveSerializer = new OptionsXMLSerializer<MapSaveOptions>("defaultMapSaveOptions.xml", "userMapSaveOptions.xml");
        private static IOptionsSerializer<MobileDeviceOptions> mobileSerializer = new OptionsXMLSerializer<MobileDeviceOptions>("defaultMobileDeviceOptions.xml", "userMobileDeviceOptions.xml");

        private static ImageList iconImages = new System.Windows.Forms.ImageList();
        public static ImageList TreeviewIcons
        {
            get
            {
                return iconImages;
            }
        }

        private static void fillImageList()
        {
            // Configure the ImageList.
            iconImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            iconImages.ImageSize = new System.Drawing.Size(16, 16);

            // Get all the icon files in the current directory.
            var iconFolder = getFolderPath(ApplicationFolder.Icons);

            Icon[] icons = fillIcons();

            // Create an Image object for each file and add it to the ImageList.
            // You can also use an Image subclass (like Icon).
            foreach (var icon in icons)
            {                          
                iconImages.Images.Add(icon);
            }
        }

        private static Icon[] fillIcons()
        {
            Icon[] iconFiles = new Icon[]
            {
                IconsResource.BarcodeGrey, //Kein flip horizontal
                IconsResource.Event,
                IconsResource.EventGrey, 
                IconsResource.BarcodeGrey, //.EventProperty, fehlt!
                IconsResource.BarcodeGrey, // EventPropertyGrey, fehlt!
                IconsResource.EventSeries, 
                IconsResource.EventSeriesGrey, 
                IconsResource.Localisation, 
                IconsResource.LocalisationGrey, 
                IconsResource.Agent, 
                IconsResource.AgentGrey, 
                IconsResource.BarcodeGrey,      //Keine schwarze CollSpec ico
                IconsResource.BarcodeGrey,
                IconsResource.Tree, 
                IconsResource.TreeGrey, 
                IconsResource.Branch, 
                IconsResource.Branch, 
                IconsResource.Plant, 
                IconsResource.PlantGrey, 
                IconsResource.Other, 
                IconsResource.OtherGrey, 
                
                IconsResource.Analysis, 
                IconsResource.AnalysisGrey, 
                IconsResource.Photography, 
                IconsResource.PhotographyGrey, 
                //IconsResource.Photography, 
                //IconsResource.PhotographyGrey, 
                IconsResource.Alga, 
                IconsResource.Assel, 
                IconsResource.Bacterium, 
                IconsResource.Bird, 
                IconsResource.Bryophyt, 
                IconsResource.Fish, 
                IconsResource.Fungus,                
                IconsResource.Insect, 
                IconsResource.Lichen, 
                IconsResource.Mammal, 
                IconsResource.Mollusc, 
                IconsResource.Myxomycete, 
                IconsResource.Virus, 
                IconsResource.Barcode,
                IconsResource.Localisation,
                IconsResource.LocalisationGrey,
                IconsResource.GPS,
                IconsResource.GPSGrey,
                IconsResource.Barcode, // Home fehlt
                IconsResource.Localisation,
                IconsResource.Localisation,
                IconsResource.Localisation,
                IconsResource.Localisation,
            };
            return iconFiles;
        }

        static log4net.Appender.FileAppender _transactionAppender;
        static log4net.ILog _Log;
        public static ILog GlobalLog { get; private set; }
        
        static OptionsAccess()
        {        
            log4net.Config.XmlConfigurator.Configure(new FileInfo(getFolderPath(ApplicationFolder.Application)+"\\log4net.config"));

            _transactionAppender = getFileAppender("TransactionLogFileAppender");

            var globalAppender = getFileAppender("GlobalLogFileAppender");
            if (globalAppender != null)
            {
                globalAppender.File = getFolderPath(ApplicationFolder.ApplicationData) + @"\log.txt";
                globalAppender.ActivateOptions();
            }

            GlobalLog = LogManager.GetLogger("GlobalLog");
            _Log = log4net.LogManager.GetLogger(typeof(OptionsAccess));

            GlobalLog.Info("Logging initialized");

            

            createDirectories();

            fillImageList();

            startTransaction();
        }

        private static FileAppender getFileAppender(string name)
        {
            var appenders = (from appender in log4net.LogManager.GetRepository().GetAppenders()
                             where appender.Name == name && appender is log4net.Appender.FileAppender
                             select appender as log4net.Appender.FileAppender);
            return (appenders.Count() > 0) ? appenders.First() : null;
        }

        public static void startTransaction()
        {
            //Example: 2010-10-08 2102
            currentTransaction = DateTime.Now.ToString("yyyy-MM-dd HHmm");
            Directory.CreateDirectory(getFolderPath(ApplicationFolder.CurrentTransaction));


            if (_transactionAppender != null)
            {
                _transactionAppender.File = getFolderPath(ApplicationFolder.CurrentTransaction) + @"\log.txt";
                _transactionAppender.ActivateOptions();
            }
            _Log.Info("Transaction started!");

            cleanTransactions();
            
                                          
        }

        private static void cleanTransactions()
        {
            var transactions = Directory.GetDirectories(getFolderPath(ApplicationFolder.Transactions));
            int transactionOverhang = transactions.Length - MAX_TRANSACTIONS_SAVED;
            if (transactionOverhang > 0)
            {
                var superfluousTransactions = (from dir in transactions
                                               orderby dir ascending
                                               select dir).Take(transactionOverhang);
                foreach (var superfluousTransaction in superfluousTransactions)
                {
                    try
                    {
                        Directory.Delete(superfluousTransaction, true);
                    }
                    catch (Exception ex)
                    {
                        _Log.ErrorFormat("Could not remove superfluous Transaction [{0}] : [{1}]", superfluousTransaction, ex);
                    }
                }
            }
        }

        private static void createDirectories()
        {
            string appData = getFolderPath(ApplicationFolder.ApplicationData);
            if (!Directory.Exists(appData))
                Directory.CreateDirectory(appData);

            string settings = getFolderPath(ApplicationFolder.Settings);
            if (!Directory.Exists(settings))
                Directory.CreateDirectory(settings);

            string pics = getFolderPath(ApplicationFolder.Pictures);
            if(!Directory.Exists(pics))
                Directory.CreateDirectory(pics);

            string maps = getFolderPath(ApplicationFolder.Maps);
            if (!Directory.Exists(maps))
                Directory.CreateDirectory(maps);
        }



        #region Directories      
       
        
        /// <summary>
        /// </summary>
        /// <param name="f"></param>
        /// <returns>The requested Application Path WITHOUT a slash at the end</returns>
        public static string getFolderPath(ApplicationFolder f)
        {
            switch (f)
            {
                case ApplicationFolder.ApplicationData:
                    return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + APP_FOLDER;
                    break;
                case ApplicationFolder.Settings:
                    return String.Format("{0}\\{1}", getFolderPath(ApplicationFolder.ApplicationData), SETTINGS_FOLDER);
                    break;
                case ApplicationFolder.Application:
                    return AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\'); ;
                    break;
                case ApplicationFolder.Language:
                    return String.Format("{0}\\{1}", getFolderPath(ApplicationFolder.Application), LANGUAGE_FOLDER);
                    break;
                case ApplicationFolder.Transactions:
                    return String.Format("{0}\\{1}", getFolderPath(ApplicationFolder.ApplicationData), TRANSACTION_FOLDER);
                    break;
                case ApplicationFolder.CurrentTransaction:
                    return String.Format("{0}\\{1}", getFolderPath(ApplicationFolder.Transactions), currentTransaction);
                    break;
                case ApplicationFolder.Pictures:
                    return String.Format("{0}\\{1}", getFolderPath(ApplicationFolder.Settings), PICTURES_FOLDER);
                    break;
                case ApplicationFolder.EmptyDatabases:
                    return String.Format("{0}\\{1}", getFolderPath(ApplicationFolder.Application), DATABASE_FOLDER);
                    break;
                case ApplicationFolder.Maps:
                    return String.Format("{0}\\{1}",getFolderPath(ApplicationFolder.Settings), MAPS_FOLDER);
                    break;
                case ApplicationFolder.Icons:
                    return String.Format("{0}\\{1}", getFolderPath(ApplicationFolder.Application), ICONS_FOLDER);
                    break;
                default:
                    throw new ArgumentException("Unknown FolderType!");
                    break;
            }
        }          
        #endregion

        public static ILanguage Language
        {
            get
            {                
                string langPath = String.Format("{0}\\{1}", getFolderPath(ApplicationFolder.Language), ProgramOptions.LanguageFile);
                return new LanguageXML(langPath);                
            }                
        }

        public static ProgramOptions ProgramOptions
        {
            get
            {
                return poSerializer.deserialize();
            }
            set
            {
                poSerializer.serialize(value);
            }
        }

        public static RepositoryOptions RepositoryOptions
        {
            get
            {
                return roSerializer.deserialize();
            }
            set
            {
                roSerializer.serialize(value);
            }
        }

        public static ActiveSyncOptions ActiveSyncOptions
        {
            get
            {
                return asoSerializer.deserialize();
            }
            set
            {
                asoSerializer.serialize(value);
            }
        }

        public static SynchronizationOptions SynchronizationOptions
        {
            get
            {
                return syncSerializer.deserialize();
            }
            set
            {
                syncSerializer.serialize(value);
            }
        }

        public static SearchOptions SearchOptions
        {
            get
            {
                return searchSerializer.deserialize();
            }
            set
            {
                searchSerializer.serialize(value);
            }
        }

        public static MapSaveOptions MapSaveOptions
        {
            get
            {
                return mapSaveSerializer.deserialize();
            }
            set
            {
                mapSaveSerializer.serialize(value);
            }
        }

        public static MobileDeviceOptions MobileDeviceOptions
        {
            get
            {
                return mobileSerializer.deserialize();
            }
            set
            {
                mobileSerializer.serialize(value);
            }
        }
    }
}
