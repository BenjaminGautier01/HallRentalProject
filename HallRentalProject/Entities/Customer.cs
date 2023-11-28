namespace HallRentalAPI.Entities
{
    public class Customer
    {
        public Customer()
        {
            Bookings = new List<Booking>();
        }


        public int CustomerID { get; set; }
        public string? Name { get; set; }
        public string ?Email { get; set; }
        public string? Phone { get; set; }

        // Navigation property for related Bookings
        public List<Booking> Bookings { get; set; }
    }
}
