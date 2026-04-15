using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MikroSDN.Models
{
    public class BridgeModel
    {
        [JsonPropertyName(".id")] // Importante: Mikrotik usa ponto antes do id
        public string id { get; set; }
        public string name { get; set; }
        public string mtu { get; set; }
        public string actual_mtu { get; set; }
        public string l2mtu { get; set; }
        public string disabled { get; set; }
    }
}
