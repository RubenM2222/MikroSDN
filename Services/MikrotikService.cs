using MikroSDN.Helpers;
using MikroSDN.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MikroSDN.Services
{
    internal class MikrotikService
    {
        private readonly HttpClient _client;

        public MikrotikService(RouterDevice device)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(device.BaseUrl)
            };

            _client.DefaultRequestHeaders.Authorization =
                AuthHelper.GetAuthHeader(device.Username, device.Password);
        }

        // GET
        public async Task<T> GetAsync<T>(string endpoint)
        {
            var response = await _client.GetAsync(endpoint);

            await HandleErrors(response);

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(json);
        }

        // POST
        public async Task<T> PostAsync<T>(string endpoint, object data)
        {
            var json = JsonConvert.SerializeObject(data);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(endpoint, content);

            await HandleErrors(response);

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(result);
        }

        // PATCH
        public async Task<T> PatchAsync<T>(string endpoint, object data)
        {
            var json = JsonConvert.SerializeObject(data);

            var request = new HttpRequestMessage(new HttpMethod("PATCH"), endpoint)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(request);

            await HandleErrors(response);

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(result);
        }

        // DELETE
        public async Task<bool> DeleteAsync(string endpoint)
        {
            var response = await _client.DeleteAsync(endpoint);

            await HandleErrors(response);

            return response.StatusCode == HttpStatusCode.OK;
        }

        // Tratamento de erros
        private async Task HandleErrors(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro API ({response.StatusCode}): {error}");
            }
        }
    }
}
