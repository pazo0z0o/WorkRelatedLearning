﻿using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MongoDbDemo
{
    public class PersonModel
    {
        [BsonId] // unique identifier of the model class
        public Guid Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        [BsonElement("dob")]
        public DateTime DateOfBirth { get; set; }
        public AddressModel PrimaryAddress { get; set; }
    }
}
