using MikroSDN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikroSDN.Data
{
    public class RouterRepository
    {
        public static List<RouterDevice> Routers { get; set; } = new List<RouterDevice>();
    }
}
