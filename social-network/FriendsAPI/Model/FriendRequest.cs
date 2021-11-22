using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsAPI.Model
{
    [BsonIgnoreExtraElements]
    public class FriendRequest
    {
        public string UserName { get; set; }
        public string FriendUserName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
