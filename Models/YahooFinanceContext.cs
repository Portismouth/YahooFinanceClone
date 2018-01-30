using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace YahooFinance.Models
{
    public class YahooFinanceContext : IdentityDbContext<User>
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public YahooFinanceContext(DbContextOptions<YahooFinanceContext> options) : base(options) { }
        public DbSet<User> users { get; set; }
    }
}