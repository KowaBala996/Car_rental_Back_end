namespace Car_rental.Entities
{
    public class BookingPayment
    {
        public string PaymentId { get; set; }
        public string BookingId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public DateTime PaymentDate { get; set; }
        public string ReceiptNumber { get; set; }

        public BookingPayment()
        {
            PaymentDate = DateTime.Now;
        }

    }
}
