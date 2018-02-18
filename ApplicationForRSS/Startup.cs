using ApplicationForRSS.Repositories;
using ApplicationForRSS.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace ApplicationForRSS
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddMvcOptions(option => option.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()));            
            var connectionString = Startup.Configuration["connectionStrings:DBConnectionString"];
            services.AddDbContext<RssFeedContext>(o => o.UseSqlServer(connectionString));
            services.AddScoped<IRssFeedRepository, RssFeedRepository>();
            services.AddScoped<IRssFeedService, RssFeedService>();           
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, RssFeedContext rssFeedContext)
        {
            loggerFactory.AddNLog();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStatusCodePages();
            app.UseMvc();
            rssFeedContext.EnsureSeedDataForContext();
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DomainObjects.RssFeedDomainObj, Models.RssFeedDto>();
                cfg.CreateMap<Models.RssFeedDto, Entities.RssFeed>();
            });
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Rss Feed Service is running");
            });
        }
    }
}
