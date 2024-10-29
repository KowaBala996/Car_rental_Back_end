using Car_rental.Entities;

namespace Car_rental.IRepository
{
    public interface IBookingRepository
    {
        void AddBooking(Booking booking);
        ICollection<Booking> GetAllBookings();
        Booking GetBookingById(string bookingId);
        void UpdateBooking(Booking booking);
        void DeleteBooking(string bookingId);
    }
}
