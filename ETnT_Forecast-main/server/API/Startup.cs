using Autofac;
using BaseService;
using DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Service;

namespace api
{
    public class Startup : AppStartupBase
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env) : base(env, configuration)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureApplicationServices(services, new OpenApiInfo
            {
                Version = "v1",
                Title = "Et&T Forecast API",
                Description = "Et&T Forecast API"
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ServiceModule());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,Seeder seeder)
        {
            ConfigureApplication(app, env);
            seeder.Seed();
        }
    }
}