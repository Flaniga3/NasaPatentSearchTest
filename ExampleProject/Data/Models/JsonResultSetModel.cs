using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ExampleProject.Data.Models
{
    public class JsonResultSetModel<T1>
        where T1 : NasaBaseModel
    {
        [JsonProperty(PropertyName = "results")]
        public IEnumerable<T1> Results { get; set; }
    }
}
