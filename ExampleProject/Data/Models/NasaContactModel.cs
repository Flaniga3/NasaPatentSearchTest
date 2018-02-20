using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ExampleProject.Data.Models
{
    public class NasaContactModel : NasaBaseModel
    {
        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }
        [JsonProperty(PropertyName = "facility")]
        public string Facility { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }
        [JsonProperty(PropertyName = "office")]
        public string Office { get; set; }
    }
}
