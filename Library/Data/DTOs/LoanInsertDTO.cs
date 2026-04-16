using Library.Enums;

namespace Library.Data.DTOs
{
    public class LoanInsertDTO
    {

        public Guid IdUser { get; set; }
        public Guid Idlibrarian { get; set; }
        public Guid IdBook { get; set; }
        public DateTime DateCheckOut { get; set; }
        public DateTime DueDate { get; set; }
    }
}
