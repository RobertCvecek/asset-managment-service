using Autofac;
using SolveX.Business.Assets.Domain.Repositories;
using SolveX.Business.Assets.Integration.Context;
using SolveX.Business.Assets.Integration.Repositories;

namespace SolveX.Business.Users.Integration;

public class AssetIntegrationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<AssetContext>();
        builder.RegisterType<AssetRepository>().As<IAssetRepository>();
        builder.RegisterType<AssetConnectionRepository>().As<IAssetConnectionRepository>();
    }
}