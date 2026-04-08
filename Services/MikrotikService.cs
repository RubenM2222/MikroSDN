using MikroSDN.Helpers;
using MikroSDN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikroSDN.Services
{
    internal class MikrotikService
    {
        private readonly HttpClient _client;

        public MikrotikService(RouterDevice device)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(device.BaseUrl);
            _client.DefaultRequestHeaders.Authorization =
                AuthHelper.GetAuthHeader(device.Username, device.Password);
        }

        public async Task<string> GetAsync(string endpoint)
        {
            var response = await _client.GetAsync(endpoint);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PostAsync(string endpoint, string json)
        {
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(endpoint, content);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PatchAsync(string endpoint, string json)
        {
            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, endpoint)
            {
                Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> DeleteAsync(string endpoint)
        {
            var response = await _client.DeleteAsync(endpoint);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
