using Car_rental.Entities;
using Car_rental.IRepository;
using Microsoft.Data.Sqlite;

namespace Car_rental.Repository
{
    public class BookingPaymentRepository : IBookingPaymentRepository
    {
            private readonly string _connectionString;

            public BookingPaymentRepository(string connectionString)
            {
                _connectionString = connectionString;
            }

            public void AddBookingPayment(BookingPayment payment)
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                    INSERT INTO BookingPayments (PaymentId, BookingId, Amount, PaymentMethod, Status, PaymentDate, ReceiptNumber)
                    VALUES (@paymentId, @bookingId, @amount, @paymentMethod, @status, @paymentDate, @receiptNumber)";

                    command.Parameters.AddWithValue("@paymentId", payment.PaymentId);
                    command.Parameters.AddWithValue("@bookingId", payment.BookingId);
                    command.Parameters.AddWithValue("@amount", payment.Amount);
                    command.Parameters.AddWithValue("@paymentMethod", payment.PaymentMethod);
                    command.Parameters.AddWithValue("@status", payment.Status);
                    command.Parameters.AddWithValue("@paymentDate", payment.PaymentDate);
                    command.Parameters.AddWithValue("@receiptNumber", payment.ReceiptNumber);

                    command.ExecuteNonQuery();
                }
            }

            public BookingPayment GetBookingPaymentById(string paymentId)
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM BookingPayments WHERE PaymentId = @paymentId";
                    command.Parameters.AddWithValue("@paymentId", paymentId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new BookingPayment()
                            {
                                PaymentId = reader.GetString(0),
                                BookingId = reader.GetString(1),
                                Amount = reader.GetDecimal(2),
                                PaymentMethod = reader.GetString(3),
                                Status = reader.GetString(4),
                                PaymentDate = reader.GetDateTime(5),
                                ReceiptNumber = reader.GetString(6)
                            };
                        }
                    }
                }
                return null; // or throw an exception if preferred
            }

            public ICollection<BookingPayment> GetAllBookingPayments()
            {
                var paymentsList = new List<BookingPayment>();
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM BookingPayments";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            paymentsList.Add(new BookingPayment()
                            {
                                PaymentId = reader.GetString(0),
                                BookingId = reader.GetString(1),
                                Amount = reader.GetDecimal(2),
                                PaymentMethod = reader.GetString(3),
                                Status = reader.GetString(4),
                                PaymentDate = reader.GetDateTime(5),
                                ReceiptNumber = reader.GetString(6)
                            });
                        }
                    }
                }
                return paymentsList;
            }

            public void UpdateBookingPayment(BookingPayment payment)
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                    UPDATE BookingPayments
                    SET Amount = @amount,
                        PaymentMethod = @paymentMethod,
                        Status = @status,
                        PaymentDate = @paymentDate,
                        ReceiptNumber = @receiptNumber
                    WHERE PaymentId = @paymentId";

                    command.Parameters.AddWithValue("@amount", payment.Amount);
                    command.Parameters.AddWithValue("@paymentMethod", payment.PaymentMethod);
                    command.Parameters.AddWithValue("@status", payment.Status);
                    command.Parameters.AddWithValue("@paymentDate", payment.PaymentDate);
                    command.Parameters.AddWithValue("@receiptNumber", payment.ReceiptNumber);
                    command.Parameters.AddWithValue("@paymentId", payment.PaymentId);

                    command.ExecuteNonQuery();
                }
            }

            public void DeleteBookingPayment(string paymentId)
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "DELETE FROM BookingPayments WHERE PaymentId = @paymentId";
                    command.Parameters.AddWithValue("@paymentId", paymentId);
                    command.ExecuteNonQuery();
                }
            }
    }
}
