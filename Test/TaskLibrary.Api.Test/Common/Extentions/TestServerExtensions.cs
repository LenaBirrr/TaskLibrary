using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskLibrary.Api.Test.Common.Extentions
{
    public static class TestServerExtensions
    {
        public static T ResolveService<T>(this TestServer testServer)
        {
            return testServer.Services.GetService<T>();
        }
    }
}
