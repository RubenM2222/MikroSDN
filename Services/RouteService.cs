using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using MikroSDN.Models;

namespace MikroSDN.Services
{
    internal class RouteService
    {
        private readonly MikrotikService _api;
        public RouteService(MikrotikService api) => _api = api;

        public Task<List<RouteModel>> GetAll() => _api.GetAsync<List<RouteModel>>("ip/route");
        public Task<RouteModel> Create(object data) => _api.PostAsync<RouteModel>("ip/route", data);
        public Task<RouteModel> Update(string id, object data) => _api.PatchAsync<RouteModel>($"ip/route/{id}", data);
        public Task<bool> Delete(string id) => _api.DeleteAsync($"ip/route/{id}");
    }
}