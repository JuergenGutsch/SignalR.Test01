using Microsoft.Owin;
using Owin;
using SignalR.Test01;

[assembly: OwinStartup(typeof(Startup), "Configuration")]
namespace SignalR.Test01
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureSignalR(app);
        }
        public static void ConfigureSignalR(IAppBuilder app)
        {
            // For more information on how to configure your application using OWIN startup, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.MapSignalR();
        }
    }
}