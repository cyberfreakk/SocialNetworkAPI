using Newtonsoft.Json;
//using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MongoDB.Bson;
using CommentService.Models;
using Newtonsoft.Json.Linq;

namespace CommentService.Services
{
    public class PostApiService : IPostApiService
    {
        private readonly HttpClient client;

        public PostApiService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<Post> getPost(string postId, string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token.Substring(7));
            var result = await client.GetAsync($"/api/Post/{postId}");
            if (result.IsSuccessStatusCode)
            {
              var res = await result.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(res);
                return new Post()
                {
                    PostID = ObjectId.Parse(json.GetValue("postID").ToString()),
                    Content = json.GetValue("content").ToString(),
                    CreatedOn = Convert.ToDateTime(json.GetValue("createdOn").ToString()),
                    user = JsonConvert.DeserializeObject<User>(json.GetValue("user").ToString())
                };
            }
            else
            {
                throw new Exception(result.StatusCode.ToString()+" error");
            }
        }
    }
}
