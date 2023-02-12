using SolveX.Business.Users.Domain.Models;
using SolveX.Framework.Integration.Repositories;

namespace SolveX.Business.Users.Domain.Repositories;

/// <summary>
/// Interface for accessing databse for users
/// </summary>
public interface IUserRepository : IGenericRepository<UserModel>
{
   
}
