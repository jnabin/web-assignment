using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAssignment.Data;
using WebAssignment.Data.ViewModel;
using WebAssignment.Models;

namespace WebAssignment.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext dbContext = new AppDbContext(); 

        // GET: Account
        public ActionResult Login()
        {
            if(Session["UserName"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = dbContext.Users.Where(x => x.Email == model.Email && x.Password == model.Password).FirstOrDefault();

                if(user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid Email/Password");
                    return View(model);
                }

                Session["UserName"] = user.Username;
                return RedirectToAction("Index", "Home");
                
            }
            
            return View(model);
        }

        public ActionResult Logout()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult SignUp()
        {
            if (Session["UserName"] != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult SignUp(User model)
        {
            if (ModelState.IsValid)
            {
                var user = dbContext.Users.Where(x => x.Email == model.Email).FirstOrDefault();
                var user2 = dbContext.Users.Where(x => x.Username == model.Username).FirstOrDefault();

                if(user != null)
                {
                    ModelState.AddModelError(string.Empty, "email already exist");
                    return View(model);
                }
                else if(user2 != null)
                {
                    ModelState.AddModelError(string.Empty, "username already exist");
                    return View(model);
                }
                else
                {
                    dbContext.Users.Add(model);
                    dbContext.SaveChanges();
                    Session["UserName"] = model.Username;

                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }
    }
}