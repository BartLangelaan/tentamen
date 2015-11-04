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
        private User _loggedInUser;
        public User LoggedInUser
        {
            get
            {
                if (_loggedInUser != null)
                    return _loggedInUser;

                return null;

                return (User) Session["userId"];
            }
            set
            {
                _loggedInUser = value;
                Session["UserId"] = value.UserId;
            }
        }
    }
}