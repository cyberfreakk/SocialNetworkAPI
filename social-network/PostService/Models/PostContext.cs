using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Models
{
    public class PostContext
    {
        MongoClient client;
        IMongoDatabase db;

        public PostContext(IConfiguration configuration)
        {
            
            /////////////////////////////
            var mongoenv = Environment.GetEnvironmentVariable("Mongo_DB");
            if (mongoenv == null)
            {
                mongoenv = configuration.GetConnectionString("MongoDBConnection");
            }
            client = new MongoClient(mongoenv);
            var dbname = Environment.GetEnvironmentVariable("DB_NAME");

            if (dbname == null)
            {
                dbname = configuration.GetSection("MongoDatabase").Value;
            }
            db = client.GetDatabase(dbname);
            /////////////////////////////////////////////////////////////////////////////////////
            //client = new MongoClient(configuration.GetConnectionString("MongoDBConnection"));
            //db = client.GetDatabase(configuration.GetSection("MongoDatabase").Value);
        }
        public IMongoCollection<Post> Posts => db.GetCollection<Post>("Posts");
    }
}
