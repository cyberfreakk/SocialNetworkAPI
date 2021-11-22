using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Exceptions
{
    public class PostNotFoundException : ApplicationException
    {
        public PostNotFoundException() { }
        public PostNotFoundException(string message) : base(message) { }
    }
}
