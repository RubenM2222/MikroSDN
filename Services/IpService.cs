using MikroSDN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikroSDN.Services
{
    internal class IpService
    {
        private readonly MikrotikService _api;

        public IpService(MikrotikService api)
        {
            _api = api;
        }

        // GET
        public Task<List<IpAddressModel>> GetAll()
        {
            return _api.GetAsync<List<IpAddressModel>>("ip/address");
        }

        // CREATE
        public Task<object> Create(string address, string iface)
        {
            var data = new
            {
                address = address,
                @interface = iface
            };

            return _api.PostAsync<object>("ip/address", data);
        }

        // UPDATE
        public Task<object> Update(string id, bool disabled)
        {
            var data = new
            {
                disabled = disabled
            };

            return _api.PatchAsync<object>($"ip/address/{id}", data);
        }

        // DELETE
        public Task<bool> Delete(string id)
        {
            return _api.DeleteAsync($"ip/address/{id}");
        }
    }
}
