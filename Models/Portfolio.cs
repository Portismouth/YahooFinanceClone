using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace YahooFinance.Models
{
    public class Portfolio : BaseEntity
    {
        public int PortfolioId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string PortName { get; set; }
        public List<Stock> Stocks { get; set; }

        public Portfolio()
        {
            Stocks = new List<Stock>();
        }
    }
}