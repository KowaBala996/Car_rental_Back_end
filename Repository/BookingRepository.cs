using Car_rental.Entities;
using Car_rental.IRepository;
using Microsoft.Data.Sqlite;

namespace Car_rental.Repository
{
    public class BookingRepository : IBookingRepository
    {
            private readonly string _connectionString;

            public BookingRepository(string connectionString)
            {
                _connectionString = connectionString;
            }

            public void AddBooking(Booking booking)
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                    INSERT INTO Bookings (BookingId, CustomerId, CarId, StartDate, EndDate, TotalPrice, Status, CreatedDate) 
                    VALUES (@bookingId, @customerId, @carId, @startDate, @endDate, @totalPrice, @status, @createdDate)";

                    command.Parameters.AddWithValue("@bookingId", booking.BookingId);
                    command.Parameters.AddWithValue("@customerId", booking.CustomerId);
                    command.Parameters.AddWithValue("@carId", booking.CarId);
                    command.Parameters.AddWithValue("@startDate", booking.StartDate);
                    command.Parameters.AddWithValue("@endDate", booking.EndDate);
                    command.Parameters.AddWithValue("@totalPrice", booking.TotalPrice);
                    command.Parameters.AddWithValue("@status", booking.Status);
                    command.Parameters.AddWithValue("@createdDate", DateTime.Now);

                    command.ExecuteNonQuery();
                }
            }

            public ICollection<Booking> GetAllBookings()
            {
                var bookingsList = new List<Booking>();
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM Bookings";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bookingsList.Add(new Booking()
                            {
                                BookingId = reader.GetString(0),
                                CustomerId = reader.GetString(1),
                                CarId = reader.GetString(2),
                                StartDate = reader.GetDateTime(3),
                                EndDate = reader.GetDateTime(4),
                                TotalPrice = reader.GetDecimal(5),
                                Status = reader.GetString(6),
                                CreatedDate = reader.GetDateTime(7)
                            });
                        }
                    }
                }
                return bookingsList;
            }

            public Booking GetBookingById(string bookingId)
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM Bookings WHERE BookingId = @bookingId";
                    command.Parameters.AddWithValue("@bookingId", bookingId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Booking()
                            {
                                BookingId = reader.GetString(0),
                                CustomerId = reader.GetString(1),
                                CarId = reader.GetString(2),
                                StartDate = reader.GetDateTime(3),
                                EndDate = reader.GetDateTime(4),
                                TotalPrice = reader.GetDecimal(5),
                                Status = reader.GetString(6),
                                CreatedDate = reader.GetDateTime(7)
                            };
                        }
                    }
                }
                return null;
            }

            public void UpdateBooking(Booking booking)
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                    UPDATE Bookings 
                    SET CustomerId = @customerId, 
                        CarId = @carId, 
                        StartDate = @startDate, 
                        EndDate = @endDate, 
                        TotalPrice = @totalPrice, 
                        Status = @status 
                    WHERE BookingId = @bookingId";

                    command.Parameters.AddWithValue("@bookingId", booking.BookingId);
                    command.Parameters.AddWithValue("@customerId", booking.CustomerId);
                    command.Parameters.AddWithValue("@carId", booking.CarId);
                    command.Parameters.AddWithValue("@startDate", booking.StartDate);
                    command.Parameters.AddWithValue("@endDate", booking.EndDate);
                    command.Parameters.AddWithValue("@totalPrice", booking.TotalPrice);
                    command.Parameters.AddWithValue("@status", booking.Status);

                    command.ExecuteNonQuery();
                }
            }

            public void DeleteBooking(string bookingId)
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "DELETE FROM Bookings WHERE BookingId = @bookingId";
                    command.Parameters.AddWithValue("@bookingId", bookingId);
                    command.ExecuteNonQuery();
                }
            }
        }
}
