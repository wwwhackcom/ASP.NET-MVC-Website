using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NickZPS.Models;

namespace NickZPS.Controllers
{   
    public class ShippersController : Controller
    {
		private readonly IShipperRepository shipperRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public ShippersController() : this(new ShipperRepository())
        {
        }

        public ShippersController(IShipperRepository shipperRepository)
        {
			this.shipperRepository = shipperRepository;
        }

        //
        // GET: /Shippers/

        public ViewResult Index()
        {
            return View(shipperRepository.AllIncluding(shipper => shipper.Orders));
        }

        //
        // GET: /Shippers/Details/5

        public ViewResult Details(int id)
        {
            return View(shipperRepository.Find(id));
        }

        //
        // GET: /Shippers/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Shippers/Create

        [HttpPost]
        public ActionResult Create(Shipper shipper)
        {
            if (ModelState.IsValid) {
                shipperRepository.InsertOrUpdate(shipper);
                shipperRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Shippers/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(shipperRepository.Find(id));
        }

        //
        // POST: /Shippers/Edit/5

        [HttpPost]
        public ActionResult Edit(Shipper shipper)
        {
            if (ModelState.IsValid) {
                shipperRepository.InsertOrUpdate(shipper);
                shipperRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Shippers/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(shipperRepository.Find(id));
        }

        //
        // POST: /Shippers/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            shipperRepository.Delete(id);
            shipperRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                shipperRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

