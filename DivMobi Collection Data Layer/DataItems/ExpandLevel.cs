using System;

namespace UBT.AI4.Bio.DivMobi.DataLayer.DataItems
{
    public enum ExpandLevel
    {
        EventSeries=0,
        Event=1,
        Specimen=2,
        IdentificationUnit=3
    }

    public class DisplayLevel
    {
        private ExpandLevel lvl=ExpandLevel.Specimen;

        public DisplayLevel(string input)
        {
            switch (input)
            {
                case "EventSeries":
                    lvl = ExpandLevel.EventSeries;
                    break;
                case "Event":
                    lvl = ExpandLevel.Event;
                    break;
                case "Specimen":
                    lvl = ExpandLevel.Specimen;
                    break;
                case "IdentificationUnit":
                    lvl = ExpandLevel.IdentificationUnit;
                    break;
                default:
                    lvl = ExpandLevel.Specimen;
                    break;
            }
        }

        public int? Index
        {
            get
            {
                if (this.lvl != null)
                    return (int)lvl;
                else
                    return null;
            }
        }

        public string DisplayLevelString
        {
            get
            {
                switch (lvl)
                {
                    case ExpandLevel.EventSeries:
                        return "EventSeries";
                    case ExpandLevel.Event:
                        return "Event";
                    case ExpandLevel.Specimen:
                        return "Specimen";
                    case ExpandLevel.IdentificationUnit:
                        return "IdentificationUnit";
                    default:
                        return "Unknown";
                }
            }
        }

        public override string ToString()
        {
            if (this.DisplayLevelString != null)
            {
                String text = "Expandlevel: " + this.DisplayLevelString;
                return text;
            }
            else return "Expandlevel: unknown";
        }
    }


}