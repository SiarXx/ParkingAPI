using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingAPI.Models;
using ParkingAPI.Properties;

namespace ParkingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingSpotController : ControllerBase
    {

        private readonly DataContext _context;

        public ParkingSpotController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ParkingSpot>>> GetAllParkingSpots()
        {
            var spots = await _context.ParkingSpots
                .Include(p => p.ParkedCar)
                .ToListAsync();

            return Ok(spots);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ParkingSpot>> GetParkingSpot(int id)
        {
            var spot = await _context.ParkingSpots
                .Include(p => p.ParkedCar)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (spot == null)
            {
                return BadRequest(ErrorMessages.ParkingSpotController_ParkingSpotNotFound);
            }

            return Ok(spot);
        }

        [HttpPost]
        public async Task<ActionResult<List<ParkingSpot>>> AddParkingSpot(ParkingSpot spot)
        {
            _ = await _context.ParkingSpots.AddAsync(spot);
            _ = await _context.SaveChangesAsync();

            return Ok(await _context.ParkingSpots.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<ParkingSpot>>> UpdateParkingSpot(ParkingSpot request)
        {
            var spot = await _context.ParkingSpots.FindAsync(request.Id);
            if (spot == null)
            {
                return BadRequest("Parking spot bot found.");
            }

            spot.Name = request.Name;
            spot.IsOccupied = request.IsOccupied;
            spot.IsReserved = request.IsReserved;

            _ = await _context.SaveChangesAsync();

            return Ok(await _context.ParkingSpots.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<ParkingSpot>> Delete(int id)
        {
            var spot = await _context.ParkingSpots.FindAsync(id);
            if (spot == null)
            {
                return BadRequest("Parking spot bot found.");
            }

            _ = _context.ParkingSpots.Remove(spot);
            _ = await _context.SaveChangesAsync();

            return Ok(spot);
        }

        [HttpPut("ParkCar{spotId:int}/{carId:int}")]
        public async Task<ActionResult<ParkingSpot>> ParkCarOn(int spotId, int carId)
        {
            var spot = await _context.ParkingSpots.FindAsync(spotId);
            var car = await _context.Cars
                .Include(c => c.Owner)
                .ThenInclude(o => o.ReservedSpots)
                .FirstOrDefaultAsync(c => c.Id == carId);
            var isValid = ValidateSpotForCar(spot, car, out string errorMessage);

            if (!isValid)
            {
                return BadRequest(errorMessage);
            }

            spot.IsOccupied = true;
            spot.ParkedCar = car;
            spot.CarId = car.Id;

            _ = await _context.SaveChangesAsync();

            return Ok(spot);
        }

        [HttpPut("FreeParkingSpot{spotId:int}")]
        public async Task<ActionResult<ParkingSpot>> FreeParkingSpot(int spotId)
        {
            var spot = await _context.ParkingSpots.FindAsync(spotId);

            if(spot is null)
            {
                return BadRequest("Parking spot not found.");
            }
            
            if (!spot.IsOccupied)
            {
                return BadRequest("Parking spot is not Occupied.");
            }

            spot.IsOccupied = false;
            spot.CarId = null;
            spot.ParkedCar = null;

            _ = await _context.SaveChangesAsync();

            return Ok(spot);
        }

        private static bool ValidateSpotForCar(ParkingSpot spot, Car car, out string errorMessage)
        {
            errorMessage = string.Empty;
            bool isValid = true;

            if (spot is null)
            {
                errorMessage = "Parking spot not found.";

                isValid = false;
            }
            else if (car is null)
            {
                errorMessage = "Car not found.";

                isValid = false;
            }
            else if (spot.IsOccupied)
            {
                errorMessage = "Parking spot is occupied.";

                isValid = false;
            }
            else if (spot.IsReserved)
            {
                var carOwner = car.Owner;
                
                if(carOwner is null || (_ = carOwner.ReservedSpots.FirstOrDefault(s => s.Id == spot.Id)) is null)
                {
                    errorMessage = "Parking spot is Reserved";

                    isValid = false;
                }
            }

            return isValid;
        }
    }
}
