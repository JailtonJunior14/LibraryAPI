using Library.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Library.Services.Helpers.PasswordHelper
{
    public class PasswordHelper : IPasswordHelper
    {
        private readonly IPasswordHasher<User> _passwordHasher;

        public PasswordHelper(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }


        public string HashPassWord(User user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }

        public bool VerifyPassword(User user, string hashedpassword, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, hashedpassword, password);  

            return result  == PasswordVerificationResult.Success;
        }
    }
}
