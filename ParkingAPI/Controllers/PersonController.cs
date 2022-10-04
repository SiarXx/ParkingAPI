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
            var persons = await _context.Persons.ToListAsync();

            return Ok(persons);
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
            _ = await _context.Persons.AddAsync(person);
            _ = await _context.SaveChangesAsync();
            var persons = await _context.Persons.ToListAsync();

            return Ok(persons);
        }

        [HttpPut("Reserve Spot{personId:int}/{spotId:int}")]
        public async Task<ActionResult<List<ParkingSpot>>> ReserveParkingSpot(int personId, int spotId)
        {
            var person = await _context.Persons
                .Include(p => p.ReservedSpots)
                .FirstOrDefaultAsync(p => p.Id == personId);
            
            if(person is null)
            {
                return BadRequest("No Person found.");
            }

            var parkingSpot = await _context.ParkingSpots.FindAsync(spotId);

            if(parkingSpot is null)
            {
                return BadRequest("No parking spot found.");
            }
            else if(parkingSpot.IsReserved)
            {
                return BadRequest("Parking spot is already reserved.");
            }
            else if(parkingSpot.IsOccupied)
            {
                return BadRequest("Parking spot is occupied");
            }

            parkingSpot.IsReserved = true;
            person.ReservedSpots.Add(parkingSpot);

            _ = await _context.SaveChangesAsync();

            return Ok(person.ReservedSpots);
        }

        [HttpPut("Revoke Spot{personId:int}/{spotId:int}")]
        public async Task<ActionResult<List<ParkingSpot>>> RevokeParkingSpot(int personId, int spotId)
        {
            var person = await _context.Persons
                .Include(p => p.ReservedSpots)
                .FirstOrDefaultAsync(p => p.Id == personId);

            if (person is null)
            {
                return BadRequest("No Person found.");
            }

            var parkingSpot = await _context.ParkingSpots.FindAsync(spotId);

            if (parkingSpot is null)
            {
                return BadRequest("No parking spot found.");
            }
            else if (!parkingSpot.IsReserved)
            {
                return BadRequest("Parking spot is not reserved reserved.");
            }

            parkingSpot.IsReserved = false;
            person.ReservedSpots.Remove(parkingSpot);

            _ = await _context.SaveChangesAsync();

            return Ok(person.ReservedSpots);
        }
    }
}
