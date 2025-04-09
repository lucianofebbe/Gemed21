using IP.Cryptography.TokenCryptography.TokenCryptographySettings;
using IP.Logs.LogsFactory;
using IP.Logs.LogsSettings;
using IP.MongoDb.MongoDbConfig;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IP.Cryptography.TokenCryptography
{
    public class TokenCryptography : ITokenCryptography
    {
        private readonly ILogsFactory logsFactory;


        public TokenCryptography(ILogsFactory iGenerateLogs)
        {
            this.logsFactory = iGenerateLogs;
        }

        public Task<string> GenerateToken(JwtConfigurationSettings settings)
        {
            try
            {
                return GenerateTokenAsync(settings);
            }
            catch (Exception ex)
            {
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
            }
        }

        private Task<string> GenerateTokenAsync(JwtConfigurationSettings settings)
        {
            try
            {
                var issuer = settings.AuthIssuer;
                var audience = settings.AuthAudience;
                var secret = settings.AuthSecret;
                var claims = new List<Claim>()
        {
            new("IdUsuario", settings.value)
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
            catch (Exception ex)
            {
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
            }
        }

        private Task<SigningCredentials> ObterSigningCredentias(string segredo)
        {
            try
            {
                return Task.FromResult(new SigningCredentials(
                    ObterChaveSimetrica(segredo),
                    SecurityAlgorithms.HmacSha256));
            }
            catch (Exception ex)
            {
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
            }
        }

        private SymmetricSecurityKey ObterChaveSimetrica(string segredo) =>
            new(Encoding.UTF8.GetBytes(segredo));
    }
}
