using FrontendPrincipal.Models;
using System.Text.Json;

namespace FrontendPrincipal.Services
{
    public class ReservacionService
    {
        private readonly HttpClient _httpClient;

        public ReservacionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Reservacion>> GetReservacionesAsync()
        {
            var response = await _httpClient.GetAsync("api/reservaciones"); // Ruta de tu API
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Reservacion>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
