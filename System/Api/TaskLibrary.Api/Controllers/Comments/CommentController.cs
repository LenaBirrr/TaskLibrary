using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskLibrary.Api.Controllers.Comments.Models;
using TaskLibrary.CommentService;
using TaskLibrary.CommentService.Models;

namespace TaskLibrary.Api.Controllers.Comments
{
        [Route("api/v{version:apiVersion}/comments")]
        [ApiController]
        [ApiVersion("1.0")]
        public class CommentController : ControllerBase
        {
            private readonly IMapper mapper;
            private readonly ILogger<CommentController> logger;
            private readonly ICommentService commentService;

            public CommentController(IMapper mapper, ILogger<CommentController> logger, ICommentService commentService)
            {
                this.mapper = mapper;
                this.logger = logger;
                this.commentService = commentService;
            }

            [HttpGet("")]
            // [Authorize(AppScopes.BooksRead)]
            public async Task<IEnumerable<CommentResponse>> GetComments()
            {
                var comments = await commentService.GetComments();
                var response = mapper.Map<IEnumerable<CommentResponse>>(comments);

                return response;
            }
            [HttpGet("Byuser/{id}")]
            public async Task<IEnumerable<CommentResponse>> GetCommentsByUser([FromRoute] Guid id)
            {
                var comments = await commentService.GetCommentsByUser(id);
                var response = mapper.Map<IEnumerable<CommentResponse>>(comments);
                
                return response;
            }

            [HttpGet("Bytask/{id}")]
            public async Task<IEnumerable<CommentResponse>> GetCommentsByTask([FromRoute] int id)
            {
                var comments = await commentService.GetCommentsByTask(id);
                var response = mapper.Map<IEnumerable<CommentResponse>>(comments);

                return response;
            }


            [HttpGet("{id}")]
            //[Authorize(AppScopes.BooksRead)]
            public async Task<CommentResponse> GetCommentById([FromRoute] int id)
            {
                var comment = await commentService.GetComment(id);
                var response = mapper.Map<CommentResponse>(comment);

                return response;
            }

            [HttpPost("")]
            //[Authorize(AppScopes.BooksWrite)]
            public async Task<CommentResponse> AddComment([FromBody] AddCommentRequest request)
            {
                var model = mapper.Map<AddCommentModel>(request);
                var comment = await commentService.AddComment(model);
                var response = mapper.Map<CommentResponse>(comment);

                return response;
            }

            [HttpPut("{id}")]
            //[Authorize(AppScopes.BooksWrite)]
            public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] UpdateCommentRequest request)
            {
                var model = mapper.Map<UpdateCommentModel>(request);
                await commentService.UpdateComment(id, model);

                return Ok();
            }

            [HttpDelete("{id}")]
            //[Authorize(AppScopes.BooksWrite)]
            public async Task<IActionResult> DeleteComment([FromRoute] int id)
            {
                await commentService.DeleteComment(id);

                return Ok();
            }
        }
    }
