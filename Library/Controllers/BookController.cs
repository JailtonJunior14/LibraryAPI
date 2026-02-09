using Library.Data.DTOs;
using Library.Logs;
using Library.Services.BookService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        // GET: api/<BookController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetAll()
        {
            try
            {
                var books = await _bookService.GetAll();

                return Ok(books);

            }catch (Exception ex)
            {
                Log.LogToFile("Getallbook controller", ex.Message);
                throw;
            }
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetById(Guid id)
        {
            try
            {
                var book = await _bookService.GetById(id);

                return Ok(book);
            }
            catch (Exception ex)
            {
                Log.LogToFile("Getbyid controller", ex.Message);
                throw;
            }
        }

        // POST api/<BookController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookInsertDTO dto)
        {
            try
            {
                var book = await _bookService.Create(dto);
                return CreatedAtAction(
                    nameof(Post),
                    book
                    );

            }catch (Exception ex)
            {
                Log.LogToFile("controllerbook post", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
