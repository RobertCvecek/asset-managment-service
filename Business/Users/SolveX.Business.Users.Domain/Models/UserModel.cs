using SolveX.Framework.Integration.Repositories.Models;

namespace SolveX.Business.Users.Domain.Models;

public class UserModel : BaseEntity
{
    public string UserName { get; set; }

    public string Password { get; set; }

    public string Role { get; set; }
}
