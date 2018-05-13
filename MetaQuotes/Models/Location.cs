using System.Runtime.Serialization;

namespace MetaQuotes.Models
{
    [DataContract]
    public class Location
    {
        [DataMember(Name = "range_ip")]
        public string RangeIp { get; set; }

        [DataMember(Name = "city", EmitDefaultValue = false)]
        public string City { get; set; }

        [DataMember(Name = "country")]
        public string Country { get; set; }

        [DataMember(Name = "region")]
        public string Region { get; set; }

        [DataMember(Name = "postal")]
        public string Postal { get; set; }

        [DataMember(Name = "org")]
        public string Organization { get; set; }

        [DataMember(Name = "lat")]
        public float Latitude { get; set; }

        [DataMember(Name = "lng")]
        public float Longitude { get; set; }
    }
}