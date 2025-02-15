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
            var url = $"{_uRLs.Users}/{id}";
            return await _httpClient.GetFromJsonAsync<Users>(url);
        }

        public async Task<bool> AddUsers(Users userData)
        {
            try
            {
                var url = $"{_uRLs.Users}";
                var response = await _httpClient.PostAsJsonAsync(url, userData);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Inserting user: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateUserAsync(int id, Users userData)
        {
            try
            {
                var url = $"{_uRLs.Users}/{id}";
                var response = await _httpClient.PutAsJsonAsync(url, userData);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating user: {ex.Message}");
                return false;
            }

        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                var url = $"{_uRLs.Users}/{id}/delete";
                var response = await _httpClient.DeleteAsync(url);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting user: {ex.Message}");
                return false;
            }
        }

    }
}
