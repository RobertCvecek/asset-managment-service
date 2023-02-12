using SolveX.Business.Users.Domain.Models;

namespace SolveX.Business.Users.Domain.DomainServices;

public interface IUserDomainService
{
    public Task<UserModel> Login(string username, string password);

    public Task<int> Register(UserModel value, bool isAdmin);
}
