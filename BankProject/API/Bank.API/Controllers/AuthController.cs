using AutoMapper;
using Bank.API.DTOs;
using Bank.BusinessLayer.Models;
using BANK.BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public AuthController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        try
        {
            var registerModel = _mapper.Map<Register>(request);
            await _userService.RegisterUser(registerModel);

            var loginRequest = new LoginRequest
            {
                Username = request.Username,
                Password = request.Password
            };

            var login = _mapper.Map<Login>(loginRequest);
            var user = await _userService.ValidateUser(login);
            var token = _userService.GenerateJwtToken(user);

            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var login = _mapper.Map<Login>(request);
        var user = await _userService.ValidateUser(login);
        if (user == null)
        {
            return Unauthorized(new { message = "errors.login" });
        }

        var token = _userService.GenerateJwtToken(user);

        return Ok(new { Token = token });
    }

    [HttpGet("tokenlogin")]
    public async Task<IActionResult> TokenLogin()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            return Unauthorized();
        }

        Guid userId = Guid.Parse(userIdClaim.Value);
        var user = await _userService.Find(userId);
        var token = _userService.GenerateJwtToken(user);

        return Ok(new { Token = token });
    }
}
