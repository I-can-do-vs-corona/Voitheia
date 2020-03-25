using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ActiveCruzer.Startup
{
    public static class Swagger
    {
        public static void InitSwagger(this IServiceCollection self)
        {
            self.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Voitheia API",
                    Description = "Api for Voitheia. Developed for the WeVsVirus hackathon",
                    TermsOfService = new Uri("https://voitheia.org/terms")
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public static void InitSwagger(this IApplicationBuilder self)
        {
            self.UseSwagger();

            self.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}