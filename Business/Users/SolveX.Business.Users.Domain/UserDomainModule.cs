using Autofac;
using Microsoft.AspNetCore.Identity;
using SolveX.Business.Users.Domain.DomainServices;
using SolveX.Business.Users.Domain.Models;
using SolveX.Business.Users.Domain.MicroServices;

namespace SolveX.Business.Users.Domain;

public class UserDomainModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<UserDomainService>().As<IUserDomainService>();
        builder.RegisterType<UserPasswordHasher>().As<IPasswordHasher< UserModel >> ().SingleInstance();
    }
}