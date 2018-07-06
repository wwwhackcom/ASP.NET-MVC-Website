using NickZPS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NickZPS.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository productRepository;

        // If you are using Dependency Injection, you can delete the following constructor
        public CartController() : this(new ProductRepository())
        {
        }

        public CartController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public ViewResult Index()
        {
            List<CartProduct> products = (List<CartProduct>)Session["cart"];
            if (products == null)
            {
                products = new List<CartProduct>();
            }
            
            decimal total = 0;
            foreach (CartProduct cp in products)
            {
                decimal t = (decimal)cp.prouduct.UnitPrice * cp.Quan;
                total += t;
            }
            ViewBag.Total = total;
            ViewBag.Gst = total * 15 / 100;
            Session["cart"] = products;

            return View(products);
        }

        // GET: ShowProduct
        public ViewResult Add(int id)
        {
            Product product = productRepository.Find(id);
            if (product == null)
                return View("Index");

            List<CartProduct> products = (List<CartProduct>)Session["cart"];
            if (products == null)
            {
                products = new List<CartProduct>();
            }
            CartProduct carProduct = new CartProduct(product);
            carProduct.Quan = 1;
            products.Add(carProduct);
            decimal total = 0;
            foreach (CartProduct cp in products)
            {
                decimal t = (decimal)cp.prouduct.UnitPrice * cp.Quan;
                total += t;
            }
            ViewBag.Total = total;
            ViewBag.Gst = total*15/100;
            Session["cart"] = products;
            return View("Index", products);
        }

        public ViewResult Remove(int id)
        {
            List<CartProduct> products = (List<CartProduct>)Session["cart"];
            if (products == null)
            {
                return View("Index");
            }

            for(int i=0;i<products.Count;i++)
            {
                CartProduct p = products[i];
                if (p.prouduct.ProductID == id)
                {
                    products.RemoveAt(i);
                    break;
                }
            }

            Session["cart"] = products;
            return View("Index", products);
        }
    }
}