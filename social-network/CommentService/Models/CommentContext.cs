using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentService.Models
{
    public class CommentContext
    {
        MongoClient client;
        IMongoDatabase db;

        public CommentContext(IConfiguration configuration)
        {
            var mongoenv = Environment.GetEnvironmentVariable("Mongo_DB");
            client = new MongoClient(mongoenv);
            if (client == null)
            {
                client = new MongoClient(configuration.GetConnectionString("MongoDBConnection"));
                db = client.GetDatabase(configuration.GetSection("MongoDatabase").Value);
            }
            if (db == null)
            {
                db = client.GetDatabase(Environment.GetEnvironmentVariable("DB_NAME"));
            }
            /////////////////////////////////////////////////////////////////////////////////////
            //client = new MongoClient(configuration.GetConnectionString("MongoDBConnection"));
            //db = client.GetDatabase(configuration.GetSection("MongoDatabase").Value);
        }
        public IMongoCollection<Post> Posts => db.GetCollection<Post>("Posts");
    }
}
