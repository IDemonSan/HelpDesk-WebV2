using HelpDesk_WebV2.Models;
using System.Text.Json;
using System.Text;

namespace HelpDesk_WebV2.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://helpdesk--api.azurewebsites.net/api/Auth/login";

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> Login(string username, string password)
        {
            var model = new LoginModel { Usuario = username, Contrasena = password };
            var jsonData = JsonSerializer.Serialize(model);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_baseUrl}", content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
            return result.Token;
        }
    }
}
