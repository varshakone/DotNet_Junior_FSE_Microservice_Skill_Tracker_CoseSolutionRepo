using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using MongoDB.Driver;



namespace User_Microservice.DataLayer
{
    public class MongoDBContext : IMongoDBContext
    {
        private IMongoDatabase _mongoDB;
        private IMongoClient _mongoClient;
        public MongoDBContext(IOptions<MongoSettings> options)
        {
            _mongoClient = new MongoClient(options.Value.Connection);
            _mongoDB = _mongoClient.GetDatabase(options.Value.DatabaseName);
        }
        public IMongoCollection<User> GetCollection<User>(string name)
        {
            return _mongoDB.GetCollection<User>(name);
        }
    }
}
