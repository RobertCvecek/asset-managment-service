using Autofac;
using SolveX.Business.Users.Domain.Repositories;
using SolveX.Business.Users.Integration.Context;
using SolveX.Business.Users.Integration.Repositories;

namespace SolveX.Business.Users.Integration;

public class UserIntegrationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<UserContext>();
        builder.RegisterType<UserRepository>().As<IUserRepository>();
    }
}