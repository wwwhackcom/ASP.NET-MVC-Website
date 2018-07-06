using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NickZPS.Models;

namespace NickZPS.Controllers
{   
    public class InvoicesController : Controller
    {
		private readonly ISysUserRepository sysuserRepository;
		private readonly IInvoiceRepository invoiceRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public InvoicesController() : this(new SysUserRepository(), new InvoiceRepository())
        {
        }

        public InvoicesController(ISysUserRepository sysuserRepository, IInvoiceRepository invoiceRepository)
        {
			this.sysuserRepository = sysuserRepository;
			this.invoiceRepository = invoiceRepository;
        }

        //
        // GET: /Invoices/

        public ViewResult Index()
        {
            return View(invoiceRepository.AllIncluding(invoice => invoice.User, invoice => invoice.InvoiceDetails));
        }

        //
        // GET: /Invoices/Details/5

        public ViewResult Details(int id)
        {
            return View(invoiceRepository.Find(id));
        }

        //
        // GET: /Invoices/Create

        public ActionResult Create()
        {
			ViewBag.PossibleUsers = sysuserRepository.All;
            return View();
        } 

        //
        // POST: /Invoices/Create

        [HttpPost]
        public ActionResult Create(Invoice invoice)
        {
            if (ModelState.IsValid) {
                invoiceRepository.InsertOrUpdate(invoice);
                invoiceRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleUsers = sysuserRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Invoices/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleUsers = sysuserRepository.All;
             return View(invoiceRepository.Find(id));
        }

        //
        // POST: /Invoices/Edit/5

        [HttpPost]
        public ActionResult Edit(Invoice invoice)
        {
            if (ModelState.IsValid) {
                invoiceRepository.InsertOrUpdate(invoice);
                invoiceRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleUsers = sysuserRepository.All;
				return View();
			}
        }

        //
        // GET: /Invoices/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(invoiceRepository.Find(id));
        }

        //
        // POST: /Invoices/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            invoiceRepository.Delete(id);
            invoiceRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                sysuserRepository.Dispose();
                invoiceRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

