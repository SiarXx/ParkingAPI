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

            return Ok(await _context.Cars.ToListAsync());
        }
        [HttpGet("GetCar{id:int}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
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

        [HttpPost("RegisterCar")]
        public async Task<ActionResult<Car>> RegsiterCar(RegisterCarDto request)
        {
            var person = await _context.Persons.FindAsync(request.PersonId);
            if (person == null)
                return BadRequest("Person not found.");
            var newCar = new Car
            {
                Registration = request.Registration,
                Make = request.Make,
                Model = request.Model,
                PersonId = request.PersonId,
                Owner = person
            };
            _context.Cars.Add(newCar);
            await _context.SaveChangesAsync();

            return await GetPersonCars(request.PersonId);
        }
        [HttpPut("ParkCar{id:int}")]
        public async Task<ActionResult<Car>> ParkCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return BadRequest("Car not Found");
            }
            var freeSpots = await _context.ParkingSpots
                .Where(ps => ps.IsOccupied == false)
                .ToListAsync();
            if (!freeSpots.Any())
            {
                return BadRequest("No free parking spot found");
            }
            ParkCarInFreeSpot(car, freeSpots);
            _ = await _context.SaveChangesAsync();

            return Ok(await GetCar(id));
        }
        private static Car ParkCarInFreeSpot(Car car, List<ParkingSpot> freeSpots)
        {
            foreach (var ps in freeSpots)
            {
                if (!ps.IsReserved)
                {
                    car.IsParked = true;
                    car.ParkedSpot = ps;
                    car.ParkedSpotId = ps.Id;
                    ps.IsOccupied = true;
                    ps.ParkedCar = car;
                    ps.CarId = car.Id;
                    break;
                }
            }

            return car;
        }
        [HttpPut("RemoveCarFromParking{id:int}")]
        public async Task<ActionResult<Car>> RemoveCarFromParking(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return BadRequest("Car not found");
            }
            else if (!car.IsParked)
            {
                return BadRequest("Car is not parked");
            }
            // TODO : Object Car from database does not have the ParkingSpot. Fix so it can
            // be accesssed with simple call rather thant FindAsync()
            var spot = await _context.ParkingSpots.FindAsync(car.ParkedSpotId);
            spot.IsOccupied = false;
            spot.ParkedCar = null;
            spot.CarId = null;
            car.IsParked = false;
            car.ParkedSpot = null;
            car.ParkedSpotId = null;

            _context.SaveChanges();

            return Ok(await GetCar(car.Id));
        }
    }
}
