
namespace Backend.Domain.Models
{
    public class HealthResponse
    {
        public string Service { get; set; }
        public string DataBase { get; set; }
        public DateTime TimeStamp { get; set; }
    }

    public class StatusResponse
    {
        public string Service { get; set; }
        public string Version { get; set; }
        public string Environment { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
