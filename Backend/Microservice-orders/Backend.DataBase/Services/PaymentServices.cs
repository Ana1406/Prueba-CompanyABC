using Backend.Core.Core.Interfaces;
using Backend.DataBase.Repositories;
using Backend.Domain.Models;
using Backend.Domain.Response;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;
using static Backend.Domain.Models.Enums;


namespace Backend.Core.Core
{
    
    public class PaymentServices : IPaymentServices
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private  string Token;
        public PaymentServices(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            this.Token = _httpContextAccessor.HttpContext?
      .Request.Headers["Authorization"]
      .ToString();
        }

        public async Task CreatePaymentAsync(OrderRequest order, string IdOrder)
        {
            if (!string.IsNullOrEmpty(this.Token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.Token.Replace("Bearer ", ""));
            }

            var paymentRequest = new PaymentRequest(order, IdOrder);

            var response = await _httpClient.PostAsJsonAsync("CreatePayment", paymentRequest);
            response.EnsureSuccessStatusCode();
        }

        public async Task<PaymentResponse> GetPaymentByOrderId(string IdOrder)
        {
            if (!string.IsNullOrEmpty(this.Token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.Token.Replace("Bearer ", ""));
            }


            var response = await _httpClient.GetAsync($"GetPaymentByOrderId?orderId={IdOrder}");
            response.EnsureSuccessStatusCode();
            var payment = await response.Content.ReadFromJsonAsync<GeneralResponse<PaymentResponse>>();

            return payment.Data!;
        }
    }
}
