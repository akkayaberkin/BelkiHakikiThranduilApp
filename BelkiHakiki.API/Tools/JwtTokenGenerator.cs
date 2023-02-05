using Microsoft.IdentityModel.Tokens;
using BelkiHakiki.Core;
using BelkiHakiki.Core.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BelkiHakiki.API.Tools
{
    public class JwtTokenGenerator
    {
        public static string GenerateToken(AppUsers appUser)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenSettings.Key));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, appUser.AppRole));
            claims.Add(new Claim(ClaimTypes.Name, appUser.Username));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()));
            //Roller geliyorsa dto içinde gelen  rolleri foreach ile dönebiliriz.
            
            JwtSecurityToken token = new JwtSecurityToken(issuer:JwtTokenSettings.Issuer,audience:JwtTokenSettings.Audience,
                                                        claims:claims,DateTime.UtcNow,DateTime.UtcNow.AddDays(JwtTokenSettings.Expire),
                                                        signingCredentials:signingCredentials);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            
            return handler.WriteToken(token);
        }
    }
}
