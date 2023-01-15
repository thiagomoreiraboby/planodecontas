using Microsoft.Extensions.DependencyInjection;
using planodecontas.infra.DBContexts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using planodecontas.domain.Repositorios;
using planodecontas.infra.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace planodecontas.ioc.Infra
{
    internal class InfraDependencyResolver
    {
        internal void ChildServiceRegister(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BaseDadosContext>(options =>
            options.UseInMemoryDatabase(configuration.GetConnectionString("BaseDadosContext")

            ));

            services.AddScoped<IPlanodeContaRepositorio, PlanodeContaRepositorio>();
        }
    }
}
