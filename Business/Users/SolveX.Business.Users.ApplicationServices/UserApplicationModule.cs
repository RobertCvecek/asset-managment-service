using Autofac;
using SolveX.Business.Users.API.Services;
using SolveX.Business.Users.ApplicationServices.Services;

namespace SolveX.Business.Users.ApplicationServices;

public class UserApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<UserService>().As<IUserService>();
    }
}