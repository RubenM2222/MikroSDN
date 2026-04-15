using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MikroSDN.Models
{
    public class SecurityProfileModel
    {
        [JsonPropertyName(".id")]
        public string id { get; set; }
        public string name { get; set; }
        public string mode { get; set; }
        [JsonPropertyName("authentication-types")]
        public string auth_types { get; set; }
        [JsonPropertyName("wpa2-pre-shared-key")]
        public string wpa2_key { get; set; }
    }
}
