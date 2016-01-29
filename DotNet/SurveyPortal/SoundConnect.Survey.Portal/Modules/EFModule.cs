using Autofac;
using SoundConnect.Survey.Data;
using SoundConnect.Survey.EntityFramework;

namespace SoundConnect.Survey.Portal.Modules
{
    public class EFModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(SCDbContext)).As(typeof(IDataContext)).InstancePerLifetimeScope();
        }
    }
}