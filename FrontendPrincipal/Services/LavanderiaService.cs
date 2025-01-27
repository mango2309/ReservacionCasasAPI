using FrontendPrincipal.Models;
using System.Text.Json;
using System.Net.Http;

namespace FrontendPrincipal.Services
{
    public class LavanderiaService
    {
        private readonly HttpClient _httpClient;

        public LavanderiaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Lavanderia>> GetLavanderiasAsync()
        {
            var response = await _httpClient.GetAsync("api/lavanderias"); // Ruta de tu API
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Lavanderia>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
