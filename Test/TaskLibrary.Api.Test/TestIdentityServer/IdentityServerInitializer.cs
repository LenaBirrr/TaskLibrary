using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary.Api.Test.Common;

namespace TaskLibrary.Api.Test.TestIdentityServer
{
    internal class IdentityServerInitializer
    {

        private IWebHostBuilder builder;
        private HttpClient httpClient;

        public IdentityServerInitializer GetWebHost(IConfiguration? configuration = null, Action<IServiceCollection>? configurator = null)
        {
            configuration ??= ConfigurationFactory.GetApiConfiguration();

            configurator += s =>
            {
                // Common mocks for server
                s.PostConfigureAll<JwtBearerOptions>(options =>
                {
                    options.ConfigurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                        $"{httpClient.BaseAddress}.well-known/openid-configuration",
                        new OpenIdConnectConfigurationRetriever(),
                        new HttpDocumentRetriever(httpClient) { RequireHttps = false, SendAdditionalHeaderData = true }
                        );
                });
            };

            builder = new WebHostBuilder()
                .UseConfiguration(configuration)
                .UseStartup(app => new IdentityServerStartup(configuration, configurator));

            return this;
        }

        public TestServer Build()
        {
            var server = new TestServer(builder) { BaseAddress = new Uri("http://localhost_is/") };
            httpClient = server.CreateClient();
            return server;
        }

        public static TestServer Initialize(IConfiguration? configuration = null, Action<IServiceCollection>? configurator = null)
        {
            return new IdentityServerInitializer().GetWebHost(configuration, configurator).Build();
        }
    }
}
