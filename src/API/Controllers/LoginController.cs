using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Application.ViewModels.AppSettings;
using Application.ViewModels.BadRequest;
using Application.ViewModels.Login;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/login")]
    public class LoginController : LoginBase
    {
        public LoginController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IOptions<AppSettingsJwt> appSettings) : base(userManager, signInManager, appSettings)
        { }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(BadRequest), (int)HttpStatusCode.BadRequest)]
        [Route("user")]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserRequest request)
        {
            var result = await InsertUserAsync(request.Email, request.Password);
            if (result.Succeeded) return Ok();

            return BadRequest(new BadRequest(result.Errors.Select(x => x.Description).ToList()));
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(BadRequest), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            var result = await Logar(request.Email, request.Password);
            if (result.Succeeded)
            {
                return Ok(await GenerateJwtAsync(request.Email));
            }

            if (result.IsLockedOut)
            {
                return BadRequest(new BadRequest("User temporarily blocked by invalid attempts"));
            }

            return BadRequest(new BadRequest("Incorrect username or password"));
        }

        [HttpGet]
        [Route("users")]
        [ProducesResponseType(typeof(UsuarioResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetUsersAsync()
        {
            var users = await GetAllUsersAsync();
            if (users == null || users.Count == 0) return NotFound();

            return Ok(users);
        }
    }
}
