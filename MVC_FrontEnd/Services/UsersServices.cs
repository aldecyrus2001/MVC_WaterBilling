using MVC_FrontEnd.Models;
using MVC_FrontEnd.URL;
using System.Net.Http.Json;


namespace MVC_FrontEnd.Services
{
    public class UsersServices
    {

        private readonly HttpClient _httpClient;
        private readonly URLs _uRLs;

        public UsersServices(HttpClient httpClient, URLs uRLs)
        {
            _httpClient = httpClient;
            _uRLs = uRLs;
        }

        public async Task<Users?> GetUserByIdAsync(int id)
        {
            var url = $"{_uRLs.FetchUsers}/{id}";
            return await _httpClient.GetFromJsonAsync<Users>(url);
        }
        
    }
}
