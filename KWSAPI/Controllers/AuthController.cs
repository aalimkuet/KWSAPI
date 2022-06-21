using KWS.Models;
using KWSAPI.Models;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ILogger<AuthController> _logger;
        private readonly KWSDBContext _kWSDBContext;
        public AuthController(KWSDBContext kWSDBContext)
        {
            _kWSDBContext = kWSDBContext;
        }
        [HttpPost("CreateUser")]
        public async Task<IActionResult> Create([FromBody] UserAuthen user)
        {
            if (user == null)
            {
                return BadRequest("Invalid Request");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var addUser = _kWSDBContext.UserAuthens.AddAsync(user);
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
        public async Task<IActionResult> Login([FromBody] UserAuthen user)
        {
            if (user == null)
            {
                return BadRequest("Invalid Request");
            }

            var usr = _kWSDBContext.UserAuthens.SingleOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);

            if (usr != null)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(usr.Secretkey));
                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    issuer: "http://localhost:7061",
                    audience: "http://localhost:7061",
                    signingCredentials: signingCredentials,
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5)

                    );
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { Token = token });
            }
            else
            {
                return Unauthorized();
            }

        }
    }
}
