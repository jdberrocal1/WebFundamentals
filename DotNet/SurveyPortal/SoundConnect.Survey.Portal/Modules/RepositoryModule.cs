using Autofac;
using System.Reflection;

namespace SoundConnect.Survey.Portal.Modules
{
    public class RepositoryModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("SoundConnect.Survey.DBContext"), Assembly.Load("SoundConnect.Survey.Core"))
                      .Where(t => t.Name.EndsWith("Repository"))
                      .AsImplementedInterfaces()
                      .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(Assembly.Load("SoundConnect.Survey.Application"))
                      .Where(t => t.Name.EndsWith("AppService") || (t.Name.EndsWith("Logger")))
                      .AsImplementedInterfaces()
                      .InstancePerLifetimeScope();
        }
    }
}