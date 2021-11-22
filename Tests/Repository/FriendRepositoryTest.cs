using FriendsAPI.Model;
using FriendsAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Test;
using Tests.InfraSetup;
using Xunit;

namespace Tests.Repository
{
    public class FriendRepositoryTest : IClassFixture<FriendDbFixture>
    {
        FriendDbFixture fixture;
        private readonly IFriendRepository repository;

        public FriendRepositoryTest(FriendDbFixture _fixture)
        {
            this.fixture = _fixture;
            repository = new FriendRepository(fixture.context);
        }

        [Fact, TestPriority(1)]
        public void SendRequestShouldReturnTrue()
        {
            string userName = "arunesh";
            string friendName = "sourav";
            var actual = repository.sendRequest(userName, friendName);
            Assert.True(actual);
        }

        [Fact, TestPriority(2)]
        public void AcceptRequestShouldReturnTrue()
        {
            string friendName = "arunesh";
            string userName = "sourav";
            var actual = repository.acceptRequest(userName, friendName);
            Assert.True(actual.Result);
        }


        [Fact, TestPriority(3)]
        public void GetFriendListByUserNameShouldReturnAList()
        {
            string userName = "sourav";
            var actual = repository.getFriendlistsByUserName(userName);
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<List<Friend>>(actual);
        }

        [Fact, TestPriority(4)]
        public void GetFriendRequestsByUserNameShouldReturnAList()
        {
            string userName = "sourav";
            var actual = repository.getFriendRequestsByUserName(userName);
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<List<FriendRequest>>(actual);
        }

        [Fact, TestPriority(5)]
        public void DeleteRequestShouldReturnTrue()
        {
            string friendName = "arunesh";
            string userName = "sourav";
            var actual = repository.deleteRequest(userName, friendName);
            Assert.True(actual);
        }
    }
}
