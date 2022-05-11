using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using TaskLibrary.Api.Controllers.Categories.Models;
using TaskLibrary.CategoryService;
using TaskLibrary.Common.Security;

namespace TaskLibrary.Api.Controllers.Categories
{
        [Route("api/v{version:apiVersion}/categories")]
        [ApiController]
        [ApiVersion("1.0")]
    [Authorize]
    public class CategoriesController : ControllerBase
        {
            private readonly IMapper mapper;
            private readonly ILogger<CategoriesController> logger;
            private readonly ICategoryService categoryService;

            public CategoriesController(IMapper mapper, ILogger<CategoriesController> logger, ICategoryService categoryService)
            {
                this.mapper = mapper;
                this.logger = logger;
                this.categoryService = categoryService;
            }

            [HttpGet("")]
        [Authorize(AppScopes.CategoriesRead)]
        public async Task<IEnumerable<CategoryResponse>> GetCategories()
            {
            
            var categories = await categoryService.GetCategories();
                var response = mapper.Map<IEnumerable<CategoryResponse>>(categories);

                return response;
            }

            [HttpGet("{id}")]
        [Authorize(AppScopes.CategoriesRead)]
        public async Task<CategoryResponse> GetCategoryById([FromRoute] int id)
            {
                var category = await categoryService.GetCategory(id);
                var response = mapper.Map<CategoryResponse>(category);

                return response;
            }

        [HttpPost("")]
        [Authorize(AppScopes.CategoriesWrite)]
        public async Task<CategoryResponse> AddCategory([FromBody] AddCategoryRequest request)
            {
                var model = mapper.Map<AddCategoryModel>(request);
                var category = await categoryService.AddCategory(model);
                var response = mapper.Map<CategoryResponse>(category);

                return response;
            }

            [HttpPut("{id}")]
        [Authorize(AppScopes.CategoriesWrite)]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] UpdateCategoryRequest request)
            {
                var model = mapper.Map<UpdateCategoryModel>(request);
                await categoryService.UpdateCategory(id, model);

                return Ok();
            }

            [HttpDelete("{id}")]
        [Authorize(AppScopes.CategoriesWrite)]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
            {
                await categoryService.DeleteCategory(id);

                return Ok();
            }
        }
    }