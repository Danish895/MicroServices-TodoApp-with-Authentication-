using System.ComponentModel.DataAnnotations;

namespace ToDoAuthentication.AuthenticationModel
{
    public class UserRequestModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
