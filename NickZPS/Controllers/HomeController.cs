using NickZPS.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NickZPS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IProductRepository productRepository;
        private readonly IFeedbackRepository feedbackRepository;

        // If you are using Dependency Injection, you can delete the following constructor
        public HomeController() : this(new CategoryRepository(), new ProductRepository(), new FeedbackRepository())
        {
        }

        public HomeController(ICategoryRepository categoryRepository, IProductRepository productRepository, IFeedbackRepository feedbackRepository)
        {
            this.categoryRepository = categoryRepository;
            this.productRepository = productRepository;
            this.feedbackRepository = feedbackRepository;
        }

        // GET: Index
        public ActionResult Index()
        {
            IEnumerable<Product> products = productRepository.Where("select * from Product where IsHome = '1'");
            return View(products);
        }

        public ActionResult Contact()
        {
            //ViewBag.Message = "Your contact page.";

            return View("Contact");
        }

        [HttpPost]
        public ActionResult Contact(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                feedbackRepository.InsertOrUpdate(feedback);
                feedbackRepository.Save();
                ViewBag.Message = "Thank you " + feedback.UserName + ", we have received your message, we will reply your email: " + feedback.Email + " soon";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Aboutus()
        {
            return View();
        }

    }
}