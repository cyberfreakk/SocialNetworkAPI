using FriendsAPI.Model;
using FriendsAPI.Repositories;
using FriendsService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Test;
using Tests.InfraSetup;
using Xunit;

namespace Tests.Service
{
    public class FriendServiceTest : IClassFixture<FriendDbFixture>
    {
        private readonly FriendDbFixture fixture;
        private readonly IFriendRepository repo;
        public FriendServiceTest(FriendDbFixture _fixture)
        {
            this.fixture = _fixture;
            repo = new FriendRepository(fixture.context);
        }

        [Fact, TestPriority(1)]
        public void SendRequestShouldReturnTrue()
        {
            string userName = "arunesh";
            string friendName = "sourav";

            var service = new FriendService(repo);
            var actual = service.sendRequest(userName, friendName);
            Assert.True(actual);
        }

        [Fact, TestPriority(2)]
        public void AcceptRequestShouldReturnTrue()
        {
            string friendName = "arunesh";
            string userName = "sourav";

            var service = new FriendService(repo);
            var actual = service.acceptRequest(userName, friendName);
            Assert.True(actual);
        }

        [Fact, TestPriority(3)]
        public void GetFriendListByUserNameShouldReturnAList()
        {
            string userName = "sourav";

            var service = new FriendService(repo);
            var actual = service.getFriendlistByUserName(userName);
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<List<Friend>>(actual);
        }

        [Fact, TestPriority(4)]
        public void GetFriendRequestsByUserNameShouldReturnAList()
        {
            string userName = "sourav";

            var service = new FriendService(repo);
            var actual = service.getFriendRequestByUserName(userName);
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<List<FriendRequest>>(actual);
        }

        [Fact, TestPriority(5)]
        public void DeleteRequestShouldReturnTrue()
        {
            string friendName = "arunesh";
            string userName = "sourav";

            var service = new FriendService(repo);
            var actual = service.deleteRequest(userName, friendName);
            Assert.True(actual);
        }
    }
}
