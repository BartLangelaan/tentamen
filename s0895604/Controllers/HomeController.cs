using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace s0895604.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (!db.Categories.Any())
            {
                RedirectToAction("Reset");
            }
            if (LoggedInUser == null)
            {
                ViewBag.Reviews = db.Reviews.Count();
                ViewBag.Ratings = db.Ratings.Count();
                return View();
            }
            else
            {
                return View("LoggedInIndex");
            }
        }

        public ActionResult Reset()
        {
            Session.Abandon();
            db.Database.ExecuteSqlCommand("delete from Ratings");
            db.Database.ExecuteSqlCommand("delete from Reviews");
            db.Database.ExecuteSqlCommand("delete from Categories");
            db.Database.ExecuteSqlCommand("delete from Users");

            if (db.Accounts.Count() != 0)
            {
                return Content("Resetting failed");
            }



            return RedirectToAction("Index");
        }
    }
}