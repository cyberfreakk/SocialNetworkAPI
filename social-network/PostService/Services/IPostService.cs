using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PostService.Services
{
    public interface IPostService
    {
        object CreatePost(Post post, IEnumerable<Claim> userClaims);
        bool DeletePost(string PostId, IEnumerable<Claim> userClaims);
        bool EditPost(string PostId, Post post, IEnumerable<Claim> userClaims);
        Post GetPost(string PostId);
        IEnumerable<object> GetAllPosts(string UserName);
    }
}
