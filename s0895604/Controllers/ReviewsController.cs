using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using s0895604.Models;

namespace s0895604.Controllers
{
    [AuthorizeLoggedIn]
    public class ReviewsController : BaseController
    {
        // GET: Reviews
        public ActionResult Index(int? category, string search)
        {
            if (LoggedInUser.Role == UserRole.Admin)
            {
                var reviews = db.Reviews.Include(r => r.Category).Include(r => r.User);
                return View(reviews.ToList());
            }
            else
            {
                var reviews = db.Reviews.Include(r => r.Category);

                if (category != null)
                    reviews = (from a in reviews where a.Category.CategoryId == category && a.Active select a);

                if (search != null)
                    reviews = (from a in reviews where a.Content.ToLower().Contains(search.ToLower()) || a.Name.ToLower().Contains(search.ToLower()) select a);

                ViewBag.category = new SelectList(db.Categories, "CategoryId", "Name", category);
                ViewBag.search = search;

                return View("IndexPublic", reviews);
            }
            // TODO: Add MyReviews
            // TODO: Add Reviews from Category x
            
        }

        // GET: Reviews/Mine
        public ActionResult Mine()
        {
            var reviews = (from a in db.Reviews.Include(r => r.Category) where a.UserId == LoggedInUser.UserId select a);

            return View("IndexMine", reviews);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Mine(int reviewId)
        {
            // TODO: Limit to user

            var Review = db.Reviews.Find(reviewId);
            Review.Active = !Review.Active;
            db.Entry(Review).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Mine");
        }


        // GET: Reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Reviews/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Content,CategoryId")] Review review)
        {
            review.User = LoggedInUser;
            review.UserId = LoggedInUser.UserId;
            review.CreatedDateTime = DateTime.Now;
            review.Active = true;
            if (ModelState.IsValid)
            {
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", review.CategoryId);
            return View(review);
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            // TODO: Limit to user
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", review.CategoryId);
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReviewId,Name,Content,CategoryId")] Review review)
        {
            ModelState.Remove("CreatedDateTime");
            ModelState.Remove("UserId");

            if (ModelState.IsValid)
            {
                var toUpdate = db.Reviews.Single(p => p.ReviewId == review.ReviewId);
                toUpdate.Name = review.Name;
                toUpdate.CategoryId = review.CategoryId;
                toUpdate.Content = review.Content;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", review.CategoryId);
            return View(review);
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            // TODO: Limit to user
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            // TODO: Limit to user
            db.Reviews.Remove(review);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [AuthorizeLoggedIn(true)]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
