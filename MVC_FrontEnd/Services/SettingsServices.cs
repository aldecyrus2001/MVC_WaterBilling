using MVC_FrontEnd.Models;
using MVC_FrontEnd.URL;
using System.Net.Http.Json;

namespace MVC_FrontEnd.Services
{
    public class SettingsServices
    {
        private readonly HttpClient _httpClient;
        private readonly URLs uRLs;

        public SettingsServices(HttpClient httpClient, URLs uRLs)
        {
            _httpClient = httpClient;
            this.uRLs = uRLs;
        }

        public async Task<Settings?> GetSettings()
        {
            var url = $"{uRLs.Settings}";
            return await _httpClient.GetFromJsonAsync<Settings>(url);
        }

        public async Task<bool> UpdateSettingsASync(Settings settingsData)
        {
            try
            {
                var url = $"{uRLs.Settings}";
                var response = await _httpClient.PutAsJsonAsync(url, settingsData);
                return response.IsSuccessStatusCode;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating settings: {ex.Message}");
                return false;
            }
        }
    }
}
