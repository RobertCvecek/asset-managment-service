using SolveX.Business.Assets.Domain.Models;
using SolveX.Business.Assets.Domain.Repositories;
using SolveX.Business.Assets.Integration.Context;
using SolveX.Framework.Integration.Repositories;

namespace SolveX.Business.Assets.Integration.Repositories;
public class AssetConnectionRepository : GenericRepository<AssetConnection>, IAssetConnectionRepository
{
    public AssetConnectionRepository(AssetContext context) : base(context)
    {

    }
}
