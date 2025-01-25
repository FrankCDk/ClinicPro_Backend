using AutoMapper;
using ClinicPro.Application.Dtos.Auth;
using ClinicPro.Application.Dtos.Login;
using ClinicPro.Application.Interfaces;
using ClinicPro.Core.Common;
using Microsoft.AspNetCore.Mvc;

namespace ClinicPro.Presentation.Controllers
{

    [ApiController]
    [Route("api/v1/auth/[action]")]
    public class AuthController : Controller
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
        
            LoginResponse result = await _authService.Login(request);

            return Ok(new Response<LoginResponse>
            {
                Data = result,
                Message = "Login successful",
                StatusCode = 200
            });

        }


        [HttpPost]
        public async Task<IActionResult> RenovateToken([FromBody] RenovateTokenRequest request)
        {
            await _authService.RenovateToken(request);
            return Ok(new Response<LoginResponse>
            {
                Message = "Token renovado exitosamente.",
                StatusCode = 200
            });
        }


        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            await _authService.Register(request);
            return Ok(new Response
            {
                Message = "Usuario registrado exitosamente.",
                StatusCode = 200
            });
        }

    }
}
