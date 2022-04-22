using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary.SolutionService.Models;

namespace TaskLibrary.SolutionService
{
    public interface ISolutionService
    {
        Task<IEnumerable<SolutionModel>> GetSolutions();

        Task<IEnumerable<SolutionModel>> GetSolutionsByTask(int taskId);

        Task<SolutionModel> GetSolution(int id);
        Task<SolutionModel> AddSolution(AddSolutionModel model);
        Task UpdateSolution(int id, UpdateSolutionModel model);
        Task DeleteSolution(int id);
    }
}
