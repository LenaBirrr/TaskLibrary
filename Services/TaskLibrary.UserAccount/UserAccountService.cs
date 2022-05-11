using AutoMapper;
//using Microsoft.AspNet.Identity;

using TaskLibrary.Common.Exceptions;
using TaskLibrary.Common.Validator;
using TaskLibrary.Db.Entities;
using TaskLibrary.RabbitMQService;
using TaskLibrary.RabbitMQService.Models;
using TaskLibrary.UserAccount.Models;

using Microsoft.AspNetCore.Identity;

namespace TaskLibrary.UserAccount
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly IRabbitMqTask rabbitMqTask;
        private readonly IModelValidator<RegisterUserAccountModel> registerUserAccountModelValidator;

        public UserAccountService(IMapper mapper, UserManager<User> userManager, IRabbitMqTask rabbitMqTask, IModelValidator<RegisterUserAccountModel> registerUserAccountModelValidator)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.rabbitMqTask = rabbitMqTask;
            this.registerUserAccountModelValidator = registerUserAccountModelValidator;
        }

        public async Task<UserAccountModel> Create(RegisterUserAccountModel model)
        {
            registerUserAccountModelValidator.Check(model);

            // Find user by email
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null)
                throw new ProcessException($"User account with email {model.Email} already exist.");

            // Create user account
            user = new User()
            {
                Status = UserStatus.Active,
                FullName = model.Name,
                UserName = model.Email,  // Ýòî ëîãèí. Ìû áóäåì åãî ïðèðàâíèâàòü ê email, õîòÿ ýòî è íå îáÿçàòåëüíî
                Email = model.Email,
                EmailConfirmed = true, // Òàê êàê ýòî ó÷åáíûé ïðîåêò, òî ñðàçó ñ÷èòàåì, ÷òî ïî÷òà ïîäòâåðæäåíà. Â ðåàëüíîì ïðîåêòå, ñêîðåå âñåãî, íàäî áóäåò åå ïîäòâåðäèòü ÷åðåç ññûëêó â ïèñüìå
                PhoneNumber = null,
                PhoneNumberConfirmed = false
                // ... Òàêæå çäåñü åñòü åùå èíòåðåñíûå ñâîéñòâà. Ïîñìîòðèòå â äîêóìåíòàöèè.
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                throw new ProcessException($"Creating user account is wrong. {String.Join(", ", result.Errors.Select(s => s.Description))}");


            // Send email to user
            // !!! Îáðàòèòå âíèìàíèå, ÷òî ìû íå îòïðàâëÿåì ïèñüìî, à ñîçäàåì çàäàíèå íà åãî îòïðàâêó. Äàëüøå óæå âñå ñäåëàåòñÿ ñàìî äðóãèìè ñåðâèñàìè.
            await rabbitMqTask.SendEmail(new EmailModel()
            {
                Email = model.Email,
                Subject = "TaskLibrary",
                Message = "Your account was registered successful"
            });


            // Returning the created user
            return mapper.Map<UserAccountModel>(user);
        }
    }
}
