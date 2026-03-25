using Library.Enums;

namespace Library.Data.DTOs
{
    public class LoanInsertDTO
    {
        public Guid Book { get; set; }
        public Guid IdUser { get; set; }
        public Guid Idlibrarian { get; set; }
        public LoanRole Status { get; set; }
    }
}
