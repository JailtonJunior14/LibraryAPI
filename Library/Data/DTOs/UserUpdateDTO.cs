using Library.Enums;

namespace Library.Data.DTOs
{
    public class UserUpdateDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public bool IsDeleted { get; set; }
    }
}
