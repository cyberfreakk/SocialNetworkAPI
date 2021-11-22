using FriendsAPI.Model;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsAPI.DbContext
{
    public class AppDbContext
    {
        MongoClient client;
        IMongoDatabase db;

        public AppDbContext(IConfiguration config)
        {

            //var mongoenv = Environment.GetEnvironmentVariable("Mongo_DB");
            //client = new MongoClient(mongoenv);
            //if (client == null)
            //{
            //    client = new MongoClient(config.GetConnectionString("MongoDBConnection"));
            //    db = client.GetDatabase(config.GetSection("MongoDatabase").Value);
            //}
            //if (db == null)
            //{
            //    db = client.GetDatabase(Environment.GetEnvironmentVariable("DB_NAME"));
            //}
            ////////////////////////////////////////////////////////////////////////////
            
            client = new MongoClient(config.GetConnectionString("MongoDBConnection"));
            db = client.GetDatabase(config.GetSection("MongoDatabase").Value);
        }

        public IMongoCollection<UserFriend> UserFriends => db.GetCollection<UserFriend>("UserFriends");
    }
}
