namespace Library.Data.Entities
{
    public class Book
    {
        public  Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public long ISBN { get; set; }
        public int YearPublication { get; set; }
        public int Quantity { get; set; }
        public bool IsDeleted { get; set; }
    }
}
