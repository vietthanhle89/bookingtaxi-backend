using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace bookingtaxi_backend.Model
{
    public class TripRecord
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        public string BookingID { get; set; } = null!;
        public string Long { get; set; } = null!;
        public string Lat { get; set; } = null!;

        public DateTime Date { get; set; }
    }


}
