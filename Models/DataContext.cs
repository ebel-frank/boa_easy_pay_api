using Microsoft.EntityFrameworkCore;

namespace BoaEasyPay.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Bank> Banks { get; set; }

        public DbSet<Account> Accounts { get; set; }
    }
}
