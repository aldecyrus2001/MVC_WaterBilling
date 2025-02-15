using MVC_FrontEnd.Models;
using MVC_FrontEnd.URL;
using MVC_WaterBilling_API.Services;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace MVC_FrontEnd.Services
{
    public class ConsumerServices
    {
        private readonly HttpClient _httpClient;
        private readonly URLs _uRLs;


        public ConsumerServices(HttpClient httpClient, URLs uRLs)
        {
            _httpClient = httpClient;
            _uRLs = uRLs;
        }


        public async Task<ConsumerWithUser?> GetConsumerByID(int id)
        {
            var url = $"{_uRLs.Consumer}/{id}";
            return await _httpClient.GetFromJsonAsync<ConsumerWithUser>(url);
        }

        public async Task<Consumers> GetConsumerByMeterNumber(string MeterNumber)
        {
            var url = $"{_uRLs.Consumer}/{MeterNumber}/meterNumber";
            return await _httpClient.GetFromJsonAsync<Consumers>(url);
        }

        public async Task<(bool IsSuccess, string Message)> AddConsumer(Consumers consumerData)
        {
            try
            {
                var url = $"{_uRLs.Consumer}";
                var response = await _httpClient.PostAsJsonAsync(url, consumerData);

                var responseMessage = await response.Content.ReadAsStringAsync();

                ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseMessage);
                return (response.IsSuccessStatusCode, apiResponse.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting consumer: {ex.Message}");
                return (false, $"Error: {ex.Message}");
            }
        }

        public async Task<bool> UpdateConsumerDataASync(int id, ConsumerWithUser consumerData)
        {
            try
            {
                var url = $"{_uRLs.Consumer}/{id}";
                var response = await _httpClient.PutAsJsonAsync(url, consumerData.Consumer);
                return response.IsSuccessStatusCode;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Inserting consumer: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeactivateConsumerAsync(int id)
        {
            try
            {
                var url = $"{_uRLs.Consumer}/{id}/disconnect";
                var response = await _httpClient.PutAsync(url, null);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Log the error and provide user feedback
                Console.WriteLine($"Error Deactivating consumer: {ex.Message}");
                return false;
            }
        }
    }

}
