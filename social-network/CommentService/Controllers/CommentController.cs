using CommentService.Logger;
using CommentService.Models;
using CommentService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CommentService.Controllers
{
    [TypeFilter(typeof(LoggingAspect))]

    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService service;
        private readonly IPostApiService postApiService;

        public CommentController(ICommentService service, IPostApiService postApiService)
        {
            this.service = service;
            this.postApiService = postApiService;
        }

        [HttpPost]
        [Authorize(Roles = "User, Admin")]
        public IActionResult AddComment(CommentModel comment)
        {
            try
            {
                Comment c = new Comment();
                c.Content = comment.content;
                c.CommentID = ObjectId.GenerateNewId();
                var identity = HttpContext.User.Identity as ClaimsIdentity; ;
                if (identity != null)
                {
                    return Ok(service.AddComment(comment.PostId, c, identity.Claims));
                }
                return Unauthorized("You can not create a post");
            }
            catch (Exception e)
            {

                return NotFound(e.Message);
            }

        }

        [HttpDelete("{postId}/{commentid}")]
        [Authorize(Roles = "User, Admin")]
        public IActionResult DeleteComment(string postId,string commentId)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                return Ok(service.DeleteComment(postId,commentId,identity.Claims));
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("{postId}/{commentid}")]
        [Authorize(Roles = "User, Admin")]

        public IActionResult Edit(string postId, string CommentId, CommentModel comment)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                
                return Ok(service.EditComment(postId, CommentId, comment,identity.Claims));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("{postId}/{CommentId}")]
        [Authorize(Roles = "User, Admin")]
        public IActionResult GetComment(string postId, string CommentId)
        {
            try
            {
                return Ok(service.GetComment(postId,CommentId));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("postId/{postid}")]
        [Authorize(Roles = "User, Admin")]
        public IActionResult GetAllComments(string postid)
        {
            try
            {
                return Ok(service.GetAllComments(postid));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
