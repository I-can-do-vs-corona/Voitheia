using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ActiveCruzer.Models
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
