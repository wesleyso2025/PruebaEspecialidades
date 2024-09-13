using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace LibeyTechnicalTestAPI.Controllers.LibeyUser
{
    [ApiController]
    [Route("[controller]")]
    public class LibeyUserController : Controller
    {
        private readonly ILibeyUserAggregate _aggregate;
        public LibeyUserController(ILibeyUserAggregate aggregate)
        {
            _aggregate = aggregate;
        }
        [HttpGet]
        [Route("{documentNumber}")]
        public IActionResult FindResponse(string documentNumber)
        {
            var row = _aggregate.FindResponse(documentNumber);
            if (row != null)
            {
                return Ok(row);
            }
            return NotFound($"User with document number {documentNumber} not found.");
        }
        [HttpPost]
        public IActionResult Create([FromBody] UserUpdateorCreateCommand command)
        {
            try
            {
                _aggregate.Create(command);
                return Ok("User created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating user: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("{documentNumber}")]
        public IActionResult Update(string documentNumber, [FromBody] UserUpdateorCreateCommand command)
        {
            if (command.DocumentNumber != documentNumber)
            {
                return BadRequest("Document number in path and body do not match.");
            }

            try
            {
                _aggregate.Update(command);
                return Ok("User updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating user: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("{documentNumber}")]
        public IActionResult Delete(string documentNumber)
        {
            try
            {
                _aggregate.Delete(documentNumber);
                return Ok("User deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting user: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAll()
        {
            try
            {
                var users = _aggregate.GetAll();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error fetching users: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] string term = "")
        {
            var users = _aggregate.GetAllByTerm(term);
            return Ok(users);
        }
    }
}