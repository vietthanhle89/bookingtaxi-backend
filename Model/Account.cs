﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Net;
using System;
using System.Numerics;
using System.Security.Principal;
using System.Text.Json.Serialization;
using static MongoDB.Driver.WriteConcern;
using static System.Net.Mime.MediaTypeNames;

namespace bookingtaxi_backend.Model
{
    public class Account
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        public string Email { get; set; } = null!;
        //[JsonIgnore]
        public string Password { get; set; } = null!;
        public string GivenName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? RoleID { get; set; }
        public string ProfileImage { get; set; } = null!;
        public bool Deleted { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}






