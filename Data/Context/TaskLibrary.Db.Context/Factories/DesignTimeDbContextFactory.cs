using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary.Db.Context.Context;
using Microsoft.Extensions.Configuration.Json;

namespace TaskLibrary.Db.Context.Factories
{
    public class DesignTimeDbContextFactory
    {
        public MainDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.design.json")
                 .Build();

            var options = new DbContextOptionsBuilder<MainDbContext>()
                          .UseSqlServer(configuration.GetConnectionString("MainDbContext"),
                                opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)
                           ).Options;

            return new MainDbContextFactory(options).Create();
        }
    }
}
