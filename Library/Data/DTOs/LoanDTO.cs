using Library.Data.Entities;
using Library.Enums;

namespace Library.Data.DTOs
{
    public class LoanDTO
    {
        public BookDTO Book { get; set; }
        public UserDTO User { get; set; }
        public UserDTO Librarian { get; set; }
        public Guid IdUser { get; set; }
        public Guid Idlibrarian { get; set; }
        public LoanRole Status { get; set; }
        public DateTime DateCheckOut { get; set; }
        public DateTime DueDate {  get; set; }
        public DateTime? DateReturn { get; set; }

    }
}
