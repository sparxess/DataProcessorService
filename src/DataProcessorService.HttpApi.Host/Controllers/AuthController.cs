using DataProcessorService.Application.Contracts.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DataProcessorService.HttpApi.Host.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("registration")]
    public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
    {
        var result = await _authService.RegisterAsync(request.Username, request.Password);

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _authService.LoginAsync(request.Username, request.Password);

        if (!result.Success)
        {
            return Unauthorized(new { Error = result.ErrorMessage });
        }

        return Ok(new { Token = result });
    }
}