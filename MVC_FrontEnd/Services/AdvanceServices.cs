using MVC_FrontEnd.Models;
using MVC_FrontEnd.URL;
using MVC_WaterBilling_API.Services;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace MVC_FrontEnd.Services
{
    public class AdvanceServices
    {
        private readonly HttpClient _httpClient;
        private readonly URLs _uRLs;

        public AdvanceServices(HttpClient httpClient, URLs uRLs)
        {
            _httpClient = httpClient;
            _uRLs = uRLs;
        }

        public async Task<Advances> getAdvanceWithConsumerID(int ConsumerID)
        {
            var url = $"{_uRLs.Advance}/{ConsumerID}";
            return await _httpClient.GetFromJsonAsync<Advances>(url);
        }

        public async Task<(bool isSuccess, string Message)> UpdateAdvance(string id)
        {
            var url = $"{_uRLs.Advance}";
            var response = await _httpClient.PutAsJsonAsync(url, id);

            var responseMessage = await response.Content.ReadAsStringAsync();

            ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseMessage);


            return (response.IsSuccessStatusCode, apiResponse.Message);
        }
    }
}
