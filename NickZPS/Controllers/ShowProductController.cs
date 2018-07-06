using NickZPS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NickZPS.Controllers
{
    public class ShowProductController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IRecommendationRepository recommendationRepository;

        // If you are using Dependency Injection, you can delete the following constructor
        public ShowProductController() : this(new ProductRepository(), new RecommendationRepository())
        {
        }

        public ShowProductController(IProductRepository productRepository, IRecommendationRepository recommendationRepository)
        {
            this.productRepository = productRepository;
            this.recommendationRepository = recommendationRepository;
        }

        // GET: ShowProduct
        public ViewResult Index(int id)
        {
            IEnumerable<Recommendation> recommendations = recommendationRepository.Where("select * from [Recommendation] where ProductID = '" + id + "'");
            ViewBag.Recommendations = recommendations;
            return View(productRepository.Find(id));
        }
    }
}