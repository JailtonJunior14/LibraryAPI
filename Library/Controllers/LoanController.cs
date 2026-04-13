using Library.Data.DTOs;
using Library.Data.Entities;
using Library.Logs;
using Library.Services.LoanService;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }
        // GETALL: api/<LoanController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoanDTO>>> GetAll()
        {
            try
            {
                var loans = await _loanService.GetAll();

                return Ok(loans);
            }catch (Exception ex)
            {
                Log.LogToFile("loan controller GETALL", ex.GetType().ToString(), ex.Message);

                return BadRequest(ex.Message);
            }
        }

        // GETById: api/<LoanController>/5
        [HttpGet("{id}")]
        public ActionResult<string> GetById( int id)
        {
            throw new NotImplementedException();
        }

        // POST api/<LoanController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoanInsertDTO dto)
        {
            try
            {
                var loan = await _loanService.Create(dto);

                return CreatedAtAction(
                    nameof(Post),
                    loan
                    );

            }catch(Exception ex)
            {
                Log.LogToFile("loan controller POST", ex.GetType().ToString(), ex.Message);

                return BadRequest(ex.Message);

            }
        }

        [HttpPost("/returnBook")]
        public async Task<IActionResult> ReturnBook([FromBody] ReturnBooksDTO dto)
        {
            try
            {
                var loan = await _loanService.ReturnBook(dto);

                return Ok(loan);
            }catch(Exception ex)
            {
                Log.LogToFile("loan controller ReturnedBookController", ex.GetType().ToString(), ex.Message);

                return BadRequest(ex.Message);
            }
        }

        // PUT api/<LoanController>/5>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] LoanUpdateDTO dto)
        {
            try
            {
                var loan = await _loanService.Update(dto);

                return CreatedAtAction(
                    nameof(Put),
                    loan);

            }catch(Exception ex)
            {
                Log.LogToFile("loan controller PUT", ex.GetType().ToString(), ex.Message);

                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<LoanController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
