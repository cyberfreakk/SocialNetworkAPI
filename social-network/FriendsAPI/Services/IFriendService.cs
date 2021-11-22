using FriendsAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsService.Services
{
    public interface IFriendService
    {
        public List<Friend> getFriendlistByUserName(string userName);
        public List<FriendRequest> getFriendRequestByUserName(string userName);
        public bool isFriend(string userName, string friendName);
        public bool sendRequest(string userName, string friendName);
        public bool deleteRequest(string userName, string friendName);
        public bool acceptRequest(string userName, string friendName);
        public long numberOfFriends(string userName);
        public long numberOfRequests(string userName);
        public bool deleteFriend(string userName, string friendName);

    }
}
