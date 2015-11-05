using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using s0895604.Models;

namespace s0895604
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            // Only uncomment this if you want a force-reset of the database
            // Database.SetInitializer(new DropCreateDatabaseAlways<DatabaseContext>());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
