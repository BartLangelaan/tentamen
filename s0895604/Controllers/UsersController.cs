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
    public class UsersController : BaseController
    {

        // GET: Users/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Users/Register
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Username,Password,FirstName,LastName")] User user)
        {
            if (ModelState.IsValid)
            {
                user.Role = db.Accounts.Any() ? UserRole.User : UserRole.Admin;
                user.Active = true;
                db.Accounts.Add(user);
                db.SaveChanges();
                LoggedInUser = user;
                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }


        // GET: Users/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Users/Login
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Username,Password")] User user)
        {
            User dbuser = db.Accounts.SingleOrDefault(a => a.Username == user.Username && a.Password == user.Password);
            if (dbuser != null)
            {
                LoggedInUser = dbuser;
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Je gebruikersnaam of wachtwoord klopt niet.");

            return View(user);
        }


        // GET: Users/Logout
        [AuthorizeLoggedIn]
        public ActionResult Logout()
        {
            return View(LoggedInUser);
        }

        // POST: Users/Logout
        [HttpPost]
        [AuthorizeLoggedIn, ActionName("Logout")]
        [ValidateAntiForgeryToken]
        public ActionResult LogoutConfirmed()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }




        // GET: Users
        [AuthorizeLoggedIn(true)]
        public ActionResult Index()
        {
            return View(db.Accounts.ToList());
        }

        // GET: Users/Details/5
        [AuthorizeLoggedIn(true)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Accounts.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        [AuthorizeLoggedIn(true)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeLoggedIn(true)]
        public ActionResult Create([Bind(Include = "UserId,Username,Password,FirstName,LastName,Role,Active")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        [AuthorizeLoggedIn(true)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Accounts.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeLoggedIn(true)]
        public ActionResult Edit([Bind(Include = "UserId,Username,Password,FirstName,LastName,Role,Active")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        [AuthorizeLoggedIn(true)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Accounts.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [AuthorizeLoggedIn(true)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Accounts.Find(id);
            db.Accounts.Remove(user);
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
