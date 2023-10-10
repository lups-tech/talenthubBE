using Microsoft.AspNetCore.Authorization;

namespace talenthubBE.Security
{
    public class HasScopeRequirement : IAuthorizationRequirement
    {
        public String Scope {get;}
        public String Issuer {get;}

        public HasScopeRequirement(string scope, string issuer)
        {
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
        }
    }
}