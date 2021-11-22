using PostService.Exceptions;
using PostService.Models;
using PostService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PostService.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository repo;
        public PostService(IPostRepository repo)
        {
            this.repo = repo;
        }
        public object CreatePost(Post post, IEnumerable<Claim> userClaims)
        {
            User u = new User()
            {
                UserName = userClaims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault().Value,
                EmailId = userClaims.Where(c => c.Type == ClaimTypes.Email).FirstOrDefault().Value,
                FirstName = userClaims.Where(c => c.Type == "FirstName").FirstOrDefault().Value,
                LastName = userClaims.Where(c => c.Type == "LastName").FirstOrDefault().Value,
                ContactNo = userClaims.Where(c => c.Type == ClaimTypes.MobilePhone).FirstOrDefault().Value,
                DOB = Convert.ToDateTime(userClaims.Where(c => c.Type == ClaimTypes.DateOfBirth).FirstOrDefault().Value),
                Gender = userClaims.Where(c => c.Type == ClaimTypes.Gender).FirstOrDefault().Value,
            };

            post.user = u;
            
            return repo.CreatePost(post);            
        }
        public bool DeletePost(string PostId, IEnumerable<Claim> userClaims)
        {
            var loginUser = userClaims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault().Value;
            var role = userClaims.Where(c => c.Type == "role").FirstOrDefault().Value;

            var res = repo.GetPost(MongoDB.Bson.ObjectId.Parse( PostId));
            if (res == null)
                throw new PostNotFoundException("Post not found");

            if (res.user.UserName != loginUser&&role!="Admin")
            {
                throw new Exception("You can not delete this post");

            }
            return repo.DeletePost(res);
        }

        public bool EditPost(string PostId, Post post, IEnumerable<Claim> userClaims)
        {
            var loginUser = userClaims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault().Value;
            var role = userClaims.Where(c => c.Type == "role").FirstOrDefault().Value;

            var p = repo.GetPost(MongoDB.Bson.ObjectId.Parse(PostId));
            if (p == null)
            {
                throw new PostNotFoundException("Post not found");
            }
            if (role != "Admin" &&p.user.UserName != loginUser)
            {
                throw new Exception("You can not edit this post");

            }
            User u = new User()
            {
                UserName = userClaims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault().Value,
                EmailId = userClaims.Where(c => c.Type == ClaimTypes.Email).FirstOrDefault().Value,
                FirstName = userClaims.Where(c => c.Type == "FirstName").FirstOrDefault().Value,
                LastName = userClaims.Where(c => c.Type == "LastName").FirstOrDefault().Value,
                ContactNo = userClaims.Where(c => c.Type == ClaimTypes.MobilePhone).FirstOrDefault().Value,
                DOB = Convert.ToDateTime(userClaims.Where(c => c.Type == ClaimTypes.DateOfBirth).FirstOrDefault().Value),
                Gender = userClaims.Where(c => c.Type == ClaimTypes.Gender).FirstOrDefault().Value,
            };

            post.user = u;
            return repo.EditPost(MongoDB.Bson.ObjectId.Parse(PostId), post);
        }

        public IEnumerable<object> GetAllPosts(string UserName)
        {
            if (repo.GetAllPosts(UserName) == null)
            {
                throw new PostNotFoundException("There are no posts to show for this user.");
            }
            var list= repo.GetAllPosts(UserName);
            foreach (var post in list)
            {
                yield return 
                    new
                    {
                        PostID = post.PostID.ToString(),
                        Content = post.Content,
                        CreatedOn = post.CreatedOn,
                        user = post.user,
                        comments = post.comments
                    };

            }
        }

        public Post GetPost(string PostId)
        {
            var res = repo.GetPost(MongoDB.Bson.ObjectId.Parse(PostId));
            if (res == null)
            {
                throw new PostNotFoundException("Post not found");
            }
            return res;
        }
    }
}
