using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rawpotion.Identity.Account.DependencyInjection;
using Rawpotion.Identity.Common;
using Rawpotion.Identity.Common.Extensions;
using Rawpotion.Identity.Configuration;
using Rawpotion.Identity.Data;
using Rawpotion.Identity.Data.Models;
using Rawpotion.Identity.Senders;

namespace Rawpotion.Identity
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(_configuration.GetConnectionString("Rawpotion.Identity.Connection"));
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "Something";
                    options.ClientSecret = "Something Else";
                });

            services.AddSingleton<IEmailSender, EmailSender>();
            
            var builder = services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;

                    options.EmitStaticAudienceClaim = true;

                    options.UserInteraction.LoginUrl = "~/identity/account/login";
                    options.UserInteraction.LogoutUrl = "~/identity/account/logout";
                    options.UserInteraction.ErrorUrl = "~/identity/account/error";
                })
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients)
                .AddAspNetIdentity<ApplicationUser>();

            builder.AddDeveloperSigningCredential();
            
            services.AddRazorPages();

            services.AddRawpotionService(options =>
            {
                options.AddRawpotionAccount();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseStaticFiles();
            
            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseRawpotionService();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }
    }
}