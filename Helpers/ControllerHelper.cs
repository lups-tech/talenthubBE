using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace talenthubBE.Helpers
{
    public static class ControllerHelper
    {
        public static String OrgIdFinder(ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }
        public static String UserIdFinder(ClaimsPrincipal user)
        {
            return user.Claims.Single(c => c.Type == "org_id").Value;
        }
    }
}