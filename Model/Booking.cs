using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace bookingtaxi_backend.Model
{
    public class Booking
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        public string CustomerID { get; set; } = null!;
        public DateTime BookingDate { get; set; }
        public string CarTypeID { get; set; } = null!;
        public DateTime CompleteDate { get; set; }
        public string BookingStatusID { get; set; } = null!;
        public string StartLong { get; set; } = null!;
        public string StartLat { get; set; } = null!;
        public string StartAddress { get; set; } = null!;
        public string StartPlaceID { get; set; } = null!;
        public string EndLong { get; set; } = null!;
        public string EndLat { get; set; } = null!;
        public string EndAddress { get; set; } = null!;
        public string EndPlaceID { get; set; } = null!;
        public string Distance { get; set; } = null!;
        public decimal Price { get; set; }
        public string Duration{ get; set; } = null!;

        public string MakerAccountID { get; set; } = null!;
        public string Note { get; set; } = null!;
        public bool Deleted { get; set; }
    }

    public class BookingStatus
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;
    }

    public class BookingAssignation
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        public string BookingID { get; set; } = null!;
        public string DriverID { get; set; } = null!;
        public DateTime AssignDate { get; set; }
        public bool Deleted { get; set; }
        public decimal Price { get; set; }
    }
}
