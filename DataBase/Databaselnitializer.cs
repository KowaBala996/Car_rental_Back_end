using Microsoft.Data.Sqlite;
using System.Data;

namespace Car_rental.DataBase
{
    public class DatabaseInitializer
    {
        private readonly string _connectionString;

        public DatabaseInitializer(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Initialize()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Cars (
                            CarId NVARCHAR(50) PRIMARY KEY,
                            Brand NVARCHAR(25) NOT NULL,
                            BodyType NVARCHAR(25) NOT NULL,
                            Model NVARCHAR(25) NOT NULL,
                            Transmission NVARCHAR(25) NOT NULL,
                            FuelType NVARCHAR(25) NOT NULL,
                            NumberOfSeats INT NOT NULL,
                            PricePerHour DECIMAL NOT NULL,
                            ImagePath NVARCHAR(100) NULL,
                            AvailableFrom DATE NOT NULL,
                            AvailableTo DATE NOT NULL
                        );

                        CREATE TABLE IF NOT EXISTS Customers (
                            CustomerId NVARCHAR(50) PRIMARY KEY,
                            Name NVARCHAR(50),
                            Phone NVARCHAR(15),
                            Email NVARCHAR(50),
                            Nic NVARCHAR(50),
                            Password NVARCHAR(50),
                            CarId NVARCHAR(50) NULL,
                            Address NVARCHAR(100),
                            PostalCode NVARCHAR(10),
                            DrivingLicenseNumber NVARCHAR(20),
                            FrontImagePath NVARCHAR(100),
                            ProofIdNumber NVARCHAR(20),
                            ProfileStatus NVARCHAR(10)
                        );

                        CREATE TABLE IF NOT EXISTS Bookings (
                            BookingId NVARCHAR(50) PRIMARY KEY, 
                            CustomerId NVARCHAR(50) NULL, 
                            CarId NVARCHAR(50) NULL, 
                            StartDate DATE NOT NULL, 
                            EndDate DATE NOT NULL, 
                            TotalPrice DECIMAL NOT NULL, 
                            Status NVARCHAR(20) NOT NULL, 
                            CreatedDate DATE NOT NULL
                        );

                        CREATE TABLE IF NOT EXISTS BookingPayments (
                            PaymentId NVARCHAR(50) PRIMARY KEY, 
                            BookingId NVARCHAR(50) NOT NULL, 
                            Amount DECIMAL NOT NULL, 
                            PaymentMethod NVARCHAR(25) NOT NULL, 
                            Status NVARCHAR(20) NOT NULL, 
                            PaymentDate DATE NOT NULL, 
                            ReceiptNumber NVARCHAR(50) NOT NULL
                        );

                        CREATE TABLE IF NOT EXISTS RentalDetails (
                            RentalId NVARCHAR(50) PRIMARY KEY, 
                            BookingId NVARCHAR(50) NOT NULL, 
                            RentalDate DATE NOT NULL, 
                            FullPayment DECIMAL NOT NULL, 
                            Status NVARCHAR(20) NOT NULL, 
                            FOREIGN KEY (BookingId) REFERENCES Bookings(BookingId)
                        );

                        CREATE TABLE IF NOT EXISTS ReturnDetails (
                            ReturnId NVARCHAR(50) PRIMARY KEY, 
                            RentalId NVARCHAR(50) NOT NULL, 
                            ReturnDate DATE NOT NULL, 
                            Condition NVARCHAR(20) NOT NULL, 
                            LateFees DECIMAL NOT NULL, 
                            FOREIGN KEY (RentalId) REFERENCES RentalDetails(RentalId)
                        );

                        CREATE TABLE IF NOT EXISTS Notifications (
                            NotificationId NVARCHAR(50) PRIMARY KEY, 
                            RentalId NVARCHAR(50) NOT NULL, 
                            ReturnId NVARCHAR(50) NOT NULL, 
                            Status NVARCHAR(20) NOT NULL, 
                            FOREIGN KEY (RentalId) REFERENCES RentalDetails(RentalId), 
                            FOREIGN KEY (ReturnId) REFERENCES ReturnDetails(ReturnId)
                        );
                    ";



                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

    }
}
