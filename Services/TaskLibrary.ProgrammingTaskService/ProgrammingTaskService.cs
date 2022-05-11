using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary.Common.Exceptions;
using TaskLibrary.Common.Validator;
using TaskLibrary.Db.Context.Context;
using TaskLibrary.Db.Entities;
using TaskLibrary.ProgrammingTaskService.Models;

namespace TaskLibrary.ProgrammingTaskService
{
    public class ProgrammingTaskService:IProgrammingTaskService
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;
        private readonly IMapper mapper;
        private readonly IModelValidator<AddProgrammingTaskModel> addProgrammingTaskModelValidator;
        private readonly IModelValidator<UpdateProgrammingTaskModel> updateProgrammingTaskModelValidator;

        public ProgrammingTaskService(
        IDbContextFactory<MainDbContext> contextFactory,
        IMapper mapper,
        IModelValidator<AddProgrammingTaskModel> addProgrammingTaskModelValidator,
        IModelValidator<UpdateProgrammingTaskModel> updateProgrammingTaskModelValidator)
        {
            this.contextFactory = contextFactory;
            this.mapper = mapper;
            this.addProgrammingTaskModelValidator = addProgrammingTaskModelValidator;
            this.updateProgrammingTaskModelValidator = updateProgrammingTaskModelValidator;
        }

        public async Task<IEnumerable<ProgrammingTaskModel>> GetProgrammingTasks(int offset = 0, int limit = 10)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var progtasks = context
                .ProgrammingTasks.Include(x=>x.Category)
                .AsQueryable();

            progtasks = progtasks
                .Skip(Math.Max(offset, 0))
                .Take(Math.Max(0, Math.Min(limit, 1000)));

            var data = (await progtasks.ToListAsync()).Select(progtask => mapper.Map<ProgrammingTaskModel>(progtask));

            return data;

        }

        public async Task<IEnumerable<ProgrammingTaskModel>> GetProgrammingTasksByCategory(int categoryId, int offset = 0, int limit = 10)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var progtasks = context
                .ProgrammingTasks.Include(x => x.Category)
                .Where(x => x.CategoryId.Equals(categoryId))
                .AsQueryable();

            progtasks = progtasks
                .Skip(Math.Max(offset, 0))
                .Take(Math.Max(0, Math.Min(limit, 1000)));

            var data = (await progtasks.ToListAsync()).Select(progtask => mapper.Map<ProgrammingTaskModel>(progtask));

            return data;

        }

        
        public async Task<ProgrammingTaskModel> GetProgrammingTask(int id)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var progtask = await context.ProgrammingTasks.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id.Equals(id));

            var data = mapper.Map<ProgrammingTaskModel>(progtask);

            return data;
        }
        public async Task<ProgrammingTaskModel> AddProgrammingTask(AddProgrammingTaskModel model)
        {
            addProgrammingTaskModelValidator.Check(model);

            using var context = await contextFactory.CreateDbContextAsync();

            var progtask = mapper.Map<ProgrammingTask>(model);
            await context.ProgrammingTasks.AddAsync(progtask);
            context.SaveChanges();

            return mapper.Map<ProgrammingTaskModel>(progtask);
        }
        public async Task UpdateProgrammingTask(int id, UpdateProgrammingTaskModel model)
        {
            updateProgrammingTaskModelValidator.Check(model);

            using var context = await contextFactory.CreateDbContextAsync();

            var progtask = await context.ProgrammingTasks.FirstOrDefaultAsync(x => x.Id.Equals(id));

            ProcessException.ThrowIf(() => progtask is null, $"The task (id: {id}) was not found");

            progtask = mapper.Map(model, progtask);

            context.ProgrammingTasks.Update(progtask);
            context.SaveChanges();
        }
        public async Task DeleteProgrammingTask(int id)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var progtask = await context.ProgrammingTasks.FirstOrDefaultAsync(x => x.Id.Equals(id))
                ?? throw new ProcessException($"The task (id: {id}) was not found");

            context.Remove(progtask);
            context.SaveChanges();
        }
    }
}
