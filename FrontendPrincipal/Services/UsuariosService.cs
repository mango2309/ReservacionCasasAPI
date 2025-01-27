using FrontendPrincipal.Models;
using System.Text.Json;

namespace FrontendPrincipal.Services
{
    public class UsuariosService
    {
        private readonly HttpClient _httpClient;

        public UsuariosService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Método para hacer login
        public async Task<Usuario> LoginAsync(string email, string password)
        {
            var loginRequest = new
            {
                Email = email,
                Password = password
            };

            var response = await _httpClient.PostAsJsonAsync("api/usuarios", loginRequest);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var usuario = JsonSerializer.Deserialize<Usuario>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return usuario;
            }
            else
            {
                throw new Exception("Usuario o contraseña incorrectos.");
            }
        }

        // Método para obtener un usuario por ID (si es necesario)
        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/usuarios/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var usuario = JsonSerializer.Deserialize<Usuario>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return usuario;
            }
            else
            {
                throw new Exception("No se pudo obtener el usuario.");
            }
        }

        // Método para registrar un nuevo usuario (si se requiere)
        public async Task<Usuario> RegisterAsync(Usuario newUser)
        {
            var response = await _httpClient.PostAsJsonAsync("api/usuarios", newUser);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var usuario = JsonSerializer.Deserialize<Usuario>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return usuario;
            }
            else
            {
                throw new Exception("Error al registrar el usuario.");
            }
        }
    }
}
