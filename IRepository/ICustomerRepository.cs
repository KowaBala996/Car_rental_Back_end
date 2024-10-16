using Car_rental.Entities;
using Car_rental.Models.RequestModel;
using Car_rental.Models.ResposeModel;

namespace Car_rental.IRepository
{
    public interface ICustomerRepository
    {
        void AddCustomer(Customer requestCustomerDto);
        CustomerDTO GetCustomerById(int nic);
        void UpdateCustomer(int id, RequestCustomerDTO requestCustomerDto);
        void DeleteCustomer(int id);
        
    }
}
