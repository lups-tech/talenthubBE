using Microsoft.AspNetCore.Authorization;

namespace talenthubBE.Security
{
    // HasScopeHandler.cs

    public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == "permissions" && c.Issuer == requirement.Issuer && c.Value == requirement.Scope))
            {
            context.Succeed(requirement);
            return Task.CompletedTask;
            }
            
            if (!context.User.HasClaim(c => c.Type == "scope" && c.Issuer == requirement.Issuer))
                return Task.CompletedTask;

            var scopes = context.User.FindFirst(c => c.Type == "scope" && c.Issuer == requirement.Issuer)!.Value.Split(' ');
            
            if (scopes.Any(s => s == requirement.Scope))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}