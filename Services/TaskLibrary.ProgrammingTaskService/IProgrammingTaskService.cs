using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary.CommentService.Models;
using TaskLibrary.ProgrammingTaskService.Models;

namespace TaskLibrary.ProgrammingTaskService
{
    public interface IProgrammingTaskService
    {
        Task<IEnumerable<ProgrammingTaskModel>> GetProgrammingTasks(int offset = 0, int limit = 10);
        Task<ProgrammingTaskModel> GetProgrammingTask(int id);
        Task<IEnumerable<ProgrammingTaskModel>> GetProgrammingTasksByCategory(int categoryId, int offset = 0, int limit = 10);
        Task<ProgrammingTaskModel> AddProgrammingTask(AddProgrammingTaskModel model);
        Task UpdateProgrammingTask(int id, UpdateProgrammingTaskModel model);
        Task DeleteProgrammingTask(int id);
    }
}
