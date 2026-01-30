using Microsoft.AspNetCore.Identity;

namespace Library.Data.Entities
{
    public class User : IdentityUser
    {
        public Guid Id { get; set; }
        public bool Admin { get; set; }
    }
}
