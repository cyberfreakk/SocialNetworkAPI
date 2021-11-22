using CommentService.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentService.Models
{
    public class Post
    {
        [BsonId]
        public ObjectId PostID { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public User user { get; set; }
        public List<Comment> comments { get; set; }
    }
}
