using Car_rental.Entities;

namespace Car_rental.IRepository
{
    public interface IBookingPaymentRepository
    {
        void AddBookingPayment(BookingPayment payment);
        BookingPayment GetBookingPaymentById(string paymentId);
        ICollection<BookingPayment> GetAllBookingPayments();
        void UpdateBookingPayment(BookingPayment payment);
        void DeleteBookingPayment(string paymentId);
    }
}
