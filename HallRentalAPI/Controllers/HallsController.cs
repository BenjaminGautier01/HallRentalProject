using HallRentalAPI.Data;
using HallRentalAPI.Entities;
using HallRentalModels.Dtos; // Include the namespace for HallDto
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq; // Include Linq for projection

namespace HallRentalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HallsController : ControllerBase
    {
        private readonly HallDbContext _context;

        public HallsController(HallDbContext context)
        {
            _context = context;
        }

        // GET: api/Halls
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HallDto>>> GetHalls()
        {
            // Project Hall to HallDto
            var hallsDto = await _context.Halls
                .Select(h => new HallDto
                {
                    HallID = h.HallID,
                    Name = h.Name,
                    Location = h.Location,
                    Capacity = h.Capacity,
                    Amenities = h.Amenities
                })
                .ToListAsync();

            return Ok(hallsDto);
        }

        // GET: api/Halls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HallDto>> GetHall(int id)
        {
            // Project Hall to HallDto
            var hallDto = await _context.Halls
                .Where(h => h.HallID == id)
                .Select(h => new HallDto
                {
                    HallID = h.HallID,
                    Name = h.Name,
                    Location = h.Location,
                    Capacity = h.Capacity,
                    Amenities = h.Amenities
                })
                .FirstOrDefaultAsync();

            if (hallDto == null)
            {
                return NotFound();
            }

            return hallDto;
        }

        // POST: api/Halls
        [HttpPost]
        public async Task<ActionResult<HallDto>> PostHall(HallDto hallDto)
        {
            // Map HallDto to Hall entity
            var hall = new Hall
            {
                Name = hallDto.Name,
                Location = hallDto.Location,
                Capacity = hallDto.Capacity,
                Amenities = hallDto.Amenities
                // Map other properties
            };

            _context.Halls.Add(hall);
            await _context.SaveChangesAsync();

            hallDto.HallID = (int)hall.HallID; // Set the ID from the saved entity // If HallID is nullable, coalesce to 0 or handle appropriately                             

            return CreatedAtAction(nameof(GetHall), new { id = hall.HallID }, hallDto);

        }

        // PUT: api/Halls/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHall(int id, HallDto hallDto)
        {
            if (id != hallDto.HallID)
            {
                return BadRequest();
            }

            // Find the existing hall
            var hall = await _context.Halls.FindAsync(id);
            if (hall == null)
            {
                return NotFound();
            }

            // Map changes from HallDto to the existing Hall entity
            hall.Name = hallDto.Name;
            hall.Location = hallDto.Location;
            hall.Capacity = hallDto.Capacity;
            hall.Amenities = hallDto.Amenities;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Halls.Any(h => h.HallID == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Halls/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHall(int id)
        {
            var hall = await _context.Halls.FindAsync(id);
            if (hall == null)
            {
                return NotFound();
            }

            _context.Halls.Remove(hall);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
