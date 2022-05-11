using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskLibrary.Api.Test.Common.Extentions
{
    public static class DbContextExtensions
    {
        public static async Task Truncate<T>(this DbContext context, DbSet<T> dbSet) where T : class
        {
            await context.Database.ExecuteSqlRawAsync($"TRUNCATE TABLE {dbSet.EntityType.GetTableName()}");
        }
    }
}
