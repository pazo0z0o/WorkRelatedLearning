using MongoDB.Driver;
using MongoDbDemo2;

//Could and should be doing it with appsettings -- All the info needed to connect and create a db
string connectionString = "mongodb://localhost:27017/";
string databaseName = "simple_db";
string collectionName = "people";
//--------------------------------------------------------
//Handles a connection to a database
var client = new MongoClient(connectionString);
var db = client.GetDatabase(databaseName);  
var collection = db.GetCollection<PersonModel>(collectionName);


var person = new PersonModel { FirstName = "Panos" , LastName = "Panou"};

await collection.InsertOneAsync(person);

var results = await collection.FindAsync(_ => true); // returns every record ( _ = every--discard character)

foreach(var result in results.ToList())
{
    Console.WriteLine($"{result.Id}: {result.FirstName} {result.LastName}");
}


























