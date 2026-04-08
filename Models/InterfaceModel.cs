using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikroSDN.Models
{
    public class InterfaceModel
    {
        [JsonProperty(".id")]
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string disabled { get; set; }
    }
}
