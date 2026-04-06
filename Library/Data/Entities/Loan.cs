using Library.Enums;

namespace Library.Data.Entities
{
   
    public class Loan
    {
        public Loan()
        {
            Status = 0;
        }
        public Guid Id { get; set; }
        public Guid IdBook { get; set; }
        public Guid IdUser { get; set; }
        public Guid Idlibrarian { get; set; }
        public LoanRole Status { get; set; }
        public DateTime DateCheckOut{ get; set; }
        public DateTime DateReturn { get; set; }
    }
}
