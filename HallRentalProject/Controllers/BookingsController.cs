using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HallRentalAPI.Data;
using HallRentalAPI.Entities;
using HallRentalModels.Dtos;    // Using the DTOs

namespace HallRentalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly HallDbContext _context;

        public BookingsController(HallDbContext context)
        {
            _context = context;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetBookings()
        {
            var bookings = await _context.Bookings.ToListAsync();
            var bookingDtos = bookings.Select(b => new BookingDto
            {
                BookingID = b.BookingID,
                CustomerID = b.CustomerID,
                HallID = b.HallID,
                BookingDate = b.BookingDate,
                RentalDate = b.RentalDate,
                Duration = b.Duration,
                TotalCost = b.TotalCost
            }).ToList();

            return Ok(bookingDtos);
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDto>> GetBookingDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FirstOrDefaultAsync(m => m.BookingID == id);
            if (booking == null)
            {
                return NotFound();
            }

            var bookingDto = new BookingDto
            {
                BookingID = booking.BookingID,
                CustomerID = booking.CustomerID,
                HallID = booking.HallID,
                BookingDate = booking.BookingDate,
                RentalDate = booking.RentalDate,
                Duration = booking.Duration,
                TotalCost = booking.TotalCost
            };

            return Ok(bookingDto);
        }

        // POST: api/Bookings
        [HttpPost]
        public async Task<ActionResult<BookingDto>> CreateBooking([FromBody] BookingDto bookingDto)
        {
            if (ModelState.IsValid)
            {
                var booking = new Booking
                {
                    // Map BookingDto to Booking entity
                    CustomerID = bookingDto.CustomerID,
                    HallID = bookingDto.HallID,
                    BookingDate = bookingDto.BookingDate,
                    RentalDate = bookingDto.RentalDate,
                    Duration = bookingDto.Duration,
                    TotalCost = bookingDto.TotalCost
                };

                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetBookingDetails), new { id = booking.BookingID }, bookingDto);
            }

            return BadRequest(ModelState);
        }

        // PUT: api/Bookings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditBooking(int id, [FromBody] BookingDto bookingDto)
        {
            if (id != bookingDto.BookingID)
            {
                return BadRequest();
            }

            var booking = new Booking
            {
                // Map BookingDto to Booking entity
                BookingID = bookingDto.BookingID,
                CustomerID = bookingDto.CustomerID,
                HallID = bookingDto.HallID,
                BookingDate = bookingDto.BookingDate,
                RentalDate = bookingDto.RentalDate,
                Duration = bookingDto.Duration,
                TotalCost = bookingDto.TotalCost
            };

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(booking.BookingID))
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

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingID == id);
        }
    }
}

