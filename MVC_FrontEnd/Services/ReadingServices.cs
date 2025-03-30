using MVC_FrontEnd.Models;
using MVC_FrontEnd.URL;
using MVC_WaterBilling_API.Services;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace MVC_FrontEnd.Services
{
    public class ReadingServices
    {
        private readonly HttpClient _httpClient;
        private readonly URLs _urls;

        public ReadingServices(HttpClient httpClient, URLs urls)
        {
            _httpClient = httpClient;
            _urls = urls;
        }

        public async Task<List<ReadingConsumerUser>> GetReadingWithUserConsumerAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ReadingConsumerUser>>(_urls.Reading);
        }

        public async Task<Reading> GetReadingByID(int Id)
        {
            var url = $"{_urls.Reading}/{Id}";
            return await _httpClient.GetFromJsonAsync<Reading>(url);
        }

        public async Task<Reading> GetReadingByMeterNumber(string meterNumber)
        {
            var url = $"{_urls.Reading}/{meterNumber}/search";
            return await _httpClient.GetFromJsonAsync<Reading>(url);
        }

        public async Task<bool> DeleteReadingByID(int Id)
        {
            var url = $"{_urls.Reading}/{Id}/Delete";
            var response = await _httpClient.DeleteAsync(url);

            return response.IsSuccessStatusCode;
        }

        public async Task<(bool isSuccess, string Message)> InitializedReading(Consumers consumerData)
        {
            try
            {
                var url = $"{_urls.Reading}/Initialized";
                var response = await _httpClient.PostAsJsonAsync(url, consumerData);

                var responseMessage = await response.Content.ReadAsStringAsync();

                ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseMessage);
                return (response.IsSuccessStatusCode, apiResponse.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading initialization: {ex.Message}");
                return (false, $"Error: {ex.Message}");
            }
        }

        public async Task<(bool isSuccess, string Message, int? ReadingID)> createReading(Reading readingData)
        {
            try
            {
                var url = $"{_urls.Reading}";
                var response = await _httpClient.PostAsJsonAsync(url, readingData);

                var responseMessage = await response.Content.ReadAsStringAsync();

                ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseMessage);

                int? readingID = null;
                if (response.IsSuccessStatusCode && apiResponse != null && apiResponse.ReadingID != null)
                {
                    readingID = apiResponse.ReadingID;
                }
                return (response.IsSuccessStatusCode, apiResponse.Message, readingID);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting reading: {ex.ToString}");
                return (false, $"Error: {ex.Message}", null);
            }
        }
    }
}
