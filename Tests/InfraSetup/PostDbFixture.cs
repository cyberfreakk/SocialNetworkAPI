using Microsoft.Extensions.Configuration;
using PostService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.InfraSetup
{
    public class PostDbFixture
    {
        private readonly IConfigurationRoot configuration;
        public PostContext context;
        public PostDbFixture()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.Test.json");

            configuration = builder.Build();
            context = new PostContext(configuration);
            
        }
        public void Dispose()
        {
            context = null;
        }
    }
}
