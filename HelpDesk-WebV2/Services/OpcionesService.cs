using HelpDesk_WebV2.Models;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace HelpDesk_WebV2.Services
{
    public class OpcionesService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public OpcionesService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<List<Opciones>> GetOpcionesAsync()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("JwtToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var apiUrl = _configuration["https://helpdesk--api.azurewebsites.net"];
            var response = await _httpClient.GetAsync($"{apiUrl}/api/Opciones/ListarOpciones");
            response.EnsureSuccessStatusCode();

            var opciones = await response.Content.ReadFromJsonAsync<List<Opciones>>();
            return opciones ?? new List<Opciones>();
        }
    }
}