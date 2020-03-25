using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ActiveCruzer.Startup
{
    public static class Jwt
    {
        public static void InitJwt(this IServiceCollection self)
        {
            self.AddSingleton<IPostConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>();

            self.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();
        }

        private class ConfigureJwtBearerOptions : IPostConfigureOptions<JwtBearerOptions>
        {
            public void PostConfigure(string name, JwtBearerOptions options)
            {
                var jwtAuthentication = new JwtAuthentication
                {
                    SecurityKey = "ouNtF8Xds1jE55/d+iVZ99u0f2U6lQ+AHdiPFwjVW3o=",
                    ValidAudience = "https://localhost:44314/",
                    ValidIssuer = "https://localhost:44314/"
                };


                options.ClaimsIssuer = jwtAuthentication.ValidIssuer;
                options.IncludeErrorDetails = true;
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateActor = false,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = false,
                    ValidIssuer = jwtAuthentication.ValidIssuer,
                    ValidAudience = jwtAuthentication.ValidAudience,
                    IssuerSigningKey = jwtAuthentication.SymmetricSecurityKey,
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            }
        }

        public class JwtAuthentication
        {
            public string SecurityKey { get; set; }
            public string ValidIssuer { get; set; }
            public string ValidAudience { get; set; }
            public SymmetricSecurityKey SymmetricSecurityKey => new SymmetricSecurityKey(Convert.FromBase64String(SecurityKey));
            public SigningCredentials SigningCredentials => new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        }
    }
}