using Library.Logs;
using Library.Data.DTOs;
using Library.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Library.Services.AuthService;
using Library.Services.TokenService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly TokenService _tokenService;

        public UserController(IUserService userService, IAuthService authService, TokenService tokenService)
        {
            _userService = userService;
            _authService = authService;
            _tokenService = tokenService;
        }
        // GET: api/<ValuesController>
        [Authorize(Roles = "admin, librarian")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
        {
            try
            {
                var users = await _userService.GetAll();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET getbyid user
        [Authorize(Roles = "admin, librarian")]
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<UserDTO?>>> GetById(Guid id)
        {
            try
            {
                var user = await _userService.GetById(id);

                return Ok(user);
            }
            catch (ApplicationException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                Log.LogToFile("byid toekn", ex.GetType().ToString(), ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // POST create user
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserInsertDTO dto)
        {
            try
            {
                var user = await _userService.Create(dto);
                var login = new LoginResponseDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    Role = user.Role
                };
                var token = _tokenService.GenerateToken(login);

                user.Token = token;
                return CreatedAtAction(
                    nameof(Post),
                    user
                    //token
                    );

            }
            catch (ApplicationException ex)
            {
                return Conflict(new
                {
                    message = ex.Message
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        // PUT api/<ValuesController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserUpdateDTO dto)
        {
            try
            {
                var userUpdate = await _userService.Update(dto);

                return CreatedAtAction(
                    nameof(Put), userUpdate);

            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete]
        //[HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromBody] Guid id)
        {
            try
            {
                await _userService.Delete(id);

                return NoContent();

            }catch(Exception ex)
            {
                Log.LogToFile("controller delete user",  ex.GetType().ToString(), ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
