using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.Buffers.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Nodes;
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
        public string GetPayloadData(string txtJwtIn)
        {
            //Assume the input is in a control called txtJwtIn,
            //and the output will be placed in a control called txtJwtOut
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtTokenInput = txtJwtIn;

            //Check if readable token (string is in a JWT format)
            //var readableToken = jwtHandler.CanReadToken(jwtInput);
            var txtJwtOut = string.Empty;
            
            
                var token = jwtHandler.ReadJwtToken(txtJwtIn);

                //Extract the headers of the JWT
                var headers = token.Header;
                var jwtHeader = "{";
                foreach (var h in headers)
                {
                    jwtHeader += '"' + h.Key + "\":\"" + h.Value + "\",";
                }
                jwtHeader += "}";
                txtJwtOut = "Header:\r\n" + JToken.Parse(jwtHeader).ToString((Newtonsoft.Json.Formatting)Formatting.Indented);

                //Extract the payload of the JWT
                var claims = token.Claims;
                var jwtPayload = "{";
                foreach (Claim c in claims)
                {
                    jwtPayload += '"' + c.Type + "\":\"" + c.Value + "\",";
                }
                jwtPayload += "}";
                txtJwtOut += "\r\nPayload:\r\n" + JToken.Parse(jwtPayload).ToString((Newtonsoft.Json.Formatting)Formatting.Indented);
                return txtJwtOut;
            

            //int startindex = givenjwtToken.IndexOf('.');
            //int lastindex = givenjwtToken.LastIndexOf('.');

            //// string payload = givenjwtToken.Substring(startindex, lastindex);

            //string payload = "eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ";
            //byte[] data = Convert.FromBase64String(givenjwtToken);
            //string decodedString = Encoding.UTF8.GetString(data);

            //List<String> payloads = new List<String>();

            //return decodedString;.split("\\.");



            //char[] spearator = { '.','.' };
            //String[] parts = givenjwtToken.Split(spearator);  

            //string encodedString = parts[1];

            //Console.WriteLine(encodedString);


            //var payload = decode(parts[1]);


            //return payload;
        }
        //private  List<string> decode(string encodedString)
        //{
        //    var signature = SigningCredentials.SecurityAlgorithms.HmacSha512Signature(base64Header + "." + base64Payload, "secret").toString(CryptoJS.enc.Base64);
        //    var valid = signature == base64Sign;
        //    var ghj = SigningCredentials()

        //    var valueBytes = System.Convert.FromBase64String(encodedString);
        //    return Encoding.UTF8.GetString(valueBytes);
        //    //return decodedString;
        //}


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
