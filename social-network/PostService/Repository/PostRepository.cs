using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections;

namespace PostService.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly PostContext db;
        public PostRepository(PostContext db)
        {
            this.db = db;
        }
        public object CreatePost(Post post)
        {
            db.Posts.InsertOne(post);
            return new {
                PostID  = post.PostID.ToString(),
                Content = post.Content,
                CreatedOn = post.CreatedOn,
                user = post.user,
                Comments = new ArrayList()
            };
        }

        public bool DeletePost(Post post)
        {
            
            var res = db.Posts.DeleteOne(x => x.PostID == post.PostID);
            return res.IsAcknowledged;
        }

        public bool EditPost(ObjectId PostId, Post post)
        {
            var filter = Builders<Post>.Filter.Where(x => x.PostID == PostId);
            if(filter != null)
            {
                var update = Builders<Post>.Update.Set(x => x.Content, post.Content)
                .Set(x => x.CreatedOn, DateTime.Now)
                .Set(x => x.user, post.user);
                db.Posts.UpdateOne(filter, update);
                return true;
            }
            return false;            
        }

        public List<Post> GetAllPosts(string UserName)
        {
            var list= db.Posts.Find( p=>p.user.UserName==UserName).ToList();
            return list;
        }

        public Post GetPost(ObjectId PostId)
        {
            return db.Posts.Find(x => x.PostID== PostId).FirstOrDefault();
            
        }
    }
}
