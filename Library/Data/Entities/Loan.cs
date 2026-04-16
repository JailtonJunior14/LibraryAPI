using Library.Enums;

namespace Library.Data.Entities
{
   
    public class Loan
    {
        public Loan()
        {
            DateReturn = null;
        }
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public Book Book { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid LibrarianId { get; set; }
        public User Librarian { get; set; }
        public DateTime DateCheckOut{ get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? DateReturn { get; set; }
    }
}
