using bookingtaxi_backend.Model;
using bookingtaxi_backend.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace bookingtaxi_backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly IOptions<JwtSettings> _jwtSettings;
        public AuthenticateController(AccountService service, IOptions<JwtSettings> jwtSettings) { _accountService = service; _jwtSettings = jwtSettings; }

        [HttpPost("")]
        public async Task<IActionResult> GenerateToken([FromBody] AuthRequest request)
        {

            var account = await _accountService.GetAccountByCredentials(request.Email, request.Password);

            if (account == null)
            {
                return BadRequest(new ErrorResponse("Login credentials not valid"));
            }

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Value.Secret);
            var role = await _accountService.GetRole(account.RoleID!);


            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[] {
                new Claim("ID", account.Id),
                new Claim("Role", role!.Name),
                new Claim(JwtRegisteredClaimNames.Sub, account.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())

            }),
                Expires = DateTime.Now.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return Ok(new AuthResponse()
            {
                Token = jwtToken,
                Result = true,
                Role = role.Name,
                AccountID = account.Id,
                FullName = $"{account.GivenName} {account.LastName}",
                Image = account.ProfileImage
            });
        }

    }
}
