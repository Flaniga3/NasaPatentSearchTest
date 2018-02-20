using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ExampleProject.Data.Models
{
    public class NasaPatentModel : NasaBaseModel
    {
        // Unfortunately, without complicated workarounds, the JsonProperty
        // attribute will always cause both serialization and deserialization
        // to match the PropertyName parameter. I've chose to deal with this on
        // the front-end rather than implement the workarounds here. It's just
        // easier when dealing with a stubborn public API.
        [JsonProperty(PropertyName = "category")]
        public string Category { get; set; }
        [JsonProperty(PropertyName = "client_record_id")]
        public string ClientRecordId { get; set; }
        [JsonProperty(PropertyName = "center")]
        public string Center { get; set; }
        [JsonProperty(PropertyName = "reference_number")]
        public string ReferenceNumber { get; set; }
        [JsonProperty(PropertyName = "abstract")]
        public string Abstract { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "expiration_date")]
        public DateTime? ExpirationDate { get; set; }
        [JsonProperty(PropertyName = "innovator")]
        public IEnumerable<NasaInnovatorModel> Innovators { get; set; }
        [JsonProperty(PropertyName = "concepts")]
        public IDictionary<string, string> Concepts { get; set; }
        [JsonProperty(PropertyName = "publication")]
        public IEnumerable<NasaPublicationModel> Publication { get; set; }
        [JsonProperty(PropertyName = "patent_number")]
        public string PatentNumber { get; set; }
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "serial_number")]
        public string SerialNumber { get; set; }
    }
}
