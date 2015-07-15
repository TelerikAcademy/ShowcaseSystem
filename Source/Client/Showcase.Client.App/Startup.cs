[assembly: Microsoft.Owin.OwinStartup(typeof(Showcase.Client.App.Startup))]

namespace Showcase.Client.App
{
    using Owin;
    using Showcase.Server.Api;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            WebApiStartup.StartWebApi(app);
        }
    }
}