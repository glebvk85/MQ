using System.Runtime.Serialization;

namespace MetaQuotes.Models
{
    [DataContract]
    public class IPResponse
    {
        [DataMember(Name = "location")]
        public Location Location { get; set; }
    }
}