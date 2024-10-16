namespace Car_rental.Entities
{
    public class Booking
    {
        public string BookingId { get; set; } // Format: BK-XXXX
        public string CustomerId { get; set; }
        public string CarId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } // E.g., Confirmed, Cancelled
        public DateTime CreatedDate { get; set; }

        public Booking()
        {
            CreatedDate = DateTime.Now;
        }
    }
}
