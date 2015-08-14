namespace Showcase.Server.Infrastructure.Auth
{
    using System;

    using Microsoft.Owin;
    using Microsoft.Owin.Security.OAuth;

    using Owin;

    public class AuthStartup
    {
        public static void ConfigureAuth(IAppBuilder app)
        {
            // Configure the web api token endpoint
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/api/Users/Login"),

                // Configure the application for OAuth based flow
                Provider = new ApplicationOAuthProvider("self"),

                // Tokens are valid for 2 weeks
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),

                // TODO: Set to false in production !
                AllowInsecureHttp = true
            });

            // Configure the sockets tokens endpoint
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
            {
                Provider = new ApplicationOAuthBearerAuthenticationProvider(),
            });
        }
    }
}