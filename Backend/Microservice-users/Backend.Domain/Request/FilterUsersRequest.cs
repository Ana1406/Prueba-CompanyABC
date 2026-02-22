
namespace Backend.Domain.Models
{
    public class FilterUsersRequest
    {
        public string Email { get; set; }
        public int page { get; set; } = 1;
        public int pageSize { get; set; } = 10;
    }
}
