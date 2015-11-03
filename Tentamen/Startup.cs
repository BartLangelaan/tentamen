using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tentamen.Startup))]
namespace Tentamen
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
