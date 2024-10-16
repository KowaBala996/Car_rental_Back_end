using Car_rental.Entities;
using Car_rental.IRepository;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Data;

namespace Car_rental.Repository
{
    public class RentalDetailRepository : IRentalDetailRepository
    {
        private readonly string _connectionString;

        public RentalDetailRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddRentalDetail(RentalDetail rentalDetail)
        {
            using (IDbConnection db = new SqliteConnection(_connectionString))
            {
                string sql = @"INSERT INTO RentalDetails (RentalId, BookingId, RentalDate, FullPayment, Status) 
                               VALUES (@RentalId, @BookingId, @RentalDate, @FullPayment, @Status)";
                db.Execute(sql, rentalDetail);
            }
        }

        public RentalDetail GetRentalDetailById(string rentalId)
        {
            using (IDbConnection db = new SqliteConnection(_connectionString))
            {
                string sql = "SELECT * FROM RentalDetails WHERE RentalId = @RentalId";
                return db.QueryFirstOrDefault<RentalDetail>(sql, new { RentalId = rentalId });
            }
        }

        public IEnumerable<RentalDetail> GetAllRentalDetails()
        {
            using (IDbConnection db = new SqliteConnection(_connectionString))
            {
                string sql = "SELECT * FROM RentalDetails";
                return db.Query<RentalDetail>(sql);
            }
        }

        public void UpdateRentalDetail(RentalDetail rentalDetail)
        {
            using (IDbConnection db = new SqliteConnection(_connectionString))
            {
                string sql = @"UPDATE RentalDetails 
                               SET BookingId = @BookingId, 
                                   RentalDate = @RentalDate, 
                                   FullPayment = @FullPayment, 
                                   Status = @Status 
                               WHERE RentalId = @RentalId";
                db.Execute(sql, rentalDetail);
            }
        }

        public void DeleteRentalDetail(string rentalId)
        {
            using (IDbConnection db = new SqliteConnection(_connectionString))
            {
                string sql = "DELETE FROM RentalDetails WHERE RentalId = @RentalId";
                db.Execute(sql, new { RentalId = rentalId });
            }
        }
    }
}
