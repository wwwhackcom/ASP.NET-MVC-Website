using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NickZPS.Models;

namespace NickZPS.Controllers
{   
    public class OrdersController : Controller
    {
		private readonly ISysUserRepository sysuserRepository;
		private readonly IOrderRepository orderRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public OrdersController() : this(new SysUserRepository(), new OrderRepository())
        {
        }

        public OrdersController(ISysUserRepository sysuserRepository, IOrderRepository orderRepository)
        {
			this.sysuserRepository = sysuserRepository;
			this.orderRepository = orderRepository;
        }

        //
        // GET: /Orders/

        public ViewResult Index()
        {
            return View(orderRepository.AllIncluding(order => order.User));
        }

        public ActionResult Users(int id)
        {
            return View("Index", orderRepository.Where("select * from [Order] where UserID = '" + id + "'"));
        }

        //
        // GET: /Orders/Details/5

        public ViewResult Details(int id)
        {
            return View(orderRepository.Find(id));
        }

        //
        // GET: /Orders/Create

        public ActionResult Create()
        {
			ViewBag.PossibleUsers = sysuserRepository.All;
            return View();
        } 

        //
        // POST: /Orders/Create

        [HttpPost]
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid) {
                orderRepository.InsertOrUpdate(order);
                orderRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleUsers = sysuserRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Orders/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleUsers = sysuserRepository.All;
             return View(orderRepository.Find(id));
        }

        //
        // POST: /Orders/Edit/5

        [HttpPost]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid) {
                orderRepository.InsertOrUpdate(order);
                orderRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleUsers = sysuserRepository.All;
				return View();
			}
        }

        //
        // GET: /Orders/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(orderRepository.Find(id));
        }

        //
        // POST: /Orders/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            orderRepository.Delete(id);
            orderRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                sysuserRepository.Dispose();
                orderRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

