using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NickZPS.Models;

namespace NickZPS.Controllers
{   
    public class FeedbacksController : Controller
    {
		private readonly IFeedbackRepository feedbackRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public FeedbacksController() : this(new FeedbackRepository())
        {
        }

        public FeedbacksController(IFeedbackRepository feedbackRepository)
        {
			this.feedbackRepository = feedbackRepository;
        }

        //
        // GET: /Feedbacks/

        public ViewResult Index()
        {
            return View(feedbackRepository.All);
        }

        //
        // GET: /Feedbacks/Details/5

        public ViewResult Details(int id)
        {
            return View(feedbackRepository.Find(id));
        }

        //
        // GET: /Feedbacks/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Feedbacks/Create

        [HttpPost]
        public ActionResult Create(Feedback feedback)
        {
            if (ModelState.IsValid) {
                feedbackRepository.InsertOrUpdate(feedback);
                feedbackRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Feedbacks/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(feedbackRepository.Find(id));
        }

        //
        // POST: /Feedbacks/Edit/5

        [HttpPost]
        public ActionResult Edit(Feedback feedback)
        {
            if (ModelState.IsValid) {
                feedbackRepository.InsertOrUpdate(feedback);
                feedbackRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Feedbacks/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(feedbackRepository.Find(id));
        }

        //
        // POST: /Feedbacks/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            feedbackRepository.Delete(id);
            feedbackRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                feedbackRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

