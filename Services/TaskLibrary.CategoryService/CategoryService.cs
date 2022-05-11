﻿using AutoMapper;
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

namespace TaskLibrary.CategoryService
{
    public class CategoryService:ICategoryService
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;
        private readonly IMapper mapper;
        private readonly IModelValidator<AddCategoryModel> addCategoryModelValidator;
        private readonly IModelValidator<UpdateCategoryModel> updateCategoryModelValidator;

        public CategoryService(
        IDbContextFactory<MainDbContext> contextFactory,
        IMapper mapper,
        IModelValidator<AddCategoryModel> addCategoryModelValidator,
        IModelValidator<UpdateCategoryModel> updateCategoryModelValidator)
        {
            this.contextFactory = contextFactory;
            this.mapper = mapper;
            this.addCategoryModelValidator = addCategoryModelValidator;
            this.updateCategoryModelValidator = updateCategoryModelValidator;
        }

        public async Task<IEnumerable<CategoryModel>> GetCategories(int offset = 0, int limit = 10)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var categories = context
                .Categories
                .AsQueryable();

            categories = categories
                .Skip(Math.Max(offset, 0))
                .Take(Math.Max(0, Math.Min(limit, 1000)));

            var data = (await categories.ToListAsync()).Select(category => mapper.Map<CategoryModel>(category));

            return data;

        }
        public async Task<CategoryModel> GetCategory(int id)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id.Equals(id));

            var data = mapper.Map<CategoryModel>(category);

            return data;
        }
        public async Task<CategoryModel> AddCategory(AddCategoryModel model)
        {
            addCategoryModelValidator.Check(model);

            using var context = await contextFactory.CreateDbContextAsync();

            var category = mapper.Map<Category>(model);
            await context.Categories.AddAsync(category);
            context.SaveChanges();

            return mapper.Map<CategoryModel>(category);
        }
        public async Task UpdateCategory(int id, UpdateCategoryModel model)
        {
            updateCategoryModelValidator.Check(model);

            using var context = await contextFactory.CreateDbContextAsync();

            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id.Equals(id));

            ProcessException.ThrowIf(() => category is null, $"The category (id: {id}) was not found");

            category = mapper.Map(model, category);

            context.Categories.Update(category);
            context.SaveChanges();
        }
        public async Task DeleteCategory(int id)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id.Equals(id))
                ?? throw new ProcessException($"The category (id: {id}) was not found");

            context.Remove(category);
            context.SaveChanges();
        }
    }
}
