using MVC_FrontEnd.Models;
using MVC_FrontEnd.URL;
using MVC_WaterBilling_API.Services;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace MVC_FrontEnd.Services
{
    public class BillingService
    {
        private readonly HttpClient _httpClient;
        private readonly URLs uRLs;

        public BillingService(HttpClient httpClient, URLs uRLs)
        {
            _httpClient = httpClient;
            this.uRLs = uRLs;
        }

        public async Task<(bool isSuccess, string Message)> createBilling(Bills billingData)
        {
            try
            {
                var url = $"{uRLs.Billing}";
                var response = await _httpClient.PostAsJsonAsync(url, billingData);

                var responseMessage = await response.Content.ReadAsStringAsync();
                ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseMessage);
                return (response.IsSuccessStatusCode, apiResponse.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting bills: {ex.ToString}");
                return (false, $"Error: {ex.Message}");
            }
        }
    }
}
