using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskLibrary.Api.Controllers.Solutions.Models;
using TaskLibrary.SolutionService;
using TaskLibrary.SolutionService.Models;

namespace TaskLibrary.Api.Controllers.Solutions
{
    [Route("api/v{version:apiVersion}/solutions")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SolutionController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ILogger<SolutionController> logger;
        private readonly ISolutionService solutionService;

        public SolutionController(IMapper mapper, ILogger<SolutionController> logger, ISolutionService solutionService)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.solutionService = solutionService;
        }

        [HttpGet("")]
        // [Authorize(AppScopes.BooksRead)]
        public async Task<IEnumerable<SolutionResponse>> GetSolutions()
        {
            var solutions = await solutionService.GetSolutions();
            var response = mapper.Map<IEnumerable<SolutionResponse>>(solutions);

            return response;
        }

        [HttpGet("Bytask/{id}")]
        public async Task<IEnumerable<SolutionResponse>> GetSolutionsByTask([FromRoute] int id)
        {
            var solutions = await solutionService.GetSolutionsByTask(id);
            var response = mapper.Map<IEnumerable<SolutionResponse>>(solutions);

            return response;
        }

        [HttpGet("{id}")]
        //[Authorize(AppScopes.BooksRead)]
        public async Task<SolutionResponse> GetSolutionById([FromRoute] int id)
        {
            var solution = await solutionService.GetSolution(id);
            var response = mapper.Map<SolutionResponse>(solution);

            return response;
        }

        [HttpPost("")]
        //[Authorize(AppScopes.BooksWrite)]
        public async Task<SolutionResponse> AddSolution([FromBody] AddSolutionRequest request)
        {
            var model = mapper.Map<AddSolutionModel>(request);
            var solution = await solutionService.AddSolution(model);
            var response = mapper.Map<SolutionResponse>(solution);

            return response;
        }

        [HttpPut("{id}")]
        //[Authorize(AppScopes.BooksWrite)]
        public async Task<IActionResult> UpdateSolution([FromRoute] int id, [FromBody] UpdateSolutionRequest request)
        {
            var model = mapper.Map<UpdateSolutionModel>(request);
            await solutionService.UpdateSolution(id, model);

            return Ok();
        }

        [HttpDelete("{id}")]
        //[Authorize(AppScopes.BooksWrite)]
        public async Task<IActionResult> DeletSolution([FromRoute] int id)
        {
            await solutionService.DeleteSolution(id);

            return Ok();
        }
    }
}
