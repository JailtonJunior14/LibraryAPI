using Library.Enums;

namespace Library.Data.DTOs
{
    public class ReturnBooksDTO
    {

        public Guid IdLoan { get; set; }
        public Guid IdUser { get; set; }
        public Guid Idlibrarian { get; set; }
        public Guid IdBook { get; set; }
        public DateTime DateReturn { get; set; }
        public LoanRole? Status { get; set; }
    }
}
