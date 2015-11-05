using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        protected static Random rand = new Random();

        protected static string LoremIpsum(int minWords, int maxWords,
            int minSentences, int maxSentences,
            int numParagraphs)
                {

                    var words = new[]{"lorem", "ipsum", "dolor", "sit", "amet", "consectetuer",
                "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod",
                "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat"};

                    
                    int numSentences = rand.Next(maxSentences - minSentences)
                        + minSentences + 1;
                    int numWords = rand.Next(maxWords - minWords) + minWords + 1;

                    StringBuilder result = new StringBuilder();

                    for (int p = 0; p < numParagraphs; p++)
                    {
                        for (int s = 0; s < numSentences; s++)
                        {
                            for (int w = 0; w < numWords; w++)
                            {
                                if (w > 0) { result.Append(" "); }
                                result.Append(words[rand.Next(words.Length)]);
                            }
                            result.Append(". ");
                        }
                        result.Append("\n");
                    }

                    return result.ToString();
                }

    }


}