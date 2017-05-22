using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using AspNet.Security.OpenIdConnect.Primitives;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddMvc();
            services.AddDbContext<Models.TESTContext>(options =>
            {
                options.UseSqlServer(Configuration["AppSettings:ConnectionString"]);
                options.UseOpenIddict();
            });

            services.AddOpenIddict(options =>
            {
                options.AddEntityFrameworkCoreStores<TESTContext>();
                options.AddMvcBinders();
                options.EnableTokenEndpoint("/connect/token");
                options.AllowPasswordFlow().AllowRefreshTokenFlow();
                options.SetAccessTokenLifetime(TimeSpan.FromMinutes(1));
                options.SetRefreshTokenLifetime(TimeSpan.FromMinutes(2));
                options.DisableHttpsRequirement();
                options.UseJsonWebTokens();                
                options.AddSigningKey(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["AppSettings:Jwt:SigningKey"])));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseDeveloperExceptionPage();
            app.UseOpenIddict();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

            string Authority = Configuration["AppSettings:Jwt:Issuer"];
            string Audience = Configuration["AppSettings:Jwt:Audience"];

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                Authority = Configuration["AppSettings:Jwt:Issuer"],
                Audience = Configuration["AppSettings:Jwt:Audience"],

                RequireHttpsMetadata = false,
                TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = OpenIdConnectConstants.Claims.Subject,
                    RoleClaimType = OpenIdConnectConstants.Claims.Role,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["AppSettings:Jwt:SigningKey"]))
                }
            });

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvcWithDefaultRoute();
            app.UseWelcomePage();
        }
    }
}
