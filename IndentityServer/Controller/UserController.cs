using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IndentityServer.Dto;
using IndentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Shared.DTO;

namespace IndentityServer.Controller
{
    [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        [HttpPost]
        public async Task<IActionResult> Singup(SingupDto singupDto)
        {
            var user = new ApplicationUser()
            {
                UserName = singupDto.UserName,
                City = singupDto.City,
                Email = singupDto.Email
            };
            var result = await _userManager.CreateAsync(user, singupDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(Responce<Shared.DTO.NoContent>.Fail(result.Errors.Select(x=>x.Description).ToList(),400));
            }

            return Ok(Responce<NoContent>.Success(200));

        }

        [HttpGet]
        public async Task<IActionResult> GeUser()
        {
            var useridClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
            if (useridClaim == null) return BadRequest();

            var user = await _userManager.FindByIdAsync(useridClaim.Value);

            if (user == null) return BadRequest();
            return Ok(new {Id = user.Id, Email = user.Email, UserName = user.UserName, City = user.City});



        }

    }
}
