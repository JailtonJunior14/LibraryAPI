namespace Library.Data.DTOs
{
    public class BookInsertDTO
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public long ISBN { get; set; }
        public int YearPublication { get; set; }
        public int Quantity { get; set; }
    }
}
