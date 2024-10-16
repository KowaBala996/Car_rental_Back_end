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
                    INSERT INTO Customers (Name, Phone, Email, Nic, Address, PostalCode, 
                    DrivingLicenseNumber, FrontImagePath, ProofIdNumber, ProfileStatus) 
                    VALUES (@name, @phone, @email, @nic, @address, @postalCode, 
                    @drivingLicenseNumber, @frontImagePath, @proofIdNumber, @profileStatus)";

                command.Parameters.AddWithValue("@name", requestCustomerDto.name);
                command.Parameters.AddWithValue("@phone", requestCustomerDto.phone);
                command.Parameters.AddWithValue("@email", requestCustomerDto.email);
                command.Parameters.AddWithValue("@nic", requestCustomerDto.nic);
                command.Parameters.AddWithValue("@address", requestCustomerDto.address);
                command.Parameters.AddWithValue("@postalCode", requestCustomerDto.postalCode);
                command.Parameters.AddWithValue("@drivingLicenseNumber", requestCustomerDto.drivingLicenseNumber);
                command.Parameters.AddWithValue("@frontImagePath", requestCustomerDto.FrontImagePath);
                command.Parameters.AddWithValue("@proofIdNumber", requestCustomerDto.proofIdNumber);
                command.Parameters.AddWithValue("@profileStatus", requestCustomerDto.profileStatus);

                command.ExecuteNonQuery();
            }
        }

        public CustomerDTO GetCustomerById(int nic)
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
                            profileStatus = reader.GetString(11)
                        };
                    }
                    return null; // or throw an exception if not found
                }
            }
        }

        public void UpdateCustomer(int id, RequestCustomerDTO requestCustomerDto)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    UPDATE Customers SET Name = @name, Phone = @phone, Email = @email, 
                    Nic = @nic, Address = @address, PostalCode = @postalCode, 
                    DrivingLicenseNumber = @drivingLicenseNumber, 
                    FrontImagePath = @frontImagePath,  
                    ProofIdNumber = @proofIdNumber, ProfileStatus = @profileStatus 
                    WHERE Id = @id";

                command.Parameters.AddWithValue("@id", requestCustomerDto.Id);
                command.Parameters.AddWithValue("@name", requestCustomerDto.Name);
                command.Parameters.AddWithValue("@phone", requestCustomerDto.Phone);
                command.Parameters.AddWithValue("@email", requestCustomerDto.Email);
                command.Parameters.AddWithValue("@nic", requestCustomerDto.Nic);
                command.Parameters.AddWithValue("@address", requestCustomerDto.Address);
                command.Parameters.AddWithValue("@postalCode", requestCustomerDto.PostalCode);
                command.Parameters.AddWithValue("@drivingLicenseNumber", requestCustomerDto.DrivingLicenseNumber);
                command.Parameters.AddWithValue("@frontImagePath", requestCustomerDto.FrontImagePath);
                command.Parameters.AddWithValue("@proofIdNumber", requestCustomerDto.ProofIdNumber);
                command.Parameters.AddWithValue("@profileStatus", requestCustomerDto.ProfileStatus.ToString());

                command.ExecuteNonQuery();
            }
        }

        public void DeleteCustomer(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Customers WHERE Id = @id";
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }

      
    }
}
