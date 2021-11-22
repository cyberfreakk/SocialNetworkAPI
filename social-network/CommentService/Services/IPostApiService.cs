using CommentService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentService.Services
{
    public interface IPostApiService
    {
        public  Task<Post> getPost(string postId, string token);
    }
}
