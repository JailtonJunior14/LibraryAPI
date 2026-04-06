using Library.Data.DTOs;
using Library.Logs;
using Library.Services.BookService;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
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
                Log.LogToFile("Getallbook controller", ex.GetType().ToString(), ex.Message);
                return BadRequest(ex.Message);
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
                Log.LogToFile("Getbyid controller", ex.GetType().ToString(), ex.Message);
                return BadRequest(ex.Message);
                
            }
        }

        [HttpGet("{title}")]
        public async Task<ActionResult<BookDTO>> GetByTitle(string title)
        {
            try
            {
                var books = await _bookService.GetByTitle(title);

                return Ok(books);
            }
            catch (Exception ex)
            {
                Log.LogToFile("Getbytitle controller", ex.GetType().ToString(), ex.Message);
                return BadRequest(ex.Message);

            }
        }

        [HttpGet("{author}")]
        public async Task<ActionResult<BookDTO>> GetByAuthor(string author)
        {
            try
            {
                var books = await _bookService.GetByAuthor(author);

                return Ok(books);
            }
            catch (Exception ex)
            {
                Log.LogToFile("Getbyauthor controller", ex.GetType().ToString(), ex.Message);
                return BadRequest(ex.Message);

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
                Log.LogToFile("controllerbook post", ex.GetType().ToString(), ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<BookController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] BookUpdateDTO dto)
        {
            try
            {
                var book = await _bookService.Update(dto);
                return CreatedAtAction(
                    nameof(Put),
                    book);
            }catch(Exception ex)
            {
                Log.LogToFile("controllerbook put", ex.GetType().ToString(), ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<BookController>/5
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Guid id)
        {
            try
            {
                await _bookService.Delete(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                Log.LogToFile("controller book delete", ex.GetType().ToString(), ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
