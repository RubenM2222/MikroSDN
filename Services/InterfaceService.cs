using MikroSDN.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikroSDN.Services
{
    internal class InterfaceService
    {
        private readonly MikrotikService _api;

        public InterfaceService(MikrotikService api)
        {
            _api = api;
        }

        public async Task<string> GetAllInterfaces()
        {
            var json = await _api.GetAsync("interface");
            return JsonConvert.DeserializeObject<List<InterfaceModel>>(json);
        }

        public async Task<string> GetWirelessInterfaces()
        {
            return await _api.GetAsync("interface/wireless");
        }
    }
}
