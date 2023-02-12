using AutoMapper;
using SolveX.Business.Users.API.Dtos;
using SolveX.Business.Users.API.Services;
using SolveX.Business.Users.Domain.DomainServices;
using SolveX.Business.Users.Domain.Models;

namespace SolveX.Business.Users.ApplicationServices.Services;

public class UserService : IUserService
{
    private readonly IUserDomainService _domainService;
    private readonly IMapper _mapper;

    public UserService(IUserDomainService domainService, IMapper mapper)
    {
        _domainService = domainService;
        _mapper = mapper;
    }

    public async Task<UserDto> Login(string userName, string password)
    {
        UserModel user = await _domainService.Login(userName, password);

        return _mapper.Map<UserDto>(user);
    }

    public async Task<int> Register(string userName,  string password, bool isAdmin)
    {
        return await _domainService.Register(new()
        {
            UserName = userName,
            Password = password,
        }, isAdmin);
    }
}
