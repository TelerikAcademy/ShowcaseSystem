namespace Showcase.Server.Api.Infrastructure.Auth
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Security.OAuth;

    using Showcase.Data.Models;
    using Showcase.Services.Data;
    using Showcase.Services.Data.Contracts;

    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string publicClientId;
        private readonly IUsersService usersService;

        public ApplicationOAuthProvider(string publicClientId)
            :this(publicClientId, new UsersService()) // TODO: remove this, if possible
        {
        }

        public ApplicationOAuthProvider(string publicClientId, IUsersService usersService)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            this.publicClientId = publicClientId;
            this.usersService = usersService;
        }

        public static AuthenticationProperties CreateProperties()
        {
            return new AuthenticationProperties();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var user = await this.GetUserFromContext(context);
            if (user != null)
            {
                var oAuthIdentity = ClaimsIdentityFactory.Create(user, OAuthDefaults.AuthenticationType);
                var cookiesIdentity = ClaimsIdentityFactory.Create(user, CookieAuthenticationDefaults.AuthenticationType);

                AuthenticationProperties properties = CreateProperties();
                AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
                context.Validated(ticket);
                context.Request.Context.Authentication.SignIn(cookiesIdentity);
            }
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == this.publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        private async Task<User> GetUserFromContext(OAuthGrantResourceOwnerCredentialsContext context)
        {
            User user = null;

            if (this.IsValidContext(context))
            {
                user = await this.LoginUser(context);
            }

            return user;
        }

        private bool IsValidContext(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var isValid = true;

            if (context == null)
            {
                isValid = false;
            }
            else if (string.IsNullOrEmpty(context.UserName) || string.IsNullOrWhiteSpace(context.UserName))
            {
                context.SetError("invalid_grant", string.Format("Information is not valid"));
                isValid = false;
            }
            else if (string.IsNullOrEmpty(context.Password) || string.IsNullOrWhiteSpace(context.Password))
            {
                context.SetError("invalid_grant", string.Format("Information is not valid"));
                isValid = false;
            }

            return isValid;
        }

        private async Task<User> LoginUser(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var user = await this.usersService.GetAccountAsync(context.UserName, context.Password);

            // Check if remote login credentials are correct
            if (user == null)
            {
                context.SetError("invalid_grant", string.Format("Information is not valid"));
                return null;
            }

            return user;
        }
    }
}