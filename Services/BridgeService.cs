using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using MikroSDN.Models;

namespace MikroSDN.Services
{
    internal class BridgeService
    {
        private readonly MikrotikService _api;
        public BridgeService(MikrotikService api) => _api = api;

        public Task<List<BridgeModel>> GetAll() => _api.GetAsync<List<BridgeModel>>("interface/bridge");
        public Task<BridgeModel> Create(object data) => _api.PostAsync<BridgeModel>("interface/bridge", data);
        public Task<BridgeModel> Update(string id, object data) => _api.PatchAsync<BridgeModel>($"interface/bridge/{id}", data);
        public Task<bool> Delete(string id) => _api.DeleteAsync($"interface/bridge/{id}");

        public Task<List<BridgeModel>> GetPorts() => _api.GetAsync<List<BridgeModel>>("interface/bridge/port");
        public Task<BridgeModel> AddPort(object data) => _api.PostAsync<BridgeModel>("interface/bridge/port", data);
        public Task<BridgeModel> UpdatePort(string id, object data) => _api.PatchAsync<BridgeModel>($"interface/bridge/port/{id}", data);
        public Task<bool> DeletePort(string id) => _api.DeleteAsync($"interface/bridge/port/{id}");
    }
}