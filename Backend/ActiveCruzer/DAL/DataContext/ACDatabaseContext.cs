using Microsoft.EntityFrameworkCore;

namespace ActiveCruzer.DAL.DataContext
{
    public class ACDatabaseContext : DbContext
    {
        public ACDatabaseContext(DbContextOptions<ACDatabaseContext> options) : base(options)
        {
        }

        // modelling
        public DbSet<ActiveCruzer.Models.Request> Request { get; set; }
        public DbSet<ActiveCruzer.Models.User> User { get; set; }
    }
}
