﻿//#######################################################################
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

namespace UBT.AI4.Bio.DivMobi.DataItemFormTools
{
    public class TreeViewNodeData
    {
        public int? ID;
        public string CollectorsName;
        public int IdentificationUnitID;
        public int CollectionSpecimenID;
        public int AnalysisID;
        public string AnalysisNumber;
        public DateTime AnalysisDate;
        public TreeViewNodeTypes NodeType;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeViewNodeData"/> class.
        /// </summary>
        /// <param name="ID">The ID.</param>
        /// <param name="NodeType">Type of the node.</param>
        public TreeViewNodeData(int? ID, TreeViewNodeTypes NodeType)
        {
            this.ID = ID;
            if (NodeType == null)
                this.NodeType = TreeViewNodeTypes.Unknown;
            else
                this.NodeType = NodeType;
        }

        public TreeViewNodeData(string collectorsName, int CollectionSpecimenID)
        {
            this.CollectorsName = collectorsName;
            this.CollectionSpecimenID = CollectionSpecimenID;
            this.NodeType = TreeViewNodeTypes.AgentNode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeViewNodeData"/> class.
        /// </summary>
        /// <param name="IdentificationUnitID">The identification unit ID.</param>
        /// <param name="CollectionSpecimenID">The collection specimen ID.</param>
        /// <param name="AnalysisID">The analysis ID.</param>
        /// <param name="AnalysisNumber">The analysis number.</param>
        public TreeViewNodeData(int IdentificationUnitID, int CollectionSpecimenID, int AnalysisID, string AnalysisNumber)
        {
            this.NodeType = TreeViewNodeTypes.AnalysisNode;
            this.IdentificationUnitID = IdentificationUnitID;
            this.CollectionSpecimenID = CollectionSpecimenID;
            this.AnalysisID = AnalysisID;
            this.AnalysisNumber = AnalysisNumber;
        }
        
        public TreeViewNodeData(int IdentificationUnitID, int CollectionSpecimenID, DateTime date)
        {
            this.NodeType = TreeViewNodeTypes.GeographyNode;
            this.IdentificationUnitID = IdentificationUnitID;
            this.CollectionSpecimenID = CollectionSpecimenID;
            this.AnalysisDate = date;
        }
    }
}
