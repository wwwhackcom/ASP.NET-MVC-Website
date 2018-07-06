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
    public class SysUserRolesController : Controller
    {
		private readonly ISysUserRepository sysuserRepository;
		private readonly ISysRoleRepository sysroleRepository;
		private readonly ISysUserRoleRepository sysuserroleRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public SysUserRolesController() : this(new SysUserRepository(), new SysRoleRepository(), new SysUserRoleRepository())
        {
        }

        public SysUserRolesController(ISysUserRepository sysuserRepository, ISysRoleRepository sysroleRepository, ISysUserRoleRepository sysuserroleRepository)
        {
			this.sysuserRepository = sysuserRepository;
			this.sysroleRepository = sysroleRepository;
			this.sysuserroleRepository = sysuserroleRepository;
        }

        //
        // GET: /SysUserRoles/

        public ViewResult Index()
        {
            return View(sysuserroleRepository.AllIncluding(sysuserrole => sysuserrole.SysUser, sysuserrole => sysuserrole.SysRole));
        }

        //
        // GET: /SysUserRoles/Details/5

        public ViewResult Details(int id)
        {
            return View(sysuserroleRepository.Find(id));
        }

        //
        // GET: /SysUserRoles/Create

        public ActionResult Create()
        {
			ViewBag.PossibleSysUsers = sysuserRepository.All;
			ViewBag.PossibleSysRoles = sysroleRepository.All;
            return View();
        } 

        //
        // POST: /SysUserRoles/Create

        [HttpPost]
        public ActionResult Create(SysUserRole sysuserrole)
        {
            if (ModelState.IsValid) {
                sysuserroleRepository.InsertOrUpdate(sysuserrole);
                sysuserroleRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleSysUsers = sysuserRepository.All;
				ViewBag.PossibleSysRoles = sysroleRepository.All;
				return View();
			}
        }
        
        //
        // GET: /SysUserRoles/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleSysUsers = sysuserRepository.All;
			ViewBag.PossibleSysRoles = sysroleRepository.All;
             return View(sysuserroleRepository.Find(id));
        }

        //
        // POST: /SysUserRoles/Edit/5

        [HttpPost]
        public ActionResult Edit(SysUserRole sysuserrole)
        {
            if (ModelState.IsValid) {
                sysuserroleRepository.InsertOrUpdate(sysuserrole);
                sysuserroleRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleSysUsers = sysuserRepository.All;
				ViewBag.PossibleSysRoles = sysroleRepository.All;
				return View();
			}
        }

        //
        // GET: /SysUserRoles/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(sysuserroleRepository.Find(id));
        }

        //
        // POST: /SysUserRoles/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            sysuserroleRepository.Delete(id);
            sysuserroleRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                sysuserRepository.Dispose();
                sysroleRepository.Dispose();
                sysuserroleRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

