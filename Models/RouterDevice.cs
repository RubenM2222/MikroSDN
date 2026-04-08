using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikroSDN.Models
{
    public class RouterDevice
    {
        public string Name { get; set; }
        public string IP { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string BaseUrl => $"http://{IP}/rest/";
    }
}
