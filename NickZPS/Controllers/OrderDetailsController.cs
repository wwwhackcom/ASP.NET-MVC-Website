using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NickZPS.Models;

namespace NickZPS.Controllers
{   
    public class OrderDetailsController : Controller
    {
		private readonly IOrderRepository orderRepository;
		private readonly IProductRepository productRepository;
		private readonly IOrderDetailRepository orderdetailRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public OrderDetailsController() : this(new OrderRepository(), new ProductRepository(), new OrderDetailRepository())
        {
        }

        public OrderDetailsController(IOrderRepository orderRepository, IProductRepository productRepository, IOrderDetailRepository orderdetailRepository)
        {
			this.orderRepository = orderRepository;
			this.productRepository = productRepository;
			this.orderdetailRepository = orderdetailRepository;
        }

        //
        // GET: /OrderDetails/

        public ViewResult Index()
        {
            return View(orderdetailRepository.AllIncluding(orderdetail => orderdetail.Order, orderdetail => orderdetail.Product));
        }

        public ActionResult Orders(int id)
        {
            return View("Index", orderdetailRepository.Where("select * from [OrderDetail] where OrderID = '" + id + "'"));
        }

        //
        // GET: /OrderDetails/Details/5

        public ViewResult Details(int id)
        {
            return View(orderdetailRepository.Find(id));
        }

        //
        // GET: /OrderDetails/Create

        public ActionResult Create()
        {
			ViewBag.PossibleOrders = orderRepository.All;
			ViewBag.PossibleProducts = productRepository.All;
            return View();
        } 

        //
        // POST: /OrderDetails/Create

        [HttpPost]
        public ActionResult Create(OrderDetail orderdetail)
        {
            if (ModelState.IsValid) {
                orderdetailRepository.InsertOrUpdate(orderdetail);
                orderdetailRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleOrders = orderRepository.All;
				ViewBag.PossibleProducts = productRepository.All;
				return View();
			}
        }
        
        //
        // GET: /OrderDetails/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleOrders = orderRepository.All;
			ViewBag.PossibleProducts = productRepository.All;
             return View(orderdetailRepository.Find(id));
        }

        //
        // POST: /OrderDetails/Edit/5

        [HttpPost]
        public ActionResult Edit(OrderDetail orderdetail)
        {
            if (ModelState.IsValid) {
                orderdetailRepository.InsertOrUpdate(orderdetail);
                orderdetailRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleOrders = orderRepository.All;
				ViewBag.PossibleProducts = productRepository.All;
				return View();
			}
        }

        //
        // GET: /OrderDetails/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(orderdetailRepository.Find(id));
        }

        //
        // POST: /OrderDetails/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            orderdetailRepository.Delete(id);
            orderdetailRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                orderRepository.Dispose();
                productRepository.Dispose();
                orderdetailRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

