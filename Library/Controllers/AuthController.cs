using Library.Data.DTOs;
using Library.Services.AuthService;
using Library.Services.TokenService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly TokenService _tokenService;

        public AuthController(IAuthService authService, TokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginDTO dto)
        {
            try
            {
                var login = await _authService.Login(dto.Email, dto.Password);

                var token = _tokenService.GenerateToken(login);

                return Ok(token);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message); //401
            }
        }
    }
}
