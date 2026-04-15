using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using MikroSDN.Models;

namespace MikroSDN.Services
{
    internal class WirelessService
    {
        private readonly MikrotikService _api;
        public WirelessService(MikrotikService api) => _api = api;

        // Interfaces with wireless capability
        public Task<List<InterfaceModel>> GetAllInterfaces() => _api.GetAsync<List<InterfaceModel>>("interface/wireless");
        // Enable/disable/configure wireless interface or AP mode
        public Task<InterfaceModel> UpdateInterface(string id, object data) => _api.PatchAsync<InterfaceModel>($"interface/wireless/{id}", data);

        // Wireless "networks" (SSID/AP config) and registration/clients typically live under wireless or wlan interfaces
        public Task<List<InterfaceModel>> GetRegistrationTable() => _api.GetAsync<List<InterfaceModel>>("interface/wireless/registration-table");

        // Common actions
        public Task<InterfaceModel> Create(object data) => _api.PostAsync<InterfaceModel>("interface/wireless", data);
        public Task<bool> Delete(string id) => _api.DeleteAsync($"interface/wireless/{id}");
    }
}