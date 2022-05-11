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
using TaskLibrary.NotificationService;
using TaskLibrary.SubscriptionService;

namespace TaskLibrary.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;
        private readonly IMapper mapper;
        private readonly IModelValidator<AddCommentModel> addCommentModelValidator;
        private readonly IModelValidator<UpdateCommentModel> updateCommentModelValidator;
        private readonly ISubscriptionService subscribtionService;
        private readonly INotificationService notificationService;

        public CommentService(
        IDbContextFactory<MainDbContext> contextFactory,
        IMapper mapper,
        IModelValidator<AddCommentModel> addCommentModelValidator,
        IModelValidator<UpdateCommentModel> updateCommentModelValidator,
        ISubscriptionService subscribtionService, INotificationService notificationService)
        {
            this.contextFactory = contextFactory;
            this.mapper = mapper;
            this.addCommentModelValidator = addCommentModelValidator;
            this.updateCommentModelValidator = updateCommentModelValidator;
            this.subscribtionService = subscribtionService;
            this.notificationService = notificationService;
        }

        public async Task<IEnumerable<CommentModel>> GetComments(int offset = 0, int limit = 10)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var comments = context
                .Comments.Include(x => x.User).Include(x => x.ProgrammingTask)
                .AsQueryable();

            comments = comments
                .Skip(Math.Max(offset, 0))
                .Take(Math.Max(0, Math.Min(limit, 1000)));

            var data = (await comments.ToListAsync()).Select(comment => mapper.Map<CommentModel>(comment));

            return data;

        }
        public async Task<CommentModel> GetComment(int id)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var comment = await context.Comments.Include(x=>x.User).Include(x=>x.ProgrammingTask).FirstOrDefaultAsync(x => x.Id.Equals(id));

            var data = mapper.Map<CommentModel>(comment);

            return data;
        }
        public async Task<CommentModel> AddComment(AddCommentModel model)
        {
            addCommentModelValidator.Check(model);

            using var context = await contextFactory.CreateDbContextAsync();

            var comment = mapper.Map<Comment>(model);
            await context.Comments.AddAsync(comment);
            context.SaveChanges();
            var com = await GetComment(comment.Id);
            var subs=await subscribtionService.GetSubscriptionsByTask(comment.ProgrammingTaskId);
            
            foreach(var sub in subs)
            {
                await notificationService.AddNotification(new NotificationService.Models.AddNotificationModel
                {
                    SubscribtionId = sub.Id,
                    Text = com.UserName + " оставил комментарий к задаче " + com.TaskName
                });;
                
            }

            return mapper.Map<CommentModel>(comment);
        }
        public async Task UpdateComment(int id, UpdateCommentModel model)
        {
            updateCommentModelValidator.Check(model);

            using var context = await contextFactory.CreateDbContextAsync();

            var comment = await context.Comments.FirstOrDefaultAsync(x => x.Id.Equals(id));

            ProcessException.ThrowIf(() => comment is null, $"The comment (id: {id}) was not found");

            comment = mapper.Map(model, comment);

            context.Comments.Update(comment);
            context.SaveChanges();
        }
        public async Task DeleteComment(int id)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var comment = await context.Comments.FirstOrDefaultAsync(x => x.Id.Equals(id))
                ?? throw new ProcessException($"The comment (id: {id}) was not found");

            context.Remove(comment);
            context.SaveChanges();
        }


        public async Task<IEnumerable<CommentModel>> GetCommentsByTask(int taskId)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var comments = context
                .Comments.Include(x => x.User).Include(x => x.ProgrammingTask)
                .Where(x => x.ProgrammingTaskId.Equals(taskId))
                .AsQueryable();

            comments = comments
                .Take(1000);

            var data = (await comments.ToListAsync()).Select(comment => mapper.Map<CommentModel>(comment));

            return data;
        }
        public async Task<IEnumerable<CommentModel>> GetCommentsByUser(Guid userId)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var comments = context
                .Comments.Include(x => x.User).Include(x => x.ProgrammingTask)
                .Where(x => x.UserId.Equals(userId))
                .AsQueryable();

            comments = comments
                .Take(1000);

            var data = (await comments.ToListAsync()).Select(comment => mapper.Map<CommentModel>(comment));

            return data;
        }
    }
}
