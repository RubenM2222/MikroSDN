using MikroSDN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace MikroSDN.Data
{
    public class RouterRepository
    {
        private static List<RouterDevice> _routers;
        public static List<RouterDevice> Routers
        {
            get
            {
                if (_routers == null) _routers = Services.SessionManager.LoadSessions();
                return _routers;
            }
        }

        public static void Save()
        {
            if (_routers != null)
            {
                Services.SessionManager.SaveSessions(_routers);
            }
        }
     }
}
