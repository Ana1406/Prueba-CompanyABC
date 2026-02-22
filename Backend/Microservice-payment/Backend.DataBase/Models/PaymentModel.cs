using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend.Domain.Models
{
    public class PaymentModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string NameOwner { get; set; }
        public string EmailOwner { get; set; }
        public List<string> Products { get; set; }
        public double TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
