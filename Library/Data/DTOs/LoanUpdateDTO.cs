using Library.Data.Entities;
using Library.Enums;

namespace Library.Data.DTOs
{
    public class LoanUpdateDTO
    {
        public Guid Id { get; set; }
        public Guid? IdBook { get; set; }
        public Book Book { get; set; }
        public Guid? IdUser { get; set; }
        public User User { get; set; }
        
        public DateTime? DateCheckOut { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? DateReturn { get; set; }
    }
}
