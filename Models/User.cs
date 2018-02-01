using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Identity;

namespace YahooFinance.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // public string Email{get;set;}
        // public string Password{get;set;}
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Portfolio> Portfolios { get; set; }

        public User()
        {   
            Portfolios = new List<Portfolio>();    
        }
    }
}