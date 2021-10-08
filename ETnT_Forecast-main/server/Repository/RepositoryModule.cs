using Autofac;
using DataAccess.Abstractions;

namespace DataAccess
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ForecastContext>().InstancePerLifetimeScope();
            builder.RegisterType<Seeder>().SingleInstance();
            builder.RegisterType<ForecastRepository>().As<IForecastRepository>().InstancePerLifetimeScope();
            builder.RegisterType<LookupRepository>().As<ILookupRepository>().InstancePerLifetimeScope();
        }
    }
}