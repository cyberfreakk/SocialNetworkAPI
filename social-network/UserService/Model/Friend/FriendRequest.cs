using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Model.Friend
{
    public class FriendRequest
    {
        public string UserName { get; set; }
        public string FriendUserName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
