using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend.Domain.Models
{
    public class OrderModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string IdOrder { get; set; }

        public string IdUser { get; set; }  
        public string NameApplicant { get; set; }
        public string EmailApplicant { get; set; }
        public List<string> Products { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Enabled { get; set; }
    }
}
