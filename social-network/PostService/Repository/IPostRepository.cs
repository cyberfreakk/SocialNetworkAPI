using MongoDB.Bson;
using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Repository
{
    public interface IPostRepository
    {
        object CreatePost(Post post);
        bool DeletePost(Post post);
        bool EditPost(ObjectId PostId, Post post);
        Post GetPost(ObjectId PostId);
        List<Post> GetAllPosts(string UserName);
    }
}
