using ActiveCruzer.Models;
using ActiveCruzer.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

namespace ActiveCruzer.DAL.DataContext
{
    /// <summary>
    /// Context for database connection
    /// </summary>
    public class ACDatabaseContext : DbContext
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
            modelBuilder.Entity<RegisterUserDTO>();
        }

        /// <summary>
        /// database model requests
        /// </summary>
        public Microsoft.EntityFrameworkCore.DbSet<ActiveCruzer.Models.Request> Request { get; set; }

        /// <summary>
        /// database model for registering a user
        /// </summary>
        public Microsoft.EntityFrameworkCore.DbSet<RegisterUserDTO> RegisterUserDTOs { get; set; }
    }
}
