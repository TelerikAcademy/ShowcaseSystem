namespace Showcase.Server.Infrastructure.Auth
{
    using System.Threading.Tasks;

    using Microsoft.Owin.Security.OAuth;

    public class ApplicationOAuthBearerAuthenticationProvider : OAuthBearerAuthenticationProvider
    {
        public override Task RequestToken(OAuthRequestTokenContext context)
        {
            if (string.IsNullOrEmpty(context.Token))
            {
                // try to find bearer token in a cookie 
                var tokenCookie = context.OwinContext.Request.Cookies["authentication"];
                if (!string.IsNullOrEmpty(tokenCookie))
                {
                    context.Token = tokenCookie;
                }
                else
                {
                    // try to find bearer token in the standard Authorization header - should not occur for the web app.
                    var tokenHeader = context.OwinContext.Request.Headers["Authorization"];
                    if (!string.IsNullOrEmpty(tokenHeader))
                    {
                        context.Token = tokenHeader;
                    }
                }
            }

            return Task.FromResult<object>(null);
        }
    }
}