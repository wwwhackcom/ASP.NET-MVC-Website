using NickZPS.App_Start;
using NickZPS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NickZPS.Controllers
{
    [AuthenticationFilter]
    public class CheckoutController : Controller
    {
        private readonly ISysUserRepository sysuserRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IOrderDetailRepository orderDetailRepository;

        // If you are using Dependency Injection, you can delete the following constructor
        public CheckoutController() : this(new SysUserRepository(), new OrderRepository(), new OrderDetailRepository())
        {
        }

        public CheckoutController(ISysUserRepository sysuserRepository, IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
        {
            this.sysuserRepository = sysuserRepository;
            this.orderRepository = orderRepository;
            this.orderDetailRepository = orderDetailRepository;
        }

        // GET: Checkout
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Order order)
        {
            List<CartProduct> carts = (List<CartProduct>)Session["cart"];
            if (carts == null || carts.Count <= 0)
            {
                ModelState.AddModelError("checkout", "cart is null!");
                return View();
            }

            if (ModelState.IsValid)
            {
                List<OrderDetail> orderDetails = new List<OrderDetail>();
                decimal total = 0;
                foreach (CartProduct item in carts)
                {
                    decimal t = (decimal)item.prouduct.UnitPrice * item.Quan;
                    total += t;
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.ProductID = item.prouduct.ProductID;
                    orderDetail.UnitPrice = (Decimal)item.prouduct.UnitPrice;
                    orderDetail.Quantity = (short)item.Quan;
                    orderDetail.Discount = 1.0f;
                    orderDetail.ProductName = item.prouduct.ProductName;
                    orderDetails.Add(orderDetail);
                }
                order.OrderDetails = orderDetails;
                //order.User = user;
                order.UserID = (int)Session["userId"];//user.ID;
                order.OrderDate = DateTime.Now;
                order.SubTotal = total;
                orderRepository.InsertOrUpdate(order);
                orderRepository.Save();
                Session["cart"] = null;
                Session["order"] = order;
                //ViewBag.Order = order;
                return View("Complete");
            }
            else
            {
                ViewBag.PossibleUsers = sysuserRepository.All;
                return View();
            }
        }

        public ActionResult Complete()
        {
            return View();
        }

        public ActionResult History()
        {
            int userId = (int)Session["userId"];
            return View(orderRepository.Where("select * from [Order] where UserID = '" + userId + "'"));
        }

        public ActionResult HistoryDetails(int id)
        {
            return View(orderDetailRepository.Where("select * from [OrderDetail] where OrderID = '" + id + "'"));
        }
    }
}