using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Restrictions;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer.Collections;

namespace UBT.AI4.Bio.DivMobi.DataItemFormTools
{
    public class TreeViewOperations
    {
        #region Member
        private Connector con;
        public ExpandLevel expandLvl;
        #endregion

        #region Constructor

        public TreeViewOperations(ExpandLevel lvl, Connector con)
        {
            if (con == null)
                throw new TreeViewOperationsException("No connector defined!");
            else
                this.con = con;

            if (lvl != null)
                this.expandLvl = lvl;
            else
                this.expandLvl = ExpandLevel.Event;
        }

        #endregion

        #region Treeview
        

        public TreeNode displayTreeAround(ISerializableObject iso)
        {
            TreeNode isoNode = null;           

            if (iso is IdentificationUnit)
            {
                IdentificationUnit iu = iso as IdentificationUnit;
                isoNode = displayTopDownIdentificationUnit(iu);

                if (iu.RelatedUnit != null)
                    isoNode = displayBottomUpIdentificationUnit(iu.RelatedUnit, isoNode);
                else
                {
                    if (iu.CollectionSpecimen != null)
                        displayBottomUpSpecimen(iu.CollectionSpecimen, isoNode);                    
                }
            }

            if (iso is CollectionEventSeries)
            {
                isoNode = displayTopDownEventSeries(iso as CollectionEventSeries);
            }
            
            return isoNode;
        }
        
        #region BottomUp

        public TreeNode displayBottomUpIdentificationUnitGeoAnalysis(IdentificationUnitGeoAnalysis iuga)
        {
            TreeNode iugaNode = new TreeNode();
            TreeNode subRootNode = null;

            if (iuga != null)
            {
                parameterizeIUGeoANode(iuga, iugaNode);
                IdentificationUnit iu = iuga.IdentificationUnit;
                if (iu != null)
                {
                    subRootNode = displayBottomUpIdentificationUnit(iu, iugaNode);
                }
                if (subRootNode != null)
                {
                    return subRootNode;
                }
            }
            return iugaNode;
        }

        public TreeNode displayBottomUpIdentificationUnitAnalysis(IdentificationUnitAnalysis iua)
        {
            TreeNode iuaNode = new TreeNode();
            TreeNode subRootNode = null;
            if (iua != null)
            {
                parametrizeIUANode(iua, iuaNode);
                IdentificationUnit iu = iua.IdentificationUnit;
                if (iu != null)
                {
                    subRootNode = displayBottomUpIdentificationUnit(iu, iuaNode);
                }
                if (subRootNode != null)
                {
                    return subRootNode;
                }
            }
            return iuaNode;
        }

        public TreeNode displayBottomUpIdentificationUnit(IdentificationUnit iu)
        {
            TreeNode iuNode = new TreeNode();
            TreeNode subRootNode = null;
            if (iu != null)
            {
                parametrizeIUNode(iu, iuNode);
                IdentificationUnit parent = iu.RelatedUnit;
                if (parent != null)
                {
                    subRootNode = displayBottomUpIdentificationUnit(parent, iuNode);
                }
                else
                {
                    CollectionSpecimen spec = iu.CollectionSpecimen;
                    if (spec != null)
                        subRootNode = displayBottomUpSpecimen(spec, iuNode);
                }
                if (subRootNode != null)
                {
                    return subRootNode;
                }
            }
            return iuNode;
        }

        public TreeNode displayBottomUpIdentificationUnit(IdentificationUnit iu, TreeNode childNode)
        {
            TreeNode iuNode = new TreeNode();
            TreeNode subRootNode = null;
            if (iu != null)
            {
                parametrizeIUNode(iu, iuNode);
                IdentificationUnit parent = iu.RelatedUnit;

                if(childNode != null)
                    iuNode.Nodes.Add(childNode);

                if (parent != null)
                {
                    subRootNode = displayBottomUpIdentificationUnit(parent, iuNode);
                }
                else
                {
                    CollectionSpecimen spec = iu.CollectionSpecimen;
                    subRootNode = displayBottomUpSpecimen(spec, iuNode);
                }
                if (subRootNode != null)
                {
                    return subRootNode;
                }
            }
            return iuNode;
        }

        public TreeNode displayBottomUpSpecimen(CollectionSpecimen specimen)
        {
            TreeNode specNode = new TreeNode();
            TreeNode subRootNode = null;

            if (specimen != null)
            {
                parametrizeOnlySpecimenNode(specimen, specNode);
                CollectionEvent ev = specimen.CollectionEvent;
                if (ev != null)
                {
                    subRootNode = displayBottomUpEvent(ev, specNode);
                    return subRootNode;
                }
            }
            return specNode;
        }

        public TreeNode displayBottomUpSpecimen(CollectionSpecimen specimen, TreeNode childNode)
        {
            TreeNode specNode = new TreeNode();
            TreeNode subRootNode = null;
            if (specimen != null)
            {
                parametrizeOnlySpecimenNode(specimen, specNode);
                if(childNode != null)
                    specNode.Nodes.Add(childNode);

                CollectionEvent ev = specimen.CollectionEvent;
                if (ev != null)
                {
                    subRootNode = displayBottomUpEvent(ev, specNode);
                    return subRootNode;
                }
            }
            return specNode;
        }

        public TreeNode displayBottomUpEvent(CollectionEvent ev)
        {
            TreeNode evNode = new TreeNode();
            TreeNode subRootNode = null;
            if (ev != null)
            {
                parametrizeEventNode(ev, evNode);
                //Property und LocalisationSystem werden beim parametrisieren eingebunden
                CollectionEventSeries es = ev.CollectionEventSeries;
                subRootNode = displayBottomUpEventSeries(es, evNode);
            }
            return subRootNode;          
        }

        public TreeNode displayBottomUpEvent(CollectionEvent ev, TreeNode childNode)
        {
            TreeNode eventNode = new TreeNode();
            TreeNode subRootNode = null;
            if (ev != null)
            {
                parametrizeEventNode(ev, eventNode);
                if(childNode != null)
                    eventNode.Nodes.Add(childNode);

                //Property und LocalisationSystem werden beim parametrisieren eingebunden
                CollectionEventSeries es = ev.CollectionEventSeries;
                subRootNode = displayBottomUpEventSeries(es, eventNode);
            }
            return subRootNode;            
        }

        public  TreeNode displayBottomUpEventSeries(CollectionEventSeries es, TreeNode childNode)
        {
            TreeNode esNode = new TreeNode();

            // CollectionEventSeries es darf null-Wert sein, wenn No-Event-Series gemeint ist
            parametrizeEventSeriesNode(es, esNode);

            if(childNode != null)
                esNode.Nodes.Add(childNode);
            
            return esNode;
        }

        #endregion

        #region Top-Down

        public TreeNode displayTopDownEventSeries(CollectionEventSeries es)
        {
            TreeNode esNode = new TreeNode();
            if (es != null)
            {
                parametrizeEventSeriesNode(es, esNode);
                if (es.CollectionEvents.First() != null)
                {
                    CollectionEvent first = es.CollectionEvents.First();
                    esNode.Nodes.Add(displayTopDownEvent(first));
                    while (es.CollectionEvents.HasNext())
                    {
                        CollectionEvent next = es.CollectionEvents.Next();
                        esNode.Nodes.Add(displayTopDownEvent(next));
                    }
                }
            }
            else
            {
                parametrizeEmptyEventSeriesNode(esNode);

                if (con != null)
                {
                    IRestriction r = RestrictionFactory.Eq(typeof(CollectionEvent), "_SeriesID", null);

                    IList<CollectionEvent> events = con.LoadList<CollectionEvent>(r);
                    foreach (CollectionEvent ev in events)
                    {
                        if(ev != null)
                            displayTopDownEvent(ev);
                    }
                }
            }
            return esNode;
        }

        public  TreeNode displayTopDownEvent(CollectionEvent ev)
        {
            TreeNode eventNode = new TreeNode();
            if (ev != null)
            {
                parametrizeEventNode(ev, eventNode);
                if (ev.CollectionSpecimen.First() != null)
                {
                    eventNode.Nodes.Add(displayTopDownSpecimen(ev.CollectionSpecimen.First()));
                    while (ev.CollectionSpecimen.HasNext())
                    {
                        CollectionSpecimen next = ev.CollectionSpecimen.Next();
                        eventNode.Nodes.Add(displayTopDownSpecimen(next));
                    }
                }
            }
            return eventNode;
        }

        public TreeNode displayTopDownSpecimen(CollectionSpecimen spec)
        {
            TreeNode specimenNode = new TreeNode();
            if (spec != null)
            {
                parametrizeOnlySpecimenNode(spec, specimenNode);
                IDirectAccessIterator<IdentificationUnit> ius = spec.IdentificationUnits;
                if (ius.First() != null)
                {
                    IdentificationUnit iu = ius.First();
                    if (iu.RelatedUnit == null)
                        specimenNode.Nodes.Add(displayTopDownIdentificationUnit(iu));
                    while (ius.HasNext())
                    {
                        IdentificationUnit next = ius.Next();
                        if (next.RelatedUnit == null)
                            specimenNode.Nodes.Add(displayTopDownIdentificationUnit(next));
                    }
                }
            }
            return specimenNode;
        }

        public TreeNode displayTopDownIdentificationUnit(IdentificationUnit iUnit)
        {
            TreeNode iuNode = new TreeNode();
            if (iUnit != null)
            {
                parametrizeIUNode(iUnit, iuNode);
                if (iUnit.ChildUnits.First() != null)
                {
                    iuNode.Nodes.Add(displayTopDownIdentificationUnit(iUnit.ChildUnits.First()));
                    while (iUnit.ChildUnits.HasNext())
                    {
                        IdentificationUnit next = iUnit.ChildUnits.Next();
                        iuNode.Nodes.Add(displayTopDownIdentificationUnit(next));
                    }
                }
            }
            return iuNode;
        }

        public TreeNode displayTopDownIUA(IdentificationUnitAnalysis iua)
        {
            TreeNode iuaNode = new TreeNode();
            if(iua != null)
                parametrizeIUANode(iua, iuaNode);

            return iuaNode;
        }

        public TreeNode displayTopDownIUGeoA(IdentificationUnitGeoAnalysis iuga)
        {
            TreeNode iuaNode = new TreeNode();
            if(iuga != null)
                parameterizeIUGeoANode(iuga, iuaNode);

            return iuaNode;
        }

        public TreeNode displayListTopDown(List<ISerializableObject> objects, ISerializableObject root)
        {
            TreeNode tn = new TreeNode();
            if (root != null)
            {
                if (root.GetType().Equals(typeof(CollectionEventSeries)))
                {
                    CollectionEventSeries cs = (CollectionEventSeries)root;
                    parametrizeEventSeriesNode(cs, tn);
                    foreach (ISerializableObject iso in objects)
                    {
                        if (iso != null)
                        {
                            if (iso.GetType().Equals(typeof(CollectionEvent)))
                            {
                                tn.Nodes.Add(displayListTopDown(objects, iso));
                            }
                        }
                    }
                }
                if (root.GetType().Equals(typeof(CollectionEvent)))
                {
                    CollectionEvent ce = (CollectionEvent)root;
                    parametrizeEventNode(ce, tn, objects);
                    foreach (ISerializableObject iso in objects)
                    {
                        if (iso != null)
                        {
                            if (iso.GetType().Equals(typeof(CollectionSpecimen)))
                                tn.Nodes.Add(displayListTopDown(objects, iso));
                        }
                    }
                }
                if (root.GetType().Equals(typeof(CollectionSpecimen)))
                {
                    CollectionSpecimen spec = (CollectionSpecimen)root;
                    parametrizeSpecimenNode(spec, tn, objects);
                    foreach (ISerializableObject iso in objects)
                    {
                        if (iso != null)
                        {
                            if (iso.GetType().Equals(typeof(IdentificationUnit)))
                            {
                                IdentificationUnit iu = (IdentificationUnit)iso;
                                if (iu.CollectionSpecimenID == spec.CollectionSpecimenID && iu.RelatedUnitID == null)
                                    tn.Nodes.Add(displayListTopDown(objects, iu));
                            }
                        }
                    }
                }
                if (root.GetType().Equals(typeof(IdentificationUnit)))
                {
                    IdentificationUnit iu = (IdentificationUnit)root;
                    parametrizeIUNode(iu, tn);//Muss nicht auf die Liste angewendet werden, da weder ein Iterator verwendet wird noch auf die DB zugegriffen wird)
                    foreach (ISerializableObject iso in objects)
                    {
                        if (iso != null)
                        {
                            /*if (iso.GetType().Equals(typeof(IdentificationUnitAnalysis)))
                            {
                                IdentificationUnitAnalysis iua = (IdentificationUnitAnalysis)iso;
                                if (iua.IdentificationUnitID == iu.IdentificationUnitID)
                                {
                                    TreeNode analysisNode = new TreeNode();
                                    parametrizeIUANode(iua, analysisNode);
                                    tn.Nodes.Add(analysisNode);
                                }
                            }
                            else*/ if (iso.GetType().Equals(typeof(IdentificationUnit)))
                            {
                                IdentificationUnit childUnit = (IdentificationUnit)iso;
                                if (childUnit.RelatedUnitID == iu.IdentificationUnitID)
                                {
                                    tn.Nodes.Add(displayListTopDown(objects, childUnit));
                                }
                            }
                        }
                    }
                }
            }
            return tn;
        }
        #endregion

        #endregion

        #region Node Parametrization

        public void parameterizeIUGeoANode(IdentificationUnitGeoAnalysis iuGeoAnalysis, TreeNode node)
        {
            //node.Text = iuGeoAnalysis.Geography + " (" + iuGeoAnalysis.AnalysisDate.ToShortDateString() + ")";
            //node.Text = iuGeoAnalysis.Geography + " (" + iuGeoAnalysis.AnalysisDate + ")";
            if (iuGeoAnalysis != null && node != null)
            {
                StringBuilder sb = new StringBuilder();
                if (iuGeoAnalysis.GeoLatitude != null && iuGeoAnalysis.GeoLongitude != null)
                {
                    String latD = null;
                    Double lat = (double)iuGeoAnalysis.GeoLatitude;
                    if (lat >= 0)
                        latD = "N ";
                    else
                        latD = "S ";
                    Double lon = (double)iuGeoAnalysis.GeoLongitude;
                    String lonD = null;
                    if (lon >= 0)
                        lonD = "O ";
                    else
                        lonD = "W ";
                    sb.Append(TreeViewOperations.dec2degree(lat)).Append(latD).Append(@";").Append(TreeViewOperations.dec2degree(lon)).Append(lonD);
                    node.Text = sb.ToString();
                }
                else
                {
                    node.Text = (String) iuGeoAnalysis.Geography;
                }
                node.ImageIndex = node.SelectedImageIndex = (int)TreeViewIconIndex.Geography;
                if (node.Tag == null)
                {
                    node.Tag = new TreeViewNodeData(iuGeoAnalysis.IdentificationUnitID, iuGeoAnalysis.CollectionSpecimenID, iuGeoAnalysis.AnalysisDate);
                }
            }
        }

        public void parametrizeIUANode(IdentificationUnitAnalysis iua, TreeNode iuaNode)
        {
            if (iua != null && iuaNode != null)
            {
                Analysis analysis = iua.Analysis;

                StringBuilder text = new StringBuilder();

                text.Append(iua.AnalysisResult);

                if (analysis.MeasurementUnit != null && !analysis.MeasurementUnit.Equals(String.Empty))
                {
                    text.Append(" ");
                    text.Append(analysis.MeasurementUnit);
                }
                text.Append(": ");
                text.Append(analysis.DisplayText);

                iuaNode.Text = text.ToString();

                iuaNode.ImageIndex = iuaNode.SelectedImageIndex = (int)TreeViewIconIndex.Analysis;
                if (iuaNode.Tag == null)
                    iuaNode.Tag = new TreeViewNodeData(iua.IdentificationUnit.IdentificationUnitID, iua.IdentificationUnit.CollectionSpecimenID, iua.AnalysisID, iua.AnalysisNumber);
            }
        }

        public void parametrizeOnlyIUNode(IdentificationUnit iu, TreeNode iuNode)
        {
            if (iu != null && iuNode != null)
            {
                StringBuilder sb = new StringBuilder();
                if (iu.UnitIdentifier != null)
                {
                    sb.Append("(");
                    sb.Append(iu.UnitIdentifier);
                    sb.Append(") ");
                }
                if (iu.UnitDescription != null)
                    sb.Append(iu.UnitDescription).Append(", ");
                if (iu.LastIdentificationCache != null)
                    sb.Append(iu.LastIdentificationCache);
                iuNode.Text = sb.ToString();

                iuNode.Tag = new TreeViewNodeData((int)iu.IdentificationUnitID, TreeViewNodeTypes.IdentificationUnitNode);
                iuNode.ImageIndex = 0;
                iuNode.SelectedImageIndex = 0;

                if (iu.TaxonomicGroup != null)
                {
                    switch (iu.TaxonomicGroup.ToLower())
                    {
                        case "plant":
                            if (iu.UnitDescription != null)
                            {
                                switch (iu.UnitDescription.ToLower())
                                {
                                    case "tree":
                                        iuNode.ImageIndex = (int)TreeViewIconIndex.Tree;
                                        iuNode.SelectedImageIndex = (int)TreeViewIconIndex.Tree;
                                        break;
                                    case "branch":
                                        iuNode.ImageIndex = (int)TreeViewIconIndex.Branch;
                                        iuNode.SelectedImageIndex = (int)TreeViewIconIndex.Branch;
                                        break;
                                    case "leaf":
                                        iuNode.ImageIndex = (int)TreeViewIconIndex.Plant;
                                        iuNode.SelectedImageIndex = (int)TreeViewIconIndex.Plant;
                                        break;
                                    case "gall":
                                        iuNode.ImageIndex = (int)TreeViewIconIndex.Other;
                                        iuNode.SelectedImageIndex = (int)TreeViewIconIndex.Other;
                                        break;
                                    default:
                                        iuNode.ImageIndex = (int)TreeViewIconIndex.Plant;
                                        iuNode.SelectedImageIndex = (int)TreeViewIconIndex.Plant;
                                        break;
                                }
                            }
                            else
                            {
                                iuNode.ImageIndex = (int)TreeViewIconIndex.Plant;
                                iuNode.SelectedImageIndex = (int)TreeViewIconIndex.Plant;
                            }
                            break;
                        default:
                            int index;
                            try
                            {
                                index = (int)Enum.Parse(typeof(TreeViewIconIndex), iu.TaxonomicGroup.ToLower(), true);
                            }
                            catch (Exception)
                            {
                                index = (int)TreeViewIconIndex.Unknown;
                            }
                            iuNode.ImageIndex = index;
                            iuNode.SelectedImageIndex = index;
                            break;
                    }
                }
                if(this.expandLvl==ExpandLevel.IdentificationUnit&&iu.RelatedUnit==null)
                    iuNode.BackColor = System.Drawing.Color.BlanchedAlmond;
            }
        }

        public void parametrizeIUNode(IdentificationUnit iu, TreeNode iuNode)
        {
            if (iu != null && iuNode != null)
            {
                parametrizeOnlyIUNode(iu, iuNode);
                IdentificationUnitGeoAnalysis iugae = iu.IdentificationUnitGeoAnalysis.First();
                Identification id = iu.Identifications.First();
                IdentificationUnit firstBorn = iu.ChildUnits.First();
                IdentificationUnitAnalysis iuana = iu.IdentificationUnitAnalysis.First();
                 if (iu.IdentificationUnitGeoAnalysis.First() != null)
                {
                    IdentificationUnitGeoAnalysis iuga = iu.IdentificationUnitGeoAnalysis.First();
                    TreeNode iugaNode = new TreeNode();
                    parameterizeIUGeoANode(iuga, iugaNode);
                    iuNode.Nodes.Add(iugaNode);
                    while (iu.IdentificationUnitGeoAnalysis.HasNext())
                    {
                        iuga = iu.IdentificationUnitGeoAnalysis.Next();
                        iugaNode = new TreeNode();
                        this.parameterizeIUGeoANode(iuga, iugaNode);
                        iuNode.Nodes.Add(iugaNode);
                    }
                }
                IdentificationUnitAnalysis ana = iu.IdentificationUnitAnalysis.First();
                if (iu.IdentificationUnitAnalysis.First() != null)
                {
                    IdentificationUnitAnalysis iua = iu.IdentificationUnitAnalysis.First();
                    TreeNode iuaNode = new TreeNode();
                    parametrizeIUANode(iua, iuaNode);
                    iuNode.Nodes.Add(iuaNode);
                    while (iu.IdentificationUnitAnalysis.HasNext())
                    {
                        iua = iu.IdentificationUnitAnalysis.Next();
                        iuaNode = new TreeNode();
                        parametrizeIUANode(iua, iuaNode);
                        iuNode.Nodes.Add(iuaNode);
                    }
                }
            }
        }

        public void parametrizeOnlySpecimenNode(CollectionSpecimen spec, TreeNode specimenNode)
        {
            if (spec != null && specimenNode != null)
            {
                if (spec.AccessionNumber != null && !spec.AccessionNumber.Equals(String.Empty))
                    specimenNode.Text = spec.AccessionNumber;
                if (specimenNode.Text == null || specimenNode.Text == String.Empty)
                    specimenNode.Text = "        ";
                specimenNode.ImageIndex = (int)TreeViewIconIndex.Specimen;
                specimenNode.SelectedImageIndex = (int)TreeViewIconIndex.SpecimenRed;
                specimenNode.Tag = new TreeViewNodeData((int)spec.CollectionSpecimenID, TreeViewNodeTypes.SpecimenNode);
                if(this.expandLvl==ExpandLevel.Specimen)
                    specimenNode.BackColor = System.Drawing.Color.BlanchedAlmond;
            }
        }

        public void parametrizeSpecimenNode(CollectionSpecimen spec, TreeNode specimenNode)//Agents werden nicht maehr angezeigt=>Abandoned
        {
            if (spec != null && specimenNode != null)
            {
                parametrizeOnlySpecimenNode(spec, specimenNode);

                IDirectAccessIterator<CollectionAgent> agents = spec.CollectionAgent;
                if (agents.First() != null)
                {
                    CollectionAgent agent = agents.First();
                    TreeNode agentNode = new TreeNode();
                    parameterizeCollectionAgentNode(agent, agentNode);
                    specimenNode.Nodes.Add(agentNode);
                    while (agents.HasNext())
                    {
                        CollectionAgent next = agents.Next();
                        agentNode = new TreeNode();
                        parameterizeCollectionAgentNode(agent, agentNode);
                        specimenNode.Nodes.Add(agentNode);
                    }
                }
            }
        }

        public void parametrizeSpecimenNode(CollectionSpecimen spec, TreeNode specimenNode, List<ISerializableObject> list)
        {
            if (spec != null && specimenNode != null)
            {
                parametrizeOnlySpecimenNode(spec, specimenNode);
                foreach (ISerializableObject iso in list)
                {
                    if (iso != null)
                    {
                        if (iso.GetType().Equals(typeof(CollectionAgent)))
                        {
                            CollectionAgent agent = (CollectionAgent)iso;
                            if (agent.CollectionSpecimenID == spec.CollectionSpecimenID)
                            {
                                TreeNode agentNode = new TreeNode();
                                parameterizeCollectionAgentNode(agent, agentNode);
                                specimenNode.Nodes.Add(agentNode);
                            }
                        }
                    }
                }
            }
        }

        public void parameterizeCollectionAgentNode(CollectionAgent agent, TreeNode node)
        {
            if (agent != null && node != null)
            {
                node.Text = agent.CollectorsName;
                node.ImageIndex = node.SelectedImageIndex = (int)TreeViewIconIndex.Agent;
                node.Tag = new TreeViewNodeData(agent.CollectorsName, (int)agent.CollectionSpecimenID);
            }
        }

        public void parametrizeOnlyEventNode(CollectionEvent ev, TreeNode eventNode)
        {
            if (ev != null && eventNode != null)
            {
                StringBuilder sbEventTitle = new StringBuilder();


                if (ev.CollectorsEventNumber != null && !ev.CollectorsEventNumber.Equals(string.Empty))
                {
                    sbEventTitle.Append(ev.CollectorsEventNumber);
                }

                
                if (ev.LocalityDescription != null && !ev.LocalityDescription.Equals(string.Empty))
                {
                    sbEventTitle.Append(ev.LocalityDescription);
                }

                if (ev.CollectionDate != null)
                {
                    sbEventTitle.Append(", ");
                    sbEventTitle.Append(ev.CollectionDate.Day);
                    sbEventTitle.Append(".");
                    sbEventTitle.Append(ev.CollectionDate.Month);
                    sbEventTitle.Append(".");
                    sbEventTitle.Append(ev.CollectionDate.Year);
                }
                
                eventNode.Text = sbEventTitle.ToString();
                eventNode.ImageIndex = eventNode.SelectedImageIndex = (int)TreeViewIconIndex.Event;
                eventNode.Tag = new TreeViewNodeData((int)ev.CollectionEventID, TreeViewNodeTypes.EventNode);
                if(this.expandLvl==ExpandLevel.Event)
                    eventNode.BackColor = System.Drawing.Color.BlanchedAlmond;
            }
        }

        public void parametrizeEventNode(CollectionEvent ev, TreeNode eventNode)
        {
            if (ev != null && eventNode != null)
            {
                parametrizeOnlyEventNode(ev, eventNode);

                IDirectAccessIterator<CollectionEventProperty> properties = ev.CollectionEventProperties;
                if (properties.First() != null)
                {
                    CollectionEventProperty cp = properties.First();
                    TreeNode propNode = new TreeNode();
                    parameterizeCollectionEventPropertyNode(cp, propNode);
                    eventNode.Nodes.Add(propNode);
                    while (properties.HasNext())
                    {
                        CollectionEventProperty next = properties.Next();
                        propNode = new TreeNode();
                        parameterizeCollectionEventPropertyNode(next, propNode);
                        eventNode.Nodes.Add(propNode);
                    }
                }

                IDirectAccessIterator<CollectionEventLocalisation> localisations = ev.CollectionEventLocalisation;
                if (localisations.First() != null)
                {
                    CollectionEventLocalisation loc = localisations.First();
                    TreeNode locNode = new TreeNode();
                    parameterizeCollectionEventLocalisationNode(loc, locNode);
                    eventNode.Nodes.Add(locNode);
                    while (localisations.HasNext())
                    {
                        CollectionEventLocalisation next = localisations.Next();
                        locNode = new TreeNode();
                        parameterizeCollectionEventLocalisationNode(next, locNode);
                        eventNode.Nodes.Add(locNode);
                    }
                }
            }
        }

        public void parametrizeEventNode(CollectionEvent ev, TreeNode eventNode, List<ISerializableObject> list)
        {
            if (ev != null && eventNode != null)
            {
                parametrizeOnlyEventNode(ev, eventNode);

                foreach (ISerializableObject iso in list)
                {
                    if (iso != null)
                    {
                        if (iso.GetType().Equals(typeof(CollectionEventProperty)))
                        {
                            CollectionEventProperty cp = (CollectionEventProperty)iso;
                            if (cp.CollectionEventID == ev.CollectionEventID)
                            {
                                TreeNode propNode = new TreeNode();
                                parameterizeCollectionEventPropertyNode(cp, propNode);
                                eventNode.Nodes.Add(propNode);
                            }
                        }
                        else if (iso.GetType().Equals(typeof(CollectionEventLocalisation)))
                        {
                            CollectionEventLocalisation loc = (CollectionEventLocalisation)iso;
                            if (loc.CollectionEventID == ev.CollectionEventID)
                            {
                                TreeNode locNode = new TreeNode();
                                parameterizeCollectionEventLocalisationNode(loc, locNode);
                                eventNode.Nodes.Add(locNode);
                            }
                        }
                    }
                }
            }
        }

        public void parameterizeCollectionEventPropertyNode(CollectionEventProperty ceProp, TreeNode node)
        {
            if (ceProp != null && node != null)
            {
                StringBuilder text = new StringBuilder();
                if (!ceProp.DisplayText.Equals(String.Empty))
                    text.Append(ceProp.DisplayText);

                if (con != null)
                {
                    IRestriction res = RestrictionFactory.Eq(typeof(Property), "_PropertyID", ceProp.PropertyID);
                    Property prop = con.Load<Property>(res);

                    if (prop != null && !prop.DisplayText.Equals(String.Empty))
                    {
                        text.Append(" (");
                        text.Append(prop.DisplayText);
                        text.Append(")");
                    }
                }

                node.Text = text.ToString();
                node.ImageIndex = node.SelectedImageIndex = (int)TreeViewIconIndex.SiteProperty;
                node.Tag = new TreeViewNodeData((int)ceProp.PropertyID, TreeViewNodeTypes.SitePropertyNode);
            }
        }

        public void parameterizeCollectionEventLocalisationNode(CollectionEventLocalisation ceLoc, TreeNode node)
        {
            if (ceLoc != null && node != null)
            {
                IRestriction res = RestrictionFactory.Eq(typeof(LocalisationSystem), "_LocalisationSystemID", ceLoc.LocalisationSystemID);
                LocalisationSystem loc = this.con.Load<LocalisationSystem>(res);

                if (loc != null)
                {
                    StringBuilder sb = new StringBuilder();
                    if (loc.LocalisationSystemID == 8)
                    {
                        try
                        {
                            String latD = "";
                            String lonD = "";

                            double lat = Double.Parse(ceLoc.Location2, System.Globalization.NumberStyles.AllowDecimalPoint);
                            double lon = Double.Parse(ceLoc.Location1, System.Globalization.NumberStyles.AllowDecimalPoint);

                            if (lat >= 0)
                                latD = "N ";
                            else
                                latD = "S ";

                            if (lon >= 0)
                                lonD = "O ";
                            else
                                lonD = "W ";

                            sb.Append(TreeViewOperations.dec2degree(lat)).Append(latD).Append(@";").Append(TreeViewOperations.dec2degree(lon)).Append(lonD);
                        }
                        catch (Exception)
                        {
                            sb.Append(ceLoc.Location1).Append(@";").Append(ceLoc.Location2);
                        }
                    }
                    else if (loc.LocalisationSystemID == 4)
                    {
                        try
                        {
                            double alt = Double.Parse(ceLoc.Location1);
                            sb.Append(alt.ToString("F"));
                            sb.Append(" mNN");
                        }
                        catch (Exception)
                        {
                            sb.Append(ceLoc.Location1).Append(@";").Append(ceLoc.Location2);
                        }
                    }
                    else
                    {
                        sb.Append(ceLoc.Location1).Append(@";").Append(ceLoc.Location2);
                    }

                    node.Text = sb.ToString();
                    node.ImageIndex = node.SelectedImageIndex = (int)TreeViewIconIndex.Location;
                    node.Tag = new TreeViewNodeData((int)ceLoc.LocalisationSystemID, TreeViewNodeTypes.LocalisationNode);
                }
            }
        }

        public void parametrizeEventSeriesNode(CollectionEventSeries ceSeries, TreeNode node)
        {
            if (node != null)
            {
                if (ceSeries == null)
                {
                    parametrizeEmptyEventSeriesNode(node);
                }
                else
                {
                    StringBuilder text = new StringBuilder();

                    text.Append(ceSeries.SeriesCode);

                    if (!ceSeries.Description.Equals(String.Empty))
                    {
                        text.Append(", ");
                        text.Append(ceSeries.Description);
                    }

                    if (ceSeries.DateStart != null)
                    {
                        text.Append(", ");
                        text.Append(((DateTime)ceSeries.DateStart).Day);
                        text.Append(".");
                        text.Append(((DateTime)ceSeries.DateStart).Month);
                        text.Append(".");
                        text.Append(((DateTime)ceSeries.DateStart).Year);
                        text.Append(" ");
                        text.Append(((DateTime)ceSeries.DateStart).ToShortTimeString());
                    }

                    node.Text =  text.ToString();
                    node.ImageIndex = node.SelectedImageIndex = (int)TreeViewIconIndex.EventSeries;
                    node.Tag = new TreeViewNodeData((int)ceSeries.SeriesID, TreeViewNodeTypes.EventSeriesNode);
                }
            }
            if (this.expandLvl == ExpandLevel.EventSeries)
                node.BackColor = System.Drawing.Color.BlanchedAlmond;
        }

        public void parametrizeEmptyEventSeriesNode(TreeNode node)
        {
            if (node != null)
            {
                node.Text = "No EventSeries";
                node.ImageIndex = node.SelectedImageIndex = (int)TreeViewIconIndex.Unknown;
                node.Tag = new TreeViewNodeData(null, TreeViewNodeTypes.EventSeriesNode);
            }
        }

        private void parametrizeUnknownNode(TreeNode node)
        {
            if (node != null)
            {
                node.Text = "";
                node.Tag = new TreeViewNodeData(-1, TreeViewNodeTypes.Unknown);
                node.ImageIndex = (int)TreeViewIconIndex.Unknown;
                node.SelectedImageIndex = (int)TreeViewIconIndex.Unknown;
            }
        }

        #endregion

        #region displayDataItems

        public TreeNode displayAllEventSeries(CollectionEventSeries es)
        {
            TreeNode esNode = new TreeNode();
            this.parametrizeEventSeriesNode(es, esNode);
            if (es != null)
            {
                if (es.CollectionEvents.First() != null)
                {
                    esNode.Nodes.Add(new TreeNode());
                }
            }
            else
            {
                IRestriction res = RestrictionFactory.Eq(typeof(CollectionEvent), "_SeriesID", null);
                IList<CollectionEvent> ceList = con.LoadList<CollectionEvent>(res);
                if (ceList.Count > 0)
                    esNode.Nodes.Insert(0,new TreeNode());
            }
            return esNode;
        }

        public TreeNode displayEventSeries(CollectionEventSeries es)
        {
            TreeNode esNode = new TreeNode();
           
            if (expandLvl == ExpandLevel.EventSeries)
            {
                esNode = displayTopDownEventSeries(es);
            }
            else
            {
                parametrizeEventSeriesNode(es, esNode);
                if (es != null)
                {
                    if (es.CollectionEvents.First() != null)
                    {
                        TreeNode eventNode = new TreeNode();
                        CollectionEvent ev = es.CollectionEvents.First();
                        parametrizeEventNode(ev, eventNode);
                        if (ev.CollectionSpecimen.First() != null)
                            eventNode.Nodes.Insert(0,new TreeNode());
                        esNode.Nodes.Add(eventNode);
                    }
                    while (es.CollectionEvents.HasNext())
                    {
                        TreeNode eventNode = new TreeNode();
                        CollectionEvent ev = es.CollectionEvents.Next();
                        parametrizeEventNode(ev, eventNode);
                        if (ev.CollectionSpecimen.First() != null)
                            eventNode.Nodes.Insert(0,new TreeNode());
                        esNode.Nodes.Add(eventNode);
                    }
                }
                else
                {
                    IRestriction res = RestrictionFactory.Eq(typeof(CollectionEvent), "_SeriesID", null);
                    IList<CollectionEvent> ceList = con.LoadList<CollectionEvent>(res);
                    foreach (CollectionEvent ev in ceList)
                    {
                        if (ev != null)
                        {
                            TreeNode eventNode = new TreeNode();
                            parametrizeEventNode(ev, eventNode);
                            if (ev.CollectionSpecimen.First() != null)
                                eventNode.Nodes.Insert(0, new TreeNode());
                            esNode.Nodes.Add(eventNode);
                        }
                    }
                }
            }
            return esNode;
        }

        public TreeNode displayEvent(CollectionEvent ev)
        {
            TreeNode evNode = new TreeNode();
            if (ev != null)
            {
                if (expandLvl <= ExpandLevel.Event)
                {
                    evNode = displayTopDownEvent(ev);
                }
                else
                {
                    parametrizeEventNode(ev, evNode);
                    if (ev.CollectionSpecimen.First() != null)
                    {
                        CollectionSpecimen first = ev.CollectionSpecimen.First();
                        TreeNode specimenNode = new TreeNode();
                        parametrizeOnlySpecimenNode(first, specimenNode);
                        if (first.IdentificationUnits.First() != null)
                            specimenNode.Nodes.Insert(0, new TreeNode());
                        evNode.Nodes.Add(specimenNode);
                        while (ev.CollectionSpecimen.HasNext())
                        {
                            CollectionSpecimen next = ev.CollectionSpecimen.Next();
                            TreeNode nextNode = new TreeNode();
                            parametrizeOnlySpecimenNode(next, nextNode);
                            if (next.IdentificationUnits.First() != null)
                                nextNode.Nodes.Insert(0, new TreeNode());
                            evNode.Nodes.Add(nextNode);
                        }
                    }
                }
            }
            return evNode;
        }

        public TreeNode displaySpecimen(CollectionSpecimen spec)
        {
            TreeNode specNode = new TreeNode();

            if (spec != null)
            {
                if (expandLvl <= ExpandLevel.Specimen)
                {
                    specNode = displayTopDownSpecimen(spec);
                }
                else
                {
                    parametrizeOnlySpecimenNode(spec, specNode);
                    if (spec.IdentificationUnits.First() != null)
                    {
                        IdentificationUnit first = spec.IdentificationUnits.First();
                        if (first.RelatedUnit == null)
                        {
                            TreeNode iuNode = new TreeNode();
                            parametrizeIUNode(first, iuNode);
                            if (first.ChildUnits.First() != null)
                                iuNode.Nodes.Insert(0, new TreeNode());
                            specNode.Nodes.Add(iuNode);
                            while (spec.IdentificationUnits.HasNext())
                            {
                                IdentificationUnit next = spec.IdentificationUnits.Next();
                                if (next.RelatedUnit == null)
                                {
                                    TreeNode nextNode = new TreeNode();
                                    parametrizeIUNode(next, nextNode);
                                    if (next.ChildUnits.First() != null)
                                        nextNode.Nodes.Insert(0, new TreeNode());
                                    specNode.Nodes.Add(nextNode);
                                }
                            }
                        }
                    }
                }
            }
            return specNode;
        }

        public TreeNode displayIdentificationUnit(IdentificationUnit iu)
        {
            TreeNode iuNode = new TreeNode();
            if(iu != null)
                iuNode = displayTopDownIdentificationUnit(iu); 
           
            return iuNode;
        }

        #endregion

        #region Help Functions


        //Der repräsentierende Knoten ist der Knoten des Expandlevels des InputKnotens. Falls der Inputknoten über dem Expandlevel
        //liegt, gibt es keinen repäsentierenden Knoten. Diew wird mit -1 gekennzeichnet.
        public TreeNode findRepresentantOfType(TreeNode node)
        {
            if (node != null)
            {
                TreeViewNodeData data = node.Tag as TreeViewNodeData;
                if (data == null)
                    return new TreeNode("-1");
                if (node.Parent == null && this.expandLvl != ExpandLevel.EventSeries)
                    return new TreeNode("-1");
                if (this.expandLvl != ExpandLevel.IdentificationUnit)
                {
                    if ((int)data.NodeType == (int)this.expandLvl)
                        return node;
                    while ((int)data.NodeType != (int)this.expandLvl)
                    {
                        node = node.Parent;

                        if (node == null)
                            data = null;
                        else
                            data = node.Tag as TreeViewNodeData;

                        if (data == null)
                            return new TreeNode("-1");
                    }
                }
                else
                {
                    TreeNode parent = node.Parent;
                    TreeViewNodeData parentData = parent.Tag as TreeViewNodeData;
                    if (data.NodeType != TreeViewNodeTypes.IdentificationUnitNode)
                        return new TreeNode("-1");
                    if (parentData.NodeType != TreeViewNodeTypes.IdentificationUnitNode)
                        return node;
                    while (parentData.NodeType == TreeViewNodeTypes.IdentificationUnitNode)
                    {
                        node = parent;
                        parent = node.Parent;
                        parentData = parent.Tag as TreeViewNodeData;
                    }

                }
                return node;
            }
            return new TreeNode("-1");
        }

        public static bool isRoot(ISerializableObject iso)
        {
            if (iso != null)
            {
                if (iso.GetType().Equals(typeof(CollectionEventSeries)))
                    return true;
                else if (iso.GetType().Equals(typeof(CollectionEvent)))
                {
                    CollectionEvent ce = (CollectionEvent)iso;
                    if (ce.SeriesID == null)
                        return true;
                }
                else if (iso.GetType().Equals(typeof(CollectionSpecimen)))
                {
                    CollectionSpecimen spec = (CollectionSpecimen)iso;
                    if (spec.CollectionEventID == null)
                        return true;
                }
            }
            return false;
        }

        public static String dec2degree(double dec)
        {
            StringBuilder sb = new StringBuilder();
            int i = (int) Math.Floor(dec);
            sb.Append(i.ToString()).Append(@"° ");
            dec = (dec % 1)*60;
            i = (int)Math.Floor(dec);
            sb.Append(i.ToString()).Append(@"' ");
            dec = (dec % 1) * 60;
            i = (int)Math.Round(dec);
            sb.Append(i.ToString()).Append(@"'' ");
            return sb.ToString();
        }
		#endregion
    }
}
