using Lesson1_BL.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Lesson1_BL.Auth
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly AuthOptions _authOptions;
        public TokenGenerator(IOptions<AuthOptions> options)
        {
            _authOptions = options.Value;
        }
        public string GenerateToken(string username, string role)
        {
            var identity = GetIdentity(username, role);

            var jwt = new JwtSecurityToken(
                    issuer: _authOptions.Issuer,
                    audience: _authOptions.Audience,
                    notBefore: DateTime.UtcNow,
                    claims: identity.Claims,
                    expires: DateTime.UtcNow.Add(
                        TimeSpan.FromSeconds(_authOptions.LifetimeInSeconds)),
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authOptions.Key)), 
                        SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public string GetClaimValueFromToken(string jwtToken, string claimsTypeToGet)
        {
            IdentityModelEventSource.ShowPII = true;

            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;

            validationParameters.ValidAudience = _authOptions.Audience;
            validationParameters.ValidIssuer = _authOptions.Issuer;
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authOptions.Key));

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);

            return principal.Claims.First(x => x.Type == claimsTypeToGet).Value;
        }

        private static ClaimsIdentity GetIdentity(string username, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, username),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
            };

            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }
    }
}
