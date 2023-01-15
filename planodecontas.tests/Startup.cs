using planodecontas.ioc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using planodecontas.infra.DBContexts;
using System;
using Microsoft.EntityFrameworkCore;

namespace planodecontas.tests
{
    public class Startup
    {
        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();

            Configuration = builder.Build();
            
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddApplicationPart(Assembly.Load("planodecontas.api")).AddControllersAsServices();

            DependencyResolver.RegisterServices(services, Configuration);
            services.AddDbContext<BaseDadosContext>(options =>
            {
                options.UseInMemoryDatabase("MyDatabase-" + Guid.NewGuid());
            });

            var provider = services.BuildServiceProvider();
            DataBaseInitialize.Initialize(provider);
        }

        public void Configure(IApplicationBuilder app)
        {
            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}