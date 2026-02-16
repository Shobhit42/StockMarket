using System.Security.Claims;

namespace StockMarket.Extentions
{
    public static class ClaimsExtentions
    {
        public static string GetUsername(this ClaimsPrincipal principal)
        {
            return principal.Claims.SingleOrDefault(x => x.Type.Equals("")).Value;
        }
    }
}
