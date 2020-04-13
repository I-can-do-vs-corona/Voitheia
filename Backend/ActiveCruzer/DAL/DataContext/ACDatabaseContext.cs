using ActiveCruzer.Models;
using ActiveCruzer.Models.DTO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

namespace ActiveCruzer.DAL.DataContext
{
    /// <summary>
    /// Context for database connection
    /// </summary>
    public class ACDatabaseContext : IdentityDbContext<User>
    {
        /// <summary>
        /// init db context
        /// </summary>
        /// <param name="options"></param>
        public ACDatabaseContext(DbContextOptions<ACDatabaseContext> options) : base(options)
        { }

        /// <summary>
        /// run on model creating
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Request>();
        }

        /// <summary>
        /// database model requests
        /// </summary>
        public Microsoft.EntityFrameworkCore.DbSet<ActiveCruzer.Models.Request> Request { get; set; }
    }
}
