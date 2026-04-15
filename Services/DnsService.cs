using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using MikroSDN.Models;

namespace MikroSDN.Services
{
    internal class DnsService
    {
        private readonly MikrotikService _api;
        public DnsService(MikrotikService api) => _api = api;

        public Task<DnsStaticModel> GetSettings() => _api.GetAsync<DnsStaticModel>("ip/dns");
        public Task<DnsStaticModel> UpdateSettings(object data) => _api.PatchAsync<DnsStaticModel>("ip/dns", data);
        public Task<List<DnsStaticModel>> GetStatic() => _api.GetAsync<List<DnsStaticModel>>("ip/dns/static");
        public Task<DnsStaticModel> CreateStatic(object data) => _api.PostAsync<DnsStaticModel>("ip/dns/static", data);
        public Task<bool> DeleteStatic(string id) => _api.DeleteAsync($"ip/dns/static/{id}");
    }
}