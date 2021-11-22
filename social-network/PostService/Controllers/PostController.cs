using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostService.Models;
using PostService.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using PostService.Logger;

namespace PostService.Controllers
{
    [TypeFilter(typeof(LoggingAspect))]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService service;
        public PostController(IPostService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Authorize(Roles ="User, Admin")]
        public IActionResult Create(Post post)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity; ;
                if (identity != null)
                {
                    return Ok(service.CreatePost(post, identity.Claims));
                }
                return Unauthorized("You can not create a post");
            }
            catch (Exception e)
            {

               return StatusCode(500,e.Message);
            }
            

        }

        [HttpDelete("{postid}")]
        [Authorize(Roles = "User, Admin")]
        public IActionResult Delete(string postid)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                //var loginUser = identity.Claims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault().Value;
                return Ok(service.DeletePost(postid, identity.Claims));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{postid}")]
        [Authorize(Roles = "User, Admin")]
        public IActionResult Edit(string postid, Post post)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                return Ok(service.EditPost(postid, post,identity.Claims));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{postid}")]
        [Authorize(Roles = "User, Admin")]
        public IActionResult Get(string postid)
        {
            try
            {
                var post = service.GetPost(postid);
                return Ok( new
                {
                    PostID = post.PostID.ToString(),
                    Content = post.Content,
                    CreatedOn = post.CreatedOn,
                    user = post.user,
                    comments = post.comments

                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("user/{userName}")]
        [Authorize(Roles = "User, Admin")]
 
        public IActionResult GetAll(string userName)
        {
            try
            {
                var list = service.GetAllPosts(userName);
                
                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
