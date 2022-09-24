using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingAPI.Models;

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

            return Ok(await _context.ParkingSpots.ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ParkingSpot>> GetParkingSpot(int id)
        {
            var spot = await _context.ParkingSpots.FindAsync(id);
            if (spot == null)
            {
                return BadRequest("Parking spot bot found.");
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
    }
}
