using AccountsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountsAPI.Repositories
{
    public class ApiContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "AccountsDb");
        }

        public DbSet<Account> Accounts { get; set; }
    }
}
