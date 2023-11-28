namespace HallRentalAPI.Entities
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public int BookingID { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? PaymentMethod { get; set; }
        // Navigation property for the related Booking
        public Booking? Booking { get; set; }

    }
}
