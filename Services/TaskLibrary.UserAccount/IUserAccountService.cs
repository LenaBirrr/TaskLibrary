using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary.UserAccount.Models;

namespace TaskLibrary.UserAccount
{
    public interface IUserAccountService
    {
        /// <summary>
        /// Create user account
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<UserAccountModel> Create(RegisterUserAccountModel model);




        // .. Также здесь можно разместить методы для изменения данных учетной записи, восстановления и смены пароля, подтверждения электронной почты, установки телефона и его подтверждения и т.д.
        // .. Но это уже на самостоятельно.
        // .. Удачи! Я в вас верю!  :)
    }
}
