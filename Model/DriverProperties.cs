using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace bookingtaxi_backend.Model
{    
    public class DocumentationImage
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        public string DriverID { get; set; } = null!;
        public string Image { get; set; } = null!;
    }

    public class DriverCar
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        public string DriverID { get; set; } = null!;
        public string CarTypeID { get; set; } = null!;
        public string Color { get; set; } = null!;
        public string Number { get; set; } = null!;        
        public bool Deleted { get; set; }
    }

    public class CarType
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal PricePerKm { get; set; }
    }
}
