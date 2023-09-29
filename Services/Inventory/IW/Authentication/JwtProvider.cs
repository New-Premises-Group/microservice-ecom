using IW.Interfaces;
using IW.Models.DTOs;
using IW.Models.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IW.Authentication
{
    internal sealed class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;
        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }
        public string Generate(User user)
        {
            //Custom claims use to authorize api
            var claims = new Claim[] {
                new Claim(JwtRegisteredClaimNames.Sid, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.Name),
                new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString().ToUpper()),
                new Claim(ClaimTypes.Role,user.Role.Name)
            };

            var signingCreadentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                _options.Issuer,
                _options.Audience,
                claims,
                null,
                DateTime.UtcNow.AddDays(3),
                signingCreadentials);

            string tokenValue= new JwtSecurityTokenHandler()
                .WriteToken(token);

            return tokenValue;
        }
    }
}
