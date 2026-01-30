using Library.Data.Entities;

namespace Library.Services.Helpers.PasswordHelper
{
    public interface IPasswordHelper
    {
        string HashPassWord(User user, string password);
        bool VerifyPassword(User user, string hashedpassword, string password);
    }
}
