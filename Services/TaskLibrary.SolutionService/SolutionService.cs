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
using TaskLibrary.SolutionService.Models;

namespace TaskLibrary.SolutionService
{
    public class SolutionService:ISolutionService
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;
        private readonly IMapper mapper;
        private readonly IModelValidator<AddSolutionModel> addSolutionModelValidator;
        private readonly IModelValidator<UpdateSolutionModel> updateSolutionModelValidator;

        public SolutionService(
        IDbContextFactory<MainDbContext> contextFactory,
        IMapper mapper,
        IModelValidator<AddSolutionModel> addSolutionModelValidator,
        IModelValidator<UpdateSolutionModel> updateSolutionModelValidator)
        {
            this.contextFactory = contextFactory;
            this.mapper = mapper;
            this.addSolutionModelValidator = addSolutionModelValidator;
            this.updateSolutionModelValidator = updateSolutionModelValidator;
        }

        public async Task<IEnumerable<SolutionModel>> GetSolutions(int offset = 0, int limit = 10)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var soulutions = context
                .Solutions.Include(x=>x.User).Include(x=>x.ProgrammingTask).Include(x=>x.ProgrammingLanguage)
                .AsQueryable();

            soulutions = soulutions
                .Skip(Math.Max(offset, 0))
                .Take(Math.Max(0, Math.Min(limit, 1000)));

            var data = (await soulutions.ToListAsync()).Select(soulution => mapper.Map<SolutionModel>(soulution));

            return data;

        }
        public async Task<IEnumerable<SolutionModel>> GetSolutionsByTask(int taskId, int offset = 0, int limit = 10)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var soulutions = context
                .Solutions.Include(x => x.User).Include(x => x.ProgrammingTask).Include(x => x.ProgrammingLanguage)
                .Where(x => x.ProgrammingTaskId.Equals(taskId))
                .AsQueryable();

            soulutions = soulutions
                .Skip(Math.Max(offset, 0))
                .Take(Math.Max(0, Math.Min(limit, 1000)));

            var data = (await soulutions.ToListAsync()).Select(soulution => mapper.Map<SolutionModel>(soulution));

            return data;
        }



        public async Task<SolutionModel> GetSolution(int id)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var soulutions = await context.Solutions.Include(x => x.User).Include(x => x.ProgrammingTask).Include(x => x.ProgrammingLanguage).FirstOrDefaultAsync(x => x.Id.Equals(id));

            var data = mapper.Map<SolutionModel>(soulutions);

            return data;
        }
        public async Task<SolutionModel> AddSolution(AddSolutionModel model)
        {
            addSolutionModelValidator.Check(model);

            using var context = await contextFactory.CreateDbContextAsync();

            var soulution = mapper.Map<Solution>(model);
            await context.Solutions.AddAsync(soulution);
            context.SaveChanges();

            return mapper.Map<SolutionModel>(soulution);
        }
        public async Task UpdateSolution(int id, UpdateSolutionModel model)
        {
            updateSolutionModelValidator.Check(model);

            using var context = await contextFactory.CreateDbContextAsync();

            var soulution = await context.Solutions.FirstOrDefaultAsync(x => x.Id.Equals(id));

            ProcessException.ThrowIf(() => soulution is null, $"The solution (id: {id}) was not found");

            soulution = mapper.Map(model, soulution);

            context.Solutions.Update(soulution);
            context.SaveChanges();
        }
        public async Task DeleteSolution(int id)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var soulution = await context.Solutions.FirstOrDefaultAsync(x => x.Id.Equals(id))
                ?? throw new ProcessException($"The solution (id: {id}) was not found");
            
            context.Remove(soulution);
            context.SaveChanges();
        }
    }
}
