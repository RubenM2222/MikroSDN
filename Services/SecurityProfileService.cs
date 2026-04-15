using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using MikroSDN.Models;

namespace MikroSDN.Services
{
    internal class SecurityProfileService
    {
        private readonly MikrotikService _api;
        public SecurityProfileService(MikrotikService api) => _api = api;

        public Task<List<SecurityProfileModel>> GetAll() => _api.GetAsync<List<SecurityProfileModel>>("interface/wireless/security-profiles");
        public Task<SecurityProfileModel> Create(object data) => _api.PostAsync<SecurityProfileModel>("interface/wireless/security-profiles", data);
        public Task<SecurityProfileModel> Update(string id, object data) => _api.PatchAsync<SecurityProfileModel>($"interface/wireless/security-profiles/{id}", data);
        public Task<bool> Delete(string id) => _api.DeleteAsync($"interface/wireless/security-profiles/{id}");
    }
}