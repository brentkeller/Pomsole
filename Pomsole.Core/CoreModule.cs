using Autofac;
using Pomsole.Core.Config;

namespace Pomsole.Core
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConfigManager>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<SessionManager>().As<ISessionManager>();
        }

    }
}
