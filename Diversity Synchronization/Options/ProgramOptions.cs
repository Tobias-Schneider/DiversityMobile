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
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Diversity_Synchronization.Options.Serializer;

namespace Diversity_Synchronization.Options
{
    public class ProgramOptions : IComparable
    {
        private string languageFile;
        private bool hidePassword;

        public ProgramOptions()
        {
            //Default-Werte
            languageFile = "english.lang";
            hidePassword = true;
        }

        #region Properties

        

        public string LanguageFile
        {
            get
            {                
                return languageFile;
            }
            set
            {
                languageFile = value;
            }
        }

        public bool HidePassword
        {
            get
            {
                return hidePassword;
            }
            set
            {
                hidePassword = value;
            }
        }

        #endregion

        public int CompareTo(object obj)
        {
            ProgramOptions sOp = null;
            if (obj is ProgramOptions)
            {
                sOp = obj as ProgramOptions;
            }
            else
            {
                return 1;
            }

            if (sOp != null)
            {
                if (this.languageFile.CompareTo(sOp.languageFile) != 0)
                {
                    return 1;
                }
                else
                {
                    if (this.hidePassword.CompareTo(sOp.hidePassword) != 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }

            return 1;
        }
    }
}
