using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebApplication1.AuthorizationRequirements
{
    public static class AuthorizationPolicyBuilderExtensions
    {
        public static AuthorizationPolicyBuilder RequireCustomClaim(
            this AuthorizationPolicyBuilder builder,
            string claimtype)
        {


        builder.AddRequirements(new CustomRequireClaim(claimtype));
            return builder;
        }

    }
}
