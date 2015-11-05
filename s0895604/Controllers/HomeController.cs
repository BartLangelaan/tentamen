using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using s0895604.Models;

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
            // Delete session
            Session.Abandon();

            // Delete all tables
            db.Database.ExecuteSqlCommand("delete from Ratings");
            db.Database.ExecuteSqlCommand("delete from Reviews");
            db.Database.ExecuteSqlCommand("delete from Categories");
            db.Database.ExecuteSqlCommand("delete from Users");

            // Check if all users are gone
            if (db.Accounts.Count() != 0)
            {
                return Content("Resetting failed");
            }

            // Create categories
            db.Categories.AddOrUpdate(x => x.CategoryId, 
                new Category() { CategoryId = 1, Name = "Electronica"},
                new Category() { CategoryId = 2, Name = "Restaurants" },
                new Category() { CategoryId = 3, Name = "Films" },
                new Category() { CategoryId = 4, Name = "Huishoudelijk" },
                new Category() { CategoryId = 5, Name = "Vervoer" },
                new Category() { CategoryId = 6, Name = "Tuin" }
                );

            // Create users
            db.Accounts.AddOrUpdate(x => x.UserId,
                new User() { UserId = 1, Role = UserRole.Admin, Active = true, FirstName = "Bart", LastName = "Langelaan", Username = "admin", Password = "wachtwoord"},
                new User() { UserId = 2, Role = UserRole.User, Active = true, FirstName = "Bob", LastName = "Joziasse", Username = "bob", Password = "wachtwoord"},
                new User() { UserId = 3, Role = UserRole.User, Active = true, FirstName = "Chantal", LastName = "de Maas", Username = "chantal", Password = "wachtwoord" },
                new User() { UserId = 4, Role = UserRole.User, Active = true, FirstName = "Dieuwertje", LastName = "Spruijt-Schuur", Username = "dieuwertje", Password = "wachtwoord" },
                new User() { UserId = 5, Role = UserRole.User, Active = true, FirstName = "Emiel", LastName = "Bakker", Username = "emiel", Password = "wachtwoord" },
                new User() { UserId = 6, Role = UserRole.User, Active = false, FirstName = "Stan", LastName = "Scheerder", Username = "stan", Password = "wachtwoord" }
                );

            // Create reviews
            int userId = 2;
            foreach (int categoryId in Enumerable.Range(1, 6).ToList())
            {
                for (var i = 1; i <= 3; i++)
                {
                    db.Reviews.Add(new Review()
                    {
                        CategoryId = categoryId,
                        Content = "Lorem Ipsum",
                        CreatedDateTime = DateTime.Now,
                        Name = "Lorem Ipsum",
                        UserId = userId
                    });
                    userId++;
                    if (userId > 6) userId = 2;
                }
            }
            db.SaveChanges();

            // Create ratings
            Random rnd = new Random();
            foreach (Review review in db.Reviews.ToList())
            {
                foreach (User user in db.Accounts.ToList())
                {
                    if(user.Role == UserRole.User)
                        db.Ratings.Add(new Rating()
                        {
                            ReviewId = review.ReviewId,
                            UserId = user.UserId,
                            RatingNumber = rnd.Next(1,5)
                        });
                }
            }
            db.SaveChanges();

            



            return RedirectToAction("Index");
        }
    }
}