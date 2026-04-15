using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using MikroSDN.Models;

namespace MikroSDN.Services
{
    internal class DhcpService
    {
        private readonly MikrotikService _api;
        public DhcpService(MikrotikService api) => _api = api;

        public Task<List<DhcpServerModel>> GetAllServers() => _api.GetAsync<List<DhcpServerModel>>("ip/dhcp-server");
        public Task<DhcpServerModel> CreateServer(object data) => _api.PostAsync<DhcpServerModel>("ip/dhcp-server", data);
        public Task<DhcpServerModel> UpdateServer(string id, object data) => _api.PatchAsync<DhcpServerModel>($"ip/dhcp-server/{id}", data);
        public Task<bool> DeleteServer(string id) => _api.DeleteAsync($"ip/dhcp-server/{id}");

        // Leases, networks, options can be added similarly:
        public Task<List<DhcpServerModel>> GetLeases() => _api.GetAsync<List<DhcpServerModel>>("ip/dhcp-server/lease");
    }
}