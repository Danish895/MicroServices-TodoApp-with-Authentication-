using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web.Mvc;
using ToDoAuthentication.AuthenticationModel;

namespace ToDoAuthentication.AuthenticationService
{
    public class TodoAuthenticationService : ITodoAuthenticationService
    {
        private readonly IConfiguration _configuration;
        
        private readonly UserManager<IdentityUser> _userManager;
        public TodoAuthenticationService(IConfiguration configuration, UserManager<IdentityUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager; 
        }

        
    
        public async Task<String> RegisterUserAsync(UserRequestModel user)
        {
            var existingUser = await _userManager.FindByEmailAsync(user.Email);
            if (existingUser != null)
            {
                return "Email already in use";
            }
                
            var newUser = new IdentityUser() { Email = user.Email, UserName = user.Email };
            var isCreated = await _userManager.CreateAsync(newUser, user.Password);
            if (isCreated.Succeeded)
            {
                var jwtToken = CreateToken(newUser);
                return jwtToken;
            }
            else
            {
                return "JWT is not created";
            }
        }

        public async Task<string> UserLoginAsync(UserRequestModel user)
        {
            var existingUser = await _userManager.FindByEmailAsync(user.Email);
            if (existingUser == null)
            {
                return "Enter valid User";
            }
            var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);
            if (!isCorrect)
            {
                return " Enter valid Credentials";
            }
            string jwtToken = CreateToken(existingUser);
            return  jwtToken ;
        }

        private string CreateToken(IdentityUser user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("email", user.Email),
                new Claim("id", user.Id)
            };
            //var c = new IdentityClaim(claims);

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("JwtConfig:Secret").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}
