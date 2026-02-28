
namespace Backend.Domain.Models
{
    public class PaymentRequest
    {
        public string NameOwner { get; set; }
        public string IdOrder { get; set; }
        public string EmailOwner { get; set; }
        public double TotalPrice { get; set; }
        public List<string> Products { get; set; }

        public PaymentRequest(OrderRequest orderRequest,string IdOrderIn) {
            NameOwner = orderRequest.NameApplicant;
            IdOrder=IdOrderIn;
            EmailOwner = orderRequest.EmailApplicant;
            TotalPrice = new Random().Next(1000, 10000);
            Products = orderRequest.Products;
        }
    }
}
