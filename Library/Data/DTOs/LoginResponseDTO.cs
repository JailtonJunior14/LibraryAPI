using Library.Enums;

namespace Library.Data.DTOs
{
    public class LoginResponseDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
    }
}
