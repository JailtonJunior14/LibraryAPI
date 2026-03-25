using Microsoft.AspNetCore.Identity;
using Library.Enums;

namespace Library.Data.Entities
{
    public class User : IdentityUser
    {
        //public User()
        //{
        //    IsDeleted = false;
        //}
        public Guid Id { get; set; }
        public UserRole Role { get; set; }
        public bool IsDeleted { get; set; }
    }
}
