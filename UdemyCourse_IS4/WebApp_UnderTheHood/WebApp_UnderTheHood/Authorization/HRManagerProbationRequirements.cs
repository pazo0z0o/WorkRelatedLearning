using Microsoft.AspNetCore.Authorization;

namespace WebApp_UnderTheHood.Authorization
{
    public class HRManagerProbationRequirements :IAuthorizationRequirement
    {
        public int ProbationMonths { get; }
        public HRManagerProbationRequirements(int probationMonths)
        {
            ProbationMonths = probationMonths;
        }

        public class HRManagerProbationRequirementsHandler : AuthorizationHandler<HRManagerProbationRequirements>
        {   //implemented abstract class 
            protected override Task HandleRequirementAsync(
                AuthorizationHandlerContext context,
                HRManagerProbationRequirements requirement)
            {
                if(!context.User.HasClaim(x=>x.Type == "EmploymentDate"))
                {
                    return Task.CompletedTask;
                }

                var empDate = DateTime.Parse(context.User.FindFirst(x => x.Type == "EmploymentDate").Value);

                var period = DateTime.Now - empDate;
                if (period.Days > 30 * requirement.ProbationMonths) 
                { context.Succeed(requirement); }

                return Task.CompletedTask;
            }
        }

    }
}
