using System.Security.Claims;
using Culturio.API.Models;
using Culturio.Application.CultureObjects.Models;
using Culturio.Application.Users;
using Culturio.Application.Users.Models;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Culturio.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet(Name = nameof(GetUsers))]
        //[Authorize]
        [RequiredScope("tasks.read")]
        public async Task<GetUsersResponseModel> GetUsers([FromQuery] GetUserDto model, CancellationToken token)
        {
            _logger.LogInformation("Fetching users");
            //System.Security.Claims.ClaimTypes.NameIdentifier
            var sub = this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Subject);
            var users = await _userService.GetUsers(model, token);

            return new GetUsersResponseModel(users);
        }

        [HttpGet("{id}", Name = nameof(GetUser))]        
        public async Task<IActionResult> GetUser([FromRoute] int id, CancellationToken token)
        {
            _logger.LogInformation("Fetching user by id {UserId}", id);

            UserDto user = await _userService.GetById(id, token);

            return user is not null ? Ok(user) : NotFound();
        }

        [HttpPost(Name = nameof(CreateUser))]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto model, CancellationToken token)
                                    {
            _logger.LogInformation("Creating a new user");

            int userId = await _userService.CreateUser(model, token);

            return CreatedAtAction(nameof(CreateUser), new { Id = userId });
        }

        [HttpDelete("{id}", Name = nameof(DeleteUser))]
        public async Task<IActionResult> DeleteUser([FromRoute] int id, CancellationToken token)
        {
            _logger.LogInformation("Deleting user with id {UserId}", id);

            bool userDeleted = await _userService.DeleteUser(id, token);

            return userDeleted ? NoContent() : NotFound();
        }

        [HttpPut(Name = nameof(UpdateUser))]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto model, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating user with id {UserID}", model.Id);

            await _userService.UpdateUser(model, cancellationToken);

            return NoContent();
        }

        
        [HttpGet("{id}/qrcode", Name = nameof(GetUsersQR))]
        public async Task<IActionResult> GetUsersQR([FromRoute] int id, CancellationToken token)
        {
            _logger.LogInformation("Fetching qrcode of user by id {UserId}", id);

            StringDto qrcode = await _userService.GetQRcode(id, token);

            return Ok(qrcode);
        }

        [HttpGet("{qrcode}/checkQRcode", Name = nameof(CheckQRcode))]
        public async Task<IActionResult> CheckQRcode([FromRoute] string qrcode, CancellationToken token)
        {
            _logger.LogInformation("Checking if user with qr code {qrcode} exists", qrcode);

            StringDto qrcodeResponse = await _userService.CheckQRcode(qrcode, token);

            return Ok(qrcodeResponse);
        }

        [HttpGet("userInfo", Name = nameof(GetUserInfo))]
        public async Task<IActionResult> GetUserInfo(CancellationToken token)
        {
            _logger.LogInformation("Checking user information for ids and role");

            var sub = this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Subject).Value;

            UserAuthDto userAuthDto = await _userService.GetUserInfo(sub, token);

            return Ok(userAuthDto);
        }

    }
}
