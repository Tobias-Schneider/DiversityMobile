using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;


namespace UBT.AI4.Bio.DivMobi.DataItemFormTools
{
    public enum TreeViewNodeTypes
    {

        Root=-1, //Imaginäre Ebene, die zum Anzeigen aller Eventseries verwendet wird       
        EventSeriesNode=0,
        EventNode=1,
        SpecimenNode=2,
        IdentificationUnitNode = 3,
        LocalisationNode=4,
        SitePropertyNode=5,
        AgentNode=6,    
        AnalysisNode=7,
        GeographyNode=8,
        Unknown=9
    }
}
