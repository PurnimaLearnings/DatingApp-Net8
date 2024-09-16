using System.Security.Claims;

namespace API.Extension;

public static class ClaimsPrincipalExtensions
{
    public static string GetUserName(this ClaimsPrincipal user)
    {

        var username = user.FindFirstValue(ClaimTypes.NameIdentifier);

        if (username == null)
        {
            throw new Exception("Can not get username from the token.");
        }
        return username;
    }
}