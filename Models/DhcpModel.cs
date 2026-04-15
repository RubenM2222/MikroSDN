using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MikroSDN.Models
{
    public class DhcpServerModel
    {
        [JsonPropertyName(".id")]
        public string id { get; set; }
        public string name { get; set; }
        public string @interface { get; set; } // @ porque interface é palavra reservada
        [JsonPropertyName("address-pool")]
        public string address_pool { get; set; }
        [JsonPropertyName("lease-time")]
        public string lease_time { get; set; }
        public string disabled { get; set; }
    }
}
