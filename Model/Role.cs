using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace bookingtaxi_backend.Models
{
    [BsonIgnoreExtraElements]
    public class Role
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; } = null!;
        public bool Deleted { get; set; }

        public Role() { Deleted = false; }
        public Role(string name) { Name = name; Deleted = false; }
    }



    public static class ROLEID {
        public static string DRIVER = "000000000000000000000001";
        public static string ADMIN = "000000000000000000000002";
        public static string CUSTOMER = "000000000000000000000003";
        public static string SUPPORTER = "000000000000000000000004";
    }
}
