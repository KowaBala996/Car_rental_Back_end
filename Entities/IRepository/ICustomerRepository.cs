using Car_rental.Entities;
using Car_rental.Models.RequestModel;
using Car_rental.Models.ResposeModel;

namespace Car_rental.IRepository
{
    public interface ICustomerRepository
    {
        void AddCustomer(Customer requestCustomerDto);
        CustomerDTO GetCustomerById(string nic);
        void UpdateCustomer(CustomerUpdateDTO updateDetails);
        List<CustomerDTO> GetAllCustomers();
        void DeleteCarAndCustomers(string carId);





    }
}
