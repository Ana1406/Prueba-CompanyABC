
namespace Backend.Domain.Models
{
    public class PaymentResponse
    {
        public string Id { get; set; }
        public string NameOwner { get; set; }
        public List<string> Products { get; set; }
        public double TotalPrice { get; set; }
        public string Email { get; set; }
    }
}
