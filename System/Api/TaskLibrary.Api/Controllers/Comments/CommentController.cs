using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskLibrary.Api.Controllers.Comments.Models;
using TaskLibrary.CommentService;
using TaskLibrary.CommentService.Models;
using TaskLibrary.Common.Security;

namespace TaskLibrary.Api.Controllers.Comments
{
        [Route("api/v{version:apiVersion}/comments")]
        [ApiController]
        [ApiVersion("1.0")]
        [Authorize]
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
            [Authorize(AppScopes.CommentsRead)]
            public async Task<IEnumerable<CommentResponse>> GetComments()
            {
                var comments = await commentService.GetComments();
                var response = mapper.Map<IEnumerable<CommentResponse>>(comments);

                return response;
            }
            [HttpGet("Byuser")]
            [Authorize(AppScopes.CommentsRead)]
            public async Task<IEnumerable<CommentResponse>> GetCommentsByUser()
            {

                var id = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var comments = await commentService.GetCommentsByUser(id);
                var response = mapper.Map<IEnumerable<CommentResponse>>(comments);
                
                return response;
            }

            [HttpGet("Bytask/{id}")]
            [Authorize(AppScopes.CommentsRead)]
            public async Task<IEnumerable<CommentResponse>> GetCommentsByTask([FromRoute] int id)
            {
                var comments = await commentService.GetCommentsByTask(id);
                var response = mapper.Map<IEnumerable<CommentResponse>>(comments);

                return response;
            }


            [HttpGet("{id}")]
            [Authorize(AppScopes.CommentsRead)]
            public async Task<CommentResponse> GetCommentById([FromRoute] int id)
            {
                var comment = await commentService.GetComment(id);
                var response = mapper.Map<CommentResponse>(comment);

                return response;
            }

            [HttpPost("")]
            [Authorize(AppScopes.CommentsWrite)]
            public async Task<CommentResponse> AddComment([FromBody] AddCommentRequest request)
            {
                request.UserId = new Guid (User.FindFirstValue(ClaimTypes.NameIdentifier));
                var model = mapper.Map<AddCommentModel>(request);
                var comment = await commentService.AddComment(model);
                var response = mapper.Map<CommentResponse>(comment);

                return response;
            }

            [HttpPut("{id}")]
            [Authorize(AppScopes.CommentsWrite)]
            public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] UpdateCommentRequest request)
            {
                var model = mapper.Map<UpdateCommentModel>(request);
                await commentService.UpdateComment(id, model);

                return Ok();
            }

            [HttpDelete("{id}")]
            [Authorize(AppScopes.CommentsWrite)]
            public async Task<IActionResult> DeleteComment([FromRoute] int id)
            {
                await commentService.DeleteComment(id);

                return Ok();
            }
        }
    }
