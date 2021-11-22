using FriendsAPI.DbContext;
using FriendsAPI.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsAPI.Repositories
{
    public class FriendRepository : IFriendRepository
    {
        private readonly AppDbContext db;

        public FriendRepository(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> acceptRequest(string userName, string friendName)
        {
            var sentfilter = Builders<UserFriend>.Filter.Where(x => x.UserName == friendName);
            var receivedfilter = Builders<UserFriend>.Filter.Where(x => x.UserName == userName);

            var sender = db.UserFriends.Find(x => x.UserName == friendName).FirstOrDefault();
            var receiver = db.UserFriends.Find(x => x.UserName == userName).FirstOrDefault();

            sender.sentRequests.RemoveAll(x=>x.UserName==friendName && x.FriendUserName == userName);
            receiver.receivedRequests.RemoveAll(x => x.UserName == userName && x.FriendUserName == friendName);
            if (sender.Friends==null)
            {
                sender.Friends = new List<Friend>();
            }
            sender.Friends.Add(new Friend() { UserName = userName });
            if (receiver.Friends==null)
            {
                receiver.Friends = new List<Friend>();
            }
            receiver.Friends.Add(new Friend() { UserName = friendName });
            

            await db.UserFriends.FindOneAndReplaceAsync(sentfilter,sender);
            await db.UserFriends.FindOneAndReplaceAsync(receivedfilter, receiver);

            return true;
        }

        public bool deleteRequest(string userName, string friendName)
        {
            var sentfilter = Builders<UserFriend>.Filter.Where(x => x.UserName == friendName);
            var receivedfilter = Builders<UserFriend>.Filter.Where(x => x.UserName == userName);

            var sender = db.UserFriends.Find(x => x.UserName == friendName).FirstOrDefault();
            var receiver = db.UserFriends.Find(x => x.UserName == userName).FirstOrDefault();

            receiver.receivedRequests.RemoveAll(x=>x.UserName==userName && x.FriendUserName == friendName);
            sender.sentRequests.RemoveAll(x => x.UserName == friendName && x.FriendUserName == userName);

            db.UserFriends.FindOneAndReplace(sentfilter, sender);
            db.UserFriends.FindOneAndReplace(receivedfilter, receiver);
            return true;
        }

            public List<Friend> getFriendlistsByUserName(string userName)
        {
            var res = db.UserFriends.Find(x => x.UserName == userName).FirstOrDefault();
            return res.Friends;
        }

        public List<FriendRequest> getFriendRequestsByUserName(string userName)
        {
            var task = db.UserFriends.FindAsync(x => x.UserName == userName);//.FirstOrDefault();
            var res = task.Result.FirstOrDefault();
            return res.receivedRequests;
        }

        public bool isFriend(string userName, string friendName)
        {
            var filter = Builders<UserFriend>.Filter.Where(x => x.UserName == userName);
            var res = db.UserFriends.Find(filter).FirstOrDefault();
            return res.Friends != null?  res.Friends.Find(x => x.UserName == friendName) != null:false;
            
        }

        public long numberOfFriends(string userName)
        {
            var filter = Builders<UserFriend>.Filter.Where(x => x.UserName == userName);
            var res = db.UserFriends.Find(filter).FirstOrDefault();
            
            return res.Friends.Count();
        }

        public long numberOfRequests(string userName)
        {
            var filter = Builders<UserFriend>.Filter.Where(x => x.UserName == userName);
            var res = db.UserFriends.Find(filter).FirstOrDefault();
            return res.receivedRequests.Count();
        }

        public bool sendRequest(string userName, string friendName)
        {
            //if (isFriend(userName, friendName))
            //{
            //    return false;
            //}
            var sentfilter = Builders<UserFriend>.Filter.Where(x => x.UserName == userName);
            var receivedfilter = Builders<UserFriend>.Filter.Where(x => x.UserName == friendName);

            var sender =db.UserFriends.Find(x => x.UserName == userName).FirstOrDefault();
            var receiver = db.UserFriends.Find(x => x.UserName == friendName).FirstOrDefault();
            var sentObject = new FriendRequest() { UserName = userName, FriendUserName = friendName };
            var receiveObject = new FriendRequest() { UserName = friendName, FriendUserName = userName };
            if (receiver.receivedRequests == null)
            {
                receiver.receivedRequests = new List<FriendRequest>();
            }
            receiver.receivedRequests.Add(receiveObject);
            if (sender.sentRequests == null)
            {
                sender.sentRequests = new List<FriendRequest>();
            }
            sender.sentRequests.Add(sentObject);
            db.UserFriends.FindOneAndReplace(sentfilter, sender);
            db.UserFriends.FindOneAndReplace(receivedfilter, receiver);

            return true;
        }




        public bool deleteFriend(string userName, string friendName)
        {
            if (isFriend(userName, friendName)==false)
            {
                return false;
            }
            var loginuserB = Builders<UserFriend>.Filter.Where(x => x.UserName == userName);
            var frienduserB = Builders<UserFriend>.Filter.Where(x => x.UserName == friendName);

            var loginUser = db.UserFriends.Find(x => x.UserName == userName).FirstOrDefault();
            var friend = db.UserFriends.Find(x => x.UserName == friendName).FirstOrDefault();
            
            loginUser.Friends.RemoveAll(u=>u.UserName==friendName);
            friend.Friends.RemoveAll(u => u.UserName == userName);

            db.UserFriends.FindOneAndReplace(loginuserB, loginUser);
            db.UserFriends.FindOneAndReplace(frienduserB, friend);

            return true;
        }
    }
}
