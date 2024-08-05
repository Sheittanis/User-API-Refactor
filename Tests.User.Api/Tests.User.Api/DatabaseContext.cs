using Microsoft.EntityFrameworkCore;

namespace Tests.User.Api
{
    public class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Must stay as in memory database
            optionsBuilder.UseInMemoryDatabase("Tests.User.Api");
        }

        public DbSet<Models.UserModel> Users { get; set; }
    }
}
