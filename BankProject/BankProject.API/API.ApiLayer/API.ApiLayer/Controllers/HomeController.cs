using API.BusinessLogicLayer.Models.Services.Interfaces;
using API.DataLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.ApiLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<UserModel>> GetUsers()
        {
            var users = await _userService.GetAllAsync();

            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateUser([FromBody] UserModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.PasswordHash))
            {
                return BadRequest("Invalid user data");
            }

            model.PasswordHash = HashPassword(model.PasswordHash);

            model.Id = Guid.NewGuid();
            var id = await _userService.AddAsync(model);

            return Ok(id);
        }

        private string HashPassword(string password)
        {
            // TODO
            return password;
        }
    }
}
