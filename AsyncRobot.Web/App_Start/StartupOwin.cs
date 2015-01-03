using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AsyncRobot.Web.StartupOwin))]

namespace AsyncRobot.Web {
    public class StartupOwin {
        public void Configuration(IAppBuilder app) {
            app.MapSignalR();
        }
    }
}
