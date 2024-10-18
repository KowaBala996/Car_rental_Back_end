using Car_rental.Entities;
using Car_rental.IRepository;
using Car_rental.Models.RequestModel;
using Car_rental.Models.ResposeModel;
using Microsoft.Data.Sqlite;

namespace Car_rental.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddCustomer(Customer requestCustomerDto)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
            INSERT INTO Customers (Id,  Name, Phone, Email, Nic, Password) 
            VALUES (@id, @name, @phone, @email, @nic, @password )";

                command.Parameters.AddWithValue("@id", requestCustomerDto.CarId);

                command.Parameters.AddWithValue("@name", requestCustomerDto.name);
                command.Parameters.AddWithValue("@phone", requestCustomerDto.phone);
                command.Parameters.AddWithValue("@email", requestCustomerDto.email);
                command.Parameters.AddWithValue("@nic", requestCustomerDto.nic);
                command.Parameters.AddWithValue("@password", requestCustomerDto.password);

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


        public CustomerDTO? GetCustomerById(string nic)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Customers WHERE Nic = @nic";
                command.Parameters.AddWithValue("@nic", nic);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new CustomerDTO
                        {
                            Id = reader.GetString(0),
                            Name = reader.GetString(1),
                            Phone = reader.GetString(2),
                            Email = reader.GetString(3),
                            Nic = reader.GetString(4),
                            Address = reader.GetString(5),
                            PostalCode = reader.GetString(6),
                            DrivingLicenseNumber = reader.GetString(7),
                            FrontImagePath = reader.GetString(8),
                            ProofIdNumber = reader.GetString(10),
                            ProfileStatus = reader.GetString(11)
                        };
                    }
                    return null; // or throw an exception if not found
                }
            }
        }
       public List<CustomerDTO> GetAllCustomers()
        {
            var customers = new List<CustomerDTO>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Customers";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var customer = new CustomerDTO
                        {
                            Id = reader.GetString(0),
                            Name = reader.GetString(1),
                            Phone = reader.GetString(2),
                            Email = reader.GetString(3),
                            Nic = reader.GetString(4),
                            Address = reader.IsDBNull(5) ? null : reader.GetString(5),
                            PostalCode = reader.IsDBNull(6) ? null : reader.GetString(6),
                            DrivingLicenseNumber = reader.IsDBNull(7) ? null : reader.GetString(7),
                            FrontImagePath = reader.IsDBNull(8) ? null : reader.GetString(8),
                            ProofIdNumber = reader.IsDBNull(9) ? null : reader.GetString(9),
                            ProfileStatus = reader.IsDBNull(10) ? null : reader.GetString(10) // Added null check for ProfileStatus
                        };

                        customers.Add(customer);
                    }
                }
            }

            return customers;
        }



        public void UpdateCustomer(CustomerUpdateDTO requestCustomerDto)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    UPDATE Customers SET Address = @address, PostalCode = @postalCode, 
                    DrivingLicenseNumber = @drivingLicenseNumber, 
                    FrontImagePath = @frontImagePath,  
                    ProofIdNumber = @proofIdNumber, ProfileStatus = @profileStatus 
                    WHERE Id = @id";
                command.Parameters.AddWithValue("@address", requestCustomerDto.Address);
                command.Parameters.AddWithValue("@postalCode", requestCustomerDto.PostalCode);
                command.Parameters.AddWithValue("@drivingLicenseNumber", requestCustomerDto.DrivingLicenseNumber);
                command.Parameters.AddWithValue("@frontImagePath", requestCustomerDto.FrontImagePath);
                command.Parameters.AddWithValue("@proofIdNumber", requestCustomerDto.ProofIdNumber);
                command.Parameters.AddWithValue("@profileStatus", requestCustomerDto.ProfileStatus);
                command.Parameters.AddWithValue("@id", requestCustomerDto.Id);

                command.ExecuteNonQuery();
            }
        }

        public void DeleteCarAndCustomers(string carId)
        {
            try
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();

                    // First, delete customers referencing the car
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "DELETE FROM Customers WHERE CarId = @carId";
                        command.Parameters.AddWithValue("@carId", carId);
                        command.ExecuteNonQuery();
                    }

                    // Now delete the car
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "DELETE FROM Cars WHERE CarId = @id";
                        command.Parameters.AddWithValue("@id", carId);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting car and associated customers with Car ID: " + carId, ex);
            }
        }


    }
}
