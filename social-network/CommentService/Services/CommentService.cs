using CommentService.Exceptions;
using CommentService.Models;
using CommentService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CommentService.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository repo;
        public CommentService(ICommentRepository repo)
        {
            this.repo = repo;
        }

        public Post AddComment(String postId,Comment comment, IEnumerable<Claim> userClaims)
        {
            var UserName = userClaims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault().Value;
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
            comment.user = u;
            return repo.AddComment(postId,comment);
        }

        public bool DeleteComment(String postId, string CommentId, IEnumerable<Claim> userClaims)
        { 
            Comment c = repo.GetComment(postId,CommentId);

            if ( c== null)
            {
                throw new CommentNotFoundException("Comment not found.");
            }
            
            return repo.DeleteComment(postId, CommentId, userClaims);
        }

        public bool EditComment(String postId,string CommentId, CommentModel comment, IEnumerable<Claim> userClaims)
        {
            Comment c = repo.GetComment(postId,CommentId);

            if (c == null)
            {
                throw new CommentNotFoundException("Comment not found.");
            }
            return repo.EditComment(postId,CommentId, comment,userClaims);
        }

        public IEnumerable<object> GetAllComments(string PostId)
        {
            var res = repo.GetAllComments(PostId);
            if (res == null)
            {
                throw new CommentNotFoundException("Comments not found for this post.");
            }
            foreach (var comment in res)
            {
                yield return
                    new
                    {
                        CommentID = comment.CommentID.ToString(),
                        Content = comment.Content,
                        CreatedOn = comment.CreatedOn,
                        user=comment.user
                    };

            }
            
        }

        public object GetComment(string postId,string CommentId)
        {
            var res = repo.GetComment(postId, CommentId);
            if (res == null)
            {
                throw new CommentNotFoundException("Comment not found.");
            }
            return 
                    new
                    {
                        CommentID = res.CommentID.ToString(),
                        Content = res.Content,
                        CreatedOn = res.CreatedOn,
                        user=res.user
                    }; ;
        }
    }
}
