using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingAPI.Models;

namespace ParkingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : Controller
    {
        private readonly DataContext _context;

        public CarController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllCars")]
        public async Task<ActionResult<List<Car>>> GetAllCars()
        {
            var cars = await _context.Cars
                .Include(c => c.Owner)
                .ToListAsync();

            return Ok(cars);
        }

        [HttpGet("GetCar{id:int}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            Car? car;
            try
            {
                 car = await _context.Cars
                    .Include(c => c.Owner)
                    .FirstOrDefaultAsync(c => c.Id == id);
            }
            catch(Exception e)
            {
                return BadRequest($"Could not find car. Exception occured. Exception message: {e.Message}");
            }

            if (car == null)
            {
                return BadRequest("Car not found.");
            }
                
            return Ok(car);
        }

        [HttpGet("GetPersonCars{personId:int}")]
        public async Task<ActionResult<Car>> GetPersonCars(int personId)
        {
            var cars = await _context.Cars
                .Where(c => c.PersonId == personId)
                .ToListAsync();

            return Ok(cars);
        }

        [HttpPost("AddCar")]
        public async Task<ActionResult<Car>> AddCar(Car car)
        {
            _ = await _context.Cars.AddAsync(car);
            _ = await _context.SaveChangesAsync();
            var cars = await _context.Cars.ToListAsync();

            return Ok(cars);
        }

        [HttpPost("RegisterCar")]
        public async Task<ActionResult<Car>> RegsiterCar(RegisterCarDto request)
        {
            var person = await _context.Persons.FindAsync(request.PersonId);

            if (person == null)
            {
                return BadRequest("Person not found.");
            }

            var newCar = new Car
            {
                Registration = request.Registration,
                PersonId = request.PersonId,
                Owner = person
            };

            _ = await _context.Cars.AddAsync(newCar);
            _ = await _context.SaveChangesAsync();
            var personCars = await GetPersonCars(request.PersonId);

            return Ok(personCars);
        }
    }
}
