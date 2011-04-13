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

using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer;
using UBT.AI4.Bio.DivMobi.DataLayer;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Restrictions;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer.Collections;

namespace UBT.AI4.Bio.DivMobi.DataManagement
{
    public class UserProfiles : ConnectedType
    {
        #region Member data
        private UserProfile _currentProfile = null;
        private Connector con = SERIALIZER.Connector;
        private IList<UserProfile> _userList;

        #endregion

        #region Singleton

        private static UserProfiles _instance = null;

        public static UserProfiles Instance
        {
            get
            {
                if (_instance == null)
                {
                    try
                    {
                        _instance = new UserProfiles();
                    }
                    catch (ConnectionCorruptedException ex)
                    {
                        throw new ConnectionCorruptedException("Instance of UserProfiles couldn't be retrieved. (" + ex.Message + ")");
                    }
                }

                return _instance;
            }
        }

        #endregion

        #region Constructor

        private UserProfiles()
        {
            IRestriction restrict = RestrictionFactory.TypeRestriction(typeof(UserProfile));
            _userList = con.LoadList<UserProfile>(restrict);
            this._currentProfile = SERIALIZER.CreateISerializableObject<UserProfile>();
        }

        #endregion

        #region Properties

        public UserProfile Current
        {
            get
            {
                return this._currentProfile;
            }
            set
            {
                this._currentProfile = value;
            }
        }

        public IList<UserProfile> List
        {
            get
            {
                try
                {
                    IRestriction restrict = RestrictionFactory.TypeRestriction(typeof(UserProfile));
                    _userList = con.LoadList<UserProfile>(restrict);
                }
                catch (Exception)
                {
                    // Error while loading current User List
                    this._userList = null;
                }

                return this._userList;
            }
        }

        #endregion

        #region Data Functions
        
        public UserProfile CreateUserProfile(string loginName)
        {
            UserProfile up = SERIALIZER.CreateISerializableObject<UserProfile>();

            up.LoginName = loginName;

            try
            {
                con.Save(up);
            }
            catch (Exception ex)
            {
                throw new UserProfileCorruptedException("New User Profile '" + loginName + "' couldn't be created. (" + ex.Message + ")");
            }

            return up;
        }

        public void Remove(UserProfile up)
        {
            if (up != null)
            {
                try
                {
                    con.Delete(up);
                }
                catch (Exception ex)
                {
                    throw new UserProfileCorruptedException("User Profile couldn't be deleted.");
                }
            }
        }

        public void Update(UserProfile up)
        {
            if (up != null)
            {
                try
                {
                    con.Save(up);
                }
                catch (Exception ex)
                {
                    throw new UserProfileCorruptedException("User Profile couldn't be updated. ("+ex.Message+")");
                }
            }
        }

        public UserProfile RetrieveUserProfile(int UserProfileID)
        {
            IRestriction restrict = RestrictionFactory.Eq(typeof(UserProfile), "_UserProfileID", UserProfileID);
            UserProfile up = null;
            try
            {
                up = con.Load<UserProfile>(restrict);
            }
            catch (Exception)
            {
                throw new UserProfileCorruptedException("User Profile for ID: " + UserProfileID +" couldn't be loaded.");
            }
            return up;
        }

        public UserProfile LoadUserProfile(int index)
        {
            if (this._userList == null || this._userList.Count == 0)
            {
                throw new UserProfileCorruptedException("No user in current user-list.");
            }

            if(index < 0 || index > this._userList.Count-1)
            {
                throw new UserProfileCorruptedException("Incorrect selection of user.");
            }

            UserProfile up = this._userList[index];

            if(up == null)
                throw new UserProfileCorruptedException("User data is read incorrectly.");

            //Wenn im Userprofil keine Angaben gemacht wurden, lade Default-Werte
            if (up.Context == null)
                up.Context = "Monitoring";
            if (up.LanguageContext == null)
                up.LanguageContext = "English";
            if (up.StopGPS == null)
                up.StopGPS = false;
            if (up.DefaultIUGeographyAnalysis == null)
                up.DefaultIUGeographyAnalysis = false;
            if (up.Displaylevel.Index == null)
                up.Displaylevel = new DisplayLevel("Specimen");
            if (up.ToolbarIcons == null)
                up.ToolbarIcons = "small";
            
            //Schwere Fehler, die einenen Start verhindern müssen)
            if (up.LoginName == null)
                throw new UserProfileCorruptedException("No Login Name defined.");
            if (up.ProjectID == null)
                throw new UserProfileCorruptedException("No Project ID defined.");
            if (up.UserProfileID == null)
                throw new UserProfileCorruptedException("No User Profile defined.");
            return up;
        }

        #endregion
    }
}
