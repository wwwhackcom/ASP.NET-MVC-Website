using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NickZPS.Models;

namespace NickZPS.Controllers
{   
    public class RecommendationsController : Controller
    {
		private readonly IProductRepository productRepository;
		private readonly IRecommendationRepository recommendationRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public RecommendationsController() : this(new ProductRepository(), new RecommendationRepository())
        {
        }

        public RecommendationsController(IProductRepository productRepository, IRecommendationRepository recommendationRepository)
        {
			this.productRepository = productRepository;
			this.recommendationRepository = recommendationRepository;
        }

        //
        // GET: /Recommendations/

        public ViewResult Index()
        {
            return View(recommendationRepository.All);
        }

        //
        // GET: /Recommendations/Details/5

        public ViewResult Details(int id)
        {
            return View(recommendationRepository.Find(id));
        }

        //
        // GET: /Recommendations/Create

        public ActionResult Create()
        {
			ViewBag.PossibleProducts = productRepository.All;
            return View();
        } 

        //
        // POST: /Recommendations/Create

        [HttpPost]
        public ActionResult Create(Recommendation recommendation)
        {
            if (ModelState.IsValid) {
                recommendationRepository.InsertOrUpdate(recommendation);
                recommendationRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleProducts = productRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Recommendations/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleProducts = productRepository.All;
             return View(recommendationRepository.Find(id));
        }

        //
        // POST: /Recommendations/Edit/5

        [HttpPost]
        public ActionResult Edit(Recommendation recommendation)
        {
            if (ModelState.IsValid) {
                recommendationRepository.InsertOrUpdate(recommendation);
                recommendationRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleProducts = productRepository.All;
				return View();
			}
        }

        //
        // GET: /Recommendations/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(recommendationRepository.Find(id));
        }

        //
        // POST: /Recommendations/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            recommendationRepository.Delete(id);
            recommendationRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                productRepository.Dispose();
                recommendationRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

