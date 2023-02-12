using SolveX.Business.Users.Domain.Models;
using SolveX.Business.Users.Domain.Repositories;
using SolveX.Business.Users.Integration.Context;
using SolveX.Framework.Integration.Repositories;

namespace SolveX.Business.Users.Integration.Repositories;
public class UserRepository : GenericRepository<UserModel>, IUserRepository
{
    public UserRepository(UserContext context):base(context)
    {
    }
}
