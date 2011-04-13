﻿using System;
using System.Collections.Generic;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Restrictions;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;
namespace Diversity_Synchronization
{
    public partial class FieldDataSelectionBuilder
    {
        public class SearchSpecification
        {
            public class RestrictionSpecification
            {
                public enum RestrictionType
                {
                    Equals,
                    Like,
                    BetweenDates
                }
                private int titleID;
                private RestrictionType type;
                private string property;
                public RestrictionSpecification(int titleID,string property, RestrictionType type)
                {
                    this.titleID = titleID;
                    this.type = type;
                    this.property = property;

                    StartTime = EndTime = DateTime.Today;
                }
                public int TitleID
                {
                    get
                    {
                        return titleID;
                    }
                }
                public RestrictionType Type { get { return type; } }
                public string Property{get{return property;}}
                public bool IsEnabled { get; set; }
                public string Value { get; set; }
                public DateTime StartTime { get; set; }
                public DateTime EndTime { get; set; }
                public IRestriction GetRestrictionForType(Type t)
                {
                    if(IsEnabled)
                    {
                        switch (this.Type)
	                    {
		                    case RestrictionType.Equals:
                                return RestrictionFactory.Eq(t,property,Value);
                             break;
                            case RestrictionType.Like:
                                string search = (!string.IsNullOrEmpty(Value))? Value.Trim('%',' '):"";
                                search = "%" + search + "%";
                                    
                                return RestrictionFactory.Like(t,property,search);
                             break;
                            case RestrictionType.BetweenDates:
                                return RestrictionFactory.Btw(t,property,StartTime,EndTime);
                             break;
                            default:
                             break;
	                    }
                    }
                    return null;
                }
            }

            public SearchSpecification(int titleID, Type objectType, IList<RestrictionSpecification> restrictions)
            {
                this.TitleID = titleID;
                this.ObjectType = objectType;
                this.Restrictions = restrictions;
            }
            public int TitleID
            {
                get;
                private set;
            }
            public Type ObjectType
            {
                get;
                private set;
            }
            public IList<RestrictionSpecification> Restrictions
            {
                get;
                private set;
            }

            public IRestriction GetQueryRestriction()
            {
                IRestriction projectRestriction = RestrictionFactory.Eq(typeof(CollectionProject),"_ProjectID", ConnectionsAccess.Profile.ProjectID);
                IRestriction selectionRestrictions = null;
                foreach (var restrictionSpecification in Restrictions)
                {                    
                    var currentRestriction = restrictionSpecification.GetRestrictionForType(ObjectType);
                    if (currentRestriction != null)
                    {
                        if (selectionRestrictions != null)
                            selectionRestrictions= RestrictionFactory.And().Add(selectionRestrictions).Add(currentRestriction);
                        else
                            selectionRestrictions = currentRestriction;
                    }                
                }
                if (selectionRestrictions != null)
                    return RestrictionFactory.And().Add(projectRestriction).Add(selectionRestrictions);
                else
                    return projectRestriction;
            }

        }
    }
}