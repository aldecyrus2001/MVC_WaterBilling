using MVC_FrontEnd.Models;
using MVC_FrontEnd.URL;
using System.Net.Http.Json;

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

        public async Task<ConsumersInformations?> GetConsumerByID(int id)
        {
            var url = $"{_uRLs.Consumer}/{id}";
            return await _httpClient.GetFromJsonAsync<ConsumersInformations>(url);
        }
    }

}
