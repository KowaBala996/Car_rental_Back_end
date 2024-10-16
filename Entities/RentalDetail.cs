namespace Car_rental.Entities
{
    public class RentalDetail
    {

        public string RentalId { get; set; }
        public string BookingId { get; set; }
        public DateTime RentalDate { get; set; }
        public decimal FullPayment { get; set; }
        public string Status { get; set; } // E.g., Active, Completed, Cancelled

        public RentalDetail()
        {
            RentalDate = DateTime.Now;
            Status = "Active"; // Default status can be set to Active
        }
    }
}
