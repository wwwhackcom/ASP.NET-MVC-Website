using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NickZPS.Models;

namespace NickZPS.Controllers
{   
    public class InvoiceDetailsController : Controller
    {
		private readonly IInvoiceRepository invoiceRepository;
		private readonly IProductRepository productRepository;
		private readonly IInvoiceDetailRepository invoicedetailRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public InvoiceDetailsController() : this(new InvoiceRepository(), new ProductRepository(), new InvoiceDetailRepository())
        {
        }

        public InvoiceDetailsController(IInvoiceRepository invoiceRepository, IProductRepository productRepository, IInvoiceDetailRepository invoicedetailRepository)
        {
			this.invoiceRepository = invoiceRepository;
			this.productRepository = productRepository;
			this.invoicedetailRepository = invoicedetailRepository;
        }

        //
        // GET: /InvoiceDetails/

        public ViewResult Index()
        {
            return View(invoicedetailRepository.AllIncluding(invoicedetail => invoicedetail.Invoice, invoicedetail => invoicedetail.Product));
        }

        //
        // GET: /InvoiceDetails/Details/5

        public ViewResult Details(int id)
        {
            return View(invoicedetailRepository.Find(id));
        }

        //
        // GET: /InvoiceDetails/Create

        public ActionResult Create()
        {
			ViewBag.PossibleInvoices = invoiceRepository.All;
			ViewBag.PossibleProducts = productRepository.All;
            return View();
        } 

        //
        // POST: /InvoiceDetails/Create

        [HttpPost]
        public ActionResult Create(InvoiceDetail invoicedetail)
        {
            if (ModelState.IsValid) {
                invoicedetailRepository.InsertOrUpdate(invoicedetail);
                invoicedetailRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleInvoices = invoiceRepository.All;
				ViewBag.PossibleProducts = productRepository.All;
				return View();
			}
        }
        
        //
        // GET: /InvoiceDetails/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleInvoices = invoiceRepository.All;
			ViewBag.PossibleProducts = productRepository.All;
             return View(invoicedetailRepository.Find(id));
        }

        //
        // POST: /InvoiceDetails/Edit/5

        [HttpPost]
        public ActionResult Edit(InvoiceDetail invoicedetail)
        {
            if (ModelState.IsValid) {
                invoicedetailRepository.InsertOrUpdate(invoicedetail);
                invoicedetailRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleInvoices = invoiceRepository.All;
				ViewBag.PossibleProducts = productRepository.All;
				return View();
			}
        }

        //
        // GET: /InvoiceDetails/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(invoicedetailRepository.Find(id));
        }

        //
        // POST: /InvoiceDetails/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            invoicedetailRepository.Delete(id);
            invoicedetailRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                invoiceRepository.Dispose();
                productRepository.Dispose();
                invoicedetailRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

