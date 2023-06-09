using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.AuthorizationRequirements
{
    public class CustomRequireClaim : IAuthorizationRequirement
    {


        public CustomRequireClaim(string claimType)
        {
            ClaimType = claimType;
        }

        public string ClaimType { get; }



        public class CustomRequireClaimHandler : AuthorizationHandler<CustomRequireClaim>
        {
            protected override Task HandleRequirementAsync(
                AuthorizationHandlerContext context,
                CustomRequireClaim requirement)
            {
                context.User.Claims
            }
        }
    }
}
