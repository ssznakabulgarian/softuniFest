using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebMonitoringApi.Data;
using WebMonitoringApi.Data.Models;
using WebMonitoringApi.Services;

namespace WebMonitoringApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => 
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUserService, UserService>();

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                     {
                         options.SignIn.RequireConfirmedEmail = false;
                     })
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            services.AddIdentityServer()
                    .AddDeveloperSigningCredential()
                    .AddInMemoryPersistedGrants()
                    .AddInMemoryIdentityResources(Config.GetIdentityResources())
                    .AddInMemoryApiScopes(Config.GetAPIScopes())
                    .AddInMemoryApiResources(Config.GetAPIs())
                    .AddInMemoryClients(Config.GetClients())
                    .AddAspNetIdentity<ApplicationUser>();

            services.AddAuthentication("Bearer")
                    .AddJwtBearer("Bearer",
                                  options =>
                                  {
                                      options.Authority = "https://localhost:44340/";

                                      options.Audience = "WebMonitoringApi";

                                      //options.RequireHttpsMetadata = false;

                                      options.TokenValidationParameters.ValidateIssuer = false;
                                  });

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                                       .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                                       .RequireAuthenticatedUser()
                                       .Build();
            });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseIdentityServer();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
