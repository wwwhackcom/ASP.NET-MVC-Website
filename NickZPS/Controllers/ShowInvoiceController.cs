using NickZPS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NickZPS.Controllers
{
    public class ShowInvoiceController : Controller
    {
        private readonly ISysUserRepository sysuserRepository;
        private readonly IInvoiceRepository invoiceRepository;

        // If you are using Dependency Injection, you can delete the following constructor
        public ShowInvoiceController() : this(new SysUserRepository(), new InvoiceRepository())
        {
        }

        public ShowInvoiceController(ISysUserRepository sysuserRepository, IInvoiceRepository invoiceRepository)
        {
            this.sysuserRepository = sysuserRepository;
            this.invoiceRepository = invoiceRepository;
        }

        // GET: ShowInvoice
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult Create()
        {
            Order order = (Order)Session["order"];
            if (order == null)
                return View();

            SysUser user = sysuserRepository.Find((int)Session["userId"]);
            if (user == null)
            {
                ModelState.AddModelError("showInvoice", "inter error!");
                return View();
            }

            List<InvoiceDetail> invoiceDetails = new List<InvoiceDetail>();
            foreach (OrderDetail item in order.OrderDetails)
            {
                InvoiceDetail invoiceDetail = new InvoiceDetail();
                invoiceDetail.ProductID = item.ProductID;
                invoiceDetail.UnitPrice = item.UnitPrice;
                invoiceDetail.Quantity = item.Quantity;
                invoiceDetail.Discount = item.Discount;
                invoiceDetail.ProductName = item.ProductName;
                invoiceDetails.Add(invoiceDetail);
            }

            Invoice invoice = new Invoice();
            invoice.InvoiceDetails = invoiceDetails;
            invoice.UserID = (int)Session["userId"];
            invoice.InvoiceDate = DateTime.Now;
            invoice.SubTotal = order.SubTotal;
            invoice.Discount = 1.0f;
            invoice.User = user;

            Session["order"] = null;
            return View("Index", invoice);
        }
    }
}