using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AuthServer.Core.Data;
using AuthServer.Core.Models;
using AuthServer.Core.Services;
using IdentityServer4.AspNetIdentity;
using IdentityServer4.Hosting;

namespace AuthServer.Core
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddCors();
            
            services.AddMvc();

            services.AddIdentityServer()
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddAspNetIdentity<ApplicationUser>()
                .AddProfileService<CustomProfileService>()
                .AddDeveloperSigningCredential();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            
            app.UseCors(policy =>
            {
                policy.AllowAnyOrigin();
                policy.AllowAnyMethod();
                policy.AllowAnyHeader();
            });

            app.UseStaticFiles();

            app.UseIdentityServer();
            MigrateDatabase(app);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void MigrateDatabase(IApplicationBuilder app)
        {
            var scopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var appDbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

                //appDbContext.Database.EnsureDeleted();
                appDbContext.Database.Migrate();

                var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
                roleManager.CreateAsync(new IdentityRole("Moderator")).Wait();

                var adminRole = roleManager.FindByNameAsync("Admin").Result;
                var moderatorRole = roleManager.FindByNameAsync("Moderator").Result;

                roleManager.AddClaimAsync(adminRole, new Claim("CanAccessPage", "")).Wait();
                roleManager.AddClaimAsync(adminRole, new Claim("CanAddBooks", "")).Wait();
                roleManager.AddClaimAsync(moderatorRole, new Claim("CanAccessPage", "")).Wait();

                if (userManager.Users.Any()) return;
                foreach (var user in Config.GetUsers())
                {
                    userManager.CreateAsync(user, "Password123!").Wait();
                    if (user.UserName.Equals("Admin"))
                    {
                        userManager.AddToRoleAsync(user, "Admin").Wait();
                    }
                    else if (user.UserName.Equals("Moderator"))
                    {
                        userManager.AddToRoleAsync(user, "Moderator").Wait();
                    }
                }
            }
        }
    }
}
