
namespace Backend.Domain.Models
{
    public class OrderRequest
    {
        public string NameApplicant { get; set; }
        public string EmailApplicant { get; set; }
        public List<string> Products { get; set; }
    }
}
