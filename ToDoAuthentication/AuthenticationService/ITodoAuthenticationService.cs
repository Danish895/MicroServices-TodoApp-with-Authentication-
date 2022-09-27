using Microsoft.AspNetCore.Mvc;
using ToDoAuthentication.AuthenticationModel;

namespace ToDoAuthentication.AuthenticationService
{
    public interface ITodoAuthenticationService
    {
        Task<String> RegisterUserAsync(UserRequestModel user);
        Task<String> UserLoginAsync(UserRequestModel user);
    }
}
