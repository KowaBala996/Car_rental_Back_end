using Microsoft.Data.Sqlite;

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
                    CREATE TABLE IF NOT EXISTS Cars(
                        Id NVARCHAR(50) PRIMARY KEY,  
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

                    INSERT OR IGNORE INTO Cars (Id, Brand, BodyType, Model, Transmission, FuelType, NumberOfSeats, PricePerHour, ImagePath, AvailableFrom, AvailableTo) 
                    VALUES
                    ('Car_01', 'Toyota', 'SUV', 'Highlander', 'Automatic', 'Petrol', 7, 15.5, '/images/cars/toyota_highlander.jpg', '2024-10-01', '2024-12-31');

                    CREATE TABLE IF NOT EXISTS Customers(
                        Id INTEGER PRIMARY KEY AUTOINCREMENT, 
                        Name NVARCHAR(50) NOT NULL, 
                        Phone NVARCHAR(15) NOT NULL, 
                        Email NVARCHAR(50) NOT NULL, 
                        Nic INTEGER NOT NULL, 
                        Password NVARCHAR(50) NOT NULL, 
                        Address NVARCHAR(100) NOT NULL, 
                        PostalCode NVARCHAR(10) NOT NULL, 
                        DrivingLicenseNumber NVARCHAR(20) NOT NULL, 
                        FrontImagePath NVARCHAR(100) NULL, 
                        ProofIdNumber NVARCHAR(20) NOT NULL, 
                        ProfileStatus NVARCHAR(10) NOT NULL
                    );

                    INSERT OR IGNORE INTO Customers (Name, Phone, Email, Nic, Password, Address, PostalCode, DrivingLicenseNumber, FrontImagePath, BackImagePath, ProofIdNumber, ProfileStatus) 
                    VALUES
                   ('John Doe', '1234567890', 'john.doe@example.com', '1234567', 'password123', '123 Main St, City', '12345', 'DL123456', '/images/customers/front.jpg', '/images/customers/back.jpg', 'PID123456', 'VERIFIED');

                    CREATE TABLE IF NOT EXISTS Bookings(
                        BookingId NVARCHAR(50) PRIMARY KEY, 
                        CustomerId INTEGER NOT NULL, 
                        CarId NVARCHAR(50) NOT NULL, 
                        StartDate DATE NOT NULL, 
                        EndDate DATE NOT NULL, 
                        TotalPrice DECIMAL NOT NULL, 
                        Status NVARCHAR(20) NOT NULL, 
                        CreatedDate DATE NOT NULL, 
                        FOREIGN KEY (CustomerId) REFERENCES Customers(Id), 
                        FOREIGN KEY (CarId) REFERENCES Cars(Id)
                    );

                    INSERT OR IGNORE INTO Bookings (BookingId, CustomerId, CarId, StartDate, EndDate, TotalPrice, Status, CreatedDate) 
                    VALUES
                    ('BK-0001', 1, 'Car_01', '2024-10-10', '2024-10-12', 31.0, 'Confirmed', DATE('now'));

                    CREATE TABLE IF NOT EXISTS BookingPayments(
                        PaymentId NVARCHAR(50) PRIMARY KEY, 
                        BookingId NVARCHAR(50) NOT NULL, 
                        Amount DECIMAL NOT NULL, 
                        PaymentMethod NVARCHAR(25) NOT NULL, 
                        Status NVARCHAR(20) NOT NULL, 
                        PaymentDate DATE NOT NULL, 
                        ReceiptNumber NVARCHAR(50) NOT NULL, 
                        FOREIGN KEY (BookingId) REFERENCES Bookings(BookingId)
                    );

                    INSERT OR IGNORE INTO BookingPayments (PaymentId, BookingId, Amount, PaymentMethod, Status, PaymentDate, ReceiptNumber) 
                    VALUES
                    ('PAY-0001', 'BK-0001', 31.0, 'Credit Card', 'Completed', DATE('now'), 'REC-0001');

                    CREATE TABLE IF NOT EXISTS RentalDetails(
                        RentalId NVARCHAR(50) PRIMARY KEY, 
                        BookingId NVARCHAR(50) NOT NULL, 
                        RentalDate DATE NOT NULL, 
                        FullPayment DECIMAL NOT NULL, 
                        Status NVARCHAR(20) NOT NULL, 
                        FOREIGN KEY (BookingId) REFERENCES Bookings(BookingId)
                    );

                    INSERT OR IGNORE INTO RentalDetails (RentalId, BookingId, RentalDate, FullPayment, Status) 
                    VALUES
                    ('REN-0001', 'BK-0001', DATE('now'), 31.0, 'Active');

                    CREATE TABLE IF NOT EXISTS ReturnDetails(
                        ReturnId NVARCHAR(50) PRIMARY KEY, 
                        RentalId NVARCHAR(50) NOT NULL, 
                        ReturnDate DATE NOT NULL, 
                        Condition NVARCHAR(20) NOT NULL, 
                        LateFees DECIMAL NOT NULL, 
                        FOREIGN KEY (RentalId) REFERENCES RentalDetails(RentalId)
                    );

                    INSERT OR IGNORE INTO ReturnDetails (ReturnId, RentalId, ReturnDate, Condition, LateFees) 
                    VALUES
                    ('RET-0001', 'REN-0001', DATE('now'), 'Good', 0.0);

                    CREATE TABLE IF NOT EXISTS Notifications(
                        NotificationId NVARCHAR(50) PRIMARY KEY, 
                        RentalId NVARCHAR(50) NOT NULL, 
                        ReturnId NVARCHAR(50) NOT NULL, 
                        Status NVARCHAR(20) NOT NULL, 
                        FOREIGN KEY (RentalId) REFERENCES RentalDetails(RentalId), 
                        FOREIGN KEY (ReturnId) REFERENCES ReturnDetails(ReturnId)
                    );

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
