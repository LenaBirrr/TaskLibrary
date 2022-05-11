using AutoMapper;
using TaskLibrary.UserAccount.Models;

namespace TaskLibrary.Api.Controllers.Accounts.Models
{
    public class UserAccountResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class UserAccountResponseProfile : Profile
    {
        public UserAccountResponseProfile()
        {
            CreateMap<UserAccountModel, UserAccountResponse>();
        }
    }
}
