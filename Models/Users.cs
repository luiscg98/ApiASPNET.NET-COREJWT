using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;

namespace ApiTarea.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Username")]
        [JsonProperty("Username")]
        public string Username { get; set; }

        public string password { get; set; }

        public string fecha { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
