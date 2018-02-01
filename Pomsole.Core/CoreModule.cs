using Autofac;

namespace Pomsole.Core
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SessionManager>().As<ISessionManager>();
        }

    }
}
