using MVC_FrontEnd.Models;
using MVC_FrontEnd.URL;
using MVC_WaterBilling_API.Services;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace MVC_FrontEnd.Services
{
    public class PaymentServices
    {

        private readonly HttpClient _httpClient;
        private readonly URLs _uRLs;

        public PaymentServices(HttpClient httpClient, URLs uRLs)
        {
            _httpClient = httpClient;
            _uRLs = uRLs;
        }

        public async Task<List<PaymentsWithUserConsumer>> GetPaymentsWithUserConsumersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PaymentsWithUserConsumer>>(_uRLs.Payment);
        }

        public async Task<List<PaymentsWithUserConsumer>> GetPendingPayments()
        {
            return await _httpClient.GetFromJsonAsync<List<PaymentsWithUserConsumer>>($"{_uRLs.Payment}/Pending/Payments");
        }

        public async Task<List<PaymentsWithUserConsumer>> GetConsumersPaymentWithUserConsumersAsync(string UserID)
        {
            return await _httpClient.GetFromJsonAsync<List<PaymentsWithUserConsumer>>($"{_uRLs.Payment}/ConsumersPayment/{UserID}");
        }

        public async Task<List<PaymentsWithUserConsumer>> GetOnlinePaymentsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PaymentsWithUserConsumer>>($"{_uRLs.Payment}/OnlinePayments");
        }

        public async Task<(bool isSuccess, string Message)> CreatePayment(Payments paymentData)
        {
            try
            {
                var url = $"{_uRLs.Payment}";
                var response = await _httpClient.PostAsJsonAsync(url, paymentData);

                var responseMessage = await response.Content.ReadAsStringAsync();
                ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseMessage);

                return (response.IsSuccessStatusCode, apiResponse.Message);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Inserting Payment : {ex.Message}");
                return (false, $"Error: {ex.Message}");
            }
        }

        public async Task<(bool isSuccess, string Message, int? BillID)> UpdatePaymentStatus(int id)
        {
            var url = $"{_uRLs.Payment}/{id}";
            var response = await _httpClient.PutAsJsonAsync(url, new { });

            var responseMessage = await response.Content.ReadAsStringAsync();

            ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseMessage);

            return (
                response.IsSuccessStatusCode,
                apiResponse.Message,
                apiResponse.BillID
            );
        }

        public async Task<PaymentsWithUserConsumer> GetPaymentByIdAsync(int Id)
        {
            var url = $"{_uRLs.Payment}/{Id}";
            return await _httpClient.GetFromJsonAsync<PaymentsWithUserConsumer>(url);
        }
    }
}
