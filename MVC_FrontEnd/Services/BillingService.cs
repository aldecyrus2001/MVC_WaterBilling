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

        public async Task<(bool isSuccess, string Message, int? BillID)> createBilling(Bills billingData)
        {
            try
            {
                var url = $"{uRLs.Billing}";
                var response = await _httpClient.PostAsJsonAsync(url, billingData);

                var responseMessage = await response.Content.ReadAsStringAsync();

                ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseMessage);

                int? billID = null;
                if (response.IsSuccessStatusCode && apiResponse != null && apiResponse.BillID != null)
                {
                    billID = apiResponse.BillID;
                }
                return (response.IsSuccessStatusCode, apiResponse.Message, billID);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting bills: {ex.ToString}");
                return (false, $"Error: {ex.Message}", null);
            }
        }

        public async Task<Bills?> GetBillingByReadingID(int? id)
        {
            var url = $"{uRLs.Billing}/{id}/ReadingID";
            return await _httpClient.GetFromJsonAsync<Bills>(url);
        }

        public async Task<Bills?> GetBillingByID(int? id)
        {
            var url = $"{uRLs.Billing}/{id}";
            return await _httpClient.GetFromJsonAsync<Bills>(url);
        }

        public async Task<(bool isUpdated, string Message)> UpdateBilling(int? billID)
        {
            var url = $"{uRLs.Billing}/Consumers/{billID}";
            var response = await _httpClient.PutAsJsonAsync(url, billID);

            var responseMessage = await response.Content.ReadAsStringAsync();

            ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseMessage);


            return (response.IsSuccessStatusCode, apiResponse.Message);
        }

        public async Task<(bool isUpdated, string Message)> UpdateBillingStatus(int? billID, string Status)
        {
            var url = $"{uRLs.Billing}/Consumers/{billID}/{Status}";
            var response = await _httpClient.PutAsJsonAsync(url, billID);

            var responseMessage = await response.Content.ReadAsStringAsync();

            ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseMessage);


            return (response.IsSuccessStatusCode, apiResponse.Message);
        }


        public async Task<Bills> GetBillingByReferenceNumber(string referenceNumber)
        {
            var url = $"{uRLs.Billing}/{referenceNumber}/reference";
            return await _httpClient.GetFromJsonAsync<Bills>(url);
        }

        public async Task<bool> DeleteBill(int Id)
        {
            var url = $"{uRLs.Billing}/{Id}/Delete";
            var response = await _httpClient.DeleteAsync(url);

            return response.IsSuccessStatusCode;
        }
    }
}
