using Car_rental.IRepository;
using Car_rental.Models.RequestModel;
using Car_rental.Models.ResposeModel;
using Microsoft.Data.Sqlite;

namespace Car_rental.Repository
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly string _connectionString;

        public ManagerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Get all cars
        public ICollection<CarDTO> GetAllCars()
        {
            var carList = new List<CarDTO>();
            try
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM Cars";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            carList.Add(new CarDTO()
                            {
                                CarId = reader.GetString(0),
                                Brand = reader.GetString(1),
                                BodyType = reader.GetString(2),
                                Model = reader.GetString(3),
                                Transmission = reader.GetString(4),
                                FuelType = reader.GetString(5),
                                NumberOfSeats = reader.GetInt32(6),
                                PricePerHour = reader.GetDecimal(7),
                                ImagePath = reader.IsDBNull(8) ? null : reader.GetString(8),
                                AvailableFrom = reader.GetDateTime(9),
                                AvailableTo = reader.GetDateTime(10)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving cars", ex);
            }
            return carList;
        }

        // Add a new car
        public CarDTO AddCar(CarDTO carDto)
        {

            try
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                    INSERT INTO Cars (CarId, Brand, BodyType, Model, Transmission, FuelType, NumberOfSeats, PricePerHour, ImagePath, AvailableFrom, AvailableTo)
                    VALUES (@id, @brand, @bodyType, @model, @transmission, @fuelType, @numberOfSeats, @pricePerHour, @imagePath, @availableFrom, @availableTo)";

                    command.Parameters.AddWithValue("@id", carDto.CarId);
                    command.Parameters.AddWithValue("@brand", carDto.Brand);
                    command.Parameters.AddWithValue("@bodyType", carDto.BodyType);
                    command.Parameters.AddWithValue("@model", carDto.Model);
                    command.Parameters.AddWithValue("@transmission", carDto.Transmission);
                    command.Parameters.AddWithValue("@fuelType", carDto.FuelType);
                    command.Parameters.AddWithValue("@numberOfSeats", carDto.NumberOfSeats);
                    command.Parameters.AddWithValue("@pricePerHour", carDto.PricePerHour);
                    command.Parameters.AddWithValue("@availableFrom", carDto.AvailableFrom);
                    command.Parameters.AddWithValue("@availableTo", carDto.AvailableTo);
                    command.Parameters.AddWithValue("@imagePath", carDto.ImagePath == null ? "" : carDto.ImagePath);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding car", ex);
            }
            return carDto;
        }

        // Update car details
        public void UpdateCar(CarDTO car)
        {
            try
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                    UPDATE Cars
                    SET Brand = @brand, BodyType = @bodyType, Model = @model, Transmission = @transmission, FuelType = @fuelType, 
                        NumberOfSeats = @numberOfSeats, PricePerHour = @pricePerHour, ImagePath = @imagePath, AvailableFrom = @availableFrom, AvailableTo = @availableTo
                    WHERE Id = @id";

                    command.Parameters.AddWithValue("@id", car.Id);
                    command.Parameters.AddWithValue("@brand", car.Brand);
                    command.Parameters.AddWithValue("@bodyType", car.BodyType);
                    command.Parameters.AddWithValue("@model", car.Model);
                    command.Parameters.AddWithValue("@transmission", car.Transmission);
                    command.Parameters.AddWithValue("@fuelType", car.FuelType);
                    command.Parameters.AddWithValue("@numberOfSeats", car.NumberOfSeats);
                    command.Parameters.AddWithValue("@pricePerHour", car.PricePerHour);
                    command.Parameters.AddWithValue("@imagePath", car.ImagePath ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@availableFrom", car.AvailableFrom);
                    command.Parameters.AddWithValue("@availableTo", car.AvailableTo);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating car", ex);
            }
        }

        public void DeleteCar(string carId)
        {
            try
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
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
                throw new Exception("Error deleting car with ID: " + carId, ex);
            }
        }


    }
}
