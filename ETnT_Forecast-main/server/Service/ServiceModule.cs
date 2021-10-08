using Autofac;
using DataAccess;

namespace Service
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ForecastCommandValidator>();
            builder.RegisterModule(new RepositoryModule());
        }
    }
}