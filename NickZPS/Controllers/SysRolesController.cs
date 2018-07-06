using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NickZPS.Models;
using NickZPS.App_Start;

namespace NickZPS.Controllers
{
    [AuthenticationFilter]
    public class SysRolesController : Controller
    {
		private readonly ISysRoleRepository sysroleRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public SysRolesController() : this(new SysRoleRepository())
        {
        }

        public SysRolesController(ISysRoleRepository sysroleRepository)
        {
			this.sysroleRepository = sysroleRepository;
        }

        //
        // GET: /SysRoles/

        public ViewResult Index()
        {
            return View(sysroleRepository.AllIncluding(sysrole => sysrole.SysUserRoles));
        }

        //
        // GET: /SysRoles/Details/5

        public ViewResult Details(int id)
        {
            return View(sysroleRepository.Find(id));
        }

        //
        // GET: /SysRoles/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /SysRoles/Create

        [HttpPost]
        public ActionResult Create(SysRole sysrole)
        {
            if (ModelState.IsValid) {
                sysroleRepository.InsertOrUpdate(sysrole);
                sysroleRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /SysRoles/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(sysroleRepository.Find(id));
        }

        //
        // POST: /SysRoles/Edit/5

        [HttpPost]
        public ActionResult Edit(SysRole sysrole)
        {
            if (ModelState.IsValid) {
                sysroleRepository.InsertOrUpdate(sysrole);
                sysroleRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /SysRoles/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(sysroleRepository.Find(id));
        }

        //
        // POST: /SysRoles/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            sysroleRepository.Delete(id);
            sysroleRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                sysroleRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

