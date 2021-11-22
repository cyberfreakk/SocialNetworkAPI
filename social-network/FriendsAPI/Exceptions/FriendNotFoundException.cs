using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsService.Exceptions
{
    public class FriendNotFoundException : ApplicationException 
    {
        public FriendNotFoundException() { }
        public FriendNotFoundException(string message): base(message) { }
    }
}
