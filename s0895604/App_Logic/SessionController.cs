using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace s0895604.App_Logic
{
    public class SessionController : Controller
    {
        public HttpSessionStateBase GetSession()
        {
            return Session;
        }
    }
}