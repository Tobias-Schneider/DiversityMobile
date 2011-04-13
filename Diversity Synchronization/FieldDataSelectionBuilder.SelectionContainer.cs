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

using System.Collections.Generic;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;
using System;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer.Collections;
namespace Diversity_Synchronization
{
    public partial class FieldDataSelectionBuilder
    {
        public class SelectionContainer
        {
            private ISerializableObject owner;
            private List<ISerializableObject> relatedObjects;
            private bool truncate;
            private ISerializableObject root;


            public SelectionContainer(ISerializableObject iso, bool truncate)
            {
                owner = iso;
                relatedObjects = new List<ISerializableObject>();
                this.truncate = truncate;
                if (truncate == false)
                    topDown(owner, relatedObjects);
                relatedObjects.Add(owner);
                bottomUp(owner, relatedObjects);
            }

            private void bottomUp(ISerializableObject iso, List<ISerializableObject> parents)
            {
                if (iso.GetType().Equals(typeof(IdentificationUnitAnalysis)))
                {
                    IdentificationUnitAnalysis iua = (IdentificationUnitAnalysis)iso;
                    IdentificationUnit iu = iua.IdentificationUnit;
                    if (iu != null)
                    {
                        parents.Add(iu);
                        bottomUp(iu, parents);
                    }
                    else throw new Exception();
                }
                else if (iso.GetType().Equals(typeof(IdentificationUnit)))
                {
                    IdentificationUnit iu = (IdentificationUnit)iso;
                    /*
                    IDirectAccessIterator<Identification> identifications = iu.Identifications;
                    short i = 0;
                    Identification ident = null;
                    foreach (Identification id in identifications)
                    {
                        if (id.IdentificationSequence != null && id.IdentificationSequence > i)
                        {
                            i = (short)id.IdentificationSequence;
                            ident = id;
                        }
                    }
                    if (ident != null)
                        parents.Add(ident);*/
                    IdentificationUnit relatedUnit = iu.RelatedUnit;
                    if (relatedUnit != null)
                    {
                        parents.Add(relatedUnit);
                        bottomUp(relatedUnit, parents);
                    }
                    else
                    {
                        CollectionSpecimen spec = iu.CollectionSpecimen;
                        if (spec != null)
                        {
                            parents.Add(spec);
                            bottomUp(spec, parents);
                        }
                        else
                            throw new Exception();
                    }
                }
                else if (iso.GetType().Equals(typeof(CollectionSpecimen)))
                {
                    CollectionSpecimen spec = (CollectionSpecimen)iso;
                    CollectionAgent ca = spec.CollectionAgent.First();
                    if (ca != null)
                        parents.Add(ca);
                    CollectionEvent ce = spec.CollectionEvent;
                    if (ce != null)
                    {
                        parents.Add(ce);
                        bottomUp(ce, parents);
                    }
                    else this.root = spec; ;//Warnung dass das Specimen nicht angezeigt werden kann
                }
                else if (iso.GetType().Equals(typeof(CollectionEvent)))
                {
                    CollectionEvent ce = (CollectionEvent)iso;
                    IDirectAccessIterator<CollectionEventLocalisation> locations = ce.CollectionEventLocalisation;
                    foreach (CollectionEventLocalisation loc in locations)
                    {
                        parents.Add(loc);
                    }
                    IDirectAccessIterator<CollectionEventProperty> properties = ce.CollectionEventProperties;
                    foreach (CollectionEventProperty prop in properties)
                    {
                        parents.Add(prop);
                    }
                    CollectionEventSeries cs = ce.CollectionEventSeries;
                    if (cs != null)
                    {
                        parents.Add(cs);
                        this.root = cs;
                    }
                    else this.root = ce;
                }
                else if (iso.GetType().Equals(typeof(CollectionEventSeries)))
                {
                    CollectionEventSeries cs = (CollectionEventSeries)iso;
                    this.root = cs;
                }
            }
            private void topDown(ISerializableObject iso, List<ISerializableObject> children)
            {
                if (iso.GetType().Equals(typeof(CollectionEventSeries)))
                {
                    CollectionEventSeries cs = (CollectionEventSeries)iso;
                    IDirectAccessIterator<CollectionEvent> events = cs.CollectionEvents;
                    foreach (CollectionEvent ce in events)
                    {
                        children.Add(ce);
                        topDown(ce, children);
                    }
                }
                if (iso.GetType().Equals(typeof(CollectionEvent)))
                {
                    CollectionEvent ce = (CollectionEvent)iso;
                    IDirectAccessIterator<CollectionEventLocalisation> locations = ce.CollectionEventLocalisation;
                    foreach (CollectionEventLocalisation loc in locations)
                    {
                        children.Add(loc);
                    }
                    IDirectAccessIterator<CollectionEventProperty> properties = ce.CollectionEventProperties;
                    foreach (CollectionEventProperty prop in properties)
                    {
                        children.Add(prop);
                    }
                    IDirectAccessIterator<CollectionSpecimen> specimen = ce.CollectionSpecimen;
                    foreach (CollectionSpecimen spec in specimen)
                    {
                        children.Add(spec);
                        topDown(spec, children);
                    }
                }
                if (iso.GetType().Equals(typeof(CollectionSpecimen)))
                {
                    CollectionSpecimen spec = (CollectionSpecimen)iso;
                    CollectionAgent ca = spec.CollectionAgent.First();
                    if (ca != null)
                        children.Add(ca);
                    IDirectAccessIterator<CollectionProject> projects = spec.CollectionProject;
                    foreach (CollectionProject pr in projects)
                    {
                        children.Add(pr);
                    }
                    IDirectAccessIterator<IdentificationUnit> units = spec.IdentificationUnits;
                    foreach (IdentificationUnit iu in units)
                    {
                        if (iu.RelatedUnit == null)//Hier kann der Aufwand optimiert werden indem gleich alle IdentificationUnits angehängt werden, alerdings muss dann der Fall von einer IU als Startpunkt gesondert behandelt werden
                        {
                            children.Add(iu);
                            topDown(iu, children);
                        }
                    }
                }
                if (iso.GetType().Equals(typeof(IdentificationUnit)))
                {
                    IdentificationUnit iu = (IdentificationUnit)iso;
                    IDirectAccessIterator<IdentificationUnitAnalysis> analyses = iu.IdentificationUnitAnalysis;
                    IDirectAccessIterator<IdentificationUnitGeoAnalysis> geoAnalyses = iu.IdentificationUnitGeoAnalysis;
                    IDirectAccessIterator<Identification> ids = iu.Identifications;
                    foreach (IdentificationUnitAnalysis iua in analyses)
                    {
                        children.Add(iua);
                    }
                    foreach (IdentificationUnitGeoAnalysis iuga in geoAnalyses)
                    {
                        children.Add(iuga);
                    }
                    foreach (Identification id in ids)
                    {
                        children.Add(id);
                    }
                    IDirectAccessIterator<IdentificationUnit> units = iu.ChildUnits;
                    foreach (IdentificationUnit childUnit in units)
                    {
                        children.Add(childUnit);
                        topDown(childUnit, children);
                    }
                }
            }

            public List<ISerializableObject> RelatedObjects { get { return this.relatedObjects; } }
            public ISerializableObject Owner { get { return this.owner; } }
            public ISerializableObject Root { get { return this.root; } }

            public override string ToString()
            {
                return owner.ToString();
            }
        }
    }
}
