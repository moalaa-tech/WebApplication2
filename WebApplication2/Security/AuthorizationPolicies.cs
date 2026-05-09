using Microsoft.AspNetCore.Authorization;

namespace CRM.WebApp.Security
{
    public static class AuthorizationPolicies
    {
        public const string RequireAdminRole = "RequireAdminRole";
        public const string RequireManagerRole = "RequireManagerRole";
        public const string RequireEmployeeRole = "RequireEmployeeRole";
        public const string RequireHRRole = "RequireHRRole";
        public const string RequireSalesRole = "RequireSalesRole";
        public const string RequireFinanceRole = "RequireFinanceRole";
        public const string RequireMarketingRole = "RequireMarketingRole";

        public static void AddPolicies(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(RequireAdminRole, policy =>
                    policy.RequireRole("Administrator"));

                options.AddPolicy(RequireManagerRole, policy =>
                    policy.RequireRole("Administrator", "Manager"));

                options.AddPolicy(RequireEmployeeRole, policy =>
                    policy.RequireRole("Administrator", "Manager", "Employee"));

                options.AddPolicy(RequireHRRole, policy =>
                    policy.RequireRole("Administrator", "HR"));

                options.AddPolicy(RequireSalesRole, policy =>
                    policy.RequireRole("Administrator", "Manager", "Sales"));

                options.AddPolicy(RequireFinanceRole, policy =>
                    policy.RequireRole("Administrator", "Finance"));

                options.AddPolicy(RequireMarketingRole, policy =>
                    policy.RequireRole("Administrator", "Marketing"));

                // Custom policy for data ownership
                options.AddPolicy("DataOwnership", policy =>
                    policy.Requirements.Add(new DataOwnershipRequirement()));
            });
        }
    }

    public class DataOwnershipRequirement : IAuthorizationRequirement
    {
    }

    public class DataOwnershipHandler : AuthorizationHandler<DataOwnershipRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            DataOwnershipRequirement requirement)
        {
            var userId = context.User.FindFirst("sub")?.Value ?? context.User.FindFirst("id")?.Value;
            
            if (context.Resource is IUserOwnedResource resource)
            {
                if (resource.UserId == userId || context.User.IsInRole("Administrator"))
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }

    public interface IUserOwnedResource
    {
        string UserId { get; }
    }
}