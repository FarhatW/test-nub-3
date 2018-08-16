using System;
using System.Net;
using System.Reflection;
using System.Text;
using AutoMapper;
using jce.BusinessLayer.IManagers;
using jce.BusinessLayer.SaveData;
using jce.Common.Entites.IdentityServerDbContext;
using jce.Common.Resources.personProfile;
using jce.Common.Resources.user;
using jce.Common.Setting;
using jce.DataAccess.Core;
using jce.DataAccess.Core.dbContext;
using Managers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace jce.IdentityServer
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

            var appSettingsSection = Configuration.GetSection("AuthSetting");


            var appSettings = appSettingsSection.Get<AuthSetting>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            string connectionString = Configuration.GetConnectionString("IdentityServer");
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddScoped<IUserIdentiyManager, UsersIdentiyManager>();
            services.AddScoped<IRoleManager, RoleManager>();
//            services.AddScoped<IHistoryActionManager, HistoryActionManager>();
//            services.AddScoped<ISaveHistoryActionData, SaveHistoryActionData>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDbFactory, DbFactory>();

            services.AddScoped<IdentityServerDbContext>();
            services.AddScoped<IRepository<IdentityServerDbContext>, IdentityServerRepository>();
//
//            services.AddScoped<JceDbContext>();
//            services.AddScoped<IRepository<JceDbContext>, JceRepository>();

//            services.AddDbContext<JceDbContext>(option => 
//                option.UseSqlServer(Configuration.GetConnectionString("jce_live")));
//
            services.AddDbContext<IdentityServerDbContext>(option =>
                option.UseSqlServer(Configuration.GetConnectionString("IdentityServer")));

            services.AddIdentity<User, Role>()
                //.AddRoleManager<Role>()
                .AddEntityFrameworkStores<IdentityServerDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<AuthSetting>(appSettingsSection);

            services.AddCors();

            services.AddAutoMapper();

            services.AddMvc();
            services.ConfigureApplicationCookie(options =>
            {
          
                options.Cookie.Name = "identity";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
         
                // ReturnUrlParameter requires `using Microsoft.AspNetCore.Authentication.Cookies;`
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });

            // configure identity server with in-memory stores, keys, clients and scopes
            services.AddIdentityServer(options =>
                {
                    options.UserInteraction.LoginUrl = "http://localhost:5000/login";
                    options.UserInteraction.ConsentUrl = "/consent/";
                   // options.UserInteraction.LogoutUrl = "http://localhost:5000/logOut";
                })
           
                
                .AddDeveloperSigningCredential()
                .AddAspNetIdentity<User>()
                

                // this adds the config data from DB (clients, resources)
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));

                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30;
                });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseIdentityServer();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
