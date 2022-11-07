using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace DentaClinic.Auth
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly string _audienice;
        private readonly string _issuer;
        private readonly SymmetricSecurityKey _authSigningKey;

        public JwtTokenService(IConfiguration configuration)
        {
            _audienice = configuration.GetSection("ValidAudience").Value.ToString();
            _issuer = configuration.GetSection("ValidIssuer").Value.ToString();
            _authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Secret").Value.ToString()));
        }

        public string CreateAccessToken(string email, string userId, IEnumerable<string> userRoles)
        {
            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, email),
                new(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
                new(type: JwtRegisteredClaimNames.Sub, value: userId),
            };

            authClaims.AddRange(userRoles.Select(userRole => new Claim(type: ClaimTypes.Role, value: userRole)));

            var accessSecurityToken = new JwtSecurityToken
            (
                issuer: _issuer,
                audience: _audienice,
                expires: DateTime.UtcNow.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(_authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(accessSecurityToken);
        }
    }
}
