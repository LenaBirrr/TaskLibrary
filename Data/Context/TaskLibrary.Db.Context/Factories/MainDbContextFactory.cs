using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary.Db.Context.Context;

namespace TaskLibrary.Db.Context.Factories
{
    public class MainDbContextFactory
    {
        private readonly DbContextOptions<MainDbContext> options;

        public MainDbContextFactory(DbContextOptions<MainDbContext> options)
        {
            this.options = options;
        }

        public MainDbContext Create()
        {
            return new MainDbContext(options);
        }
    }
}
