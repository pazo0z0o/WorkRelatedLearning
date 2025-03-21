﻿using Microsoft.AspNetCore.Authorization;

namespace WebApp_UnderTheHood.Authorization
{
    public class HRManagerProbationRequirement : IAuthorizationRequirement
    {
        //The interface is needed for EVERY Custom Requirement we want to make
        public HRManagerProbationRequirement(int probationMonths)
        {
            ProbationMonths = probationMonths;
        }
        public int ProbationMonths { get; set; }
    }
    //needs to implement the abstract class in the Requirement class                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        AND THEN pass it to the AuthHandler
    public class HRManagerProbationRequirementHandler : AuthorizationHandler<HRManagerProbationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HRManagerProbationRequirement requirement)
        {
            if (context.User.HasClaim(x=> x.Type == "EmploymentDate"))
            {
                return Task.CompletedTask;
            }

            if(DateTime.TryParse(context.User.FindFirst(x => x.Type == "EmploymentDate")?.Value,out DateTime employmentDate)) 
            {
                var period = DateTime.Now - employmentDate;
                if (period.Days > 30 * requirement.ProbationMonths) {
                    context.Succeed(requirement);
                }

            }
            return Task.CompletedTask;
        }
    }
}
