namespace Car_rental.Entities
{
    public class ReturnDetail
    {
       
            public string ReturnId { get; set; }
            public string RentalId { get; set; }
            public DateTime ReturnDate { get; set; }
            public string Condition { get; set; } // E.g., Good, Damaged
            public decimal LateFees { get; set; }

            public ReturnDetail()
            {
                ReturnDate = DateTime.Now;
            }
        

    }
}
