using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenNETCF.Desktop.Communication;
using System.Security;
using System.Security.Permissions;
using System.Net;
using System.IO;
using System.Xml;
using System.Drawing.Imaging;
using System.Globalization;
using UBT.AI4.Bio.DivMobi.DataManagement;

namespace UserSyncGui
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class GoogleMapsForm : Form
    {
        private RAPI rapi;
        private int deviceWidth;
        private int deviceHeight;

        private int overlayX = 0;
        private int overlayY = 0;

        private int deltaX = 0;
        private int deltaY = 0;

        private bool GoogleMap;

        public String deviceMapDirectory;

        #region Constructor

        public GoogleMapsForm(int width, int height, bool googleMap)
        {
            InitializeComponent();

            this.GoogleMap = googleMap;
            this.buttonSave.Enabled = false;
            this.buttonSaveToDevice.Enabled = false;

            this.setMobileSize(width, height);

            if (googleMap)
            {
                webBrowserMap.AllowWebBrowserDrop = false;
                webBrowserMap.IsWebBrowserContextMenuEnabled = false;
                webBrowserMap.WebBrowserShortcutsEnabled = false;
                webBrowserMap.ObjectForScripting = this;
                try
                {
                    rapi = new RAPI();

                    if (rapi.DevicePresent)
                    {
                        this.setMobileSize(rapi.GetDeviceCapabilities(DeviceCaps.HorizontalResolution), rapi.GetDeviceCapabilities(DeviceCaps.VerticalResolution));
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Device Size couldn't be automatically retrieved. Default values will be used.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    this.setMobileSize(width, height);
                }

                String fullPath = Environment.CurrentDirectory + @"\Ressources\GoogleMap.html";

                try
                {
                    if (File.Exists(fullPath))
                    {
                        System.Uri u = new Uri(fullPath);
                        webBrowserMap.Navigate(u);
                        showOverlay();
                        webBrowserMap.Visible = true;
                        pictureBoxMap.Visible = false;
                        this.groupBoxGoogleMap.Enabled = true;
                        this.groupBoxMap.Enabled = false;
                        showToolStripMenuItem.Enabled = true;
                        openMapToolStripMenuItem.Enabled = false;
                    }
                    else
                    {
                        throw new ConnectionCorruptedException("GoogleMaps Script couldn't be located. Please ensure that there is a directory named 'Ressources' and a HTML-File 'GoogleMaps.html' within.");
                    }
                }
                catch (Exception)
                {
                    throw new DataFunctionsException("Exception while opening GoogleMaps Script.");
                }
            }
            else
            {
                try
                {
                    OpenFileDialog dlg = new OpenFileDialog();
                    dlg.Title = "Choose map to be shown";
                    dlg.Filter = "JPEG (*.JPEG, *.JPG)|*.jpg;*.jpeg|GIF (*.gif)|*.gif|PNG (*.png)|*.png|Bitmap (*.bmp)|*.bmp";
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        pictureBoxMap.Image = zoomImage(new Bitmap(dlg.FileName));
                        readSettingsFromXML(Path.GetDirectoryName(dlg.FileName), Path.GetFileNameWithoutExtension(dlg.FileName));

                        deltaX = (pictureBoxMap.Width - pictureBoxMap.Image.Width) / 2;
                        deltaY = (pictureBoxMap.Height - pictureBoxMap.Image.Height) / 2;
                        showOverlay();
                    }

                    webBrowserMap.Visible = false;
                    pictureBoxMap.Visible = true;
                    this.groupBoxGoogleMap.Enabled = false;
                    this.groupBoxMap.Enabled = true;
                    showToolStripMenuItem.Enabled = false;
                    openMapToolStripMenuItem.Enabled = true;
                }
                catch (Exception)
                {
                    throw new DataFunctionsException("Exception while opening map and reading settings from XML file.");
                }
            }
        }
        
        #endregion

        #region Properties
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

        public int DeviceWidth
        {
            get
            {
                return this.deviceWidth;
            }
        }

        public int DeviceHeight
        {
            get
            {
                return this.deviceHeight;
            }
        }
        #endregion

        #region Events

        private void buttonShowMap_Click(object sender, EventArgs e)
        {
            if (Latitude != 0.0 && Longitude != 0.0)
            {
                if (webBrowserMap.Document != null)
                {
                    Object[] obj = new Object[2];
                    obj[0] = (Object)Latitude;
                    obj[1] = (Object)Longitude;
                    HtmlDocument doc = webBrowserMap.Document;
                    webBrowserMap.Document.InvokeScript("loadGPS", obj);
                }
            }
            else
            {
                this.getCurrentPosition();
            }

            this.showOverlay();
        }

        private void buttonFromMap_Click(object sender, EventArgs e)
        {
            this.getCurrentPosition();
        }

        private void buttonSaveToDevice_Click(object sender, EventArgs e)
        {
            this.buttonSaveToDevice.Enabled = false;
            this.showOverlay();
            
            if (GoogleMap)
            {
                this.getCurrentPosition();
            }



            try
            {
                if (rapi.DevicePresent && rapi.Connected)
                {
                    if (this.saveToDevice())
                    {
                        MessageBox.Show("Map successfully saved to connected device");
                    }
                    else
                    {
                        MessageBox.Show("Error while saving to connected device. Please try again.");
                    }
                }
                else
                {
                    MessageBox.Show("There is no device connected!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("DirectCopy Not Succesfull. You need To copy manually.");
            }
            

            this.buttonSaveToDevice.Enabled = true;
        }

        private void mobileOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MobileDeviceOptions optForm = new MobileDeviceOptions(this.deviceWidth, this.deviceHeight);

            if(optForm.ShowDialog() == DialogResult.OK)
            {
                this.setMobileSize(optForm.Width, optForm.Height);
                this.showOverlay();

                //this.userControlGoogleMaps.Refresh();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            this.buttonSave.Enabled = false;
            if (checkBoxUseDeviceDimensions.Checked)
            {
                this.showOverlay();
            }

            if (GoogleMap)
            {
                this.getCurrentPosition();
            }

            if (this.saveToLocalMachine())
            {
                MessageBox.Show("Map successfully saved to local machine");
            }
            else
            {
                MessageBox.Show("Error while saving to local machine. Please try again.");
            }
            this.buttonSave.Enabled = true;
        }

        private void textBoxFileName_TextChanged(object sender, EventArgs e)
        {
            if (GoogleMap)
            {
                if (this.textBoxFileName.Text.Equals(String.Empty))
                {
                    this.buttonSave.Enabled = false;
                    this.buttonSaveToDevice.Enabled = false;
                }
                else
                {
                    this.buttonSave.Enabled = true;
                    if(rapi!=null)
                        if (rapi.DevicePresent && rapi.Connected)
                            this.buttonSaveToDevice.Enabled = true;
                }
            }
            else
            {
                if (this.textBoxFileName.Text.Equals(String.Empty) || !coordsDefined())
                {
                    this.buttonSave.Enabled = false;
                    this.buttonSaveToDevice.Enabled = false;
                }
                else
                {
                    this.buttonSave.Enabled = true;
                }
            }
        }

        private void checkBoxNotNorthbound_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxNotNorthbound.Checked)
            {
                this.groupBoxPoint3.Enabled = true;
                this.textBoxSELong.Enabled = true;
                this.textBoxSELat.Enabled = true;
            }
            else
            {
                this.groupBoxPoint3.Enabled = false;
                this.textBoxSELong.Enabled = false;
                this.textBoxSELat.Enabled = false;
            }
        }

        private void openMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Choose Map to show";
            dlg.Filter = "JPEG (*.JPEG, *.JPG)|*.jpg;*.jpeg|GIF (*.gif)|*.gif|PNG (*.png)|*.png|Bitmap (*.bmp)|*.bmp";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBoxNELat.Text = String.Empty;
                textBoxNELong.Text = String.Empty;

                textBoxSWLat.Text = String.Empty;
                textBoxSWLong.Text = String.Empty;

                textBoxSELat.Text = String.Empty;
                textBoxSELong.Text = String.Empty;

                pictureBoxMap.Image = zoomImage(new Bitmap(dlg.FileName));
                readSettingsFromXML(Path.GetDirectoryName(dlg.FileName), Path.GetFileNameWithoutExtension(dlg.FileName));

                deltaX = (pictureBoxMap.Width - pictureBoxMap.Image.Width) / 2;
                deltaY = (pictureBoxMap.Height - pictureBoxMap.Image.Height) / 2;
                this.showOverlay();
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text.Contains('.'))
            {
                ((TextBox)sender).Text.Replace('.', ',');
            }
        }

        private void pictureBoxMap_MouseDown(object sender, MouseEventArgs e)
        {
            if (pictureBoxMap.Image != null)
            {
                if ((e.X + deviceWidth - deltaX) > pictureBoxMap.Image.Width)
                    this.overlayX = pictureBoxMap.Image.Width - deviceWidth;
                else if (e.X <= deltaX)
                    this.overlayX = 0;
                else
                    this.overlayX = e.X - deltaX;

                if ((e.Y + deviceHeight - deltaY) > pictureBoxMap.Image.Height)
                    this.overlayY = pictureBoxMap.Image.Height - deviceHeight;
                else if (e.Y <= deltaY)
                    this.overlayY = 0;
                else
                    this.overlayY = e.Y - deltaY;

                this.showOverlay();
            }
        }

        #endregion

        #region Save Map

        private void showOverlay()
        {
            if (GoogleMap)
            {
                Object[] obj = new Object[2];
                obj[0] = (Object)deviceWidth;
                obj[1] = (Object)deviceHeight;

                //HtmlDocument doc = webBrowserMap.Document;
                webBrowserMap.Document.InvokeScript("showOverlay", obj);
            }
            else
            {
                if (pictureBoxMap.Image != null)
                {
                    pictureBoxMap.Refresh();
                    Graphics g = pictureBoxMap.CreateGraphics();
                    g.DrawRectangle(new Pen(Color.Red, 3), new Rectangle(overlayX + deltaX, overlayY + deltaY, deviceWidth, deviceHeight));
                    g.Save();
                }
            }
        }

        private bool saveToDevice()
        {
            if (GoogleMap)
            {
                try
                {
                    saveGoogleMapToDevice();
                }
                catch (Exception)
                {
                    return false;
                } 
            }
            else
            {
                try
                {
                    saveMapToDevice();
                }
                catch (Exception)
                {
                    return false;
                } 

            }
            return true;
        }

        private void saveGoogleMapToDevice()
        {
            if (this.deviceMapDirectory != null)
            {
                Object[] obj = new Object[2];
                obj[0] = (Object)deviceWidth;
                obj[1] = (Object)deviceHeight;

                HtmlDocument doc = webBrowserMap.Document;
                Object url = webBrowserMap.Document.InvokeScript("loadImageWithParam", obj);

                StringBuilder builder = new StringBuilder();
                builder.Append(this.textBoxFileName.Text);
                builder.Append(".png");
                String localFileName = builder.ToString();

                if (this.DownloadFile(url.ToString(), localFileName) > 0)
                {
                    builder = new StringBuilder();
                    builder.Append(this.textBoxFileName.Text);
                    builder.Append(".xml");

                    ImageOptions iOpt = new ImageOptions();
                    iOpt.Name = this.textBoxFileName.Text.Trim();
                    iOpt.Description = this.textBoxFileDescription.Text.Trim();

                    if (webBrowserMap.Document != null)
                    {
                        doc = webBrowserMap.Document;
                        Object temp;
                        temp = doc.InvokeScript("getSWLatitude");
                        iOpt.SWLat = float.Parse(temp.ToString());
                        iOpt.SELat = float.Parse(temp.ToString());

                        temp = doc.InvokeScript("getSWLongitude");
                        iOpt.SWLong = float.Parse(temp.ToString());

                        temp = doc.InvokeScript("getNELatitude");
                        iOpt.NELat = float.Parse(temp.ToString());

                        temp = doc.InvokeScript("getNELongitude");
                        iOpt.NELong = float.Parse(temp.ToString());
                        iOpt.SELong = float.Parse(temp.ToString());

                        temp = doc.InvokeScript("getZoomLevel");
                        iOpt.ZoomLevel = int.Parse(temp.ToString());
                    }

                    this.writeSettingsToXML(builder.ToString(), iOpt);

                    try
                    {
                        if (!rapi.DeviceFileExists(this.deviceMapDirectory))
                        {
                            rapi.CreateDeviceDirectory(this.deviceMapDirectory);
                        }
                        rapi.CopyFileToDevice(localFileName, this.deviceMapDirectory + @"\" + localFileName, true);
                        rapi.CopyFileToDevice(builder.ToString(), this.deviceMapDirectory + @"\" + localFileName, true);
                        File.Delete(localFileName);
                        File.Delete(builder.ToString());
                        this.textBoxFileDescription.Clear();
                        this.textBoxFileName.Clear();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("DirectCopy Not Succesfull. You need To copy manually.");
                    }
      
                }
            }
        }

        private void saveMapToDevice()
        {
            if (pictureBoxMap.Image == null)
            {
                throw new Exception("No map loaded to be saved.");
            }

            if (this.deviceMapDirectory != null)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(this.textBoxFileName.Text);
                builder.Append(".png");
                String localFileName = builder.ToString();

                if (!Directory.Exists(Environment.CurrentDirectory + @"\Maps"))
                {
                    Directory.CreateDirectory(Environment.CurrentDirectory + @"\Maps");
                }

                //Cut and Save Image
                Bitmap img = new Bitmap(deviceWidth, deviceHeight);
                Graphics g = Graphics.FromImage(img);
                g.DrawImageUnscaled(pictureBoxMap.Image, new Rectangle(overlayX, overlayY, deviceWidth, deviceHeight));
                g.Save();
                img.Save(localFileName, ImageFormat.Png);

                ImageOptions iOpt = new ImageOptions();
                iOpt.Name = this.textBoxFileName.Text.Trim();
                iOpt.Description = this.textBoxFileDescription.Text.Trim();

                try
                {
                    CultureInfo info = null;
                    if (textBoxNELong.Text.Contains(","))
                        info = System.Globalization.CultureInfo.CreateSpecificCulture("de-de");
                    else if (textBoxNELong.Text.Contains("."))
                        info = System.Globalization.CultureInfo.CreateSpecificCulture("en-us");


                    // for now: maps are assumed northbound
                    // otherwise quotients must be differently calculated
                    float deltaLongitude = float.Parse(textBoxNELong.Text.Trim(), info) - float.Parse(textBoxSWLong.Text.Trim());
                    float ratioLongitude = deltaLongitude / pictureBoxMap.Image.Width;

                    float deltaLatitude = float.Parse(textBoxNELat.Text.Trim(), info) - float.Parse(textBoxSWLat.Text.Trim());
                    float ratioLatitude = deltaLatitude / pictureBoxMap.Image.Height;

                    iOpt.NELong = float.Parse(textBoxNELong.Text.Trim(), info) - ((pictureBoxMap.Image.Width - (overlayX + deviceWidth)) * ratioLongitude);
                    iOpt.NELat = float.Parse(textBoxNELat.Text.Trim(), info) - (overlayY * ratioLatitude);

                    iOpt.SWLong = float.Parse(textBoxSWLong.Text.Trim(), info) + (overlayX * ratioLongitude);
                    iOpt.SWLat = float.Parse(textBoxSWLat.Text.Trim(), info) + ((pictureBoxMap.Image.Height - (overlayY + deviceHeight)) * ratioLatitude);

                    //if (checkBoxNotNorthbound.Checked)
                    //{
                    //    //ToDo: calculate SE Corner
                    //}
                    //else
                    //{
                    iOpt.SELong = iOpt.NELong;
                    iOpt.SELat = iOpt.SWLat;
                    //}
                }
                catch (Exception e)
	            {
            		MessageBox.Show("GPS Data couldn`t be calculated. XML File couldn't be saved!");
                    throw e;
	            }

                builder = new StringBuilder();
                builder.Append(Environment.CurrentDirectory + @"\Maps");
                builder.Append(@"\");
                builder.Append(this.textBoxFileName.Text.Trim());
                builder.Append(".xml");

                this.writeSettingsToXML(builder.ToString(), iOpt);
                try
                {
                    if (!rapi.DeviceFileExists(this.deviceMapDirectory))
                    {
                        rapi.CreateDeviceDirectory(this.deviceMapDirectory);
                    }
                    rapi.CopyFileToDevice(localFileName, this.deviceMapDirectory + @"\" + localFileName, true);
                    rapi.CopyFileToDevice(builder.ToString(), this.deviceMapDirectory + @"\" + localFileName, true);

                    File.Delete(localFileName);
                    File.Delete(builder.ToString());

                    this.textBoxFileDescription.Clear();
                    this.textBoxFileName.Clear();
                }
                catch (Exception e)
                {
                    MessageBox.Show("DirectCopy Not Succesfull. You need To copy manually.");
                }
            }
        }

        private bool saveToLocalMachine()
        {
            if (GoogleMap)
            {
                try
                {
                    saveGoogleMapToLocalMachine();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
            else // Map isn't google Map
            {
                try
                {
                    saveMapToLocalMachine();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return false;
                }
                return true;
            }
        }

        private void saveGoogleMapToLocalMachine()
        {
            Object url;
            HtmlDocument doc = webBrowserMap.Document;

            if (this.checkBoxUseDeviceDimensions.Checked)
            {
                Object[] obj = new Object[2];
                obj[0] = (Object)deviceWidth;
                obj[1] = (Object)deviceHeight;

                url = webBrowserMap.Document.InvokeScript("loadImageWithParam", obj);
            }
            else
            {
                url = webBrowserMap.Document.InvokeScript("loadImage");
            }

            StringBuilder builder = new StringBuilder();
            builder.Append(Environment.CurrentDirectory + @"\Maps");
            builder.Append(@"\");
            builder.Append(this.textBoxFileName.Text.Trim());
            builder.Append(".png");

            String localFileName = builder.ToString();
            if (!Directory.Exists(Environment.CurrentDirectory + @"\Maps"))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + @"\Maps");
            }
            if (this.DownloadFile(url.ToString(), localFileName) > 0)
            {
                builder = new StringBuilder();
                builder.Append(Environment.CurrentDirectory + @"\Maps");
                builder.Append(@"\");
                builder.Append(this.textBoxFileName.Text.Trim());
                builder.Append(".xml");

                ImageOptions iOpt = new ImageOptions();
                iOpt.Name = this.textBoxFileName.Text.Trim();
                iOpt.Description = this.textBoxFileDescription.Text.Trim();

                if (webBrowserMap.Document != null)
                {
                    doc = webBrowserMap.Document;
                    Object temp;
                    temp = doc.InvokeScript("getSWLatitude");
                    iOpt.SWLat = float.Parse(temp.ToString());
                    iOpt.SELat = float.Parse(temp.ToString());

                    temp = doc.InvokeScript("getSWLongitude");
                    iOpt.SWLong = float.Parse(temp.ToString());

                    temp = doc.InvokeScript("getNELatitude");
                    iOpt.NELat = float.Parse(temp.ToString());

                    temp = doc.InvokeScript("getNELongitude");
                    iOpt.NELong = float.Parse(temp.ToString());
                    iOpt.SELong = float.Parse(temp.ToString());

                    temp = doc.InvokeScript("getZoomLevel");
                    iOpt.ZoomLevel = int.Parse(temp.ToString());
                }

                this.writeSettingsToXML(builder.ToString(), iOpt);

                this.textBoxFileDescription.Clear();
                this.textBoxFileName.Clear();
            }
        }

        private void saveMapToLocalMachine()
        {
            if (pictureBoxMap.Image == null)
            {
                throw new Exception("No map loaded to be saved.");
            }

            StringBuilder builder = new StringBuilder();
            builder.Append(Environment.CurrentDirectory + @"\Maps");
            builder.Append(@"\");
            builder.Append(this.textBoxFileName.Text.Trim());
            builder.Append(".png");

            String localFileName = builder.ToString();
            if (!Directory.Exists(Environment.CurrentDirectory + @"\Maps"))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + @"\Maps");
            }

            ImageOptions iOpt = new ImageOptions();
            iOpt.Name = this.textBoxFileName.Text.Trim();
            iOpt.Description = this.textBoxFileDescription.Text.Trim();

            if (this.checkBoxUseDeviceDimensions.Checked)
            {
                //Cut and Save Image
                Bitmap img = new Bitmap(deviceWidth, deviceHeight);
                Graphics g = Graphics.FromImage(img);
                g.DrawImageUnscaledAndClipped(pictureBoxMap.Image, new Rectangle((-1)*overlayX, (-1)*overlayY, deviceWidth+overlayX, deviceHeight+overlayY));
                g.Save();
                img.Save(localFileName, ImageFormat.Png);

                try 
                {	
                    CultureInfo info = null;
                    if (textBoxNELong.Text.Contains(","))
                        info = System.Globalization.CultureInfo.CreateSpecificCulture("de-de");
                    else if (textBoxNELong.Text.Contains("."))
                        info = System.Globalization.CultureInfo.CreateSpecificCulture("en-us");

                    // for now: maps are assumed northbound
                    // otherwise quotients must be differently calculated
                    float deltaLongitude = float.Parse(textBoxNELong.Text.Trim(), info) - float.Parse(textBoxSWLong.Text.Trim(), info);
                    float ratioLongitude = deltaLongitude / pictureBoxMap.Image.Width;

                    float deltaLatitude = float.Parse(textBoxNELat.Text.Trim(), info) - float.Parse(textBoxSWLat.Text.Trim(), info);
                    float ratioLatitude = deltaLatitude / pictureBoxMap.Image.Height;

                    iOpt.NELong = float.Parse(textBoxNELong.Text.Trim(), info) - ((pictureBoxMap.Image.Width - overlayX - deviceWidth) * ratioLongitude);
                    iOpt.NELat = float.Parse(textBoxNELat.Text.Trim(), info) - (overlayY * ratioLatitude);

                    iOpt.SWLong = float.Parse(textBoxSWLong.Text.Trim(), info) + (overlayX * ratioLongitude);
                    iOpt.SWLat = float.Parse(textBoxSWLat.Text.Trim(), info) + ((pictureBoxMap.Image.Height - overlayY - deviceHeight) * ratioLatitude);

                    //if (checkBoxNotNorthbound.Checked)
                    //{
                    //    //ToDo: calculate SE Corner
                    //}
                    //else
                    //{
                    iOpt.SELong = iOpt.NELong;
                    iOpt.SELat = iOpt.SWLat;
                    //}
                }
                catch (Exception e)
	            {
            		MessageBox.Show("GPS Data couldn`t be calculated. XML File couldn't be saved!");
                    throw e;
	            }
            }
            else
            {
                pictureBoxMap.Image.Save(localFileName, ImageFormat.Png);

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
            builder.Append(this.textBoxFileName.Text.Trim());
            builder.Append(".xml");

            this.writeSettingsToXML(builder.ToString(), iOpt);

            this.textBoxFileDescription.Clear();
            this.textBoxFileName.Clear();
        }
        
        private int DownloadFile(String remoteFilename, String localFilename)
        {
            // Function will return the number of bytes processed
            // to the caller. Initialize to 0 here.
            int bytesProcessed = 0;

            // Assign values to these objects here so that they can
            // be referenced in the finally block
            Stream remoteStream = null;
            Stream localStream = null;
            WebResponse response = null;

            // Use a try/catch/finally block as both the WebRequest and Stream
            // classes throw exceptions upon error
            try
            {
                // Create a request for the specified remote file name
                WebRequest request = WebRequest.Create(remoteFilename);
                if (request != null)
                {
                    // Send the request to the server and retrieve the
                    // WebResponse object
                    response = request.GetResponse();
                    if (response != null)
                    {
                        // Once the WebResponse object has been retrieved,
                        // get the stream object associated with the response's data
                        remoteStream = response.GetResponseStream();

                        // Create the local file
                        localStream = File.Create(localFilename);

                        // Allocate a 1k buffer
                        byte[] buffer = new byte[1024];
                        int bytesRead;

                        // Simple do/while loop to read from stream until
                        // no bytes are returned
                        do
                        {
                            // Read data (up to 1k) from the stream
                            bytesRead = remoteStream.Read(buffer, 0, buffer.Length);

                            // Write the data to the local file
                            localStream.Write(buffer, 0, bytesRead);

                            // Increment total bytes processed
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
                // Close the response and streams objects here
                // to make sure they're closed even if an exception
                // is thrown at some point
                if (response != null) response.Close();
                if (remoteStream != null) remoteStream.Close();
                if (localStream != null) localStream.Close();
            }

            // Return total bytes processed to caller.
            return bytesProcessed;
        }

        #endregion

        #region Helper Methods

        private void setMobileSize(int width, int height)
        {
            this.deviceWidth = width;
            this.deviceHeight = height;
        }

        private void getCurrentPosition()
        {
            // Current centered Latitude and Longitude for GPS WGS84 
            if (webBrowserMap.Document != null)
            {
                HtmlDocument doc = webBrowserMap.Document;
                Object temp;
                temp = doc.InvokeScript("getLatitude");
                this.textBoxLatitude.Text = temp.ToString();
                this.textBoxLatitude.Refresh();

                temp = doc.InvokeScript("getLongitude");
                this.textBoxLongitude.Text = temp.ToString();
                this.textBoxLongitude.Refresh();
            }
        }

        private bool coordsDefined()
        {
            if (this.textBoxNELong.Text.Equals(String.Empty) || this.textBoxNELat.Text.Equals(String.Empty))
                return false;

            if (this.textBoxSWLong.Text.Equals(String.Empty) || this.textBoxSWLat.Text.Equals(String.Empty))
                return false;

            if (this.checkBoxNotNorthbound.Checked)
            {
                if (this.textBoxSELong.Text.Equals(String.Empty) || this.textBoxSELat.Text.Equals(String.Empty))
                    return false;
            }
            return true;
        }

        private Bitmap zoomImage(Bitmap image)
        {
            if (image != null)
            {
                int imgWidth = image.Size.Width;
                int imgHeight = image.Size.Height;

                double imgRate = imgWidth / imgHeight;

                if (imgWidth > this.pictureBoxMap.Width || imgHeight > this.pictureBoxMap.Height)
                {
                    if (imgWidth > imgHeight)
                    {
                        imgWidth = this.pictureBoxMap.Width;
                        imgHeight = (int)(imgWidth / imgRate);
                    }
                    else
                    {
                        imgHeight = this.pictureBoxMap.Height;
                        imgWidth = (int)(imgHeight * imgRate);
                    }
                }
                return new Bitmap(image, new Size(imgWidth, imgHeight));
            }
            return null;
        }

        private void writeSettingsToXML(String path, ImageOptions iOpt)
        {
            XmlTextWriter writer = new XmlTextWriter(path, null);

            // console print out for testing purpose
            // XmlTextWriter writer = new XmlTextWriter(Console.Out);

            writer.Formatting = Formatting.Indented;

            // Starts a new document
            writer.WriteStartDocument();

            //Write comments
            writer.WriteComment("Map-Image Options");

            // Add elements to the file
            writer.WriteStartElement("ImageOptions", "");

            writer.WriteStartElement("Name", "");
            writer.WriteString(iOpt.Name);
            writer.WriteEndElement();

            writer.WriteStartElement("Description", "");
            writer.WriteString(iOpt.Description);
            writer.WriteEndElement();

            writer.WriteStartElement("SWLat", "");
            writer.WriteString(iOpt.SWLat.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("SWLong", "");
            writer.WriteString(iOpt.SWLong.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("SELat", "");
            writer.WriteString(iOpt.SELat.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("SELong", "");
            writer.WriteString(iOpt.SELong.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("NELat", "");
            writer.WriteString(iOpt.NELat.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("NELong", "");
            writer.WriteString(iOpt.NELong.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("ZoomLevel", "");
            writer.WriteString(iOpt.ZoomLevel.ToString());
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
                
                FileInfo[] fileInfo = dirInfo.GetFiles(fileName+".xml");

                if (fileInfo.Length > 0)
                {
                    XmlTextReader reader;
                    reader = new XmlTextReader(fileInfo[0].FullName);
                    reader.WhitespaceHandling = WhitespaceHandling.None;

                    while (reader.Read())
                    {
                        if (reader.LocalName.Equals("Name"))
                        {
                            textBoxFileName.Text = reader.ReadElementContentAsString();
                        }

                        if (reader.LocalName.Equals("Description"))
                        {
                            textBoxFileDescription.Text = reader.ReadElementContentAsString();
                        }

                        if (reader.LocalName.Equals("SWLat"))
                        {
                            textBoxSWLat.Text = reader.ReadElementContentAsString();
                        }
                        if (reader.LocalName.Equals("SWLong"))
                        {
                            textBoxSWLong.Text = reader.ReadElementContentAsString();
                        }
                        
                        if (reader.LocalName.Equals("SELat"))
                        {
                            textBoxSELat.Text = reader.ReadElementContentAsString();
                        }
                        if (reader.LocalName.Equals("SELong"))
                        {
                            textBoxSELong.Text = reader.ReadElementContentAsString();
                        }

                        if (reader.LocalName.Equals("NELat"))
                        {
                            textBoxNELat.Text = reader.ReadElementContentAsString();
                        }
                        if (reader.LocalName.Equals("NELong"))
                        {
                            textBoxNELong.Text = reader.ReadElementContentAsString();
                        }
                    }
                }
            }
        }

        #endregion
    }
}