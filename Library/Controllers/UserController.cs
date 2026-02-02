using Library.Logs;
using Library.Data.DTOs;
using Library.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService; 
        }
        // GET: api/<ValuesController>
        [Authorize]
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
                Log.LogToFile("byid toekn", ex.Message);
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

                return CreatedAtAction(
                    nameof(Post),
                    user);

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
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
