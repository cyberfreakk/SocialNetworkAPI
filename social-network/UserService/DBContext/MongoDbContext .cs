using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Model;
using UserService.Model.Friend;

namespace UserService.DBContext
{
    public class MongoDbContext : IdentityDbContext<UserIdentity>
    {
        MongoClient client;
        IMongoDatabase db;
        public MongoDbContext(IConfiguration config)
        {
            var mongoenv = Environment.GetEnvironmentVariable("Mongo_DB");
            if (mongoenv == null)
            {
                mongoenv = config.GetConnectionString("MongoDBConnection");
            }
            client = new MongoClient(mongoenv);
            var dbname = Environment.GetEnvironmentVariable("DB_NAME");

            if (dbname == null)
            {
                dbname = config.GetSection("MongoDatabase").Value;
            }
            db = client.GetDatabase(dbname);
            
            
        }

    public IMongoCollection<UserFriend> UserFriends => db.GetCollection<UserFriend>("UserFriends");
    }
}
