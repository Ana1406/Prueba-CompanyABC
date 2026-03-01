using Backend.Core.Core.Interfaces;
using Backend.DataBase.Repositories;
using Backend.Domain.Models;
using Backend.Domain.Response;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;
using static Backend.Domain.Models.Enums;


namespace Backend.Core.Core
{
    
    public class OrdersServices : IOrderServices
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrdersServices(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ProductResponse> GetOrderByOrderId( string IdOrder)
        {
            var token = _httpContextAccessor.HttpContext?
       .Request.Headers["Authorization"]
       .ToString();
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));
            }

            var response = await _httpClient.GetAsync($"GetOrderByOrderId?orderId={IdOrder}");
            response.EnsureSuccessStatusCode();
            var order = await response.Content.ReadFromJsonAsync<GeneralResponse<ProductResponse>>();

            return order.Data!;
        }

  
    }
}
