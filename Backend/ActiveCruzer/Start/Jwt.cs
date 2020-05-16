using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ActiveCruzer.Startup
{
    public static class Jwt
    {
        private static IConfiguration _configuration;

        public static void InitJwt(this IServiceCollection self, IConfiguration  configuration)
        {
            _configuration = configuration;

            self.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var jwtAuthentication = new JwtAuthentication();

                    options.ClaimsIssuer = jwtAuthentication.ValidIssuer;
                    options.IncludeErrorDetails = true;
                    options.RequireHttpsMetadata = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateActor = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtAuthentication.ValidIssuer,
                        ValidAudience = jwtAuthentication.ValidAudience,
                        IssuerSigningKey = jwtAuthentication.SymmetricSecurityKey,
                        NameClaimType = ClaimTypes.NameIdentifier
                    };
                });
        }

        public class JwtAuthentication
        {
            public string SecurityKey { get; set; } = _configuration["JwPee"];
            public string ValidIssuer { get; set; } = _configuration["Jwt:Issuer"];
            public string ValidAudience { get; set; } = _configuration["Jwt:Audience"];
            public SymmetricSecurityKey SymmetricSecurityKey => new SymmetricSecurityKey(Convert.FromBase64String(SecurityKey));
            public SigningCredentials SigningCredentials => new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        }
    }
}