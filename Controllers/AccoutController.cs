using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using YahooFinance.Models;
using System.Linq;


namespace YahooFinance.Controllers
{
    public class AccountController : Controller
    {
        private YahooFinanceContext _context;
        public AccountController(YahooFinanceContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("register")]

        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(Register regUser, Portfolio portfolio)
        {
            User exists = _context.Users.SingleOrDefault(user => user.Email == regUser.Email);
            if (ModelState.IsValid)
            {
                if (exists != null)
                {
                    TempData["UserError"] = "An account with this email already exists!";
                    return View();
                }
                else
                {
                    PasswordHasher<Register> hasher = new PasswordHasher<Register>();
                    string HashedPW = hasher.HashPassword(regUser, regUser.Password);
                    User newUser = new User
                    {
                        FirstName = regUser.FirstName,
                        LastName = regUser.LastName,
                        Email = regUser.Email,
                        Password = HashedPW,
                    };

                    _context.Add(newUser);

                    

                    _context.SaveChanges();

                    User LoggedIn = _context.Users.SingleOrDefault(user => user.Email == regUser.Email);
                    HttpContext.Session.SetInt32("UserId", LoggedIn.UserId);
                    HttpContext.Session.SetString("userName", LoggedIn.FirstName);


                    int? id = HttpContext.Session.GetInt32("userId");
                    User currentUser = _context.Users.SingleOrDefault(u => u.UserId == (int)id);

                     Portfolio thisportfolio = _context.Portfolios.SingleOrDefault(w => w.PortfolioId == (int)id);

                    Portfolio newPortfolio = new Portfolio
                    {
                        UserId = portfolio.UserId,
                       
                    };
                     _context.Add(newPortfolio);
                    _context.SaveChanges();

                    



                    //If the creation failed, add the errors to the View Model
                    return Redirect("/");
                }

            }
            return View();
        }


        [HttpGet]
        [Route("signin")]

        public IActionResult LoginPage()
        {

            return View();
        }



        [HttpPost]
        [Route("signin")]
        public IActionResult LoginPage(Login model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                User LoggingIn = _context.Users.SingleOrDefault(user => user.Email == model.Email);
                if (LoggingIn == null)
                {
                    ViewBag.NoUser = "It looks like you need to register!";
                    return View();
                }
                else
                {
                    PasswordHasher<Login> hasher = new PasswordHasher<Login>();
                    if (hasher.VerifyHashedPassword(model, LoggingIn.Password, model.Password) == 0)
                    {
                        ViewBag.NoUser = "Invalid Email or Password";
                        return View();
                    }
                    else
                    {
                        //Grabbing User from DB and putting into session
                        User LoggedIn = _context.Users.SingleOrDefault(user => user.Email == model.Email);
                        HttpContext.Session.SetInt32("UserId", LoggedIn.UserId);
                        HttpContext.Session.SetString("UserName", LoggedIn.FirstName);
                        ViewBag.NoUser = "";
                        return Redirect("/");
                    }
                }
            }
        }













    }
}