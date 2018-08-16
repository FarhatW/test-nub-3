using System.IO;
using System.Text;
using AutoMapper;
using jce.BusinessLayer.Core;
using jce.BusinessLayer.Exceptions;
using jce.BusinessLayer.IEnumManagers;
using jce.BusinessLayer.IManagers;
using jce.BusinessLayer.SaveData;
using jce.Common.Resources.adminProfile;
using jce.Common.Resources.personProfile;
using jce.Common.Setting;
using jce.DataAccess.Core;
using jce.DataAccess.Core.dbContext;
using Managers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace jce.BackOffice
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _env = env;

            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }
        private IHostingEnvironment _env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton(Configuration);



            services.AddScoped<IJceProfileManager, JceProfileManager>();
            services.AddScoped<IChildManager, ChildManager>();
            services.AddScoped<IHistoryActionManager, HistoryActionManager>();
            services.AddScoped<ISaveHistoryActionData, SaveHistoryActionData>();
            services.AddScoped<IEventManager, EventManager>();
            services.AddScoped<IScheduleManager, ScheduleManager>();
            services.AddScoped<ICommandManager, CommandManager>();

            services.AddScoped<IRoleManager, RoleManager>();
            services.AddScoped<ICatalogManager, CatalogManager>();
            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped<IPintelSheetManager, PintelSheetManager>();
            services.AddScoped<ICeManager, CeManager>();

            services.AddScoped<ICeSetupManager, CeSetupManager>();
            services.AddScoped<IGoodManager, GoodManager>();
            services.AddScoped<IBatchManager, BatchManager>();
            services.AddScoped<ISupplierManager, SupplierManager>();

            //Enumeration Classes Managers
            services.AddScoped<IAgeGroupManager, AgeGroupManager>();
            services.AddScoped<ILetterIndexManager, LetterIndexManager>();
            services.AddScoped<IGoodDepartmentManager, GoodDepartmentManager>();
            services.AddScoped<IOriginManager, OriginManager>();
            services.AddScoped<IProductTypeManager, ProductTypeManager>();
            services.AddScoped<ICatalogTypeManager, CatalogTypeManager>();
            services.AddScoped<ICatalogChoiceTypeManager, CatalogChoiceTypeManager>();

            //FileOrganizer

            services.AddScoped<IFileManager, FileManager>();
            services.AddScoped<IFolderManager, FolderManager>();


            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDbFactory, DbFactory>();
            services.AddScoped<JceDbContext>();
            services.AddScoped<IRepository<JceDbContext>, JceRepository>();

            services.AddCors();
            Mapper.Reset();
            services.AddAutoMapper();

            if (_env.IsEnvironment("Test"))
            {
                services.AddDbContext<JceDbContext>(options =>
                options.UseInMemoryDatabase("TestDb"));
            }
            else
            {
                services.AddDbContext<JceDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("jce_live")));
                services.Configure<PhotoSettings>(Configuration.GetSection("PhotoSettings"));
                //configure strongly typed settings objects
                var appSettingsSection = Configuration.GetSection("AuthSetting");
                services.Configure<AuthSetting>(appSettingsSection);
                // configure jwt authentication
                var appSettings = appSettingsSection.Get<AuthSetting>();
                var key = Encoding.ASCII.GetBytes(appSettings.Secret);
                services.Configure<IISOptions>(options =>
                {
                    options.ForwardClientCertificate = false;
                });

            }


            services.AddMvc(
                options =>
            {
                options.Filters.Add(typeof(JsonExceptionFilter));
            }
            );

            //services.AddMvcCore()
            //    .AddAuthorization()
            //    .AddJsonFormatters();
            services.AddMvcCore();

            //services.AddAuthentication("Bearer")
            //    .AddIdentityServerAuthentication(options =>
            //    {
            //        options.Authority = "http://localhost:5000";
            //        options.RequireHttpsMetadata = false;

            //        options.ApiName = "jce";
            //    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
             .CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<JceDbContext>();
                //EnsureDatabaseCreated(dbContext);
            }
        }

        public virtual void SetUpDataBase(IServiceCollection services)
        {
            services.AddDbContext<JceDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("TestDb"), sqlOptions => sqlOptions.MigrationsAssembly("TokenAuthWebApiCore.Server")));
        }

        public virtual void EnsureDatabaseCreated(JceDbContext dbContext)
        {
            // run Migrations
            dbContext.Database.Migrate();
        }
    }
}
