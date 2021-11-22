using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentService.Models
{
    public class CommentModel
    {
        public string PostId { get; set; }
        public string content { get; set; }
    }
}
