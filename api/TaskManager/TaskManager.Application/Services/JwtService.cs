using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Interfaces.Services;
using TaskManager.Domain.Models.Request;
using TaskManager.Domain.Models.Response;

namespace TaskManager.Application.Services
{
    public class JwtService : IJwtService
    {
        private AuthResponse _authResponse;
        private readonly IAppSettingsConfigurations _appSettings;
        private readonly int TempoExpiracaoToken = 30;

        public JwtService(IAppSettingsConfigurations appSettings)
        {
            _authResponse = new AuthResponse();
            _appSettings = appSettings;
            string tempoExpiracaoAppSettings = _appSettings.GetJwtKeyExpirationTime();

            if (!string.IsNullOrEmpty(tempoExpiracaoAppSettings))
                TempoExpiracaoToken = int.Parse(tempoExpiracaoAppSettings);
        }

        public AuthResponse Get(AuthRequest model)
        {
            var jtiClaim = new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
            var _claims = new Claim[]
            {
                jtiClaim,
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
            };

            var now = DateTime.Now;
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.GetJwtKey()));

            var expiration = now.AddMinutes(TempoExpiracaoToken);

            JwtSecurityToken JwtToken = new JwtSecurityToken(
                claims: _claims,
                expires: expiration,
                notBefore: now,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );

            var encodedToken = new JwtSecurityTokenHandler().WriteToken(JwtToken);

            _authResponse.Success = true;
            _authResponse.Token = encodedToken;
            _authResponse.ExpiresInSeconds = TimeSpan.FromMinutes(TempoExpiracaoToken).TotalSeconds;

            return _authResponse;
        }
    }
}
