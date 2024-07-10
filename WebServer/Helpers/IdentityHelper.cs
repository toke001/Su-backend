using System.Security.Claims;

namespace WebServer.Helpers
{
    public static class IdentityHelper
    {
        public static string? GetClaim(this HttpContext context, string claimName)
        {
            var identity = context.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userIdClaim = identity.FindFirst(claimName);
                if (userIdClaim != null)
                {
                    return userIdClaim.Value.ToString();
                }
            }
            return null;
        }

        public static int GetAccessLevel(this HttpContext context)
        {
            var identity = context.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var katoCodeClaim = identity.FindFirst("kcd");
                if (katoCodeClaim != null)
                {
                    ///100000000
                    ///10 00 00 000
                    if (katoCodeClaim.Value.Contains("0000000")) //область
                    {
                        return 1;
                    } else
                    if (katoCodeClaim.Value.Contains("00000")) // район
                    {
                        return 2;
                    } else
                    if (katoCodeClaim.Value.Contains("000")) // город
                    {
                        return 3;
                    } else
                    if (katoCodeClaim.Value.Contains("00")) // с.о.
                    {
                        return 4;
                    } else
                    return 5;
                }
            }
            return 0;
        }
    }
}
