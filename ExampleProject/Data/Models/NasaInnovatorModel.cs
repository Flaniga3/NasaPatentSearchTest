using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ExampleProject.Data.Models
{
    public class NasaInnovatorModel : NasaBaseModel
    {
        [JsonProperty(PropertyName = "fname")]
        public string FirstName { get; set; }
        [JsonProperty(PropertyName = "lname")]
        public string LastName { get; set; }
        [JsonProperty(PropertyName = "mname")]
        public string MiddleName { get; set; }
        [JsonProperty(PropertyName = "company")]
        public string Company { get; set; }
        [JsonProperty(PropertyName = "order")]
        public string Order { get; set; }
    }
}
