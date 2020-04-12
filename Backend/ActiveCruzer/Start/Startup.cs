using System;
using System.Configuration;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using ActiveCruzer.BLL;
using ActiveCruzer.DAL.DataContext;
using ActiveCruzer.Models;
using ActiveCruzer.Scheduler;
using ActiveCruzer.Startup;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pomelo.EntityFrameworkCore.MySql;

namespace ActiveCruzer.Start
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public static string BingKey;
        private static IConfiguration _configuration;

        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json",
                    optional: false,
                    reloadOnChange: true)
                .AddEnvironmentVariables();

            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // add service from mysql framework
            services.AddDbContext<ACDatabaseContext>(options =>
                options.UseMySql(_configuration.GetValue<string>("ActiveCrzuerDB-ConnectionString")));

            services.AddIdentityCore<User>(o =>
                {
                    o.Password.RequireDigit = true;
                    o.Password.RequireLowercase = false;
                    o.Password.RequireUppercase = false;
                    o.Password.RequireNonAlphanumeric = false;
                    o.Password.RequiredLength = 8;
                    o.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<ACDatabaseContext>()
                .AddDefaultTokenProviders();

            // register services for interface and related bll
            services.AddTransient<IMyRequestsBll, MyRequestBll>();
            services.AddTransient<IRequestBll, RequestBll>();
            services.AddTransient<IEmailSenderBll, EmailSenderBll>();
            services.AddTransient<UserBLL>();

            services.AddHostedService<ScheduleTask>();

            services.InitJwt(_configuration);

            services.AddControllers();
            services.AddAutoMapper(GetType().Assembly);

            services.AddCors(o => o.AddPolicy(MyAllowSpecificOrigins, builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.InitSwagger();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.InitSwagger();

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
    }
}