using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NickZPS.Models;
using NickZPS.App_Start;

namespace NickZPS.Controllers
{
    public class SysUsersController : Controller
    {
		private readonly ISysUserRepository sysuserRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public SysUsersController() : this(new SysUserRepository())
        {
        }

        public SysUsersController(ISysUserRepository sysuserRepository)
        {
			this.sysuserRepository = sysuserRepository;
        }

        //
        // GET: /SysUsers/

        public ViewResult Index()
        {
            return View(sysuserRepository.AllIncluding(sysuser => sysuser.SysUserRoles, sysuser => sysuser.Orders, sysuser => sysuser.Invoices));
        }

        //
        // GET: /SysUsers/Details/5

        public ViewResult Details(int id)
        {
            return View(sysuserRepository.Find(id));
        }

        //
        // GET: /SysUsers/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /SysUsers/Create

        [HttpPost]
        public ActionResult Create(SysUser sysuser)
        {
            if (ModelState.IsValid) {
                sysuserRepository.InsertOrUpdate(sysuser);
                sysuserRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /SysUsers/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(sysuserRepository.Find(id));
        }

        //
        // POST: /SysUsers/Edit/5

        [HttpPost]
        public ActionResult Edit(SysUser sysuser)
        {
            if (ModelState.IsValid) {
                sysuserRepository.InsertOrUpdate(sysuser);
                sysuserRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /SysUsers/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(sysuserRepository.Find(id));
        }

        //
        // POST: /SysUsers/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            sysuserRepository.Delete(id);
            sysuserRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                sysuserRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

