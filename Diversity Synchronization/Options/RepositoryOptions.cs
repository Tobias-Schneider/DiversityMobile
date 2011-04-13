using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Diversity_Synchronization.Options
{
    public class RepositoryOptions : IComparable
    {
        private string connectionName;
        private bool sqlAuthentification;
        private string lastUsername;
        private string ipAddress;
        private string ipPort;
        private string initialCatalogPath;
        private string taxonNamesInitialCatalogPath;

        public RepositoryOptions() {
            //Default-Werte
            this.connectionName = "Standard";
            this.sqlAuthentification = true;
            this.lastUsername = "";
            this.ipAddress = "127.0.0.1";
            this.ipPort = "";
            this.initialCatalogPath = "";
            this.taxonNamesInitialCatalogPath = "";
        }

        #region Properties

        public string ConnectionName
        {
            get
            {
                return connectionName;
            }
            set
            {
                connectionName = value;
            }
        }

        public bool SqlAuthentification
        {
            get
            {
                return sqlAuthentification;
            }
            set
            {
                sqlAuthentification = value;
            }
        }

        public string LastUsername
        {
            get
            {
                return lastUsername;
            }
            set
            {
                lastUsername = value;
            }
        }

        public string IPAddress
        {
            get
            {
                return ipAddress;
            }
            set
            {
                ipAddress = value;
            }
        }

        public string IPPort
        {
            get
            {
                return ipPort;
            }
            set
            {
                ipPort = value;
            }
        }

        public string InitialCatalog
        {
            get
            {
                return initialCatalogPath;
            }
            set
            {
                initialCatalogPath = value;
            }
        }

        public string TaxonNamesInitialCatalog
        {
            get
            {
                return taxonNamesInitialCatalogPath;
            }
            set
            {
                taxonNamesInitialCatalogPath = value;
            }
        }

        #endregion

        public int CompareTo(object obj)
        {
            RepositoryOptions sOp = null;
            if (obj is RepositoryOptions)
            {
                sOp = obj as RepositoryOptions;
            }
            else
            {
                return 1;
            }

            if (sOp != null)
            {
                if (this.connectionName.CompareTo(sOp.connectionName) != 0)
                {
                    return 1;
                }
                else
                {
                    if (this.sqlAuthentification.CompareTo(sOp.sqlAuthentification) != 0)
                    {
                        return 1;
                    }
                    else
                    {
                        if (this.lastUsername.CompareTo(sOp.lastUsername) != 0)
                        {
                            return 1;
                        }
                        else
                        {
                            if (this.ipAddress.CompareTo(sOp.ipAddress) != 0)
                            {
                                return 1;
                            }
                            else
                            {
                                if (this.ipPort.CompareTo(sOp.ipPort) != 0)
                                {
                                    return 1;
                                }
                                else
                                {
                                    if (this.initialCatalogPath.CompareTo(sOp.initialCatalogPath) != 0)
                                    {
                                        return 1;
                                    }
                                    else
                                    {
                                        if (this.taxonNamesInitialCatalogPath.CompareTo(sOp.taxonNamesInitialCatalogPath) != 0)
                                        {
                                            return 1;
                                        }
                                        else
                                        {
                                            return 0;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return 1;
        }
    }
}
