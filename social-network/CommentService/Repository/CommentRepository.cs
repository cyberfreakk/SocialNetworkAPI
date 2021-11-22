using CommentService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Security.Claims;

namespace CommentService.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly CommentContext db;
        public CommentRepository(CommentContext db)
        {
            this.db = db;
        }

        public Post AddComment(string postId,Comment comment)
        {
            Post post = db.Posts.Find(x => x.PostID == ObjectId.Parse(postId)).FirstOrDefault();
            if (post != null)
            {
               
                if (post.comments == null) {
                    post.comments = new List<Comment>();
                }
                post.comments.Add(comment);

                var filter = Builders<Post>.Filter.Where(x => x.PostID == ObjectId.Parse(postId));
                 db.Posts.FindOneAndReplace(filter,post);
                return post;
            }
            return null;
        }

        public bool DeleteComment(string postId,string CommentId, IEnumerable<Claim> userClaims)
        {
            Post post = db.Posts.Find(x => x.PostID == ObjectId.Parse(postId)).FirstOrDefault();
            if (post != null)
            {
                var delComment = post.comments.Find(c => c.CommentID == ObjectId.Parse(CommentId));
                var loginUser = userClaims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault().Value;
                var role = userClaims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault().Value;

                if (role != "Admin" && delComment.user.UserName != loginUser)
                {
                    throw new Exception("You can not edit this comment");
                }
                post.comments.Remove(delComment);
                var filter = Builders<Post>.Filter.Where(x => x.PostID == ObjectId.Parse(postId));
                db.Posts.FindOneAndReplace(filter, post);
                return true;
            }
            return false;
        }

        public bool EditComment(string postId, string CommentId, CommentModel comment, IEnumerable<Claim> userClaims)
        {
            Post post = db.Posts.Find(x => x.PostID == ObjectId.Parse(postId)).FirstOrDefault();
            if (post != null)
            {
                var loginUser = userClaims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault().Value;
                var role = userClaims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault().Value;

                var commentInd = post.comments.FindIndex(c => c.CommentID == ObjectId.Parse(CommentId));
                var comt = post.comments[commentInd];
                if( role != "Admin" && comt.user.UserName != loginUser )
                {
                    throw new Exception("You can not edit this comment");
                }
                comt.Content = comment.content;
                post.comments[commentInd] = comt;
                var filter = Builders<Post>.Filter.Where(x => x.PostID == ObjectId.Parse(postId));
                db.Posts.FindOneAndReplace(filter, post);
                return true;
            }
            throw new Exception("Something went wrong"); ;
        }

        public List<Comment> GetAllComments(string PostId)
        {
            return db.Posts.Find(x => x.PostID == ObjectId.Parse(PostId)).FirstOrDefault().comments;            
        }

        public Comment GetComment(string postId,string CommentId)
        {
            Post post = db.Posts.Find(x => x.PostID == ObjectId.Parse(postId)).FirstOrDefault();

            return post.comments.Find(x => x.CommentID == ObjectId.Parse(CommentId));
        }
    }
}
