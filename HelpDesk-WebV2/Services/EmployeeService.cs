using HelpDesk_WebV2.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace HelpDesk_WebV2.Services
{
    public class EmployeeService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmployeeService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Empleado>> GetEmployees()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("JwtToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync("https://helpdesk--api.azurewebsites.net/api/Empleados/Listar");
            response.EnsureSuccessStatusCode();

            var employees = await response.Content.ReadFromJsonAsync<IEnumerable<Empleado>>();
            return employees;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("JwtToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.DeleteAsync($"https://helpdesk--api.azurewebsites.net/api/Empleados/Eliminar/{id}");
            response.EnsureSuccessStatusCode();

            return response.IsSuccessStatusCode;
        }

        public async Task<Empleado> ObtenerEmpleado(int id)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("JwtToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"https://helpdesk--api.azurewebsites.net/api/Empleados/ObtenerEmpleado/{id}");
            response.EnsureSuccessStatusCode();
            var employee = await response.Content.ReadFromJsonAsync<Empleado>();
            return employee;
        }

        public async Task<bool> Registrar(Empleado empleado)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("JwtToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PostAsJsonAsync("https://helpdesk--api.azurewebsites.net/api/Empleados/Registrar", empleado);
            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Actualizar(Empleado empleado)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("JwtToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PutAsJsonAsync($"https://helpdesk--api.azurewebsites.net/api/Empleados/Actualizar/{empleado.id_empleado}", empleado);
            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
        }
    }
}
