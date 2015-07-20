namespace Showcase.Server.Api.Infrastructure.Auth
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Security.OAuth;

    using Showcase.Data.Models;
    using Showcase.Services.Logic;
    using Showcase.Services.Logic.Contracts;

    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string publicClientId;
        private readonly IAccountProvider accountProvider;

        public ApplicationOAuthProvider(string publicClientId)
            :this(publicClientId, new AccountProvider()) // TODO: remove this, if possible
        {
        }

        public ApplicationOAuthProvider(string publicClientId, IAccountProvider accountProvider)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            this.publicClientId = publicClientId;
            this.accountProvider = accountProvider;
        }

        public static AuthenticationProperties CreateProperties()
        {
            return new AuthenticationProperties();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            var user = await this.GetUserFromContext(context, userManager);

            if (user != null)
            {
                var oAuthIdentity = await user.GenerateUserIdentityAsync(userManager, OAuthDefaults.AuthenticationType);
                var cookiesIdentity = await user.GenerateUserIdentityAsync(userManager, CookieAuthenticationDefaults.AuthenticationType);

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

        private async Task<User> GetUserFromContext(OAuthGrantResourceOwnerCredentialsContext context, ApplicationUserManager userManager)
        {
            User user = null;

            if (this.IsValidContext(context))
            {
                user = await this.LoginUser(context, userManager);
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

        private async Task<User> LoginUser(OAuthGrantResourceOwnerCredentialsContext context, ApplicationUserManager userManager)
        {
            var user = await userManager.FindByNameAsync(context.UserName);
            user = await userManager.FindAsync(context.UserName, context.Password);
            if (user == null)
            {
                user = this.accountProvider.GetAccount(context.UserName, context.Password);
                if (user == null)
                {
                    context.SetError("invalid_grant", string.Format("Information is not valid"));
                    return null;
                }

                var result = await userManager.CreateAsync(user, context.Password);
                this.ValidateIdentityResult(result, context);
                if (context.HasError)
                {
                    return null;
                }
            }

            return user;

            //var userEmail = User.GetUserEmail(context.UserName);
            //var user = await userManager.FindAsync(userEmail, context.Password);

            //if (user != null && !string.IsNullOrEmpty(user.Authentication.SecondPassword))
            //{
            //    var formData = await context.Request.ReadFormAsync();
            //    var secondPassword = formData["SecondPassword"];
            //    if (!DataValidator.SecondPasswordIsCorrect(user, secondPassword))
            //    {
            //        context.SetError("invalid_grant", "Invalid second password");
            //        user = null;
            //    }
            //}

            //return user;
        }

        private void ValidateContext(OAuthGrantResourceOwnerCredentialsContext context)
        {
            if (context == null || string.IsNullOrEmpty(context.UserName) || string.IsNullOrWhiteSpace(context.UserName))
            {
                context.SetError("invalid_grant", "The username is invalid");
            }
        }

        private void ValidateIdentityResult(IdentityResult result, OAuthGrantResourceOwnerCredentialsContext context)
        {
            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    var error = result.Errors.FirstOrDefault();
                    if (error != null)
                    {
                        context.SetError("error", error);
                    }
                }
            }
        }
    }
}