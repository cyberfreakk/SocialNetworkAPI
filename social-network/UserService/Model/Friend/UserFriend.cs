using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Model.Friend
{
    public class UserFriend
    {        
    
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<Friend> Friends { get; set; }
        public List<FriendRequest> Requests { get; set; }

    }
}
