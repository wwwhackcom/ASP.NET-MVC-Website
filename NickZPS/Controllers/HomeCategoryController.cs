using NickZPS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NickZPS.Controllers
{
    public class HomeCategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IProductRepository productRepository;

        // If you are using Dependency Injection, you can delete the following constructor
        public HomeCategoryController() : this(new CategoryRepository(), new ProductRepository())
        {
        }

        public HomeCategoryController(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            this.categoryRepository = categoryRepository;
            this.productRepository = productRepository;
        }

        // GET: HomeCategory
        public ActionResult Index()
        {
            IEnumerable<Category> c = categoryRepository.All;
            ViewBag.Categories = c;

            return View(productRepository.AllIncluding(product => product.Category, product => product.OrderDetails));
        }

        public ActionResult Products(int id)
        {
            IEnumerable<Category> c = categoryRepository.All;
            ViewBag.Categories = c;
            IEnumerable<Product> products = productRepository.Where("select * from Product where CategoryID = '" + id + "'");
            return View("Index", products);
        }

        [HttpPost]
        public ActionResult Search(string searchText)
        {
            ViewBag.SearchTxt = searchText;
            IEnumerable<Category> c = categoryRepository.All;
            ViewBag.Categories = c;
            string sql = StringHelper.FilterSql(searchText);
            IEnumerable<Product> products = productRepository.Where("select * from Product where productName like '%" + sql + "%'");
            return View("Index", products);
        }
    }
}