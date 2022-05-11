namespace TaskLibrary.WEB;

using System.Threading.Tasks;
using TaskLibrary.Web;

public interface IAuthService
{
    Task<LoginResult> Login(LoginModel loginModel);
    Task Logout();
}