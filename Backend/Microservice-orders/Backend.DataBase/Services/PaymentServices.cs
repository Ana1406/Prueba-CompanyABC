using Backend.Core.Core.Interfaces;
using Backend.DataBase.Repositories;
using Backend.Domain.Models;
using Backend.Domain.Response;
using System.Net.Http.Json;
using static Backend.Domain.Models.Enums;


namespace Backend.Core.Core
{
    
    public class PaymentServices : IPaymentServices
    {
        private readonly HttpClient _httpClient;

        public PaymentServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreatePaymentAsync(OrderRequest order, string IdOrder)
        {
            var paymentRequest = new PaymentRequest(order, IdOrder);

            var response = await _httpClient.PostAsJsonAsync("CreatePayment", paymentRequest);
            response.EnsureSuccessStatusCode();
        }

        public async Task<PaymentResponse> GetPaymentByOrderId(string IdOrder)
        {

            var response = await _httpClient.GetAsync($"GetPaymentByOrderId?orderId={IdOrder}");
            response.EnsureSuccessStatusCode();
            var payment = await response.Content.ReadFromJsonAsync<GeneralResponse<PaymentResponse>>();

            return payment.Data!;
        }
    }
}
