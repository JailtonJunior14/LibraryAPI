namespace Library.Data.DTOs
{
    public class UserInsertDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}
