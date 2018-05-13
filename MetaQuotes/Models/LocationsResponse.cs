using System.Runtime.Serialization;

namespace MetaQuotes.Models
{
    [DataContract]
    public class LocationsResponse
    {
        [DataMember(Name = "locations")]
        public Location[] Locations { get; set; }
    }
}