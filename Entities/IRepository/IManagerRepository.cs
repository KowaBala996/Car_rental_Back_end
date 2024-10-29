using Car_rental.Models.RequestModel;
using Car_rental.Models.ResposeModel;

namespace Car_rental.IRepository
{
    public interface IManagerRepository
    {
        // Method to get all cars
        ICollection<CarDTO> GetAllCars();

        // Method to add a new car
        CarDTO AddCar(CarDTO car);

        // Method to update an existing car
        void UpdateCar(CarDTO car);

        // Method to delete a car by ID
        void DeleteCar(string carId);
    }
}
