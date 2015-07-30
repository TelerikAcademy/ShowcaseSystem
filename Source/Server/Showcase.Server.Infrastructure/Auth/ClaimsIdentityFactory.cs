namespace Showcase.Server.Infrastructure.Auth
{
    using System;
    using System.Security.Claims;

    using Showcase.Data.Models;

    public static class ClaimsIdentityFactory
    {
        public static ClaimsIdentity Create(User user, string authenticationType)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            var id = new ClaimsIdentity(authenticationType, ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            id.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.String));
            id.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName, ClaimValueTypes.String));

            return id;
        }
    }
}