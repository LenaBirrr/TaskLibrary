using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary.Db.Context.Context;

namespace TaskLibrary.Db.Context.Setup
{
    public class DbSeed
    {
        private static void AddDefaultInfo(MainDbContext context)
        {
            if (context.ProgrammingLanguages.Any() || context.ProgrammingTasks.Any() || context.Categories.Any())
                 return;

             var lang = new Entities.ProgrammingLanguage()
             {
                 Name = "C#",
                 Paradigm="Объектно-ориентированный",
                 Realization=".Net",
                 Level="Высокоуровневый"
             };
            context.ProgrammingLanguages.Add(lang);

            var category = new Entities.Category()
            {
                Name = "Для начинающих"
            };
            context.Categories.Add(category);


            var task = new Entities.ProgrammingTask()
            {
                Name = "Sum",
                ProblemStatement = "Найти сумму всех целых чисел, лежащих между 1 и N включительно. В единственной строке расположено число N, по модулю не превосходящее 10000. Выведите целое число — ответ задачи. Тестовый ввод -3",
                Answers = "-5",
                Category=category,
            };
             context.ProgrammingTasks.Add(task);
     

            
             context.SaveChanges();
        }

        public static void Execute(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetService<IServiceScopeFactory>()?.CreateScope();
            ArgumentNullException.ThrowIfNull(scope);

            var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<MainDbContext>>();
            using var context = factory.CreateDbContext();

            AddDefaultInfo(context);
        }
    }
}
