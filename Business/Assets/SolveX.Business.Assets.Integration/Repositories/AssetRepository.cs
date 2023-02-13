using Microsoft.EntityFrameworkCore;
using SolveX.Business.Assets.Domain.Models;
using SolveX.Business.Assets.Domain.Repositories;
using SolveX.Business.Assets.Integration.Context;
using SolveX.Framework.Integration.Repositories;
namespace SolveX.Business.Assets.Integration.Repositories;
public class AssetRepository : GenericRepository<Asset>, IAssetRepository
{
    private readonly AssetContext _context;
    public AssetRepository(AssetContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Asset>> GetAll()
    {
        return await _context.Asset.ToListAsync();
    }
}
