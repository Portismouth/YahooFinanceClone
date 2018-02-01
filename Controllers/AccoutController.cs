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
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(YahooFinanceContext context, UserManager<User> userManager,
                SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet]
        [Route("/RegisterUser")]

        public IActionResult RegisterPage()
        {

            return View("Register");
        }

        

        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> Register(Register regUser)
        {
            User exists = _context.Users.SingleOrDefault(user => user.Email == regUser.Email);
            if (ModelState.IsValid)
            {
                if (exists != null)
                {
                    ModelState.AddModelError("Email", "An account with this email already exists!");
                    return RedirectToAction("RegisterPage");
                }
                else
                {


                    User newUser = new User
                    {
                        FirstName = regUser.FirstName,
                        LastName = regUser.LastName,
                        Email  = regUser.Email,
                        UserName = regUser.Email
                        // PasswordHash = regUser.Password
                    };
                    IdentityResult result = await _userManager.CreateAsync(newUser, regUser.Password);
                    if(result.Succeeded)
        {
            HttpContext.Session.SetString("UserId", newUser.Id);
            // HttpContext.Session.SetInt32("UserId", exists.Id);
            //Sign In the newly created User
            //We're using the SignInManager, not the UserManager!
            // await _signInManager.SignInAsync(newUser, isPersistent: false);
        }
        //If the creation failed, add the errors to the View Model
        foreach( var error in result.Errors )
        {
        }
    }
                
            }
            return RedirectToAction("RegisterPage");

        }


        [HttpGet]
        [Route("/LoginPage")]

        public IActionResult LoginPage()
        {

            return View();
        }



    [HttpPost]
    [Route("signin")]
    public async Task<IActionResult> Login(Login model)
    {
        if (ModelState.IsValid)
        {
            User LoggingIn = _context.users.Where(u => u.Email == model.Email).SingleOrDefault();
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (result.Succeeded)
            {
                User LoggedIn = _context.users.SingleOrDefault(u => u.Email == model.Email);
                HttpContext.Session.SetString("UserId", LoggedIn.Id);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                TempData["PWError"] = "Invalid login attempt.";
                return View(model);
            }
        }

        // If we got this far, something failed, redisplay form
        return View(model);
    }


        










    }
}