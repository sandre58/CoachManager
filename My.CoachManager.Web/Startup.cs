using System.IO;
using System.Security.Principal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using My.CoachManager.Application.Services.AddressModule;
using My.CoachManager.Application.Services.CategoryModule;
using My.CoachManager.Application.Services.InjuryModule;
using My.CoachManager.Application.Services.PersonModule;
using My.CoachManager.Application.Services.PositionModule;
using My.CoachManager.Application.Services.RosterModule;
using My.CoachManager.Application.Services.SeasonModule;
using My.CoachManager.Application.Services.TrainingModule;
using My.CoachManager.Application.Services.UserModule;
using My.CoachManager.CrossCutting.Logging.Supervision;
using My.CoachManager.Domain.AppModule.Services;
using My.CoachManager.Domain.CategoryModule.Services;
using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.InjuryModule.Services;
using My.CoachManager.Domain.PersonModule.Services;
using My.CoachManager.Domain.RosterModule.Services;
using My.CoachManager.Domain.SeasonModule.Services;
using My.CoachManager.Domain.SquadModule.Services;
using My.CoachManager.Domain.TrainingModule.Services;
using My.CoachManager.Infrastructure.Data.Core;
using My.CoachManager.Infrastructure.Data.UnitOfWorks;
using My.CoachManager.Web.Attributes;

namespace My.CoachManager.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Register Logger
            var loggerProvider = new LoggerProvider();
            var loggerFactory = new Microsoft.Extensions.Logging.LoggerFactory();
            var logger = LoggerFactory.CreateLogger("Services");

            Logger.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/NLog.config"));
            loggerFactory.AddProvider(loggerProvider);
            services.AddSingleton(logger);

            // Configure database.
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Home"))
                .UseLoggerFactory(loggerFactory));

            // Register types
            RegisterTypes(services);

            // Register Domain Layer
            RegisterDomainServices(services);

            // Register Application Layer
            RegisterApplicationServices(services);

            // Compatibility Asp .Net Core 2.1
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Configures Filters
            services.AddMvc(
                config =>
                {
                    config.Filters.Add(new LogActionFilterAttribute(logger));
                    config.Filters.Add(new WebExceptionFilterAttribute());
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        /// <summary>
        /// Register Types.
        /// </summary>
        /// <param name="services"></param>
        private void RegisterTypes(IServiceCollection services)
        {
            // Register logger

            // Register Data Layer
            services.AddTransient<IQueryableUnitOfWork, DataContext>();
            services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));

            // Register User authencation
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IPrincipal>(
                provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);
        }

        /// <summary>
        /// Register domain services.
        /// </summary>
        /// <param name="services"></param>
        private void RegisterDomainServices(IServiceCollection services)
        {
            services.AddTransient(typeof(ICrudDomainService<,>), typeof(CrudDomainService<,>));
            services.AddTransient<ICategoryDomainService, CategoryDomainService>();
            services.AddTransient<ISeasonDomainService, SeasonDomainService>();
            services.AddTransient<ISquadDomainService, SquadDomainService>();
            services.AddTransient<IRosterDomainService, RosterDomainService>();
            services.AddTransient<IInjuryDomainService, InjuryDomainService>();
            services.AddTransient<ITrainingDomainService, TrainingDomainService>();
            services.AddTransient<IPlayerDomainService, PlayerDomainService>();
        }

        /// <summary>
        /// Register application services.
        /// </summary>
        /// <param name="services"></param>
        private void RegisterApplicationServices(IServiceCollection services)
        {
            services.AddTransient<ICategoryAppService, CategoryAppService>();
            services.AddTransient<IPositionAppService, PositionAppService>();
            services.AddTransient<IUserAppService, UserAppService>();
            services.AddTransient<ISquadAppService, SquadAppService>();
            services.AddTransient<IRosterAppService, RosterAppService>();
            services.AddTransient<IInjuryAppService, InjuryAppService>();
            services.AddTransient<ITrainingAppService, TrainingAppService>();
            services.AddTransient<ISeasonAppService, SeasonAppService>();
            services.AddTransient<IAddressAppService, AddressAppService>();
            services.AddTransient<ICountryAppService, CountryAppService>();
            services.AddTransient<IPlayerAppService, PlayerAppService>();
        }
    }
}