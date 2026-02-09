using Library.Enums;

namespace Library.Data.DTOs
{
    public class UserDTO
    {
        public UserRole Role { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
