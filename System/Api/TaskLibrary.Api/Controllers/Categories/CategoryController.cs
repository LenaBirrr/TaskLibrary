using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskLibrary.Api.Controllers.Categories.Models;
using TaskLibrary.CategoryService;

namespace TaskLibrary.Api.Controllers.Categories
{
        [Route("api/v{version:apiVersion}/categories")]
        [ApiController]
        [ApiVersion("1.0")]
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
            // [Authorize(AppScopes.BooksRead)]
            public async Task<IEnumerable<CategoryResponse>> GetCategories()
            {
                var categories = await categoryService.GetCategories();
                var response = mapper.Map<IEnumerable<CategoryResponse>>(categories);

                return response;
            }

            [HttpGet("{id}")]
            //[Authorize(AppScopes.BooksRead)]
            public async Task<CategoryResponse> GetCategoryById([FromRoute] int id)
            {
                var category = await categoryService.GetCategory(id);
                var response = mapper.Map<CategoryResponse>(category);

                return response;
            }

            [HttpPost("")]
            //[Authorize(AppScopes.BooksWrite)]
            public async Task<CategoryResponse> AddCategory([FromBody] AddCategoryRequest request)
            {
                var model = mapper.Map<AddCategoryModel>(request);
                var category = await categoryService.AddCategory(model);
                var response = mapper.Map<CategoryResponse>(category);

                return response;
            }

            [HttpPut("{id}")]
            //[Authorize(AppScopes.BooksWrite)]
            public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] UpdateCategoryRequest request)
            {
                var model = mapper.Map<UpdateCategoryModel>(request);
                await categoryService.UpdateCategory(id, model);

                return Ok();
            }

            [HttpDelete("{id}")]
            //[Authorize(AppScopes.BooksWrite)]
            public async Task<IActionResult> DeleteCategory([FromRoute] int id)
            {
                await categoryService.DeleteCategory(id);

                return Ok();
            }
        }
    }