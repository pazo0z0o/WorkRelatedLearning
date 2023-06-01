using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoExample
{
    public class Playlist
    {
        public ObjectId _id { get; set; }

        [BsonElement("user")]
        public string? Username { get; set; } = null!;
        public List<string> Items { get; set; } = null!;

        public Playlist(string username, List<string> movieIds)
        {
            this.Username = username;
            this.Items = movieIds;
        }

    }
}
