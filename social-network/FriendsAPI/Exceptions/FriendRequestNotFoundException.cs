using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsService.Exceptions
{
    public class FriendRequestNotFoundException : ApplicationException
    {
        public FriendRequestNotFoundException() { }
        public FriendRequestNotFoundException(string message): base(message) { }
    }
}
