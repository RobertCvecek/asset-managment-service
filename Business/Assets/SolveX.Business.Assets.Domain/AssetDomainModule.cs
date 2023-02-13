using Autofac;
using SolveX.Business.Assets.Domain.DomainServices;

namespace SolveX.Business.Users.Integration;

public class AssetDomainModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<AssetDomainService>().As<IAssetDomainService>();
    }
}