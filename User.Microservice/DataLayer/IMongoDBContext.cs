using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace User_Microservice.DataLayer
{
  public  interface IMongoDBContext
    {
        IMongoCollection<User> GetCollection<User>(string name);
    }
}
