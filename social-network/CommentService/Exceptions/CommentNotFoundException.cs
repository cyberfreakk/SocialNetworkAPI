using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentService.Exceptions
{
    public class CommentNotFoundException : ApplicationException
    {
        public CommentNotFoundException() { }
        public CommentNotFoundException(string message) : base(message) { }
    }
}
