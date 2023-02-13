using SolveX.Business.Assets.Domain.Models;

namespace SolveX.Business.Assets.Domain.DomainServices;
public interface IAssetDomainService
{
    public Task<int> Create(int id, string title, string data, IEnumerable<int> links);

    public Task<Asset> Get(int id);

    public Task<Asset?> Get(string title);

    public Task<Asset?> Get(string atribute, string attributeValue);
    
    public Task<IEnumerable<Asset>> GetLinkedAssets(int assetId);
}
