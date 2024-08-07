using FutBinProject.Business.User.Dtos;
using FutBinProject.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FutBin_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(NewUserDto newuser)
        {
            var user = new ApplicationUser()
            {
                UserName = newuser.Name,
                Email = newuser.Email,
                Address = newuser.Address,
            };
            var result = await _userManager.CreateAsync(user, newuser.Password);

            return Ok(new { Message = "User Registered Successfully" });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel loginmodel)
        {
            var user = await _userManager.FindByEmailAsync(loginmodel.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginmodel.Password))
            {
                var authenticationClaims = new List<Claim>
                    {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.UserName)
                    };

                //Generate the token with claims 
                var jwtToken = GetToken(authenticationClaims);

                //return the token
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtToken),

                });
            }
            return Unauthorized();
        }

        private JwtSecurityToken GetToken(List<Claim> authenticationClaims)
        {
            var authenticationSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("I hate My Brain Cuz it is so toxic and I like it"));

            var Token = new JwtSecurityToken(

                    expires: DateTime.Now.AddDays(2).ToLocalTime(),
                    claims: authenticationClaims,
                    signingCredentials: new SigningCredentials(authenticationSigninKey, SecurityAlgorithms.HmacSha256)
                );
            return Token;
        }
    }
}
