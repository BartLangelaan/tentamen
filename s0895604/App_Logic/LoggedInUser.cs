using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using s0895604.Models;

namespace s0895604.App_Logic
{
    public static class LoggedInUser
    {
        private static User _user;
        public static User User
        {
            get
            {
                var sessionController = new SessionController();
                if (_user == null && sessionController.Session != null && sessionController.Session["UserId"] != null)
                {
                    // TODO: Find User from UserId
                }
                return _user;
            }
            set
            {
                var sessionController = new SessionController();
                sessionController.Session["UserId"] = value.UserId;
                _user = value;
            }
        }
    }
}