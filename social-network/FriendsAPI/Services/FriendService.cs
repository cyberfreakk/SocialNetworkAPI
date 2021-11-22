using FriendsAPI.Model;
using FriendsAPI.Repositories;
using FriendsService.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsService.Services
{
    public class FriendService : IFriendService
    {
        private readonly IFriendRepository repo;
        public FriendService(IFriendRepository repo)
        {
            this.repo = repo;
        }

        public bool acceptRequest(string userName, string friendName)
        {
            var task = repo.acceptRequest(userName, friendName);
            task.Wait();
            var res = task.Result;
            if ( res== false)
            {
                throw new FriendRequestNotFoundException();
            }
            return res;
        }

        public bool deleteRequest(string userName, string friendName)
        {
            var res = repo.deleteRequest(userName, friendName);
            if ( res== false)
            {
                throw new FriendRequestNotFoundException();
            }
            return res;
        }

        public List<Friend> getFriendlistByUserName(string userName)
        {
            return repo.getFriendlistsByUserName(userName);
        }

        public List<FriendRequest> getFriendRequestByUserName(string userName)
        {
            return repo.getFriendRequestsByUserName(userName);
        }

        public bool isFriend(string userName, string friendName)
        {
            return repo.isFriend(userName, friendName);
        }

        public long numberOfFriends(string userName)
        {
            return repo.numberOfFriends(userName);
        }

        public long numberOfRequests(string userName)
        {
            return repo.numberOfRequests(userName);
        }

        public bool sendRequest(string userName, string friendName)
        {
            return repo.sendRequest(userName, friendName);
        }

        public bool deleteFriend(string userName, string friendName)
        {
            return repo.deleteFriend(userName, friendName);
        }
    }
}
