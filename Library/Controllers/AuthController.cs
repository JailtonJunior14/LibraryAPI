using Library.Data.DTOs;
using Library.Services.AuthService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginDTO dto)
        {
            try
            {
                var login = await _authService.Login(dto.Email, dto.Password);

                return Ok(login);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message); //401
            }
        }
    }
}
