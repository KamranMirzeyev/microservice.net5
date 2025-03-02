﻿using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Service.Catalog.Models
{
    public class Course
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }

        public string UserId { get; set; }

        public string Picture { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreateTime { get; set; }

        public Feature Feature { get; set; }
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string CatagoryId { get; set; }

        [BsonIgnore]
        public Category Catagory { get; set; }
    }
}