using Autofac;
using Pomsole.Core.Config;
using Pomsole.Core.Data;
using Pomsole.Core.IO;

namespace Pomsole.Core
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConfigManager>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<DataManager>().As<IDataManager>();
            builder.RegisterType<FileManager>().As<IFileManager>();
            builder.RegisterType<SessionManager>().As<ISessionManager>();
        }

    }
}
