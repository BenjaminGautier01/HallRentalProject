using HallRentalAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace HallRentalAPI.Data
{
    public class HallDbContext : DbContext
    {
        public HallDbContext(DbContextOptions<HallDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Hall> Halls { get; set; } = null!;
        public DbSet<Booking> Bookings { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;




    }
}
