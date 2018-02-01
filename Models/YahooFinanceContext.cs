using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace YahooFinance.Models
{
    public class YahooFinanceContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public YahooFinanceContext(DbContextOptions<YahooFinanceContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Stock> Stocks { get; set; }
    }
}