using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace TaskManager.Extensions
{
    public static class JsonWebToken
    {
        public static void AddJsonWebToken(this IServiceCollection services, IConfiguration Configuration)
        {
            byte[] keyAsBytes = Encoding.ASCII.GetBytes(Configuration.GetSection("JwtConfigurations:key").Value);
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(keyAsBytes),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    TokenDecryptionKey = new SymmetricSecurityKey(keyAsBytes),
                    ClockSkew = TimeSpan.FromMinutes(0)
                };
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
        }
    }
}
