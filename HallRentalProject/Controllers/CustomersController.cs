using HallRentalAPI.Data;
using HallRentalAPI.Entities;
using HallRentalModels.Dtos;    // Making sure to include the namespace where the DTOs are
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq; // Include Linq for projection

namespace HallRentalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly HallDbContext _context;

        public CustomersController(HallDbContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
        {
            // Using projection to map entities to DTOs
            var customerDtos = await _context.Customers
                .Select(c => new CustomerDto
                {
                    CustomerID = c.CustomerID,
                    Name = c.Name,
                    Email = c.Email,
                    Phone = c.Phone
                })
                .ToListAsync();

            return Ok(customerDtos);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
        {
            // Using projection to map the entity to DTO
            var customerDto = await _context.Customers
                .Where(c => c.CustomerID == id)
                .Select(c => new CustomerDto
                {
                    CustomerID = c.CustomerID,
                    Name = c.Name,
                    Email = c.Email,
                    Phone = c.Phone
                })
                .FirstOrDefaultAsync();

            if (customerDto == null)
            {
                return NotFound();
            }

            return customerDto;
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<ActionResult<CustomerDto>> PostCustomer(CustomerDto customerDto)
        {
            // Mapping DTO to entity before adding to context
            var customer = new Customer
            {
                Name = customerDto.Name,
                Email = customerDto.Email,
                Phone = customerDto.Phone
                // Map other properties if necessary
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            // Return the DTO with the new ID assigned by the database
            customerDto.CustomerID = customer.CustomerID;

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.CustomerID }, customerDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, [FromBody] CustomerDto customerDto)
        {
            if (id != customerDto.CustomerID)
            {
                return BadRequest("Mismatched ID in URL and body.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            customer.Name = customerDto.Name;
            customer.Email = customerDto.Email;
            customer.Phone = customerDto.Phone;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Customers.Any(e => e.CustomerID == id))
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


        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

