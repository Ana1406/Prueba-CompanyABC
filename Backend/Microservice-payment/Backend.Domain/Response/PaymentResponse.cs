
namespace Backend.Domain.Models
{
    public class PaymentResponse
    {
        public string IdPayment { get; set; }
        public string IdOrder { get; set; }
        public string NameOwner { get; set; }
        public List<string> Products { get; set; }
        public double TotalPrice { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
    }
}
