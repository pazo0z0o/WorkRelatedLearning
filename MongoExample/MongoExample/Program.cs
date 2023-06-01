using MongoDB.Driver;
using MongoExample;

MongoClient client = new MongoClient("mongodb+srv://kossmasstamoss:070891p%40p%40r0P0ulos@trainingcluster.ijf9u0g.mongodb.net/");

//List<string> databases = client.ListDatabaseNames().ToList();           //just checking if my connection is working
//foreach (var database in databases) { Console.WriteLine(database); }        

var playlistCollection = client.GetDatabase("sample_mflix").GetCollection<Playlist>("playlist");
//Items list from Playlist.cs
List<string> movieList = new List<string>();
movieList.Add("Gladiator");

//playlistCollection.InsertOne(new Playlist("nicraboy",movieList));

//Filtering for later use -- Could be a different file with many of them, if we make them abstract enough
FilterDefinition<Playlist> filter = Builders<Playlist>.Filter.Eq("Username", "nicraboy");

List<Playlist> results = playlistCollection.Find(filter).ToList();
foreach (Playlist entry in results)
{
    Console.WriteLine(entry.Items.ToString());
}
//update operation with filter for already existing value
UpdateDefinition<Playlist> update = Builders<Playlist>.Update.AddToSet<string>("Items", "5678");
//uses the 
playlistCollection.UpdateOne(filter, update);
results = playlistCollection.Find(filter).ToList();
foreach (var entry in results)
{
    Console.WriteLine(entry.Items.ToString());

}












































































