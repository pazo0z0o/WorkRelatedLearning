using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MongoDbDemo
{
    public class PersonModel
    {
        [BsonId] // unique identifier of the model class
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }
        public AddressModel PrimaryAddress { get; set; }
    }
}
