using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PostService.Models;
using PostService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Tests.InfraSetup;
using Xunit;

namespace Tests.Service
{
    public class PostServiceTest : IClassFixture<PostDbFixture>
    {
        private readonly PostDbFixture fixture;
        private readonly IPostRepository repo;

        public PostServiceTest(PostDbFixture _fixture)
        {
            this.fixture = _fixture;
            repo = new PostRepository(fixture.context);
        }

        [Fact]
        public Post CreatePostShouldSuccess()
        {
            var post = this.GetPost();            
            var service = new PostService.Services.PostService(repo);
            var actual = service.CreatePost(post, this.GetClaims());
            var expected = "Post Content Created";
            var val = JObject.Parse(JsonConvert.SerializeObject(actual)).GetValue("Content");
            Assert.Equal(expected, val);
            return post;
        }

        [Fact]
        public void DeletePostShouldReturnTrue()
        {
            var post = CreatePostShouldSuccess();
            var service = new PostService.Services.PostService(repo);
            var actual = service.DeletePost(post.PostID.ToString(), this.GetClaims());
            Assert.True(actual);
        }

        [Fact]
        public void EditPostShouldReturnTrue()
        {
            var post = CreatePostShouldSuccess();
            var service = new PostService.Services.PostService(repo);
            var pid = post.PostID.ToString();
            var actual = service.EditPost(pid, post, this.GetClaims());
            Assert.True(actual);
        }

        [Fact]
        public void GetPostShouldReturnPost()
        {
            var post = CreatePostShouldSuccess();
            var service = new PostService.Services.PostService(repo);
            var actual = service.GetPost(post.PostID.ToString());            
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<Post>(actual);
        }

        [Fact]
        public void GetAllPostsShouldReturnAList()
        {
            string username = "";            
            var service = new PostService.Services.PostService(repo);
            var actual = service.GetAllPosts(username);
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<IEnumerable<Object>>(actual);
        }


        private Post GetPost()
        {
            Post post = new Post();
            post.Content = "Post Content Created";
            post.CreatedOn = new DateTime();
            post.user = this.GetUser();            
            return post;
        }

        private User GetUser()
        {
            User user = new User();
            user.UserName = "user1";
            user.FirstName = "first";
            user.LastName = "last";
            user.EmailId = "user@gmail.com";
            user.Password = "Pass@123";
            user.ContactNo = "9999999999";
            user.DOB = new DateTime();
            user.Gender = "Male";
            return user;
        }

        private IEnumerable<Claim> GetClaims()
        {
            User user = this.GetUser();
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.EmailId),
                    new Claim(ClaimTypes.MobilePhone, user.ContactNo),
                    new Claim("role", "User"),
                    new Claim(ClaimTypes.Gender, user.Gender),
                    new Claim(ClaimTypes.DateOfBirth, user.DOB.ToString()),
                    new Claim("FirstName", user.FirstName),
                    new Claim("LastName", user.LastName)
                };
            return authClaims;
        }
    }
}
