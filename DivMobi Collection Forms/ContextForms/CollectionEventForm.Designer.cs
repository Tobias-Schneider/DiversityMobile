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
namespace UBT.AI4.Bio.DivMobi.Forms.ContextForms
{
    partial class CollectionEventForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CollectionEventForm));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.textBoxCollectionTime = new System.Windows.Forms.TextBox();
            this.labelCollectionTimeCaption = new System.Windows.Forms.Label();
            this.textBoxCollectorsEventNumber = new System.Windows.Forms.TextBox();
            this.labelCollectorsEventNumberCaption = new System.Windows.Forms.Label();
            this.buttonHabitat = new System.Windows.Forms.Button();
            this.buttonLocality = new System.Windows.Forms.Button();
            this.buttonMethod = new System.Windows.Forms.Button();
            this.textBoxTimeSpan = new System.Windows.Forms.TextBox();
            this.labelTimespan = new System.Windows.Forms.Label();
            this.labelDateCaption = new System.Windows.Forms.Label();
            this.dateTimePickerCollectionEventDate = new System.Windows.Forms.DateTimePicker();
            this.buttonNewDate = new System.Windows.Forms.Button();
            this.labelVersionCaption = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelDateSupplement = new System.Windows.Forms.Label();
            this.textBoxDateSupplement = new System.Windows.Forms.TextBox();
            this.textBoxRefTitle = new System.Windows.Forms.TextBox();
            this.labelRefTitle = new System.Windows.Forms.Label();
            this.textBoxCountryCache = new System.Windows.Forms.TextBox();
            this.labelCountryCache = new System.Windows.Forms.Label();
            this.buttonNotes = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.labelCEText = new System.Windows.Forms.Label();
            this.pictureBoxFormImage = new System.Windows.Forms.PictureBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxCollectionTime
            // 
            this.textBoxCollectionTime.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxCollectionTime.Location = new System.Drawing.Point(77, 158);
            this.textBoxCollectionTime.MaxLength = 50;
            this.textBoxCollectionTime.Name = "textBoxCollectionTime";
            this.textBoxCollectionTime.Size = new System.Drawing.Size(140, 19);
            this.textBoxCollectionTime.TabIndex = 4;
            // 
            // labelCollectionTimeCaption
            // 
            this.labelCollectionTimeCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelCollectionTimeCaption.Location = new System.Drawing.Point(10, 162);
            this.labelCollectionTimeCaption.Name = "labelCollectionTimeCaption";
            this.labelCollectionTimeCaption.Size = new System.Drawing.Size(59, 20);
            this.labelCollectionTimeCaption.Text = "Time:";
            // 
            // textBoxCollectorsEventNumber
            // 
            this.textBoxCollectorsEventNumber.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxCollectorsEventNumber.Location = new System.Drawing.Point(78, 62);
            this.textBoxCollectorsEventNumber.MaxLength = 50;
            this.textBoxCollectorsEventNumber.Name = "textBoxCollectorsEventNumber";
            this.textBoxCollectorsEventNumber.Size = new System.Drawing.Size(140, 19);
            this.textBoxCollectorsEventNumber.TabIndex = 2;
            // 
            // labelCollectorsEventNumberCaption
            // 
            this.labelCollectorsEventNumberCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelCollectorsEventNumberCaption.Location = new System.Drawing.Point(10, 62);
            this.labelCollectorsEventNumberCaption.Name = "labelCollectorsEventNumberCaption";
            this.labelCollectorsEventNumberCaption.Size = new System.Drawing.Size(59, 20);
            this.labelCollectorsEventNumberCaption.Text = "Event No.:";
            // 
            // buttonHabitat
            // 
            this.buttonHabitat.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonHabitat.Location = new System.Drawing.Point(124, 274);
            this.buttonHabitat.Name = "buttonHabitat";
            this.buttonHabitat.Size = new System.Drawing.Size(93, 20);
            this.buttonHabitat.TabIndex = 8;
            this.buttonHabitat.Text = "Habitat";
            this.buttonHabitat.Click += new System.EventHandler(this.buttonHabitat_Click);
            // 
            // buttonLocality
            // 
            this.buttonLocality.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonLocality.Location = new System.Drawing.Point(11, 274);
            this.buttonLocality.Name = "buttonLocality";
            this.buttonLocality.Size = new System.Drawing.Size(93, 20);
            this.buttonLocality.TabIndex = 6;
            this.buttonLocality.Text = "Locality";
            this.buttonLocality.Click += new System.EventHandler(this.buttonLocality_Click);
            // 
            // buttonMethod
            // 
            this.buttonMethod.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonMethod.Location = new System.Drawing.Point(11, 300);
            this.buttonMethod.Name = "buttonMethod";
            this.buttonMethod.Size = new System.Drawing.Size(93, 20);
            this.buttonMethod.TabIndex = 5;
            this.buttonMethod.Text = "Method";
            this.buttonMethod.Click += new System.EventHandler(this.buttonMethod_Click);
            // 
            // textBoxTimeSpan
            // 
            this.textBoxTimeSpan.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxTimeSpan.Location = new System.Drawing.Point(77, 185);
            this.textBoxTimeSpan.MaxLength = 50;
            this.textBoxTimeSpan.Name = "textBoxTimeSpan";
            this.textBoxTimeSpan.Size = new System.Drawing.Size(140, 19);
            this.textBoxTimeSpan.TabIndex = 29;
            // 
            // labelTimespan
            // 
            this.labelTimespan.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelTimespan.Location = new System.Drawing.Point(11, 186);
            this.labelTimespan.Name = "labelTimespan";
            this.labelTimespan.Size = new System.Drawing.Size(59, 20);
            this.labelTimespan.Text = "Timespan:";
            // 
            // labelDateCaption
            // 
            this.labelDateCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelDateCaption.Location = new System.Drawing.Point(10, 99);
            this.labelDateCaption.Name = "labelDateCaption";
            this.labelDateCaption.Size = new System.Drawing.Size(44, 20);
            this.labelDateCaption.Text = "Date:";
            // 
            // dateTimePickerCollectionEventDate
            // 
            this.dateTimePickerCollectionEventDate.CalendarFont = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.dateTimePickerCollectionEventDate.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.dateTimePickerCollectionEventDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerCollectionEventDate.Location = new System.Drawing.Point(60, 97);
            this.dateTimePickerCollectionEventDate.Name = "dateTimePickerCollectionEventDate";
            this.dateTimePickerCollectionEventDate.Size = new System.Drawing.Size(105, 25);
            this.dateTimePickerCollectionEventDate.TabIndex = 35;
            this.dateTimePickerCollectionEventDate.Visible = false;
            // 
            // buttonNewDate
            // 
            this.buttonNewDate.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonNewDate.Location = new System.Drawing.Point(171, 97);
            this.buttonNewDate.Name = "buttonNewDate";
            this.buttonNewDate.Size = new System.Drawing.Size(46, 22);
            this.buttonNewDate.TabIndex = 36;
            this.buttonNewDate.Text = "Edit";
            this.buttonNewDate.Click += new System.EventHandler(this.buttonNewDate_Click);
            // 
            // labelVersionCaption
            // 
            this.labelVersionCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelVersionCaption.Location = new System.Drawing.Point(10, 39);
            this.labelVersionCaption.Name = "labelVersionCaption";
            this.labelVersionCaption.Size = new System.Drawing.Size(59, 20);
            this.labelVersionCaption.Text = "Version:";
            // 
            // labelVersion
            // 
            this.labelVersion.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelVersion.Location = new System.Drawing.Point(78, 39);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(140, 20);
            this.labelVersion.Text = "Version";
            // 
            // labelDateSupplement
            // 
            this.labelDateSupplement.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelDateSupplement.Location = new System.Drawing.Point(10, 136);
            this.labelDateSupplement.Name = "labelDateSupplement";
            this.labelDateSupplement.Size = new System.Drawing.Size(59, 19);
            this.labelDateSupplement.Text = "Date Sup.:";
            // 
            // textBoxDateSupplement
            // 
            this.textBoxDateSupplement.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxDateSupplement.Location = new System.Drawing.Point(77, 133);
            this.textBoxDateSupplement.MaxLength = 50;
            this.textBoxDateSupplement.Name = "textBoxDateSupplement";
            this.textBoxDateSupplement.Size = new System.Drawing.Size(140, 19);
            this.textBoxDateSupplement.TabIndex = 44;
            // 
            // textBoxRefTitle
            // 
            this.textBoxRefTitle.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxRefTitle.Location = new System.Drawing.Point(77, 211);
            this.textBoxRefTitle.MaxLength = 50;
            this.textBoxRefTitle.Name = "textBoxRefTitle";
            this.textBoxRefTitle.Size = new System.Drawing.Size(140, 19);
            this.textBoxRefTitle.TabIndex = 47;
            // 
            // labelRefTitle
            // 
            this.labelRefTitle.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelRefTitle.Location = new System.Drawing.Point(11, 211);
            this.labelRefTitle.Name = "labelRefTitle";
            this.labelRefTitle.Size = new System.Drawing.Size(59, 20);
            this.labelRefTitle.Text = "Ref. Title:";
            // 
            // textBoxCountryCache
            // 
            this.textBoxCountryCache.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxCountryCache.Location = new System.Drawing.Point(77, 238);
            this.textBoxCountryCache.MaxLength = 50;
            this.textBoxCountryCache.Name = "textBoxCountryCache";
            this.textBoxCountryCache.Size = new System.Drawing.Size(140, 19);
            this.textBoxCountryCache.TabIndex = 50;
            // 
            // labelCountryCache
            // 
            this.labelCountryCache.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelCountryCache.Location = new System.Drawing.Point(11, 237);
            this.labelCountryCache.Name = "labelCountryCache";
            this.labelCountryCache.Size = new System.Drawing.Size(59, 20);
            this.labelCountryCache.Text = "Country:";
            // 
            // buttonNotes
            // 
            this.buttonNotes.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonNotes.Location = new System.Drawing.Point(124, 300);
            this.buttonNotes.Name = "buttonNotes";
            this.buttonNotes.Size = new System.Drawing.Size(93, 20);
            this.buttonNotes.TabIndex = 52;
            this.buttonNotes.Text = "Notes";
            this.buttonNotes.Click += new System.EventHandler(this.buttonNotes_Click);
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.labelCEText);
            this.panelTop.Controls.Add(this.pictureBoxFormImage);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(235, 32);
            // 
            // labelCEText
            // 
            this.labelCEText.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelCEText.Location = new System.Drawing.Point(46, 6);
            this.labelCEText.Name = "labelCEText";
            this.labelCEText.Size = new System.Drawing.Size(171, 20);
            this.labelCEText.Text = "Text";
            this.labelCEText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pictureBoxFormImage
            // 
            this.pictureBoxFormImage.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBoxFormImage.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxFormImage.Image")));
            this.pictureBoxFormImage.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxFormImage.Name = "pictureBoxFormImage";
            this.pictureBoxFormImage.Size = new System.Drawing.Size(40, 32);
            this.pictureBoxFormImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonCancel.Location = new System.Drawing.Point(125, 326);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(92, 20);
            this.buttonCancel.TabIndex = 63;
            this.buttonCancel.Text = "Cancel";
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonOk.Location = new System.Drawing.Point(11, 326);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(92, 20);
            this.buttonOk.TabIndex = 62;
            this.buttonOk.Text = "OK";
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // CollectionEventForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoScrollMargin = new System.Drawing.Size(5, 10);
            this.ClientSize = new System.Drawing.Size(235, 372);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.buttonNotes);
            this.Controls.Add(this.textBoxCountryCache);
            this.Controls.Add(this.labelCountryCache);
            this.Controls.Add(this.textBoxRefTitle);
            this.Controls.Add(this.labelRefTitle);
            this.Controls.Add(this.labelDateSupplement);
            this.Controls.Add(this.textBoxDateSupplement);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.labelVersionCaption);
            this.Controls.Add(this.buttonNewDate);
            this.Controls.Add(this.dateTimePickerCollectionEventDate);
            this.Controls.Add(this.labelDateCaption);
            this.Controls.Add(this.textBoxTimeSpan);
            this.Controls.Add(this.labelTimespan);
            this.Controls.Add(this.buttonHabitat);
            this.Controls.Add(this.buttonLocality);
            this.Controls.Add(this.buttonMethod);
            this.Controls.Add(this.textBoxCollectionTime);
            this.Controls.Add(this.labelCollectionTimeCaption);
            this.Controls.Add(this.textBoxCollectorsEventNumber);
            this.Controls.Add(this.labelCollectorsEventNumberCaption);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "CollectionEventForm";
            this.Text = "Diversity Mobile";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.CollectionEventForm_Closing);
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxCollectionTime;
        private System.Windows.Forms.Label labelCollectionTimeCaption;
        private System.Windows.Forms.TextBox textBoxCollectorsEventNumber;
        private System.Windows.Forms.Label labelCollectorsEventNumberCaption;
        private System.Windows.Forms.Button buttonHabitat;
        private System.Windows.Forms.Button buttonLocality;
        private System.Windows.Forms.Button buttonMethod;
        private System.Windows.Forms.TextBox textBoxTimeSpan;
        private System.Windows.Forms.Label labelTimespan;
        private System.Windows.Forms.Label labelDateCaption;
        private System.Windows.Forms.DateTimePicker dateTimePickerCollectionEventDate;
        private System.Windows.Forms.Button buttonNewDate;
        private System.Windows.Forms.Label labelVersionCaption;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelDateSupplement;
        private System.Windows.Forms.TextBox textBoxDateSupplement;
        private System.Windows.Forms.TextBox textBoxRefTitle;
        private System.Windows.Forms.Label labelRefTitle;
        private System.Windows.Forms.TextBox textBoxCountryCache;
        private System.Windows.Forms.Label labelCountryCache;
        private System.Windows.Forms.Button buttonNotes;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label labelCEText;
        private System.Windows.Forms.PictureBox pictureBoxFormImage;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
    }
}