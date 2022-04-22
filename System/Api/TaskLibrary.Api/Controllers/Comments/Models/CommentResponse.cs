using AutoMapper;
using TaskLibrary.CommentService.Models;

namespace TaskLibrary.Api.Controllers.Comments.Models
{
    public class CommentResponse
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string UserName { get; set; }

        public string TaskName { get; set; }
        public int ProgrammingTaskId { get; set; }

        public Guid UserId { get; set; }
    }
    public class CommentResponseProfile : Profile
    {
        public CommentResponseProfile()
        {
            CreateMap<CommentModel, CommentResponse>();
        }
    }
}
