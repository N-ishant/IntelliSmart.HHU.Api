using IntelliSmart.HHU.Api.Models.Account;

namespace IntelliSmart.HHU.Api.Services
{
    public interface IAccountService
    {
        Register ValidateUser(Login loginmodel);
        bool RegisterUser(Register registermodel);
        bool UpdateUser(Register registermodel);
        bool DeleteUser(int userId);
    }
}