using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TestBookCatalog.Models
{
    internal static class JwtOptions
    {
        private const string issuer = "TestBookCatalog";
        private const string audience = "TestBookCatalog_audience";
        private const string key = "SUPERPUPERSECRETKEY";
        /// <summary>
        /// 10 minutes
        /// </summary>
        private const int lifeTime = 600;

        internal static ValidateResponse BuildValidateResponse(User user)
        {
            var token = GetJwtSecurityToken(user);

            return new ValidateResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = user.RefreshToken,
                Expiration = token.ValidTo,
                UserId = user.Id
            };
        }

        internal static TokenValidationParameters BuildTokenParameters()
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateAudience = true,
                ValidAudience = audience,
                ValidateLifetime = true,
                IssuerSigningKey = GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true,
                RequireExpirationTime = true,
                ClockSkew = TimeSpan.FromSeconds(5)
            };
        }

        private static JwtSecurityToken GetJwtSecurityToken(User user)
        {
            var identity = GetIdentity(user);

            var now = DateTime.UtcNow;
            return new JwtSecurityToken(
                   issuer: issuer,
                   audience: audience,
                   notBefore: now,
                   claims: identity.Claims,
                   expires: now.Add(TimeSpan.FromSeconds(lifeTime)),
                   signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha512));
        }

        private static ClaimsIdentity GetIdentity(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString()),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            return new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimTypes.Role);
        }

        private static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }
    }
}
