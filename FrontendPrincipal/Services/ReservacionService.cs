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

        public async Task<List<Casa>> GetCasasAsync()
        {
            var response = await _httpClient.GetAsync("api/casas");  // Ruta para obtener las casas

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var casas = JsonSerializer.Deserialize<List<Casa>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                // Asignar imágenes manualmente basadas en algún criterio
                foreach (var casa in casas)
                {
                    switch (casa.Nombre)
                    {
                        case "Casa de Jotade":
                            casa.UrlImagen = "/images/csjtd.jpg";
                            break;
                        case "Casa Moderna":
                            casa.UrlImagen = "/images/csmdr.jpg";
                            break;
                        case "Casa de Piedra":
                            casa.UrlImagen = "/images/cspdr.jpg";
                            break;
                        case "Edificio Safiro":
                            casa.UrlImagen = "/images/edfmdr.jpg";
                            break;
                    }
                }

                return casas;
            }
            else
            {
                throw new Exception("No se pudieron obtener las casas.");
            }
        }



        // Método para crear una nueva reserva
        public async Task CrearReservaAsync(Reservacion reserva)
        {
            var response = await _httpClient.PostAsJsonAsync("api/reservaciones", reserva);  // Ruta para crear la reserva

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("No se pudo realizar la reserva.");
            }
        }

        public async Task<Casa> GetCasaByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/casas/{id}"); // Ruta para obtener la casa por ID
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Casa>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            else
            {
                return null; // Si no existe la casa, devolvemos null
            }
        }
    }
}
