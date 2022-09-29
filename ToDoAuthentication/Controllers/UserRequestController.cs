using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.Buffers.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Nodes;
//using System.Web.Mvc;
using System.Xml;
using ToDoAuthentication.AuthenticationModel;
using ToDoAuthentication.AuthenticationService;

namespace ToDoAuthentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserRequestController : ControllerBase
    {
        private ITodoAuthenticationService _todoAuthenticationService;

        public UserRequestController(ITodoAuthenticationService itodoAuthenticationService)
        {
            _todoAuthenticationService = itodoAuthenticationService;
        }

        [HttpPost]
        [Route("Get-payload")]
        public string GetPayloadData(string inputtxtJwt)
        {
            //JwtSecurityToken jwtSecurityToken = new JwtSecurityToken();
            //jwtSecurityToken.Decode(token.Split(new char[1] { '.' }), token);
            //return jwtSecurityToken;

            /*
            
            var jwtHandler = new JwtSecurityTokenHandler();
            var txtJwtOut = string.Empty;
            var token = jwtHandler.ReadJwtToken(inputtxtJwt);
            var jwtclaims = token.Claims;
            string jwtPayloadData = "{";
            foreach (var claims in jwtclaims)
            {
                var hjh = claims.Type;
                jwtPayloadData += '"' + claims.Type + "\":\"" + claims.Value + "\",";
            }
            jwtPayloadData += "}";
            var headers = token.Header;
            var jwtHeader = "{";
            foreach (var h in headers)
            {
                jwtHeader += '"' + h.Key + "\":\"" + h.Value + "\",";
            }
            jwtHeader += "}";

            txtJwtOut += "Payload:" + jwtPayloadData + "Header: " + jwtHeader;
            return txtJwtOut;
            */

            char[] spearator = { '.', '.' };
            String[] parts = inputtxtJwt.Split(spearator);

            string encodedString = parts[1];

            var deserialisedJwtHeaderData = JsonExtensions.DeserializeJwtHeader(Base64UrlEncoder.Decode(encodedString));
            var SerialisedJwtHeaderData = deserialisedJwtHeaderData.SerializeToJson();

            return SerialisedJwtHeaderData;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<String> getUserId()
        {
            return User.Claims.Where(x => x.Type == "id").First().Value;
            //return Ok("Hello");
        }

        [HttpPost]
        [Route("Register")]
        public async Task<string> UserRegistration(UserRequestModel user)
        {
            await _todoAuthenticationService.RegisterUserAsync(user);
            return $"Your email - {user.Email}, has been successfully registered";
        }

        

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> UserLogin(UserRequestModel user)
        {
            var loginToken = await _todoAuthenticationService.UserLoginAsync(user);
            return Ok(new
            {
                Success = true,
                Token = loginToken
            });
        }
    }
}
