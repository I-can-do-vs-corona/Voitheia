using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using ActiveCruzer.DAL.DataContext;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace ActiveCruzer
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public static string BingKey;
        public static string SqlConnectionStringBuilder;
        private static IConfiguration _configuration;
        private string _connectionString;

        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json",
                    optional: false,
                    reloadOnChange: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
            _configuration = configuration;
            _connectionString = _configuration["ActiveCrzuerDB-ConnectionString"];
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ACDatabaseContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString(_connectionString)));
            services.Configure<JwtAuthentication>(Configuration.GetSection("JwtAuthentication"));
            services.AddSingleton<IPostConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();

            services.AddControllersWithViews();

            services.AddAutoMapper(GetType().Assembly);

            services.AddCors(o => o.AddPolicy(MyAllowSpecificOrigins, builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            RegisterSwaggerGen(services);

            BingKey = _configuration["RingumsBing"];
        }

        private static void RegisterSwaggerGen(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Voithea API",
                    Description = "Voll krasse Voithea api",
                    TermsOfService = new Uri("https://voithea.org/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Gerd Wagner",
                        Email = "gerd.wagner@voithea.org",
                        Url = new Uri("https://twitter.com/Gerdi"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "I will geld",
                        Url = new Uri("https://voithea.org/license"),
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.DescribeAllEnumsAsStrings();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            ConfigureSwaggerGen(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(MyAllowSpecificOrigins);


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        }

        private static void ConfigureSwaggerGen(IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
        }

        private class ConfigureJwtBearerOptions : IPostConfigureOptions<JwtBearerOptions>
        {
            private readonly IOptions<JwtAuthentication> _jwtAuthentication;

            public ConfigureJwtBearerOptions(IOptions<JwtAuthentication> jwtAuthentication)
            {
                _jwtAuthentication = jwtAuthentication ?? throw new System.ArgumentNullException(nameof(jwtAuthentication));
            }

            public void PostConfigure(string name, JwtBearerOptions options)
            {
                var jwtAuthentication = _jwtAuthentication.Value;

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
    }
}