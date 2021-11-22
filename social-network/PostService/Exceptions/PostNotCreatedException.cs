using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Exceptions
{
    public class PostNotCreatedException: ApplicationException
    {
        public PostNotCreatedException() { }
        public PostNotCreatedException(string message) : base(message) { }
    }
}
