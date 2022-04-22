using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary.CommentService.Models;
using TaskLibrary.Common.Exceptions;
using TaskLibrary.Common.Validator;
using TaskLibrary.Db.Context.Context;
using TaskLibrary.Db.Entities;
using TaskLibrary.ProgrammingLanguageService.Models;

namespace TaskLibrary.ProgrammingLanguageService
{
    public class ProgrammingLanguageService : IProgrammingLanguageService
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;
        private readonly IMapper mapper;
        private readonly IModelValidator<AddProgrammingLanguageModel> addProgrammingLanguageModelValidator;
        private readonly IModelValidator<UpdateProgrammingLanguageModel> updateProgrammingLanguageModelValidator;

        public ProgrammingLanguageService(
        IDbContextFactory<MainDbContext> contextFactory,
        IMapper mapper,
        IModelValidator<AddProgrammingLanguageModel> addProgrammingLanguageModelValidator,
        IModelValidator<UpdateProgrammingLanguageModel> updateProgrammingLanguageModelValidator)
        {
            this.contextFactory = contextFactory;
            this.mapper = mapper;
            this.addProgrammingLanguageModelValidator = addProgrammingLanguageModelValidator;
            this.updateProgrammingLanguageModelValidator = updateProgrammingLanguageModelValidator;
        }

        public async Task<IEnumerable<ProgrammingLanguageModel>> GetProgrammingLanguages()
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var proglangs = context
                .ProgrammingLanguages
                .AsQueryable();

            proglangs = proglangs
                .Take(1000);

            var data = (await proglangs.ToListAsync()).Select(proglang => mapper.Map<ProgrammingLanguageModel>(proglang));

            return data;

        }
        public async Task<ProgrammingLanguageModel> GetProgrammingLanguage(int id)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var proglang = await context.ProgrammingLanguages.FirstOrDefaultAsync(x => x.Id.Equals(id));

            var data = mapper.Map<ProgrammingLanguageModel>(proglang);

            return data;
        }
        public async Task<ProgrammingLanguageModel> AddProgrammingLanguage(AddProgrammingLanguageModel model)
        {
            addProgrammingLanguageModelValidator.Check(model);

            using var context = await contextFactory.CreateDbContextAsync();

            var proglang = mapper.Map<ProgrammingLanguage>(model);
            await context.ProgrammingLanguages.AddAsync(proglang);
            context.SaveChanges();

            return mapper.Map<ProgrammingLanguageModel>(proglang);
        }
        public async Task UpdateProgrammingLanguage(int id, UpdateProgrammingLanguageModel model)
        {
            updateProgrammingLanguageModelValidator.Check(model);

            using var context = await contextFactory.CreateDbContextAsync();

            var proglang = await context.ProgrammingLanguages.FirstOrDefaultAsync(x => x.Id.Equals(id));

            ProcessException.ThrowIf(() => proglang is null, $"The proglang (id: {id}) was not found");

            proglang = mapper.Map(model, proglang);

            context.ProgrammingLanguages.Update(proglang);
            context.SaveChanges();
        }
        public async Task DeleteProgrammingLanguage(int id)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var proglang = await context.ProgrammingLanguages.FirstOrDefaultAsync(x => x.Id.Equals(id))
                ?? throw new ProcessException($"The proglang (id: {id}) was not found");

            context.Remove(proglang);
            context.SaveChanges();
        }
    }
}
