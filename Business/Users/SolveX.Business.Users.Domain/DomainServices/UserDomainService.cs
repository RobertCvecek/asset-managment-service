using Microsoft.Extensions.Logging;
using SolveX.Business.Users.Domain.Models;
using SolveX.Business.Users.Domain.Repositories;
using System.ComponentModel.DataAnnotations;
using SolveX.Framework.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SolveX.Business.Users.Domain.DomainServices;

public class UserDomainService : IUserDomainService
{
    private readonly IUserRepository _repository;
    private readonly ILogger<UserDomainService> _logger;
    private readonly IPasswordHasher<UserModel> _passwordHasher;

    public UserDomainService(IUserRepository repository, ILogger<UserDomainService> logger, IPasswordHasher<UserModel> passwordHasher)
    {
        _repository = repository;
        _logger = logger;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserModel> Login(string username, string password)
    {
        _logger.LogDebug($"Logging in user with username [{username}] ");

        ValidateUser(username, password);

        UserModel? user = (await _repository.Query().Where(usr => usr.UserName == username).ToListAsync()).FirstOrDefault();

        if(user is null)
        {
            throw new DataNotFoundException("The user was not found");
        }
        if(_passwordHasher.VerifyHashedPassword(user, user.Password ,password) == PasswordVerificationResult.Failed)
        {
            throw new BadDataException("The provided password is not correct");
        }

        return user;
    }

    public async Task<int> Register(UserModel user, bool isAdmin)
    {
        _logger.LogDebug($"Creating user with username [{user.UserName}]");

        ValidateUser(user.UserName,  user.Password);

        if((await _repository.Query().Where(usr => usr.UserName == user.UserName).ToListAsync()).Count is not 0)
        {
            throw new BadDataException("User already exists");
        }

        user.Password = _passwordHasher.HashPassword(user, user.Password);

        user.Role = isAdmin ? "Admin" : "Basic";

        return await _repository.InsertAsync(user);
    }

    private void ValidateUser(string username, string password)
    {
        if( username == String.Empty)
        {
            throw new BadDataException("The username or email was not provided");
        }
       
        if(password is null || password == String.Empty)
        {
            throw new BadDataException("Password was not provided");
        }
    }
}
