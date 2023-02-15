using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using SolveX.Business.Assets.Domain.Models;
using SolveX.Business.Assets.Integration.Context;

namespace SolveX.Business.Assets.IntegrationTests.Generators;
public class AssetTestDataGenerator
{
    public AssetContext _context;
    public AssetTestDataGenerator(AssetContext context)
    {
        _context = context;
    }
    /// <summary>
    /// Inserts asset
    /// </summary>
    /// <returns>Id of asset</returns>
    public async Task<int> Insert(Asset asset)
    {
        _context.Set<Asset>().Add(asset);
        return await _context.SaveChangesAsync();
    }

}
