using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskLibrary.Api.Controllers.ProgrammingTasks.Models;
using TaskLibrary.Common.Security;
using TaskLibrary.ProgrammingTaskService;
using TaskLibrary.ProgrammingTaskService.Models;

namespace TaskLibrary.Api.Controllers.ProgrammingTasks
{
    [Route("api/v{version:apiVersion}/tasks")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class ProgrammingTaskController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ILogger<ProgrammingTaskController> logger;
        private readonly IProgrammingTaskService programmingTaskService;

        public ProgrammingTaskController(IMapper mapper, ILogger<ProgrammingTaskController> logger, IProgrammingTaskService programmingTaskService)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.programmingTaskService = programmingTaskService;
        }

        [HttpGet("")]
        [Authorize(AppScopes.ProgrammingTasksRead)]
        public async Task<IEnumerable<ProgrammingTaskResponse>> GetProgrammingTasks()
        {
            var programmingTasks = await programmingTaskService.GetProgrammingTasks();
            var response = mapper.Map<IEnumerable<ProgrammingTaskResponse>>(programmingTasks);

            return response;
        }
        [HttpGet("Bycategory/{id}")]
        [Authorize(AppScopes.ProgrammingTasksRead)]

        public async Task<IEnumerable<ProgrammingTaskResponse>> GetProgrammingTasksByCategory([FromRoute] int id)
        {
            var programmingTasks = await programmingTaskService.GetProgrammingTasksByCategory(id);
            var response = mapper.Map<IEnumerable<ProgrammingTaskResponse>>(programmingTasks);

            return response;
        }
        

        [HttpGet("{id}")]
        [Authorize(AppScopes.ProgrammingTasksRead)]
        public async Task<ProgrammingTaskResponse> GetProgrammingTaskById([FromRoute] int id)
        {
            var programmingTask = await programmingTaskService.GetProgrammingTask(id);
            var response = mapper.Map<ProgrammingTaskResponse>(programmingTask);

            return response;
        }

        [HttpPost("")]
        [Authorize(AppScopes.ProgrammingTasksWrite)]
        public async Task<ProgrammingTaskResponse> AddProgrammingTask([FromBody] AddProgrammingTaskRequest request)
        {
            var model = mapper.Map<AddProgrammingTaskModel>(request);
            var programmingTask = await programmingTaskService.AddProgrammingTask(model);
            var response = mapper.Map<ProgrammingTaskResponse>(programmingTask);

            return response;
        }

        [HttpPut("{id}")]
        [Authorize(AppScopes.ProgrammingTasksWrite)]
        public async Task<IActionResult> UpdateProgrammingTask([FromRoute] int id, [FromBody] UpdateProgrammingTaskRequest request)
        {
            var model = mapper.Map<UpdateProgrammingTaskModel>(request);
            await programmingTaskService.UpdateProgrammingTask(id, model);

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(AppScopes.ProgrammingTasksWrite)]
        public async Task<IActionResult> DeleteProgrammingTask([FromRoute] int id)
        {
            await programmingTaskService.DeleteProgrammingTask(id);

            return Ok();
        }
    }
}
