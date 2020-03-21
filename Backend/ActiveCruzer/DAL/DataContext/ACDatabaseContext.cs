using Microsoft.EntityFrameworkCore;

namespace ActiveCruzer.DAL.DataContext
{
    /// <summary>
    /// Context for database connection
    /// </summary>
    public class ACDatabaseContext : DbContext
    {
        /// <summary>
        /// basic constructor for database context
        /// </summary>
        /// <param name="options"></param>
        public ACDatabaseContext(DbContextOptions<ACDatabaseContext> options) : base(options)
        {
        }

        /// <summary>
        /// database model requests
        /// </summary>
        public DbSet<ActiveCruzer.Models.Request> Request { get; set; }
        /// <summary>
        /// database model users
        /// </summary>
        public DbSet<ActiveCruzer.Models.User> User { get; set; }
    }
}
