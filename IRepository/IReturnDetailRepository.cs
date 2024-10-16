using Car_rental.Entities;

namespace Car_rental.IRepository
{
    public interface IReturnDetailRepository
    {
        void AddReturnDetail(ReturnDetail returnDetail);
        ReturnDetail GetReturnDetailById(string returnId);
        List<ReturnDetail> GetAllReturnDetails();
        void UpdateReturnDetail(ReturnDetail returnDetail);
        void DeleteReturnDetail(string returnId);
    }
}
