using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskLibrary.Api.Controllers.ProgrammingLanguages.Models;
using TaskLibrary.Common.Security;
using TaskLibrary.ProgrammingLanguageService;
using TaskLibrary.ProgrammingLanguageService.Models;

namespace TaskLibrary.Api.Controllers.ProgrammingLanguages
{
    [Route("api/v{version:apiVersion}/languages")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class ProgrammingLanguageController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ILogger<ProgrammingLanguageController> logger;
        private readonly IProgrammingLanguageService programmingLanguageService;

        public ProgrammingLanguageController(IMapper mapper, ILogger<ProgrammingLanguageController> logger, IProgrammingLanguageService programmingLanguageService)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.programmingLanguageService = programmingLanguageService;
        }

        [HttpGet("")]
        [Authorize(AppScopes.ProgrammingLanguagesRead)]
        public async Task<IEnumerable<ProgrammingLanguageResponse>> GetProgrammingLanguages()
        {
            var programmingLanguages = await programmingLanguageService.GetProgrammingLanguages();
            var response = mapper.Map<IEnumerable<ProgrammingLanguageResponse>>(programmingLanguages);

            return response;
        }

        [HttpGet("{id}")]
        [Authorize(AppScopes.ProgrammingLanguagesRead)]
        public async Task<ProgrammingLanguageResponse> GetProgrammingLanguageById([FromRoute] int id)
        {
            var programmingLanguage = await programmingLanguageService.GetProgrammingLanguage(id);
            var response = mapper.Map<ProgrammingLanguageResponse>(programmingLanguage);

            return response;
        }

        [HttpPost("")]
        [Authorize(AppScopes.ProgrammingLanguagesWrite)]
        public async Task<ProgrammingLanguageResponse> AddProgrammingLanguage([FromBody] AddProgrammingLanguageRequest request)
        {
            var model = mapper.Map<AddProgrammingLanguageModel>(request);
            var programmingLanguage = await programmingLanguageService.AddProgrammingLanguage(model);
            var response = mapper.Map<ProgrammingLanguageResponse>(programmingLanguage);

            return response;
        }

        [HttpPut("{id}")]
        [Authorize(AppScopes.ProgrammingLanguagesWrite)]
        public async Task<IActionResult> UpdateProgrammingLanguage([FromRoute] int id, [FromBody] UpdateProgrammingLanguageRequest request)
        {
            var model = mapper.Map<UpdateProgrammingLanguageModel>(request);
            await programmingLanguageService.UpdateProgrammingLanguage(id, model);

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(AppScopes.ProgrammingLanguagesWrite)]
        public async Task<IActionResult> DeleteProgrammingLanguage([FromRoute] int id)
        {
            await programmingLanguageService.DeleteProgrammingLanguage(id);

            return Ok();
        }
    }
}
