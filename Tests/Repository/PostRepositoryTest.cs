using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PostService.Models;
using PostService.Repository;
using Tests.InfraSetup;
using Xunit;

namespace Tests.Repository
{
    public class PostRepositoryTest :IClassFixture<PostDbFixture>
    {
        PostDbFixture fixture;
        private readonly IPostRepository repository;

        public PostRepositoryTest(PostDbFixture _fixture)
        {
            this.fixture = _fixture;
            repository = new PostRepository(fixture.context);
        }
        [Fact]
        public void CreatePostShouldSuccess()
        {
            Post post = new Post();            
            post.Content = "This is post content";
            post.CreatedOn = new DateTime();
            post.user = this.GetUser();
            var actual = repository.CreatePost(post);
            var expected = "This is post content";
            Assert.Equal(JObject.Parse(JsonConvert.SerializeObject(actual)).GetValue("Content"), expected);
        }

        [Fact]
        public void DeletePostShouldReturnTrue()
        {
            Post post = new Post();
            post.Content = "This is post content";
            post.CreatedOn = new DateTime();
            post.user = this.GetUser();
            var actual = repository.DeletePost(post);
            Assert.True(actual);
        }

        [Fact]
        public void EditPostShouldReturnTrue()
        {
            Post post = new Post();
            post.Content = "Post Content Updated";
            post.CreatedOn = new DateTime();
            post.user = this.GetUser();
            var val = JObject.Parse(JsonConvert.SerializeObject(this.GetPost())).GetValue("PostID").ToString();
            var actual = repository.EditPost(new ObjectId(val), post);
            Assert.True(actual);
        }

        [Fact]
        public void GetAllPostsShouldReturnAlist()
        {
            string username = "user1";

            var actual = repository.GetAllPosts(username);
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<List<Post>>(actual);
        }

        [Fact]
        public void GetPostShouldReturnAPost()
        {
            var val = JObject.Parse(JsonConvert.SerializeObject(this.GetPost())).GetValue("PostID").ToString();
            var actual = repository.GetPost(new ObjectId(val));
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<Post>(actual);
        }

        private Object GetPost()
        {
            Post post = new Post();
            post.Content = "Post Content";
            post.CreatedOn = new DateTime();
            post.user = this.GetUser();
            var actual = repository.CreatePost(post);
            return actual;
        }

        //private Post GetPost()
        //{
        //    Post post = new Post();
        //    post.Content = "Post Content";
        //    post.CreatedOn = new DateTime();
        //    post.user = this.GetUser();
        //    return post;
        //}

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
    }
}
