﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ChatApp.Web.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            IoCContainer.Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Add ApplicationDBContext to DI
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(IoCContainer.Configuration.GetConnectionString("DefaultConnection")));

            // AddIdentity adds cookie based authentication
            // Adds scoped classes for things like UserManager, SignInManager, PasswordHashers etc...
            // NOTE: Automatically adds the validated user from a cookie to the HttpContext.User
            services.AddIdentity<ApplicationUser, IdentityRole>()

                // Adds UserStore and RoleStore from this context
                //That are consumed by the UserManager and RoleManager
                .AddEntityFrameworkStores<ApplicationDbContext>()
                
                // Adds a provider that generates unique keys and hashed for things like
                // forgot password links, phone number verification code etc...
                .AddDefaultTokenProviders();

            // Add JWT Authentication for api clients
            services.AddAuthentication().
                AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = IoCContainer.Configuration["Jwt:Issuer"],
                        ValidAudience = IoCContainer.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(IoCContainer.Configuration["Jwt:SecretKey"])),
                    };
                });



            // Change password policy
            services.Configure<IdentityOptions>(options =>
            {
                // make weak passwrod
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            });

            // Change login URL
            services.ConfigureApplicationCookie(options =>
            {
                // Redirect to /login
                options.LoginPath = "/login";

                //Change cookie timeout
                options.ExpireTimeSpan = TimeSpan.FromSeconds(1500);

            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            // Store instance of the DI service provider so our application can access it anywhere
            IoCContainer.Provider = (ServiceProvider) serviceProvider;

            // Setup Identity
            app.UseAuthentication();
       

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/Home/Error");

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{moreInfo?}");

                routes.MapRoute(
                    name: "aboutPage",
                    template: "more",
                    defaults: new { controller = "About", action = "TellMeMore" });
            });
        }
    }
}
