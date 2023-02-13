using SolveX.Business.Assets.Domain.Models;
using SolveX.Framework.Integration.Repositories;

namespace SolveX.Business.Assets.Domain.Repositories;

public interface IAssetRepository : IGenericRepository<Asset>
{
    public Task<IEnumerable<Asset>> GetAll();
}
