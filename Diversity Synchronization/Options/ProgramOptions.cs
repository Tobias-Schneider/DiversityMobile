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
