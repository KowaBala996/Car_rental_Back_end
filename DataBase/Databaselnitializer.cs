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
                        Id NVARCHAR(50) PRIMARY KEY,
                        Name NVARCHAR(50),
                        Phone NVARCHAR(15),
                        Email NVARCHAR(50),
                        Nic NVARCHAR(50),
                        Password NVARCHAR(50),
                        Address NVARCHAR(100),
                        PostalCode NVARCHAR(10),
                        DrivingLicenseNumber NVARCHAR(20),
                        FrontImagePath NVARCHAR(100),
                        ProofIdNumber NVARCHAR(20),
                        ProfileStatus NVARCHAR(10)
                    );

                    CREATE TABLE IF NOT EXISTS Bookings (
                        BookingId NVARCHAR(50) PRIMARY KEY, 
                        CustomerId NVARCHAR(50) NOT NULL, 
                        CarId NVARCHAR(50) NOT NULL, 
                        StartDate DATE NOT NULL, 
                        EndDate DATE NOT NULL, 
                        TotalPrice DECIMAL NOT NULL, 
                        Status NVARCHAR(20) NOT NULL, 
                        CreatedDate DATE NOT NULL, 
                        FOREIGN KEY (CustomerId) REFERENCES Customers(Id), 
                        FOREIGN KEY (CarId) REFERENCES Cars(CarId)
                    );

                    CREATE TABLE IF NOT EXISTS BookingPayments (
                        PaymentId NVARCHAR(50) PRIMARY KEY, 
                        BookingId NVARCHAR(50) NOT NULL, 
                        Amount DECIMAL NOT NULL, 
                        PaymentMethod NVARCHAR(25) NOT NULL, 
                        Status NVARCHAR(20) NOT NULL, 
                        PaymentDate DATE NOT NULL, 
                        ReceiptNumber NVARCHAR(50) NOT NULL, 
                        FOREIGN KEY (BookingId) REFERENCES Bookings(BookingId)
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

                    -- Insert initial data in the correct order
                    INSERT OR REPLACE INTO Cars (CarId, Brand, BodyType, Model, Transmission, FuelType, NumberOfSeats, PricePerHour, ImagePath, AvailableFrom, AvailableTo)
                    VALUES
                        ('Car_01', 'Toyota', 'Sedan', 'Corolla', 'Automatic', 'Gasoline', 5, 25.00, '/images/cars/car_01.jpg', '2024-01-01', '2024-12-31'),
                        ('Car_02', 'Honda', 'SUV', 'CR-V', 'Automatic', 'Gasoline', 7, 30.00, '/images/cars/car_02.jpg', '2024-02-01', '2024-12-31'),
                        ('Car_03', 'Ford', 'Hatchback', 'Fiesta', 'Manual', 'Diesel', 5, 22.50, '/images/cars/car_03.jpg', '2024-03-01', '2024-12-31'),
                        ('Car_04', 'BMW', 'Sedan', '3 Series', 'Automatic', 'Gasoline', 5, 45.00, '/images/cars/car_04.jpg', '2024-04-01', '2024-12-31'),
                        ('Car_05', 'Audi', 'SUV', 'Q5', 'Automatic', 'Gasoline', 5, 50.00, '/images/cars/car_05.jpg', '2024-05-01', '2024-12-31'),
                        ('Car_06', 'Tesla', 'Sedan', 'Model S', 'Automatic', 'Electric', 5, 70.00, '/images/cars/car_06.jpg', '2024-06-01', '2024-12-31'),
                        ('Car_07', 'Chevrolet', 'Pickup', 'Silverado', 'Automatic', 'Diesel', 5, 40.00, '/images/cars/car_07.jpg', '2024-07-01', '2024-12-31'),
                        ('Car_08', 'Hyundai', 'Hatchback', 'i30', 'Manual', 'Gasoline', 5, 20.00, '/images/cars/car_08.jpg', '2024-08-01', '2024-12-31'),
                        ('Car_09', 'Mercedes-Benz', 'SUV', 'GLC', 'Automatic', 'Gasoline', 5, 55.00, '/images/cars/car_09.jpg', '2024-09-01', '2024-12-31'),
                        ('Car_10', 'Volkswagen', 'Sedan', 'Passat', 'Automatic', 'Diesel', 5, 35.00, '/images/cars/car_10.jpg', '2024-10-01', '2024-12-31');

                    INSERT OR IGNORE INTO Customers (Id, Name, Phone, Email, Nic, Password, Address, PostalCode, DrivingLicenseNumber, FrontImagePath, ProofIdNumber, ProfileStatus) 
                    VALUES
                    ('CUST-001', 'John Doe', '1234567890', 'john.doe@example.com', '1234567', 'password123', '123 Main St, City', '12345', 'DL123456', '/images/customers/front.jpg', 'PID123456', 'VERIFIED');

                    INSERT OR IGNORE INTO Bookings (BookingId, CustomerId, CarId, StartDate, EndDate, TotalPrice, Status, CreatedDate) 
                    VALUES
                    ('BK-0001', 'CUST-001', 'Car_01', '2024-10-10', '2024-10-12', 31.0, 'Confirmed', DATE('now'));

                    INSERT OR IGNORE INTO BookingPayments (PaymentId, BookingId, Amount, PaymentMethod, Status, PaymentDate, ReceiptNumber) 
                    VALUES
                    ('PAY-0001', 'BK-0001', 31.0, 'Credit Card', 'Completed', DATE('now'), 'REC-0001');

                    INSERT OR IGNORE INTO RentalDetails (RentalId, BookingId, RentalDate, FullPayment, Status) 
                    VALUES
                    ('REN-0001', 'BK-0001', DATE('now'), 31.0, 'Active');

                    INSERT OR IGNORE INTO ReturnDetails (ReturnId, RentalId, ReturnDate, Condition, LateFees) 
                    VALUES
                    ('RET-0001', 'REN-0001', DATE('now'), 'Good', 0.0);

                    INSERT OR IGNORE INTO Notifications (NotificationId, RentalId, ReturnId, Status) 
                    VALUES
                    ('NOT-0001', 'REN-0001', 'RET-0001', 'Unread');
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
