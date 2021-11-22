using CommentService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CommentService.Repository
{
    public interface ICommentRepository
    {
        public Post AddComment(string postId, Comment comment);
        public bool DeleteComment(string postId, string CommentId, IEnumerable<Claim> userClaims);
        public bool EditComment(string postId, string CommentId, CommentModel comment, IEnumerable<Claim> userClaims);
        public List<Comment> GetAllComments(string PostId);
        public Comment GetComment(string postId, string CommentId);
    }
}
