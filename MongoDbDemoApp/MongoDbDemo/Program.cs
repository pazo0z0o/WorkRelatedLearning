using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoCRUD db = new MongoCRUD("AddressBook");
            //What if we add a new property after declaring 2-3 documents
            //PersonModel person = new PersonModel
            //{   Name = "Rider",
            //    LastName = "Biden",

            //    PrimaryAddress = new AddressModel
            //    {
            //    StreetAddress = "101 Oak Street",a
            //    City = "Boston",
            //    State = "Massachuchets",
            //    ZiplCode = "12345"
            //    }
            //};

            //db.InsertRecord("Users", person);
            //The new person document is going to ALSO have the AddressModel object but not the other 2 previous

            //Do an if, and if ID = null , THEN insert) = good practice 
            //db.InsertRecord("Users",new PersonModel { Name="Popi" , LastName = "Popper"});
            //-----------------------------------------------------------------

            var record = db.LoadRecords<PersonModel>("Users");

            foreach (var rec in record)
            {
                Console.WriteLine($"{rec.Id}: {rec.Name} {rec.LastName}");

                if (rec.PrimaryAddress != null)
                { Console.WriteLine(rec.PrimaryAddress.City); }
            }

            //load by id 
            var oneRec = db.LoadRecordById<PersonModel>("Users", new Guid("8c04634f-3b0f-429f-a512-f8d742d73700"));

            Console.ReadLine();
        }
    }

    public class MongoCRUD
    {
        private IMongoDatabase db;
        //database connection mostly handled 
        public MongoCRUD(string database)
        {
            var client = new MongoClient();
            db = client.GetDatabase(database);
        }

        //insert 
        public void InsertRecord<T>(string table, T record)
        {
            //on the record I can pass a whole object or list or anything
            var collection = db.GetCollection<T>(table);
            collection.InsertOne(record);
        }

        //read document
        public List<T> LoadRecords<T>(string table)
        {
            var collection = db.GetCollection<T>(table);
            return collection.Find(new BsonDocument()).ToList();
        }

        public T LoadRecordById<T>(string table, Guid id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id); //Eq = equals , Gt= greater than etc

            return collection.Find(filter).FirstOrDefault();
        }

        //update or insert
        public void UpsertRecords<T>(string table, Guid id, T record)
        {
            var collection = db.GetCollection<T>(table);

            var result = collection.ReplaceOne(new BsonDocument("_id", id), record,
                new UpdateOptions { IsUpsert = true });
        }
        //delete
        public void DeleteRecord<T>(string table, Guid id)
        {
            var collection = db.GetCollection<T>(table);

            var filter = Builders<T>.Filter.Eq("Id", id); //Eq = equals , Gt= greater than etc
            collection.DeleteOne(filter);

        }

    }
}
