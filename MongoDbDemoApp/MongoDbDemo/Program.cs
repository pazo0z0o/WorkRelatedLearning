using MongoDB.Bson.Serialization.Attributes;
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
            
            //Do an if, and if ID = null , THEN insert) = good practice 
            db.InsertRecord("Users",new PersonModel { Name="Kosmas" , LastName = "Stamos"});
            
            Console.ReadLine();
        }
    }
    
    public class PersonModel
    {
        [BsonId] // unique identifier of the model class
        public string Name { get; set; }
        public string LastName { get; set; }

        public Guid Id { get; set; }
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
        {//on the record I can pass a whole object or list or anything
            var collection = db.GetCollection<T>(table);
            collection.InsertOne(record);

        }






    }
}
