using Library.Data.Entities;
using Library.Data.Persistence;
using Library.Enums;
using Microsoft.AspNetCore.Identity;

namespace Library.Data.Seeds
{
    public class InitializerDB
    {
        public static async Task Seed(IServiceProvider provider)
        {
            var dbContext = provider.GetRequiredService<LibraryDbContext>();

            await UserSeed.Seed(dbContext);
            await BookSeed.Seed(dbContext);
            await LoanSeed.Seed(dbContext);

        }
    }
}
