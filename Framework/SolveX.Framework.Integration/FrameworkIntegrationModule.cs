using Autofac;
using SolveX.Framework.Integration.Database;

namespace SolveX.Framework.Integration;

public class FrameworkIntegrationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().SingleInstance();
    }
}