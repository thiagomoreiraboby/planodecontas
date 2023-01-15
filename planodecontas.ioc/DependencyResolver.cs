using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using planodecontas.ioc.Application;
using planodecontas.ioc.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planodecontas.ioc
{
    public static class DependencyResolver
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            new ApplicationDependencyResolver().ChildServiceRegister(services);
            new InfraDependencyResolver().ChildServiceRegister(services, configuration);
        }
    }
}
