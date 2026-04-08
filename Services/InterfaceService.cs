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

        public Task<List<InterfaceModel>> GetAll()
        {
            return _api.GetAsync<List<InterfaceModel>>("interface");
        }

        public Task<List<InterfaceModel>> GetWireless()
        {
            return _api.GetAsync<List<InterfaceModel>>("interface/wireless");
        }
    }
}
