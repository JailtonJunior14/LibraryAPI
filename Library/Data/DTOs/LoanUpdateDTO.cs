using Library.Enums;

namespace Library.Data.DTOs
{
    public class LoanUpdateDTO
    {
        public Guid Id { get; set; }
        public Guid? IdBook { get; set; }
        public Guid? IdUser { get; set; }
        public LoanRole? Status { get; set; }
        public DateTime? DateCheckOut { get; set; }
        public DateTime? DateReturn { get; set; }
    }
}
