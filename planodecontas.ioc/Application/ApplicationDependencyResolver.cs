using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using planodecontas.application.AutoMapper;
using planodecontas.application.Contrato;
using planodecontas.application.Servicos;
using planodecontas.domain.Repositorios;
using planodecontas.infra.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planodecontas.ioc.Application
{
    internal class ApplicationDependencyResolver
    {
        internal void ChildServiceRegister(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile(provider.CreateScope().ServiceProvider.GetService<IGestaodeCodigoServico>()));

            }).CreateMapper());
            services.AddScoped<IPlanodeContaServico, PlanodeContaServico>();
            services.AddScoped<IGestaodeCodigoServico, GestaodeCodigoServico>();
            
        }
    }
}
