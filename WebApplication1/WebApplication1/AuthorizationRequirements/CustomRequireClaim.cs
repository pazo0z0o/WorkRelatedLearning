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

        public  class CustomRequireClaimHandler : AuthorizationHandler<CustomRequireClaim>
        {
            public CustomRequireClaimHandler()
            {
                
            }
            protected override Task HandleRequirementAsync(
                AuthorizationHandlerContext context,
                CustomRequireClaim requirement)
            {
                var hasClaim = context.User.Claims.Any(x => x.Type == requirement.ClaimType);
                if (hasClaim)
                {
                    context.Succeed(requirement);
                    
                }
                return Task.CompletedTask;
            }
        }
    }
}
