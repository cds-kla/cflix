using CFlix.ActiveDirectory.NETStandard;
using CFlix.Data;
using CFlix.Extensions;
using CFlix.Models;
using CFlix.Services;
using CFlix.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Linq;
using System.Net;

namespace CFlix
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
            services.AddDbContext<CFlixAuthContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            
            var identity = services.AddIdentity<CFlixUser, IdentityRole>(options =>
            {
                options.User.AllowedUserNameCharacters = "-._abcdefghijklmnopqrstuvwxyzé";
            });

            if (bool.TryParse(Configuration.GetSection("Cflix")["UseLdap"], out var useLdap) && useLdap)
            {
                services.AddActiveDirectory(Configuration.GetSection("ConnectionStrings").GetValue<string>("LdapUrl"));
                identity.AddSignInManager<CFlixSignInManager>();
            }

            identity.AddEntityFrameworkStores<CFlixAuthContext>()
                .AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AlphaUser",
                                  policy => policy.Requirements.Add(new AccountTypeRequirement(AccountType.Alpha)));
            });

            services.AddScoped<IAuthorizationHandler, AccountTypeAuthorizationHandler>();

            services.AddDbContext<CFlixContext>(options => options.UseMySql(Configuration.GetConnectionString("CflixConnection")));
            services.AddDbContext<CFlixROContext>(options => options.UseMySql(Configuration.GetConnectionString("CflixROConnection")));

            if (bool.TryParse(Configuration.GetSection("Cflix")["UseRedis"], out var useRedis) && useRedis)
            {
                var addresses = Dns.GetHostAddressesAsync(Configuration.GetConnectionString("Redis")).Result;
                var address = addresses.FirstOrDefault()?.MapToIPv4().ToString() ?? Configuration.GetConnectionString("Redis");

                var redis = ConnectionMultiplexer.Connect(address);

                services.AddDataProtection().PersistKeysToRedis(redis, "DataProtection-Keys");
                services.AddDistributedRedisCache(o =>
                {
                    o.Configuration = Configuration.GetConnectionString("Redis");
                    o.InstanceName = "CFlixRedis";
                });
            }

            services.ConfigureApplicationCookie(options =>
            {

                options.Cookie.Name = "CFlix.Authentication";
                options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Login");
                options.LogoutPath = new Microsoft.AspNetCore.Http.PathString("/Account/Logout");
                options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Error/403");

                // FAILLE XSS
                options.Cookie.HttpOnly = false;
            });

            services.AddMvc();

            services.AddTransient<IMediaRepository, MediaRepository>();
            services.AddTransient<IAchievementRepository, AchievementRepository>();
            services.AddSingleton<INewsReleaseRepository, NewsReleaseRepository>();
            services.AddSingleton<IViewerRepository, ViewerRepository>();
            services.Configure<CFlixConfiguration>(Configuration.GetSection("Cflix"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStatusCodePagesWithReExecute("/Error/{0}");

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
