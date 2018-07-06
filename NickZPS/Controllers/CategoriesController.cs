using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NickZPS.Models;
using System.IO;

namespace NickZPS.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository categoryRepository;

        // If you are using Dependency Injection, you can delete the following constructor
        public CategoriesController() : this(new CategoryRepository())
        {
        }

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        //
        // GET: /Categories/

        public ViewResult Index()
        {
            return View(categoryRepository.All);
        }

        //
        // GET: /Categories/Details/5

        public ViewResult Details(int id)
        {
            return View(categoryRepository.Find(id));
        }

        //
        // GET: /Categories/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Categories/Create

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid) {
                categoryRepository.InsertOrUpdate(category);
                categoryRepository.Save();
                return RedirectToAction("Index");
            } else {
                return View();
            }
        }

        //
        // GET: /Categories/Edit/5

        public ActionResult Edit(int id)
        {
            return View(categoryRepository.Find(id));
        }

        //
        // POST: /Categories/Edit/5

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid) {
                categoryRepository.InsertOrUpdate(category);
                categoryRepository.Save();
                return RedirectToAction("Index");
            } else {
                return View();
            }
        }

        public ActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file, Category category)
        {
            if (file == null)
            {
                categoryRepository.InsertOrUpdate(category);
                categoryRepository.Save();
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
            category.PicturePath = "/UploadImage/" + fileName;

            categoryRepository.InsertOrUpdate(category);
            categoryRepository.Save();

            return RedirectToAction("Index");
            //return View("Edit", category);
        }

        //
        // GET: /Categories/Delete/5

        public ActionResult Delete(int id)
        {
            return View(categoryRepository.Find(id));
        }

        //
        // POST: /Categories/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            categoryRepository.Delete(id);
            categoryRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                categoryRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

