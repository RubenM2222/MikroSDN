using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikroSDN.Models
{
    public class IpAddressModel
    {
        [JsonProperty(".id")]
        public string id { get; set; }       // .id do MikroTik

        public string address { get; set; }
        public string network { get; set; }
        public string @interface { get; set; }
        public string disabled { get; set; }
    }
}
