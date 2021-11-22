using FriendsAPI.DbContext;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.InfraSetup
{
    public class FriendDbFixture
    {
        private readonly IConfigurationRoot configuration;
        public AppDbContext context;
        public FriendDbFixture()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");

            configuration = builder.Build();
            context = new AppDbContext(configuration);

        }
        public void Dispose()
        {
            context = null;
        }
    }
}
