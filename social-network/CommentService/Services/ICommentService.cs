using CommentService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CommentService.Services
{
    public interface ICommentService
    {
        public Post AddComment(String postId, Comment comment, IEnumerable<Claim> userClaims);
        public bool DeleteComment(String postId, string CommentId, IEnumerable<Claim> userClaims);
        public bool EditComment(String postId, string CommentId, CommentModel comment, IEnumerable<Claim> userClaims);
        public object GetComment(string postId, string CommentId);
        public IEnumerable<object> GetAllComments(string PostId);
    }
}
