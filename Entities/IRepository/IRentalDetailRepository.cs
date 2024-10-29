using Car_rental.Entities;

namespace Car_rental.IRepository
{
    public interface IRentalDetailRepository
    {
        void AddRentalDetail(RentalDetail rentalDetail);
        RentalDetail GetRentalDetailById(string rentalId);
        IEnumerable<RentalDetail> GetAllRentalDetails();
        void UpdateRentalDetail(RentalDetail rentalDetail);
        void DeleteRentalDetail(string rentalId);
    }
}
