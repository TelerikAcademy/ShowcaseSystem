namespace Showcase.Server.Api.Infrastructure.Auth
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.Owin;
    using Microsoft.Owin.Security.OAuth;

    using Owin;

    using Showcase.Data;

    public class AuthStartup
    {
        public static void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(ShowcaseDbContext.Create);

            // Configure the web api token endpoint
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/api/Account/Login"),

                // Configure the application for OAuth based flow
                Provider = new ApplicationOAuthProvider("self"),

                // Tokens are valid for 2 weeks
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),

                // TODO: Set to false in production !
#if DEBUG
                AllowInsecureHttp = true
#endif
            });

            // Configure the sockets tokens endpoint
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
            {
                Provider = new ApplicationOAuthBearerAuthenticationProvider(),
            });
        }
    }
}