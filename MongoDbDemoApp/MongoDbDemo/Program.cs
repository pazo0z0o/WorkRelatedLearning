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
            //What if we add a new property after declaring 2-3 documents
            PersonModel person = new PersonModel
            {   Name = "Rider",
                LastName = "Biden",
                
                PrimaryAddress = new AddressModel
                {
                StreetAddress = "101 Oak Street",
                City = "Boston",
                State = "Massachuchets",
                ZiplCode = "12345"
                }
            };
            
            db.InsertRecord("Users", person);
            //The new person document is going to ALSO have the AddressModel object but not the other 2 previous
            
            //Do an if, and if ID = null , THEN insert) = good practice 
            //db.InsertRecord("Users",new PersonModel { Name="Popi" , LastName = "Popper"});
            
            Console.ReadLine();
        }
    }
    
    public class PersonModel
    {
        [BsonId] // unique identifier of the model class
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        public string LastName { get; set; }
        public AddressModel PrimaryAddress { get; set; }
    }

    public class AddressModel 
    { 
    public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZiplCode { get; set; }
    
    
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
