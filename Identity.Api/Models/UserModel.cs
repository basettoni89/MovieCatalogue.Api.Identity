using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCatalogue.Api.Identity.Models
{
    public class UserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.Decimal128)]
        public int ID { get; set; }

        [BsonElement("Username")]
        public string Username { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }
    }
}
