using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NickZPS.Models;
using System.IO;

namespace NickZPS.Controllers
{   
    public class ProductsController : Controller
    {
		private readonly ICategoryRepository categoryRepository;
		private readonly IProductRepository productRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public ProductsController() : this(new CategoryRepository(), new ProductRepository())
        {
        }

        public ProductsController(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
			this.categoryRepository = categoryRepository;
			this.productRepository = productRepository;
        }

        //
        // GET: /Products/

        public ViewResult Index()
        {
            return View(productRepository.AllIncluding(product => product.Category, product => product.OrderDetails));
        }

        //
        // GET: /Products/Details/5

        public ViewResult Details(int id)
        {
            return View(productRepository.Find(id));
        }

        //
        // GET: /Products/Create

        public ActionResult Create()
        {
			ViewBag.PossibleCategories = categoryRepository.All;
            return View();
        } 

        //
        // POST: /Products/Create

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid) {
                productRepository.InsertOrUpdate(product);
                productRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleCategories = categoryRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Products/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleCategories = categoryRepository.All;
             return View(productRepository.Find(id));
        }

        //
        // POST: /Products/Edit/5

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid) {
                productRepository.InsertOrUpdate(product);
                productRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleCategories = categoryRepository.All;
				return View();
			}
        }

        //
        // GET: /Products/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(productRepository.Find(id));
        }

        //
        // POST: /Products/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            productRepository.Delete(id);
            productRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                categoryRepository.Dispose();
                productRepository.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file, Product product)
        {
            if (file == null)
            {
                productRepository.InsertOrUpdate(product);
                productRepository.Save();
                return RedirectToAction("Index");
            }

            if (file.ContentLength <= 0)
            {
                ModelState.AddModelError("Upload", "Please select a file!");
                return View();
            }

            if (!StringHelper.IsPicture(file.FileName))
            {
                ModelState.AddModelError("Upload", "Please select a picture format file!");
                return View();
            }


            string fileName = StringHelper.GetTimeStamp();//file.FileName.Substring(0, file.FileName.LastIndexOf("."));
            var filePath = Server.MapPath(string.Format("~/{0}", "UploadImage"));
            string suffix = file.FileName.Substring(file.FileName.LastIndexOf(".")).ToLower();
            fileName += suffix;
            file.SaveAs(Path.Combine(filePath, fileName));
            product.ProductPoster = "/UploadImage/" + fileName;

            productRepository.InsertOrUpdate(product);
            productRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

