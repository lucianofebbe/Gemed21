using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IP.Cryptography.TokenCryptography
{
    public class TokenCryptography : ITokenCryptography
    {
        private readonly IJwtConfiguration _jwtConfiguration;

        public TokenCryptography(IJwtConfiguration jwtConfiguration)
        {
            _jwtConfiguration = jwtConfiguration;
        }

        public Task<string> GenerateToken(string value)
        {
            return GenerateTokenAsync(value);
        }

        public Task<string> GenerateToken(int value)
        {
            return GenerateTokenAsync(value.ToString());
        }

        private Task<string> GenerateTokenAsync(string value)
        {
            var issuer = _jwtConfiguration.AuthIssuer;
            var audience = _jwtConfiguration.AuthAudience;
            var secret = _jwtConfiguration.AuthSecret;
            var claims = new List<Claim>()
        {
            new("IdUsuario", value.ToString())
        };

            var tokenHandler = new JwtSecurityTokenHandler();
            return Task.FromResult(tokenHandler
                .WriteToken(
                    new JwtSecurityToken(
                        issuer: issuer,
                        audience: audience,
                        claims: claims,
                        expires: DateTime.Now.Add(TimeSpan.FromMinutes(120)),
                        signingCredentials: ObterSigningCredentias(secret).Result
                    )));
        }

        private Task<SigningCredentials> ObterSigningCredentias(string segredo)
        {
            return Task.FromResult(new SigningCredentials(
                ObterChaveSimetrica(segredo),
                SecurityAlgorithms.HmacSha256));
        }

        private SymmetricSecurityKey ObterChaveSimetrica(string segredo) =>
            new(Encoding.UTF8.GetBytes(segredo));
    }
}
