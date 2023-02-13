using Autofac;
using SolveX.Business.Assets.API.Services;
using SolveX.Business.Assets.ApplicationServices.Services;

namespace SolveX.Business.Users.Integration;

public class AssetApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<AssetService>().As<IAssetService>();
    }
}