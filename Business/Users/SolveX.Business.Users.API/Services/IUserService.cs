using SolveX.Business.Users.API.Dtos;
namespace SolveX.Business.Users.API.Services;

public interface IUserService
{
    public Task<int> Register(string userName,  string password, bool isAdmin);

    public Task<UserDto> Login(string userName, string password);
}
