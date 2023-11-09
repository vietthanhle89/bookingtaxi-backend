using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace bookingtaxi_backend.Model
{
    [BsonIgnoreExtraElements]
    public class DriverStatus
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; } = null!;
        public bool Deleted { get; set; }

        public DriverStatus() { Deleted = false; }
        public DriverStatus(string name) { Name = name; Deleted = false; }
    }


    public static class DriverStatusID
    {
        public static string TAKING_BREAK = "000000000000000000000001";
        public static string READY = "000000000000000000000002";
        public static string ON_THE_GO = "000000000000000000000003";
    }
}
