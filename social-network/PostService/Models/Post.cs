using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Models
{
    public class Post
    {
        [BsonId]
        public ObjectId PostID { get; set; } 
        [Required]
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public User user { get; set; }
        public List<Comment> comments { get; set; }

    }
}
