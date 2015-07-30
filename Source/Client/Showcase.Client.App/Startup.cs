[assembly: Microsoft.Owin.OwinStartup(typeof(Showcase.Client.App.Startup))]

namespace Showcase.Client.App
{
    using Owin;
    using Showcase.Server.Api;
    using Showcase.Server.Infrastructure.Auth;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AuthStartup.ConfigureAuth(app);
            WebApiStartup.StartWebApi(app);
        }
    }
}