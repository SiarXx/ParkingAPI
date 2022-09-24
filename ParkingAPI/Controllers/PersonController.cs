using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingAPI.Models;

namespace ParkingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : Controller
    {
        private readonly DataContext _context;

        public PersonController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Person>>> GetAllPersons()
        {
            return Ok(await _context.Persons.ToListAsync());
        }

        [HttpGet("id")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return BadRequest("Person not found.");
            }

            return Ok(person);
        }
        [HttpDelete]
        public async Task<ActionResult<List<Person>>> RemovePerson(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return BadRequest("Person not found.");
            }

            return Ok(person);
        }
        [HttpPost]
        public async Task<ActionResult<List<Person>>> AddPerson(Person person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            return Ok(await _context.Persons.ToListAsync());
        }
    }
}
