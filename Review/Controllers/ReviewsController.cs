using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Review.Models;

namespace Review.Controllers
{
    public class ReviewsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: /Reviews?productId=1
        public ActionResult Index(int productId = 1)
        {
            var reviews = db.CustReviews
                            .Where(r => r.ProductId == productId)
                            .OrderByDescending(r => r.ReviewDate)
                            .ToList();
            return View(reviews);
        }

        // GET: /Reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            CustReview review = db.CustReviews.Find(id);
            if (review == null) return HttpNotFound();
            return View(review);
        }

        // GET: /Reviews/Create
        public ActionResult Create(int productId = 1)
        {
            return View(new CustReview { ProductId = productId, ReviewDate = DateTime.Now });
        }

        // POST: /Reviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,Rating,Comment")] CustReview review)
        {
            review.ReviewDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.CustReviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index", new { productId = review.ProductId });
            }
            return View(review);
        }

        // GET: /Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            CustReview review = db.CustReviews.Find(id);
            if (review == null) return HttpNotFound();
            return View(review);
        }

        // POST: /Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustReview review = db.CustReviews.Find(id);
            db.CustReviews.Remove(review);
            db.SaveChanges();
            return RedirectToAction("Index", new { productId = review.ProductId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
