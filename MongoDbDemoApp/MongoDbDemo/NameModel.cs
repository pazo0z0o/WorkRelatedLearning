using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbDemo
{
    public class NameModel
    {
        [BsonId] // unique identifier of the model class
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

    }
}
