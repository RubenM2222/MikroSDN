using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MikroSDN.Models
{
    public class RouteModel
    {
        [JsonPropertyName(".id")]
        public string id { get; set; }
        [JsonPropertyName("dst-address")]
        public string dst_address { get; set; }
        public string gateway { get; set; }
        public string distance { get; set; }
        public string active { get; set; }
        public string static_route { get; set; }
    }
}
