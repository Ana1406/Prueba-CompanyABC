
namespace Backend.Domain.Models
{
    public class PaymentRequest
    {
        public string NameOwner { get; set; }
        public string IdOrder { get; set; }
        public string EmailOwner { get; set; }
        public double TotalPrice { get; set; }
        public List<string> Products { get; set; }
    }
}
