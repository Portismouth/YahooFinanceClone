using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace YahooFinance.Models
{
    public class Stock : BaseEntity
    {
        public int StockId { get; set; }
        // public string UserId { get; set; }
        // public User User { get; set; }
        public int PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; }
    }
}