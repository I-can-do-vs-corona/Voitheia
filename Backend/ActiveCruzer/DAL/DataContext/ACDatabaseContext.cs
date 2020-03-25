using ActiveCruzer.Models;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace ActiveCruzer.DAL.DataContext
{
    /// <summary>
    /// Context for database connection
    /// </summary>
    public class ACDatabaseContext : DbContext
    {
        /// <summary>
        /// connection string for mysql connection
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// assign connectionstring
        /// </summary>
        /// <param name="connectionString"></param>
        public ACDatabaseContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        /// <summary>
        /// return mysql connection
        /// </summary>
        /// <returns></returns>
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        /// <summary>
        /// database model requests
        /// </summary>
        public DbSet<ActiveCruzer.Models.Request> Request { get; set; }
        /// <summary>
        /// database model users
        /// </summary>
        public DbSet<User> User { get; set; }
    }
}
