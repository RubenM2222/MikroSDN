using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MikroSDN.Models
{
    // Para as configurações globais (ip/dns)
    public class DnsSettingsModel
    {
        public string servers { get; set; }
        [JsonPropertyName("dynamic-servers")]
        public string dynamic_servers { get; set; }
        [JsonPropertyName("allow-remote-requests")]
        public string allow_remote_requests { get; set; }
    }

    // Para os registos estáticos (ip/dns/static)
    public class DnsStaticModel
    {
        [JsonPropertyName(".id")]
        public string id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string ttl { get; set; }
    }
}
