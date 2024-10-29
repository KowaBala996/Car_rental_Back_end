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
            INSERT INTO Customers (CustomerId, name, phone, email, nic, password, carId) 
            VALUES (@id, @name, @phone, @email, @nic, @password, @carId)";

                command.Parameters.AddWithValue("@id", requestCustomerDto.Id);
                command.Parameters.AddWithValue("@name", requestCustomerDto.name);
                command.Parameters.AddWithValue("@phone", requestCustomerDto.phone);
                command.Parameters.AddWithValue("@email", requestCustomerDto.email);
                command.Parameters.AddWithValue("@nic", requestCustomerDto.nic);
                command.Parameters.AddWithValue("@password", requestCustomerDto.password);
                command.Parameters.AddWithValue("@carId", requestCustomerDto.carId ?? (object)DBNull.Value);
                
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
                command.CommandText = "SELECT * FROM Customers WHERE nic = @nic";
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
                            Password = reader.GetString(5),
                            Address = reader.IsDBNull(6) ? null : reader.GetString(6),
                            PostalCode = reader.IsDBNull(7) ? null : reader.GetString(7),
                            DrivingLicenseNumber = reader.IsDBNull(8) ? null : reader.GetString(8),
                            FrontImagePath = reader.IsDBNull(9) ? null : reader.GetString(9),
                            ProofIdNumber = reader.IsDBNull(10) ? null : reader.GetString(10),
                            ProfileStatus = reader.IsDBNull(11) ? null : reader.GetString(11),
                            CarId = reader.IsDBNull(12) ? null : reader.GetString(12),
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
                            Password =reader.GetString(5),
                            CarId = reader.IsDBNull(6) ? null : reader.GetString(6),
                            Address = reader.IsDBNull(7) ? null : reader.GetString(7),
                            PostalCode = reader.IsDBNull(8) ? null : reader.GetString(8),
                            DrivingLicenseNumber = reader.IsDBNull(9) ? null : reader.GetString(9),
                            FrontImagePath = reader.IsDBNull(10) ? null : reader.GetString(10),
                            ProofIdNumber = reader.IsDBNull(11) ? null : reader.GetString(11),
                            ProfileStatus = reader.IsDBNull(12) ? null : reader.GetString(12),
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
            WHERE CustomerId = @id";

                command.Parameters.AddWithValue("@address", requestCustomerDto.Address ?? string.Empty);
                command.Parameters.AddWithValue("@postalCode", requestCustomerDto.PostalCode ?? string.Empty);
                command.Parameters.AddWithValue("@drivingLicenseNumber", requestCustomerDto.DrivingLicenseNumber ?? string.Empty);
                command.Parameters.AddWithValue("@frontImagePath", requestCustomerDto.FrontImagePath ?? string.Empty);
                command.Parameters.AddWithValue("@proofIdNumber", requestCustomerDto.ProofIdNumber ?? string.Empty);
                command.Parameters.AddWithValue("@profileStatus", requestCustomerDto.ProfileStatus ?? string.Empty);
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
