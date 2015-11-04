using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using s0895604.Models;

namespace s0895604.Controllers
{
    public class BaseController : Controller
    {
        protected DatabaseContext db = new DatabaseContext();

        private User _loggedInUser;
        public User LoggedInUser
        {
            get
            {
                // If is cached, return cache
                if (_loggedInUser != null)
                    return _loggedInUser;

                // If not logged in, return null
                if (Session["UserId"] == null) return null;

                // If logged in, return user
                int userId = (int) Session["UserId"];
                var user = db.Accounts.First(a => a.UserId == userId);
                _loggedInUser = user;
                return user;
            }
            set
            {
                _loggedInUser = value;
                Session["UserId"] = value.UserId;
                Session["UserRole"] = value.Role;
            }
        }

        public class AuthorizeLoggedInAttribute : AuthorizeAttribute
        {

            protected bool Admin;

            protected override bool AuthorizeCore(HttpContextBase httpContext)
            {
                if (httpContext.Session == null || httpContext.Session["UserId"] == null) return false;

                if (Admin)
                {
                    return (UserRole) httpContext.Session["UserRole"] == UserRole.Admin;
                }

                return true;
            }

            public AuthorizeLoggedInAttribute(bool admin = false)
            {
                Admin = admin;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.LoggedInUser = LoggedInUser; //Add whatever
            base.OnActionExecuting(filterContext);
        }

    }


}