using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FiscalClientMVC.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Server.Data;
using Server.Models;
using Server.Services;
using Server.Hubs;

namespace Server
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<ChatContext>(
                options => options.UseSqlite("Data Source=chat.db"));

            //services.AddDbContext<ChatContext>(
            //    options => options.UseSqlServer(_config.GetConnectionString("Default")));

            services.AddIdentity<ChatUser, IdentityRole>(identityOptions => {
                identityOptions.User.RequireUniqueEmail = false;

                identityOptions.Password.RequiredLength = 4;
                identityOptions.Password.RequiredUniqueChars = 2;
                identityOptions.Password.RequireDigit = false;
                identityOptions.Password.RequireNonAlphanumeric = false;
                identityOptions.Password.RequireUppercase = false;
                identityOptions.Password.RequireLowercase = true;

                identityOptions.SignIn.RequireConfirmedEmail = false;
                identityOptions.SignIn.RequireConfirmedPhoneNumber = false;
            })
            .AddEntityFrameworkStores<ChatContext>();

            services.AddSingleton<HubUserService>();

            services.AddSignalR();


            services.AddCors(options => {
                options.AddPolicy("FromClient", p => p.WithOrigins("http://localhost:1337"));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ChatContext _context, UserManager<ChatUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ChatUser> signInManager)
        {
            _context.Database.EnsureDeleted();
            if (_context.Database.EnsureCreated())
            {
                SeedUsersAndRoles.CreateInitialUsers(userManager, roleManager, signInManager);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("FromClient");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapHub<ChatHub>("/chathub");
                endpoints.MapControllers();
            });
        }
    }
}
