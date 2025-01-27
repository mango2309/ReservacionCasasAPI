using FrontendReservacionCasas.Models;
using System.Text.Json;
using System.Net.Http;

namespace FrontendReservacionCasas.Services
{
    public class TiendaService
    {
        private readonly HttpClient _httpClient;

        public TiendaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Tienda>> GetTiendasAsync()
        {
            var response = await _httpClient.GetAsync("api/tiendas"); // Ruta de tu API
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Tienda>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
