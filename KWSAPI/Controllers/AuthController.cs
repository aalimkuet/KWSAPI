using KWS.Models;
using KWSAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KWSAPI.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IConfiguration _config;
        private readonly KWSDBContext _kWSDBContext;
        public AuthController(KWSDBContext kWSDBContext, IConfiguration config)
        {
            _kWSDBContext = kWSDBContext;
            _config = config;
        }
        [HttpPost("CreateUser")]
        [EnableRateLimiting("AuthenticationPolicy")]
        public async Task<IActionResult> Create([FromBody] UserInfo user)
        {
            if (user == null)
            {
                return BadRequest("Invalid Request");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var addUser = _kWSDBContext.UserInfo.AddAsync(user);
                    await _kWSDBContext.SaveChangesAsync();
                    return Ok(new { Message = "User Create Successfully" });
                }
                else
                {
                    return BadRequest("Invalid Request");
                }
            }
        }
        [HttpPost("Login")]
        //[EnableRateLimiting("AuthenticationPolicy")]
        public async Task<IActionResult> Login([FromBody] UserInfo user)
        {
            if (user == null)
            {
                return BadRequest("Invalid Request");
            }

            var usr = _kWSDBContext.UserInfo.SingleOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);

            if (usr != null)
            {

                var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("Jwt:Key"));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user?.UserName ?? ""),
                new Claim(JwtRegisteredClaimNames.Email, usr ?.Email ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    Issuer = _config.GetValue<string>("Jwt:Issuer"),
                    Audience = _config.GetValue<string>("Jwt:Audience"),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);
                 


                ///var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(usr.Secretkey));
                //var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                //var tokenOptions = new JwtSecurityToken(
                //    issuer: _config.GetValue<string>("Jwt:Issuer"),
                //    audience: _config.GetValue<string>("Jwt:Issuer"),
                //    signingCredentials: signingCredentials,
                //    claims: new List<Claim>(),
                //    expires: DateTime.Now.AddMinutes(5)
                //    );
                //var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return await Task.FromResult(Ok(new { Token = jwtToken }));
            }
            else
            {
                return Unauthorized();
            }

        }
    }
}
