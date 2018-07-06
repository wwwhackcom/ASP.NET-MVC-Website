using NickZPS.DAL;
using NickZPS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NickZPS.Controllers
{
    public class AccountController : Controller
    {
        //private AccountContext db = new AccountContext();
        private readonly ISysUserRepository sysuserRepository;
        private readonly ISysUserRoleRepository sysuserroleRepository;

        // If you are using Dependency Injection, you can delete the following constructor
        public AccountController() : this(new SysUserRepository(), new SysUserRoleRepository())
        {
        }

        public AccountController(ISysUserRepository sysuserRepository, ISysUserRoleRepository sysuserroleRepository)
        {
            this.sysuserRepository = sysuserRepository;
            this.sysuserroleRepository = sysuserroleRepository;
        }

        public ActionResult Login()
        {
            ViewBag.LoginState = "Please login in!";
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            string email = fc["inputEmail"];
            string password = fc["inputPassword"];

            var users = sysuserRepository.Login(email, password);
            if (users.Count() > 0)
            {
                ViewBag.LoginState = email + "login successfully!";
                var user = users.FirstOrDefault();
                if (user.IsInactive)
                {
                    ViewBag.LoginState = user.UserName + " is Inactive!";
                    return View();
                }

                Session["username"] = user.UserName;
                Session["userId"] = user.ID;
                Session["user"] = user;
                if (user.UserName.Equals("Admin"))
                {
                    return RedirectToAction("Index", "Categories");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.LoginState = email + " Not exsit!";
                return View();
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(SysUser user)
        {
            var u = sysuserRepository.FindByEmail(user.Email);
            if (u != null)
            {
                ModelState.AddModelError("", user.Email + " already exsits!");
                return View();
            }

            if (ModelState.IsValid)
            {
                var sysUserRoles = new List<SysUserRole>
                {
                    new SysUserRole{ SysRoleId = 2 }
                };
                user.SysUserRoles = sysUserRoles;

                sysuserRepository.InsertOrUpdate(user);
                sysuserRepository.Save();
                
                Session["username"] = user.UserName;
                Session["userId"] = user.ID;
                Session["user"] = user;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session["username"] = null;
            Session["userId"] = null;
            Session["user"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult UserProfile(int id)
        {
            return View(sysuserRepository.Find(id));
        }

        [HttpPost]
        public ActionResult UserProfile(SysUser sysuser)
        {
            if (ModelState.IsValid)
            {
                sysuserRepository.InsertOrUpdate(sysuser);
                sysuserRepository.Save();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
    }
}