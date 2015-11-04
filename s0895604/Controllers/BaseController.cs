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
                if (_loggedInUser != null)
                    return _loggedInUser;

                if (Session["UserId"] == null) return null;
                int userId = (int) Session["UserId"];
                var user = db.Accounts.First(a => a.UserId == userId);
                _loggedInUser = user;
                return user;
            }
            set
            {
                _loggedInUser = value;
                Session["UserId"] = value.UserId;
            }
        }

        public class AuthorizeLoggedInAttribute : AuthorizeAttribute
        {

            protected override bool AuthorizeCore(HttpContextBase httpContext)
            {
                if (httpContext.Session["UserId"] != null)
                {
                    return true;
                }
                return false;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.LoggedInUser = LoggedInUser; //Add whatever
            base.OnActionExecuting(filterContext);
        }

    }


}