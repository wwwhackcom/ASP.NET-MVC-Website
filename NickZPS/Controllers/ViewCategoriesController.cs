using NickZPS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NickZPS.Controllers
{
    public class ViewCategoriesController : Controller
    {
        private readonly ICategoryRepository categoryRepository;

        // If you are using Dependency Injection, you can delete the following constructor
        public ViewCategoriesController() : this(new CategoryRepository())
        {
        }

        public ViewCategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        // GET: ViewCategories
        public ViewResult Index()
        {
            return View(categoryRepository.All);
        }
    }
}