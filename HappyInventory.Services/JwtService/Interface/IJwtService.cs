using HappyInventory.Models.Models.Identity;
using System.Security.Claims;

namespace HappyInventory.Services.JwtService.Interface
{
    public  interface IJwtService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
