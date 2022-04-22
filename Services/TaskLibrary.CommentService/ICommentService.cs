using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary.CommentService.Models;

namespace TaskLibrary.CommentService
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentModel>> GetComments();
        Task<IEnumerable<CommentModel>> GetCommentsByTask(int taskId);
        Task<IEnumerable<CommentModel>> GetCommentsByUser(Guid userId);
        Task<CommentModel> GetComment(int id);
        Task<CommentModel> AddComment(AddCommentModel model);
        Task UpdateComment(int id, UpdateCommentModel model);
        Task DeleteComment(int id);
    }
}
