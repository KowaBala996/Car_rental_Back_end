using Car_rental.Entities;
using Car_rental.IRepository;
using Microsoft.Data.Sqlite;

namespace Car_rental.Repository
{
    public class ReturnDetailRepository : IReturnDetailRepository
    {
        private readonly string _connectionString;

        public ReturnDetailRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddReturnDetail(ReturnDetail returnDetail)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                var query = "INSERT INTO ReturnDetails (ReturnId, RentalId, ReturnDate, Condition, LateFees) " +
                            "VALUES (@ReturnId, @RentalId, @ReturnDate, @Condition, @LateFees)";

                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReturnId", returnDetail.ReturnId);
                    command.Parameters.AddWithValue("@RentalId", returnDetail.RentalId);
                    command.Parameters.AddWithValue("@ReturnDate", returnDetail.ReturnDate);
                    command.Parameters.AddWithValue("@Condition", returnDetail.Condition);
                    command.Parameters.AddWithValue("@LateFees", returnDetail.LateFees);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public ReturnDetail GetReturnDetailById(string returnId)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                var query = "SELECT * FROM ReturnDetails WHERE ReturnId = @ReturnId";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReturnId", returnId);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ReturnDetail
                            {
                                ReturnId = reader["ReturnId"].ToString(),
                                RentalId = reader["RentalId"].ToString(),
                                ReturnDate = (DateTime)reader["ReturnDate"],
                                Condition = reader["Condition"].ToString(),
                                LateFees = (decimal)reader["LateFees"]
                            };
                        }
                    }
                }
            }
            return null;
        }

        public List<ReturnDetail> GetAllReturnDetails()
        {
            var returnDetails = new List<ReturnDetail>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                var query = "SELECT * FROM ReturnDetails";
                using (var command = new SqliteCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime returnDate;
                            DateTime.TryParse(reader["ReturnDate"].ToString(), out returnDate); // Try to parse, defaults to DateTime.MinValue on failure

                            long lateFeesLong = (long)reader["LateFees"]; // Read as long
                            decimal lateFees = Convert.ToDecimal(lateFeesLong); // Convert to decimal

                            returnDetails.Add(new ReturnDetail
                            {
                                ReturnId = reader["ReturnId"].ToString(),
                                RentalId = reader["RentalId"].ToString(),
                                ReturnDate = returnDate, // Set parsed date or DateTime.MinValue
                                Condition = reader["Condition"].ToString(),
                                LateFees = lateFees // Set the converted value
                            });
                        }
                    }
                }
            }

            return returnDetails;
        }



        public void UpdateReturnDetail(ReturnDetail returnDetail)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                var query = "UPDATE ReturnDetails SET RentalId = @RentalId, ReturnDate = @ReturnDate, " +
                            "Condition = @Condition, LateFees = @LateFees WHERE ReturnId = @ReturnId";

                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReturnId", returnDetail.ReturnId);
                    command.Parameters.AddWithValue("@RentalId", returnDetail.RentalId);
                    command.Parameters.AddWithValue("@ReturnDate", returnDetail.ReturnDate);
                    command.Parameters.AddWithValue("@Condition", returnDetail.Condition);
                    command.Parameters.AddWithValue("@LateFees", returnDetail.LateFees);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteReturnDetail(string returnId)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                var query = "DELETE FROM ReturnDetails WHERE ReturnId = @ReturnId";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReturnId", returnId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
