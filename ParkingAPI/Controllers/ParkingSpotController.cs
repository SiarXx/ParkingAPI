using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<List<ParkingSpot>>> Get()
        {

            return Ok(await _context.ParkingSpots.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ParkingSpot>> Get(int id)
        {
            var spot = await _context.ParkingSpots.FindAsync(id);
            if (spot == null)
                return BadRequest("Parking spot bot found.");
            return Ok(spot);
        }

        [HttpPost]
        public async Task<ActionResult<List<ParkingSpot>>> AddParkingSpot(ParkingSpot spot)
        {
            _context.ParkingSpots.Add(spot);
            await _context.SaveChangesAsync();
            return Ok(await _context.ParkingSpots.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<ParkingSpot>>> UpdateParkingSpot(ParkingSpot request)
        {
            var spot = await _context.ParkingSpots.FindAsync(request.Id);
            if (spot == null)
                return BadRequest("Parking spot bot found.");

            spot.Name = request.Name;
            spot.isOccupied = request.isOccupied;
            spot.isReserved = request.isReserved;

            await _context.SaveChangesAsync();
            return Ok(await _context.ParkingSpots.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<ParkingSpot>> Delete(int id)
        {
            var spot = await _context.ParkingSpots.FindAsync(id);
            if (spot == null)
                return BadRequest("Parking spot bot found.");

            _context.ParkingSpots.Remove(spot);
            await _context.SaveChangesAsync();
            return Ok(await _context.ParkingSpots.ToListAsync());
        }
    }
}
