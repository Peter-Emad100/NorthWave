using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthWave.Application.DTOs.Auth;
using NorthWave.Application.Interfaces.ServicesInterfaces;

namespace NorthWave.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);
            return Ok(result);
        }

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyTwoFactor(VerifyTwoFactorDto dto)
        {
            var response = await _authService.VerifyTwoFactorAsync(dto);

            return Ok(response);
        }

    }
}
