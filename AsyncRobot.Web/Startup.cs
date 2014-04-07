using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AsyncRobot.Web.Startup))]
namespace AsyncRobot.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
