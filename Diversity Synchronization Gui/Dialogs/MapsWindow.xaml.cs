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
using System.Windows.Shapes;
using Diversity_Synchronization.Options.Language;
using Diversity_Synchronization;
using Diversity_Synchronization_Gui.Options;
using System.Security.Permissions;
using System.IO;
using System.ComponentModel;
using Microsoft.Win32;
using System.Net;
using System.Xml;

namespace Diversity_Synchronization_Gui
{
    /// <summary>
    /// Interaktionslogik für MapsWindow.xaml
    /// </summary>
    
    public partial class MapsWindow : Window, ILanguageRefreshable, INotifyPropertyChanged, IDataErrorInfo
    {
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        [System.Runtime.InteropServices.ComVisibleAttribute(true)]
        public class MapsCOMInterface
        {
            MapsWindow owner;
            public MapsCOMInterface(MapsWindow owner)
            {
                this.owner = owner;
            }
        }

        public class MapMetaData : IDataErrorInfo
        {
            public String Name
            {
                set;
                get;                
            }
            public String Description
            {
                set;
                get;
            }
            public float SWLat
            {
                set;
                get;
            }
            public float SWLong
            {
                set;
                get;
            }
            public float SELat
            {
                set;
                get;
            }
            public float SELong
            {
                set;
                get;
            }
            public float NELat
            {
                set;
                get;
            }
            public float NELong
            {
                set;
                get;
            }

            public int ZoomLevel
            {
                set;
                get;
            }

            #region IDataErrorInfo Member

            public string Error
            {
                get { return null; }
            }

            public string this[string columnName]
            {
                get
                {
                    string res = null;

                    if (columnName == "Name")
                    {
                        if (Name != null && Name.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) > -1)
                            res = "Name must not contain characters forbidden in file names!";
                        else if (Name == null || Name == "")
                            res = "Name field may not be empty";
                    }
                    return res;
                }
            }

            #endregion
        }

        private int width = OptionsAccess.MobileDeviceOptions.ScreenWidth;
        public int DeviceWidth
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
                showOverlay();
                OptionsAccess.MobileDeviceOptions.ScreenWidth = value;                
            }
        }
        public int height = OptionsAccess.MobileDeviceOptions.ScreenHeight;
        public int DeviceHeight
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
                showOverlay();
                OptionsAccess.MobileDeviceOptions.ScreenHeight = value;
            }
        }  
     
        private float Latitude
        {
            get
            {
                if (this.textBoxLatitude.Text.Equals(String.Empty))
                    return 0;
                else
                    try
                    {
                        return float.Parse(this.textBoxLatitude.Text);
                    }
                    catch (Exception)
                    {
                        return 0;
                    }
            }
        }

        private float Longitude
        {
            get
            {
                if (this.textBoxLongitude.Text.Equals(String.Empty))
                    return 0;
                else
                    try
                    {
                        return float.Parse(this.textBoxLongitude.Text);
                    }
                    catch (Exception)
                    {
                        return 0;
                    }
            }
        }        
       

        MapMetaData mapInfo = new MapMetaData();
        public MapMetaData MapInfo 
        {
            get
            {
                return mapInfo;
            }
        }

        

        #region IDataErrorInfo Member

        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;

                if (columnName == "DeviceHeight")
                {
                    if (this.DeviceHeight < 0 || this.DeviceHeight > 640)
                    {
                        result = "The Height must be between 0 and 640!";
                    }
                }

                if (columnName == "DeviceWidth")
                {
                    if (this.DeviceWidth < 0 || this.DeviceWidth > 640)
                    {
                        result = "The Width must be between 0 and 640!";
                    }
                }
                
                return result;
            }
        }

        #endregion

        private bool GoogleMap = true;
        public MapsWindow()
        {
            InitializeComponent();
            DataContext = this;
            RefreshLanguage();

            checkBoxDeviceSize.IsChecked = OptionsAccess.MapSaveOptions.UseDeviceDimensions;
            if (GoogleMap)
            {
                string mapsHTML = OptionsAccess.getFolderPath(ApplicationFolder.Application) + @"\Maps\GoogleMap.html";
                if (!File.Exists(mapsHTML))
                {
                    //Log
                }
                else
                {
                    webbrowserMap.Source = new Uri(mapsHTML);
                    webbrowserMap.ObjectForScripting = new MapsCOMInterface(this);

                    webbrowserMap.Loaded += new RoutedEventHandler(webbrowserMap_Loaded);
                }

                /*try
                {
                    rapi = new RAPI();

                    if (rapi.DevicePresent)
                    {
                        this.setMobileSize(rapi.GetDeviceCapabilities(DeviceCaps.HorizontalResolution), rapi.GetDeviceCapabilities(DeviceCaps.VerticalResolution));
                    }
                }
                catch (Exception)
                {
                    //TODO Log
                }*/
            }
            else
            {
                /*try
                {
                    OpenFileDialog dlg = new OpenFileDialog();
                    dlg.Title = "Choose map to be shown";
                    dlg.Filter = "JPEG (*.JPEG, *.JPG)|*.jpg;*.jpeg|GIF (*.gif)|*.gif|PNG (*.png)|*.png|Bitmap (*.bmp)|*.bmp";
                    if (dlg.ShowDialog() == true)
                    {
                        imageMap = zoomImage(new Bitmap(dlg.FileName));
                        readSettingsFromXML(Path.GetDirectoryName(dlg.FileName), Path.GetFileNameWithoutExtension(dlg.FileName));

                        deltaX = (pictureBoxMap.Width - pictureBoxMap.Image.Width) / 2;
                        deltaY = (pictureBoxMap.Height - pictureBoxMap.Image.Height) / 2;
                        showOverlay();
                    }

                    webbrowserMap.Visible = false;
                    pictureBoxMap.Visible = true;
                    this.groupBoxGoogleMap.Enabled = false;
                    this.groupBoxMap.Enabled = true;
                    showToolStripMenuItem.Enabled = false;
                    openMapToolStripMenuItem.Enabled = true;
                }
                catch (Exception)
                {
                    throw new DataFunctionsException("Exception while opening map and reading settings from XML file.");
                }*/
            }           
        }

        void webbrowserMap_Loaded(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            showOverlay();
            webbrowserMap.Loaded -= webbrowserMap_Loaded;
        }

        public void RefreshLanguage()
        {
            ILanguage lf = OptionsAccess.Language;

            this.Title = lf.getLanguageString(1401);
            this.menuItemMenu.Header = lf.getLanguageString(1402);
            this.menuItemClose.Header = lf.getLanguageString(1403);            
           
            this.menuItemHelp.Header = lf.getLanguageString(1408);
            this.textPageCaption.Content = lf.getLanguageString(1421);
            
            this.groupBoxPosition.Header = lf.getLanguageString(1423);
            this.labelLongitude.Content = lf.getLanguageString(1424);
            this.labelLatitude.Content = lf.getLanguageString(1425);
            this.buttonGetFromMap.Content = lf.getLanguageString(1426);
            this.buttonShowMap.Content = lf.getLanguageString(1427);

            this.checkBoxDeviceSize.Content = lf.getLanguageString(1428);
            this.labelDeviceHeight.Content = lf.getLanguageString(1429);
            this.labelDeviceWidth.Content = lf.getLanguageString(1430);
            
            this.groupBoxFile.Header = lf.getLanguageString(1438);
            this.labelFile.Content = lf.getLanguageString(1439);
            this.labelDescription.Content = lf.getLanguageString(1440);
            this.buttonSave.Content = lf.getLanguageString(1441);
        }

        #region Menu Events

        private void menuItemClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void menuItemSaveOptions_Click(object sender, RoutedEventArgs e)
        {
            MapSaveOptionsWindow msow = new MapSaveOptionsWindow();
            msow.ShowDialog();
        }

        

        private void menuItemHelp_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void buttonGetFromMap_Click(object sender, RoutedEventArgs e)
        {
            this.getCurrentPosition();
        }

        private void buttonShowMap_Click(object sender, RoutedEventArgs e)
        {
            if (Latitude != 0.0 && Longitude != 0.0)
            {
                if (webbrowserMap.Document != null)
                {
                    Object[] obj = new Object[2];
                    obj[0] = (Object)Latitude;
                    obj[1] = (Object)Longitude;                    
                    webbrowserMap.InvokeScript("loadGPS", obj);
                }
            }
            else
            {
                this.getCurrentPosition();
            }

            this.showOverlay();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            this.buttonSave.IsEnabled = false;
            if (OptionsAccess.MapSaveOptions.UseDeviceDimensions)
            {
                this.showOverlay();
            }

            if (GoogleMap)
            {
                this.getCurrentPosition();
            }

            if (IsValid(this))
            {
                if (this.saveToLocalMachine())
                {
                    (new MessageBoxWindow(1450, 1450, 1451, 1454)).ShowDialog();                    
                }
                else
                {
                    (new MessageBoxWindow(1452, 1452, 1453, 1454)).ShowDialog();
                }
            }
            buttonSave.IsEnabled = true;
        }

        #endregion
                
        private int overlayX = 0;
        private int overlayY = 0;

        private int deltaX = 0;
        private int deltaY = 0;

        

        public String deviceMapDirectory;
        
        
        

        #region Save Map

        private void showOverlay()
        {
            if (GoogleMap)
            {
                Object[] obj = new Object[2];
                obj[0] = (Object)DeviceWidth;
                obj[1] = (Object)DeviceHeight;

                webbrowserMap.InvokeScript("showOverlay", obj);
            }
        }                

       

       

        // Validate all dependency objects in a window
        bool IsValid(DependencyObject node)
        {
            // Check if dependency object was passed
            if (node != null)
            {
                // Check if dependency object is valid.
                // NOTE: Validation.GetHasError works for controls that have validation rules attached 
                bool isValid = !Validation.GetHasError(node);
                if (!isValid)
                {
                    // If the dependency object is invalid, and it can receive the focus,
                    // set the focus
                    if (node is IInputElement) Keyboard.Focus((IInputElement)node);
                    return false;
                }
            }

            // If this dependency object is valid, check all child dependency objects
            foreach (object subnode in LogicalTreeHelper.GetChildren(node))
            {
                if (subnode is DependencyObject)
                {
                    // If a child dependency object is invalid, return false immediately,
                    // otherwise keep checking
                    if (IsValid((DependencyObject)subnode) == false) return false;
                }
            }

            // All dependency objects are valid
            return true;
        }

        

        

        
        

        //private void pictureBoxMap_MouseDown(object sender, MouseEventArgs e)
        //{
        //    if (pictureBoxMap.Image != null)
        //    {
        //        if ((e.X + deviceWidth - deltaX) > pictureBoxMap.Image.Width)
        //            this.overlayX = pictureBoxMap.Image.Width - deviceWidth;
        //        else if (e.X <= deltaX)
        //            this.overlayX = 0;
        //        else
        //            this.overlayX = e.X - deltaX;

        //        if ((e.Y + deviceHeight - deltaY) > pictureBoxMap.Image.Height)
        //            this.overlayY = pictureBoxMap.Image.Height - deviceHeight;
        //        else if (e.Y <= deltaY)
        //            this.overlayY = 0;
        //        else
        //            this.overlayY = e.Y - deltaY;

        //        this.showOverlay();
        //    }
        //}

        #endregion

        #region Save Map        

        private bool saveToLocalMachine()
        {
            if (GoogleMap)
            {
                try
                {
                    saveGoogleMapToLocalMachine();
                    SyncStatus.Instance.Sync |= SyncStatus.SyncState.Maps;
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
            return false;
            //else // Map isn't google Map
            //{
            //    try
            //    {
            //        saveMapToLocalMachine();
            //    }
            //    catch (Exception e)
            //    {
            //        MessageBox.Show(e.Message);
            //        return false;
            //    }
            //    return true;
            //}
        }

        private void saveGoogleMapToLocalMachine()
        {
            Object url;            

            if (OptionsAccess.MapSaveOptions.UseDeviceDimensions)
            {
                Object[] obj = new Object[2];
                obj[0] = (Object)DeviceWidth;
                obj[1] = (Object)DeviceHeight;

                url = webbrowserMap.InvokeScript("loadImageWithParam", obj);
            }
            else
            {
                url = webbrowserMap.InvokeScript("loadImage");
            }

            StringBuilder builder = new StringBuilder();
            builder.Append(OptionsAccess.getFolderPath(ApplicationFolder.Maps));
            builder.Append(@"\");
            builder.Append(this.textBoxFile.Text.Trim());
            builder.Append(".png");

            String localFileName = builder.ToString();            
            if (this.DownloadFile(url.ToString(), localFileName) > 0)
            {
                builder = new StringBuilder();
                builder.Append(OptionsAccess.getFolderPath(ApplicationFolder.Maps));
                builder.Append(@"\");
                builder.Append(this.textBoxFile.Text.Trim());
                builder.Append(".xml");

                
                MapInfo.Name = this.textBoxFile.Text.Trim();
                MapInfo.Description = this.textBoxDescription.Text.Trim();

                if (webbrowserMap.Document != null)
                {                    
                    Object temp;
                    temp = webbrowserMap.InvokeScript("getSWLatitude");
                    MapInfo.SWLat = float.Parse(temp.ToString());
                    MapInfo.SELat = float.Parse(temp.ToString());

                    temp = webbrowserMap.InvokeScript("getSWLongitude");
                    MapInfo.SWLong = float.Parse(temp.ToString());

                    temp = webbrowserMap.InvokeScript("getNELatitude");
                    MapInfo.NELat = float.Parse(temp.ToString());

                    temp = webbrowserMap.InvokeScript("getNELongitude");
                    MapInfo.NELong = float.Parse(temp.ToString());
                    MapInfo.SELong = float.Parse(temp.ToString());

                    temp = webbrowserMap.InvokeScript("getZoomLevel");
                    MapInfo.ZoomLevel = int.Parse(temp.ToString());
                }

                this.writeSettingsToXML(builder.ToString());

                this.textBoxDescription.Clear();
                this.textBoxFile.Clear();
            }
        }

        private void saveMapToLocalMachine()
        {
            /*if (imageMap.Source == null)
            {
                throw new Exception("No map loaded to be saved.");
            }

            StringBuilder builder = new StringBuilder();
            builder.Append(Environment.CurrentDirectory + @"\Maps");
            builder.Append(@"\");
            builder.Append(this.textBoxFile.Text.Trim());
            builder.Append(".png");

            String localFileName = builder.ToString();
            if (!Directory.Exists(Environment.CurrentDirectory + @"\Maps"))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + @"\Maps");
            }

            ImageOptions iOpt = new ImageOptions();
            iOpt.Name = this.textBoxFile.Text.Trim();
            iOpt.Description = this.textBoxFileDescription.Text.Trim();

            if (OptionsAccess.MapSaveOptions.UseDeviceDimensions)
            {
                //Cut and Save Image
                Bitmap img = new Bitmap(deviceWidth, deviceHeight);
                Graphics g = Graphics.FromImage(img);
                g.DrawImageUnscaledAndClipped(pictureBoxMap.Image, new Rectangle((-1) * overlayX, (-1) * overlayY, deviceWidth + overlayX, deviceHeight + overlayY));
                g.Save();
                img.Save(localFileName, ImageFormat.Png);

                try
                {
                    CultureInfo info = null;
                    if (textBoxNELong.Text.Contains(","))
                        info = System.Globalization.CultureInfo.CreateSpecificCulture("de-de");
                    else if (textBoxNELong.Text.Contains("."))
                        info = System.Globalization.CultureInfo.CreateSpecificCulture("en-us");

                     //for now: maps are assumed northbound
                     //otherwise quotients must be differently calculated
                    float deltaLongitude = float.Parse(textBoxNELong.Text.Trim(), info) - float.Parse(textBoxSWLong.Text.Trim(), info);
                    float ratioLongitude = deltaLongitude / pictureBoxMap.Image.Width;

                    float deltaLatitude = float.Parse(textBoxNELat.Text.Trim(), info) - float.Parse(textBoxSWLat.Text.Trim(), info);
                    float ratioLatitude = deltaLatitude / pictureBoxMap.Image.Height;

                    iOpt.NELong = float.Parse(textBoxNELong.Text.Trim(), info) - ((pictureBoxMap.Image.Width - overlayX - deviceWidth) * ratioLongitude);
                    iOpt.NELat = float.Parse(textBoxNELat.Text.Trim(), info) - (overlayY * ratioLatitude);

                    iOpt.SWLong = float.Parse(textBoxSWLong.Text.Trim(), info) + (overlayX * ratioLongitude);
                    iOpt.SWLat = float.Parse(textBoxSWLat.Text.Trim(), info) + ((pictureBoxMap.Image.Height - overlayY - deviceHeight) * ratioLatitude);

                    if (checkBoxNotNorthbound.Checked)
                    {
                        //ToDo: calculate SE Corner
                    }
                    else
                    {
                    iOpt.SELong = iOpt.NELong;
                    iOpt.SELat = iOpt.SWLat;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("GPS Data couldn`t be calculated. XML File couldn't be saved!");
                    throw e;
                }
            }
            else
            {
                imageMap..Save(localFileName, ImageFormat.Png);

                iOpt.NELong = float.Parse(textBoxNELong.Text.Trim());
                iOpt.NELat = float.Parse(textBoxNELat.Text.Trim());

                iOpt.SWLong = float.Parse(textBoxSWLong.Text.Trim());
                iOpt.SWLat = float.Parse(textBoxSWLat.Text.Trim());

                if (checkBoxNotNorthbound.Checked)
                {
                    iOpt.SELong = float.Parse(textBoxSELong.Text.Trim());
                    iOpt.SELat = float.Parse(textBoxSELat.Text.Trim());
                }
                else
                {
                    iOpt.SELong = iOpt.NELong;
                    iOpt.SELat = iOpt.SWLat;
                }
            }

            builder = new StringBuilder();
            builder.Append(Environment.CurrentDirectory + @"\Maps");
            builder.Append(@"\");
            builder.Append(this.textBoxFile.Text.Trim());
            builder.Append(".xml");

            this.writeSettingsToXML(builder.ToString(), iOpt);

            this.textBoxFileDescription.Clear();
            this.textBoxFile.Clear();
            */
        }

        private int DownloadFile(String remoteFilename, String localFilename)
        {
             //Function will return the number of bytes processed
             //to the caller. Initialize to 0 here.
            int bytesProcessed = 0;

             //Assign values to these objects here so that they can
             //be referenced in the finally block
            Stream remoteStream = null;
            Stream localStream = null;
            WebResponse response = null;

             //Use a try/catch/finally block as both the WebRequest and Stream
             //classes throw exceptions upon error
            try
            {
                //Create a request for the specified remote file name
                WebRequest request = WebRequest.Create(remoteFilename);
                if (request != null)
                {
                     //Send the request to the server and retrieve the
                     //WebResponse object
                    response = request.GetResponse();
                    if (response != null)
                    {
                         //Once the WebResponse object has been retrieved,
                         //get the stream object associated with the response's data
                        remoteStream = response.GetResponseStream();

                        //Create the local file
                        localStream = File.Create(localFilename);

                        //Allocate a 1k buffer
                        byte[] buffer = new byte[1024];
                        int bytesRead;

                         //Simple do/while loop to read from stream until
                         //no bytes are returned
                        do
                        {
                            //Read data (up to 1k) from the stream
                            bytesRead = remoteStream.Read(buffer, 0, buffer.Length);

                            //Write the data to the local file
                            localStream.Write(buffer, 0, bytesRead);

                            //Increment total bytes processed
                            bytesProcessed += bytesRead;
                        } while (bytesRead > 0);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                 //Close the response and streams objects here
                 //to make sure they're closed even if an exception
                 //is thrown at some point
                if (response != null) response.Close();
                if (remoteStream != null) remoteStream.Close();
                if (localStream != null) localStream.Close();
            }

            //Return total bytes processed to caller.
            return bytesProcessed;
        }

        #endregion

       
       

        #region Helper Methods

        private void setMobileSize(int width, int height)
        {
            OptionsAccess.MobileDeviceOptions.ScreenWidth = width;
            OptionsAccess.MobileDeviceOptions.ScreenHeight = height;
        }

        private void getCurrentPosition()
        {
            //Current centered Latitude and Longitude for GPS WGS84 
            if (webbrowserMap.Document != null)
            {
                Object temp;
                temp = webbrowserMap.InvokeScript("getLatitude");
                this.textBoxLatitude.Text = temp.ToString();
                

                temp = webbrowserMap.InvokeScript("getLongitude");
                this.textBoxLongitude.Text = temp.ToString();
                
            }
        }

        private bool coordsDefined()
        {
            //TODO
            return true;
        }

        //private Bitmap zoomImage(Bitmap image)
        //{
        //    if (image != null)
        //    {
        //        int imgWidth = image.Size.Width;
        //        int imgHeight = image.Size.Height;

        //        double imgRate = imgWidth / imgHeight;

        //        if (imgWidth > this.pictureBoxMap.Width || imgHeight > this.pictureBoxMap.Height)
        //        {
        //            if (imgWidth > imgHeight)
        //            {
        //                imgWidth = this.pictureBoxMap.Width;
        //                imgHeight = (int)(imgWidth / imgRate);
        //            }
        //            else
        //            {
        //                imgHeight = this.pictureBoxMap.Height;
        //                imgWidth = (int)(imgHeight * imgRate);
        //            }
        //        }
        //        return new Bitmap(image, new Size(imgWidth, imgHeight));
        //    }
        //    return null;
        //}


        private void writeSettingsToXML(String path)
        {
            XmlTextWriter writer = new XmlTextWriter(path, null);

            // console print out for testing purposes
            // XmlTextWriter writer = new XmlTextWriter(Console.Out);

            writer.Formatting = Formatting.Indented;

            // Starts a new document
            writer.WriteStartDocument();

            //Write comments
            writer.WriteComment("Map-Image Options");

            // Add elements to the file
            writer.WriteStartElement("ImageOptions", "");

            writer.WriteStartElement("Name", "");
            writer.WriteString(MapInfo.Name);
            writer.WriteEndElement();

            writer.WriteStartElement("Description", "");
            writer.WriteString(MapInfo.Description);
            writer.WriteEndElement();

            writer.WriteStartElement("SWLat", "");
            writer.WriteString(MapInfo.SWLat.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("SWLong", "");
            writer.WriteString(MapInfo.SWLong.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("SELat", "");
            writer.WriteString(MapInfo.SELat.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("SELong", "");
            writer.WriteString(MapInfo.SELong.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("NELat", "");
            writer.WriteString(MapInfo.NELat.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("NELong", "");
            writer.WriteString(MapInfo.NELong.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("ZoomLevel", "");
            writer.WriteString(MapInfo.ZoomLevel.ToString());
            writer.WriteEndElement();

            writer.WriteEndElement();

            // Ends the document
            writer.WriteEndDocument();

            writer.Flush();
            writer.Close();
        }

        private void readSettingsFromXML(String directory, String fileName)
        {
            if (Directory.Exists(directory))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(directory);

                FileInfo[] fileInfo = dirInfo.GetFiles(fileName + ".xml");

                if (fileInfo.Length > 0)
                {
                    XmlTextReader reader;
                    reader = new XmlTextReader(fileInfo[0].FullName);
                    reader.WhitespaceHandling = WhitespaceHandling.None;

                    while (reader.Read())
                    {
                        if (reader.LocalName.Equals("Name"))
                        {
                            MapInfo.Name = reader.ReadElementContentAsString();
                        }

                        if (reader.LocalName.Equals("Description"))
                        {
                            MapInfo.Description = reader.ReadElementContentAsString();
                        }

                        if (reader.LocalName.Equals("SWLat"))
                        {
                            MapInfo.SWLat = reader.ReadElementContentAsFloat();
                        }
                        if (reader.LocalName.Equals("SWLong"))
                        {
                            MapInfo.SWLong = reader.ReadElementContentAsFloat();
                        }

                        if (reader.LocalName.Equals("SELat"))
                        {
                            MapInfo.SELat = reader.ReadElementContentAsFloat();
                        }
                        if (reader.LocalName.Equals("SELong"))
                        {
                            MapInfo.SELong = reader.ReadElementContentAsFloat();
                        }

                        if (reader.LocalName.Equals("NELat"))
                        {
                            MapInfo.NELat = reader.ReadElementContentAsFloat();
                        }
                        if (reader.LocalName.Equals("NELong"))
                        {
                            MapInfo.NELong = reader.ReadElementContentAsFloat();
                        }
                    }
                }
            }
        }

        #endregion
    
        #region INotifyPropertyChanged Member

        public event PropertyChangedEventHandler  PropertyChanged;

        #endregion

        private void checkBoxDeviceSize_Checked(object sender, RoutedEventArgs e)
        {
            
            if (checkBoxDeviceSize.IsChecked != OptionsAccess.MapSaveOptions.UseDeviceDimensions)
                OptionsAccess.MapSaveOptions.UseDeviceDimensions = checkBoxDeviceSize.IsChecked ?? false;

        }

        private void menuItemMenu_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}
