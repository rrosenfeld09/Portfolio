using System; 
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ManifestSoftware.Models;

namespace ManifestSoftware.Controllers
{
    public class UserController : BaseEntity
    {

        public MyContext _context;

        public UserController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            if(IsUserInSession())
            {
                return RedirectToAction("SelectLoad", "Load");
            }

            return View();
        }

        [HttpGet("login/register")]
        public IActionResult LogReg()
        {
            if(IsUserInSession())
            {
                return RedirectToAction("SelectLoad", "Load");
            }
            else
            {
                return View();
            }
        }

        [HttpPost("create_user_action")]
        public IActionResult CreateUserAction(LogRegViewModel sumbittedUser)
        {
            if(ModelState.IsValid)
            {
                if(_context.users.Any(p => p.email == sumbittedUser.user.email))
                {
                    TempData["Error"] = "That email is already registered, please sign in";
                    return View("LogReg");
                }
                if(sumbittedUser.user.password != sumbittedUser.user.confirm_pw)
                {
                    TempData["Error"] = "Passwords don't match";
                    return View("LogReg");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                sumbittedUser.user.password = Hasher.HashPassword(sumbittedUser.user, sumbittedUser.user.password);

                _context.users.Add(sumbittedUser.user);
                _context.SaveChanges();

                User returnedUser = _context.users
                .Where(p => p.email == sumbittedUser.user.email)
                .FirstOrDefault();

                HttpContext.Session.SetInt32("loggedUser", returnedUser.user_id);
                return RedirectToAction("SelectLoad", "Load");
            }
            return View("LogReg");
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpPost("login_action")]
        public IActionResult LoginAction(LogRegViewModel sumbittedUser)
        {
            User returnUser = _context.users
            .Where(p => p.email == sumbittedUser.loginUser.email)
            .FirstOrDefault();

            if(ModelState.IsValid)
            {
                if(returnUser == null)
                {
                    TempData["ErrorLogin"] = "Email not yet registered";
                    return View("LogReg");
                }
                
                var Hasher = new PasswordHasher<User>();
                if(0 != Hasher.VerifyHashedPassword(returnUser, returnUser.password, sumbittedUser.loginUser.password))
                {
                    HttpContext.Session.SetInt32("loggedUser", returnUser.user_id);
                    return RedirectToAction("SelectLoad", "Load");
                }
            }
            if(sumbittedUser.loginUser.email != null)
            {
                TempData["ErrorLogin"] = "Wrong email/password";
            }
            return View("LogReg");
        }
    }
}

