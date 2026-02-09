namespace Library.Data.Entities
{
    public class Loan
    {
        public Guid Id { get; set; }
        public Guid Book { get; set; }
        public Guid IdUser { get; set; }
        public Guid Idlibrarian { get; set; }
    }
}
