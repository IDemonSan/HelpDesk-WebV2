using HelpDesk_WebV2.Models;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace HelpDesk_WebV2.Services
{
    public class TicketsService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public TicketsService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<List<Tickets>> GetTicketsAsync()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("JwtToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var apiUrl = _configuration["https://helpdesk--api.azurewebsites.net"];
            var response = await _httpClient.GetAsync($"{apiUrl}/api/Tickets/Listar");
            response.EnsureSuccessStatusCode();

            var tickets = await response.Content.ReadFromJsonAsync<List<Tickets>>();
            return tickets ?? new List<Tickets>();
        }
    }
}