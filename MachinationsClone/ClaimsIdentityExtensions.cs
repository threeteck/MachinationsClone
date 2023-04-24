using System;

namespace MachinationsClone
{
    public static class ClaimsIdentityExtensions
    {
        public static Guid GetId(this System.Security.Claims.ClaimsPrincipal principal)
        {
            var id = principal.FindFirst("id");
            return Guid.Parse(id.Value);
        }
        
        public static string GetIdString(this System.Security.Claims.ClaimsPrincipal principal)
        {
            var id = principal.FindFirst("id");
            return id.Value;
        }
    }
}