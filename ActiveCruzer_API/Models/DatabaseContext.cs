using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActiveCruzer.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<ActiveCruzer.Models.Request> Request { get; set; }
        public DbSet<ActiveCruzer.Models.User> User { get; set; }
    }
    
}
