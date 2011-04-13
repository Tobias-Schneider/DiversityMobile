using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UBT.AI4.Bio.DivMobi.SyncBase
{
    public interface IImage
    {
        string URI { get; set; }
        Guid Rowguid { get; }
        string Type { get; set; }
        float Altitude { get; set; }
        float Latitude { get; set; }
        float Longitude { get; set; }
        string Author{get;set;}
        string Timestamp { get; set; }
        string ProjectID { get; set; }
    }
}
